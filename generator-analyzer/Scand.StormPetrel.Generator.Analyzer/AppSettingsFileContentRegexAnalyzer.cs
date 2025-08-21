using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Scand.StormPetrel.Generator.Common.TargetProject;
using System;
using System.Text.Json;
using System.Threading;

namespace Scand.StormPetrel.Generator.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class AppSettingsFileContentRegexAnalyzer : AppSettingsFileContentAnalyzer
    {
        private const string DiagnosticId = "SCANDSP1003";
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            "Invalid Regex pattern in Storm Petrel settings JSON file",
            "Regex is invalid: {1}",
            CategoryName.AppSettings,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: HelpLink(DiagnosticId));

        public AppSettingsFileContentRegexAnalyzer()
            : base(Rule)
        {
        }

        protected override void AnalyzeFileContent(AdditionalText file, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            var config = MainConfig.GetNew(file, out var _, out var isJsonException, out var invalidRegexes);

            if (!isJsonException)
            {
                var jsonText = file.GetText(cancellationToken).ToString();

                if (!string.IsNullOrEmpty(jsonText))
                {
                    foreach (var invalidRegex in invalidRegexes)
                    {
                        var startIndexes = FindStartIndexes(jsonText, invalidRegex, JsonTokenType.String);
                        AddDiagnostics(Rule, reportDiagnostic, jsonText, startIndexes, invalidRegex.Length, file.Path, invalidRegex);
                    }
                }
            }
        }
    }
}
