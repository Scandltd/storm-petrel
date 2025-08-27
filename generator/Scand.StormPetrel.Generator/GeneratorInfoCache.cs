using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Scand.StormPetrel.Generator.Common.TargetProject;
using Serilog;
using Serilog.Core;
using Serilog.Settings.Configuration;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;

namespace Scand.StormPetrel.Generator
{
    internal static partial class GeneratorInfoCache
    {
        /// <summary>
        /// Cache implementation according to `a host with multiple loaded projects may share the same generator instance across multiple projects` [1].
        /// [1] <see cref="https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#pipeline-based-execution"/>
        /// </summary>
        private static readonly ConcurrentDictionary<string, (MainConfigParsed MainConfig, ILogger Logger)> ProjectRootDirToInfo = new ConcurrentDictionary<string, (MainConfigParsed MainConfig, ILogger Logger)>();
        private static readonly char[] PossiblePathSeparators = new[] { '/', '\\' };
        public static (MainConfigParsed MainConfig, ILogger Logger) Get(AdditionalText configAdditionalText, string filePath, bool reinit = false)
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
            (MainConfigParsed MainConfig, ILogger Logger) info = default;
            if (projectRootDir == null)
            {
                projectRootDir = GetProjectRootFromFileSystem(filePath);
            }
            else
            {
                info = ProjectRootDirToInfo[projectRootDir];
            }

            if (!reinit && info != default)
            {
                return info;
            }

            var mainConfig = MainConfig.GetNew(configAdditionalText, out var jsonContent, out var isJsonException, out _);
            var logger = Logger.None;
            if (jsonContent != null)
            {
                var pathEscaped = projectRootDir.Replace(@"\", @"\\"); //no other json escapes because a path is safe for json
                jsonContent = jsonContent.Replace(MainConfig.StormPetrelRootPath, pathEscaped);
                logger = NewLogger(jsonContent);
                logger.Warning("New logger has been created, project root path: {ProjectRootPath}; is json exception and thus default config is used: {isJsonException}", projectRootDir, isJsonException);
            }
            var infoNew = (mainConfig, logger);
            infoNew = ProjectRootDirToInfo.AddOrUpdate(projectRootDir, _ => infoNew, (_, _unused) => infoNew);
            return infoNew;
        }
        public static string ToSourcePath(string configPath, string filePath)
        {
            string rootDir = null;
            filePath = filePath
                .Replace($"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}", $"{Path.DirectorySeparatorChar}StormPetrelDotDot{Path.DirectorySeparatorChar}")
                .Replace($"{Path.DirectorySeparatorChar}.{Path.DirectorySeparatorChar}", $"{Path.DirectorySeparatorChar}StormPetrelDot{Path.DirectorySeparatorChar}");
            
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
                var filePathSegments = filePath.Split(Path.DirectorySeparatorChar);
                var rootDirSegments = rootDir.Split(Path.DirectorySeparatorChar);
                int startIndex = 0;
                int minLength = Math.Min(filePathSegments.Length, rootDirSegments.Length);

                for (int i = 0; i < minLength; i++)
                {
                    if (rootDirSegments[i] == filePathSegments[i])
                    {
                        startIndex++;
                    }
                    else
                    {
                        break;
                    }
                }

                result = string.Join($"{Path.DirectorySeparatorChar}", filePathSegments.Skip(startIndex));
            }

            result = result
                        .Replace($"{Path.VolumeSeparatorChar}", "")
                        .TrimStart(Path.DirectorySeparatorChar);

            return result;
        }
        private static string GetProjectRootFromCache(string filePath) =>
            ProjectRootDirToInfo
                .Keys
                .Where(x => filePath.StartsWith(x, StringComparison.OrdinalIgnoreCase)
                                && filePath.Length > x.Length
                                && Array.IndexOf(PossiblePathSeparators, filePath[x.Length]) > -1)
                .FirstOrDefault();
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
        private static Logger NewLogger(string jsonContent)
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
                    var logger = new LoggerConfiguration()
                                    .ReadFrom
                                    .Configuration(configuration, options)
                                    .CreateLogger();
                    return logger;
                }
            }
        }
    }
}
