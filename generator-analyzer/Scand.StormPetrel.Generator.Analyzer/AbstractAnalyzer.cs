using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scand.StormPetrel.Generator.Common;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Scand.StormPetrel.Generator.Analyzer
{
    public abstract class AbstractAnalyzer : DiagnosticAnalyzer
    {
        private protected AbstractAnalyzer()
        {
        }
        protected AbstractAnalyzer(params DiagnosticDescriptor[] descriptors)
        {
            SupportedDiagnostics = descriptors.ToImmutableArray();
        }
        protected abstract void AnalyzeCompilation(CompilationStartAnalysisContext context);
        public sealed override void Initialize(AnalysisContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
            context.EnableConcurrentExecution();
            context.RegisterCompilationStartAction(AnalyzeCompilation);
        }
        public sealed override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
        internal static string HelpLink(string id) => $"https://github.com/Scandltd/storm-petrel/tree/main/generator-analyzer/rules/{id}.md";
        protected static bool TryParseTestMethod(ISymbol methodCandidate, out IMethodSymbol methodSymbol, out MethodDeclarationSyntax method)
        {
            if (methodCandidate is IMethodSymbol tempMethodSymbol
                    && tempMethodSymbol.ContainingType?.Name.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase) == false
                    && TryParseMethodSyntax(tempMethodSymbol, out var tempMethod)
                    && MethodHelperCommon.GetTestAttributeNames(tempMethod).Any())
            {
                methodSymbol = tempMethodSymbol;
                method = tempMethod;
                return true;
            }
            methodSymbol = null;
            method = null;
            return false;
        }
        private protected static bool TryParseMethodSyntax(IMethodSymbol methodSymbol, out MethodDeclarationSyntax method)
        {
            if (methodSymbol.IsPartialDefinition
                && methodSymbol.PartialImplementationPart?.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is MethodDeclarationSyntax tempPartialMethod)
            {
                method = tempPartialMethod;
                return true;
            }
            else if (methodSymbol.DeclaringSyntaxReferences.FirstOrDefault()?.GetSyntax() is MethodDeclarationSyntax tempMethod)
            {
                method = tempMethod;
                return true;
            }
            method = null;
            return false;
        }
        protected static AdditionalText GetAppSettingsAdditionalText(SymbolAnalysisContext context) =>
            Utils
                .GetAppSetting(context
                                    .Options
                                    .AdditionalFiles
                                    .Where(Utils.IsAppSettings),
                                x => x.Path);
    }
}
