using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Scand.StormPetrel.Generator.Analyzer
{
    internal static class AppSettingsFileReferenceAnalyzerHelpers
    {
        public const string DiagnosticId = "SCANDSP1000";
        private const string Title = "Unreferenced Storm Petrel settings JSON file";
        private const string MessageFormat = "File '{0}' exists but is not referenced as `C# analyzer additional file` in the project";
        private const string Category = CategoryName.AppSettings;

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: AbstractAnalyzer.HelpLink(DiagnosticId));
        public static Diagnostic CreateDiagnostic(string appSettingsFilePath)
        {
            var sourceText = SourceText.From("");
            var textSpan = new TextSpan(start: 0, length: 0);
            var lineSpan = sourceText.Lines.GetLinePositionSpan(textSpan);
            return Diagnostic.Create(
                    Rule,
                    Location.Create(appSettingsFilePath, textSpan, lineSpan),
                    appSettingsFilePath);
        }
    }
}