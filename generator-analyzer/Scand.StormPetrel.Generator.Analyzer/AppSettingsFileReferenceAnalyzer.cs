using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scand.StormPetrel.Generator.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class AppSettingsFileReferenceAnalyzer : AbstractAnalyzer
    {
        private static readonly ConcurrentDictionary<string, string> _cacheOfDirPathToAppSettingsFilePath
            = new ConcurrentDictionary<string, string>();
        private readonly Func<string, IEnumerable<string>> _enumerateFilesFunc;
        private readonly AppSettingsFileReferenceAnalyzerCache _cache;
        public AppSettingsFileReferenceAnalyzer()
            : this(_cacheOfDirPathToAppSettingsFilePath, () => DateTime.UtcNow.Ticks, Directory.EnumerateFiles)
        {
        }
        internal AppSettingsFileReferenceAnalyzer(IDictionary<string, string> cacheDict, Func<long> ticksProvider, Func<string, IEnumerable<string>> enumerateFilesFunc)
            : base(AppSettingsFileReferenceAnalyzerHelpers.Rule)
        {
            _cache = new AppSettingsFileReferenceAnalyzerCache(ticksProvider, cacheDict);
            _enumerateFilesFunc = enumerateFilesFunc;
        }
        protected override void AnalyzeCompilation(CompilationStartAnalysisContext context)
        {
            context.RegisterAdditionalFileAction(x => AnalyzeAdditionalFiles(
                                                                new[] { x.AdditionalFile },
                                                                x.Compilation.SyntaxTrees,
                                                                x.ReportDiagnostic));
            context.RegisterCompilationEndAction(x => AnalyzeAdditionalFiles(
                                                                x.Options.AdditionalFiles,
                                                                x.Compilation.SyntaxTrees,
                                                                x.ReportDiagnostic));
        }
        private void AnalyzeAdditionalFiles(IEnumerable<AdditionalText> additionalFiles, IEnumerable<SyntaxTree> syntaxTrees, Action<Diagnostic> reportDiagnostic)
        {
            bool isReferenced = additionalFiles.Any(Common.Utils.IsAppSettings);
            if (isReferenced)
            {
                return;
            }

            var filePath = syntaxTrees.FirstOrDefault()?.FilePath;
            if (filePath == null)
            {
                //False positive `no report` is possible if no files in the project
                return;
            }

            var dirPath = Path.GetDirectoryName(filePath);
            if (_cache.IsExpired())
            {
                _cache.Clear();
            }
            else if (_cache.TryGetValue(dirPath, out var appSettingsFilePath))
            {
                if (!string.IsNullOrEmpty(appSettingsFilePath))
                {
                    AddDiagnostic(appSettingsFilePath, reportDiagnostic);
                }
                return;
            }

            var currentPath = dirPath;
            do
            {
                var filesInfo = _enumerateFilesFunc(currentPath)
                                    .Select(x => (FilePath: x,
                                                    IsAppSettings: Common.Utils.IsAppSettings(Path.GetFileName(x)),
                                                    IsCsProj: ".csproj".Equals(Path.GetExtension(x), StringComparison.OrdinalIgnoreCase)))
                                    .Where(x => x.IsCsProj || x.IsAppSettings)
                                    .ToArray();
                var appSettingsFilePath = Common
                                            .Utils
                                            .GetAppSetting(filesInfo
                                                            .Where(x => x.IsAppSettings)
                                                            .Select(x => x.FilePath),
                                                            x => x);
                if (!string.IsNullOrEmpty(appSettingsFilePath))
                {
                    _cache.AddOrUpdate(dirPath, appSettingsFilePath);
                    AddDiagnostic(appSettingsFilePath, reportDiagnostic);
                    break;
                }
                var projectFilePath = filesInfo
                                        .FirstOrDefault(x => x.IsCsProj)
                                        .FilePath;
                if (!string.IsNullOrEmpty(projectFilePath))
                {
                    _cache.AddOrUpdate(dirPath, null);
                    break;
                }
                currentPath = Path.GetDirectoryName(currentPath);
                if (string.IsNullOrEmpty(currentPath))
                {
                    break;
                }
            } while (true);
        }
        private static void AddDiagnostic(string appSettingsFilePath, Action<Diagnostic> reportDiagnostic)
        {
            var diagnostic = AppSettingsFileReferenceAnalyzerHelpers.CreateDiagnostic(appSettingsFilePath);
            reportDiagnostic(diagnostic);
        }
    }
}
