using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Scand.StormPetrel.Generator.Common;
using Scand.StormPetrel.Generator.Common.TargetProject;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Scand.StormPetrel.Generator.Analyzer
{
    public abstract partial class AppSettingsFileContentAnalyzer : AbstractAnalyzer
    {
        protected AppSettingsFileContentAnalyzer(DiagnosticDescriptor descriptor)
            : base(descriptor)
        {
        }

        protected override void AnalyzeCompilation(CompilationStartAnalysisContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            context.RegisterAdditionalFileAction(x => AnalyzeAdditionalFiles(
                                                               new[] { x.AdditionalFile },
                                                               x.ReportDiagnostic,
                                                               context.CancellationToken));
            context.RegisterCompilationEndAction(x => AnalyzeAdditionalFiles(
                                                                x.Options.AdditionalFiles,
                                                                x.ReportDiagnostic,
                                                                context.CancellationToken));
        }
        protected abstract void AnalyzeFileContent(AdditionalText file, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken);

        protected void AnalyzeAdditionalFiles(IEnumerable<AdditionalText> additionalFiles,
            Action<Diagnostic> reportDiagnostic,
            CancellationToken cancellationToken)
        {
            var files = additionalFiles.Where(Utils.IsAppSettings);

            foreach (var file in files)
            {
                AnalyzeFileContent(file, reportDiagnostic, cancellationToken);
            }
        }

        protected static ImmutableHashSet<string> GetPropertyNames(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type
                .GetProperties()
                .Select(p => p.Name)
                .ToImmutableHashSet(StringComparer.OrdinalIgnoreCase);
        }

        protected static void AddDiagnostics(DiagnosticDescriptor rule, Action<Diagnostic> reportDiagnostic,
            string text, IEnumerable<int> startIndexes, int length, string filePath, string parameter)
        {
            if (startIndexes == null)
            {
                throw new ArgumentNullException(nameof(startIndexes));
            }

            foreach (var startIndex in startIndexes)
            {
                var location = GetLocation(text, startIndex, length, filePath);
                AddDiagnostic(rule, location, reportDiagnostic, filePath, parameter);
            }
        }

        protected static void AddDiagnostic(DiagnosticDescriptor rule, Location location,
            Action<Diagnostic> reportDiagnostic, string filePath, string parameter = "")
        {
            if (reportDiagnostic == null)
            {
                throw new ArgumentNullException(nameof(reportDiagnostic));
            }

            if (location == null)
            {
                location = GetLocation(string.Empty, 0, 0, filePath);
            }

            var diagnostic = Diagnostic.Create(rule, location, filePath, parameter);
            reportDiagnostic(diagnostic);
        }

        protected static Location GetLocation(string text, int startIndex, int length, string filePath)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(filePath))
            {
                return Location.None;
            }

            var sourceText = SourceText.From(text);
            var span = new TextSpan(startIndex, length);
            return Location.Create(filePath, span, sourceText.Lines.GetLinePositionSpan(span));
        }


        private static Utf8JsonReader GetJsonReader(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var reader = new Utf8JsonReader(bytes, new JsonReaderOptions()
            {
                CommentHandling = MainConfig.JsonOptions.ReadCommentHandling,
                AllowTrailingCommas = MainConfig.JsonOptions.AllowTrailingCommas,
            });
            return reader;
        }

        protected static IEnumerable<int> FindStartIndexes(string text, string value, JsonTokenType jsonTokenType)
        {
            var reader = GetJsonReader(text);
            var startIndexes = new List<int>();

            while (reader.Read())
            {
                if (reader.TokenType == jsonTokenType && reader.GetString() == value)
                {
                    startIndexes.Add((int)reader.TokenStartIndex);
                }
            }

            return startIndexes;
        }
    }
}
