using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Scand.StormPetrel.Generator.Common.TargetProject;
using System;
using System.Threading;

namespace Scand.StormPetrel.Generator.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class AppSettingsFileContentFormedAnalyzer : AppSettingsFileContentAnalyzer
    {
        private const string DiagnosticId = "SCANDSP1001";
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            "Malformed Storm Petrel settings JSON file",
            "Config file '{0}' is not well-formed",
            CategoryName.AppSettings,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: HelpLink(DiagnosticId));

        public AppSettingsFileContentFormedAnalyzer()
            : base(Rule)
        {
        }

        protected override void AnalyzeFileContent(AdditionalText file, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            var config = MainConfig.GetNew(file, out _, out var isJsonException, out _);

            if (isJsonException)
            {
                AddDiagnostic(Rule, null, reportDiagnostic, file.Path);
            }
        }
    }
}
