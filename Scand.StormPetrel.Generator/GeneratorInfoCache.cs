using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using Serilog.Settings.Configuration;
using Scand.StormPetrel.Generator.TargetProject;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Scand.StormPetrel.Generator
{
    internal static partial class GeneratorInfoCache
    {
        /// <summary>
        /// Cache implementation according to `a host with multiple loaded projects may share the same generator instance across multiple projects` [1].
        /// [1] <see cref="https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#pipeline-based-execution"/>
        /// </summary>
        private static readonly ConcurrentDictionary<string, Info> ProjectRootDirToInfo = new ConcurrentDictionary<string, Info>();

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip };

        public static (MainConfig MainConfig, ILogger Logger) Get(AdditionalText configAdditionalText, string filePath, bool reinit = false)
        {
            if (configAdditionalText != null)
            {
                // this path might decrease file system calls count
                filePath = configAdditionalText.Path;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            var projectRootDir = GetProjectRootFromCache(filePath);
            Info info = null;
            if (projectRootDir == null)
            {
                projectRootDir = GetProjectRootFromFileSystem(filePath);
            }
            else
            {
                info = ProjectRootDirToInfo[projectRootDir];
            }

            if (!reinit && info != null)
            {
                return (info.MainConfig, info.Logger);
            }

            var infoNew = GetNew(configAdditionalText, projectRootDir);
            infoNew = ProjectRootDirToInfo.AddOrUpdate(projectRootDir, _ => infoNew, (_, _unused) => infoNew);
            return (infoNew.MainConfig, infoNew.Logger);
        }

        public static string ToSourcePath(string configPath, string filePath)
        {
            string rootDir = null;
            if (!string.IsNullOrEmpty(configPath))
            {
                rootDir = GetProjectRootFromCache(configPath);
            }
            if (string.IsNullOrEmpty(rootDir))
            {
                rootDir = GetProjectRootFromCache(filePath);
            }
            var result = filePath;
            if (!string.IsNullOrEmpty(rootDir))
            {
                int startIndex = 0;
                int minLength = Math.Min(rootDir.Length, filePath.Length);
                for (int i = 0; i < minLength; i++)
                {
                    if (rootDir[i] != filePath[i])
                    {
                        break;
                    }
                    startIndex++;
                }
                result = filePath
                            .Substring(startIndex)
                            .TrimStart('/')
                            .TrimStart('\\');
            }
            return Escape(result);

            string Escape(string path)
            {
                return path.Replace(":", "");
            }
        }

        private static string GetProjectRootFromCache(string filePath)
        {
            var rootDir = ProjectRootDirToInfo
                                .Keys
                                .Where(x => filePath.StartsWith(x, StringComparison.OrdinalIgnoreCase))
                                .FirstOrDefault();
            return rootDir;
        }

        private static string GetProjectRootFromFileSystem(string filePath)
        {
            string currentLevelPath = filePath;
            do
            {
                string directoryPath = Path.GetDirectoryName(currentLevelPath);
                if (directoryPath == null)
                {
                    // In some cases (e.g. filePath is from referenced shared project) we cannot find csproj file.
                    // So, just use its directory.
                    return Path.GetDirectoryName(filePath);
                }
                directoryPath = directoryPath
                                    .TrimEnd('/')
                                    .TrimEnd('\\');
                var csprojFiles = Directory.GetFiles(directoryPath, "*.csproj", SearchOption.TopDirectoryOnly);
                if (csprojFiles.Length > 0)
                {
                    return directoryPath;
                }
                currentLevelPath = directoryPath;
            }
            while (true);
        }

        private static Info GetNew(AdditionalText configAdditionalText, string projectRootDir)
        {
            MainConfig config;
            ILogger logger = Logger.None;
            var jsonContent = configAdditionalText == null
                                ? MainConfig.DefaultJson
                                : configAdditionalText.GetText(CancellationToken.None)?.ToString();
            bool isJsonException = false;
            if (jsonContent != null)
            {
                jsonContent = ReplaceJsonTokens(jsonContent);
            }
            try
            {
                config = JsonSerializer.Deserialize<MainConfig>(jsonContent, JsonOptions);
            }
            catch (JsonException)
            {
                // json can be not well formed, use default
                jsonContent = ReplaceJsonTokens(MainConfig.DefaultJson);
                config = JsonSerializer.Deserialize<MainConfig>(jsonContent);
                isJsonException = true;
            }

            if (config.Serilog == MainConfig.SerilogDefault)
            {
                jsonContent = ReplaceJsonTokens(MainConfig.DefaultJson);
            }
            else if (config.Serilog == null)
            {
                jsonContent = null;
            }
            config.Serilog = null; // we only need it to detect Serilog configuration. So, clear it now.

            if (jsonContent != null)
            {
                using (var jsonStream = new MemoryStream(2 * jsonContent.Length))
                {
                    using (var sw = new StreamWriter(jsonStream, Encoding.UTF8))
                    {
                        sw.Write(jsonContent);
                        sw.Flush();
                        jsonStream.Position = 0;

                        var configuration = new ConfigurationBuilder()
                                                    .AddJsonStream(jsonStream)
                                                    .Build();

                        // Follow suggestion from `No Serilog:Using configuration section is defined and no Serilog assemblies were found...`
                        // exception message which appears if we don't have `options` variable
                        var options = new ConfigurationReaderOptions(typeof(Serilog.Sinks.File.FileSink).Assembly);
                        logger = new LoggerConfiguration()
                                        .ReadFrom
                                        .Configuration(configuration, options)
                                        .CreateLogger();
                        logger.Warning("New logger has been created, project root path: {ProjectRootPath}; is json exception and thus default config is used: {isJsonException}", projectRootDir, isJsonException);
                    }
                }
            }

            return new Info()
            {
                Logger = logger,
                MainConfig = config,
            };

            string ReplaceJsonTokens(string jsonContentVar)
            {
                var pathEscaped = projectRootDir.Replace(@"\", @"\\"); //no other json escapes because a path is safe for json
                jsonContentVar = jsonContentVar.Replace(MainConfig.StormPetrelRootPath, pathEscaped);
                return jsonContentVar;
            }
        }
    }
}
