using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scand.StormPetrel.Generator.Common;
using Scand.StormPetrel.Generator.Common.TargetProject;

namespace Scand.StormPetrel.Generator.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class IsTestMethodSuitableForBaselineUpdateAnalyzer : AbstractAnalyzer
    {
        public IsTestMethodSuitableForBaselineUpdateAnalyzer()
            : base(IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
        {
        }
        protected override void AnalyzeCompilation(CompilationStartAnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSyntaxNode, SymbolKind.Method);
        }
        private void AnalyzeSyntaxNode(SymbolAnalysisContext context)
        {
            if (!TryParseTestMethod(context.Symbol, out var _, out MethodDeclarationSyntax method))
            {
                return;
            }
            AdditionalText configFile = GetAppSettingsAdditionalText(context);
            var config = MainConfig.GetNew(configFile, out var _, out var __, out var ___);
            var varHelper = new VarHelper(config.TestVariablePairConfigs);
            var varPairInfoList = varHelper.GetVarPairs(method);
            if (varPairInfoList.Count == 0)
            {
                var methodName = method.Identifier.Text;
                var location = method.GetLocation();
                var d = IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers
                            .CreateDiagnostic(methodName, location);
                context.ReportDiagnostic(d);
            }
        }
    }
}
