using Microsoft.CodeAnalysis;

namespace Scand.StormPetrel.Generator.Analyzer
{
    internal class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers
    {
        public const string DiagnosticId = "SCANDSP3000";
        private const string Title = "Test data source method design unsuitable for baseline updates";
        private const string MessageFormat = "Storm Petrel cannot detect baselines to update in '{0}' data source method, see more details for use cases and configuration: https://github.com/Scandltd/storm-petrel/blob/main/generator/README.md";
        private const string Category = CategoryName.TestMethodDataSource;

        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            Title,
            MessageFormat,
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: AbstractAnalyzer.HelpLink(DiagnosticId));
        public static Diagnostic CreateDiagnostic(string methodName, Location methodLocation) =>
            Diagnostic.Create(
                        Rule,
                        methodLocation,
                        methodName);
    }
}
