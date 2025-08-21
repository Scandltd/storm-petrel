using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Scand.StormPetrel.Generator.Common;
using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Scand.StormPetrel.Generator.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed partial class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzer : AbstractAnalyzer
    {
        public IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzer()
            : base(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
        {
        }
        protected override void AnalyzeCompilation(CompilationStartAnalysisContext context)
        {
            var analysisState = new AnalysisState();
            context.RegisterSymbolAction(symbolAnalysisContext => CollectAttributeData(symbolAnalysisContext, analysisState), SymbolKind.Method);
            context.RegisterCompilationEndAction(compilationEndContext => AnalyzeCompilationEnd(compilationEndContext, analysisState));
        }
        private static void CollectAttributeData(SymbolAnalysisContext symbolAnalysisContext, AnalysisState analysisState)
        {
            if (!TryParseTestMethod(symbolAnalysisContext.Symbol, out IMethodSymbol methodSymbol, out MethodDeclarationSyntax method))
            {
                return;
            }
            foreach (AttributeData attribute in methodSymbol.GetAttributes())
            {
                string attributeFullName = attribute.AttributeClass?.ToDisplayString();
                var kind = SupportedMethodInfo.GetTestCaseSourceKind(attributeFullName);
                if (kind != TestCaseSourceKind.XUnitClassData)
                {
                    string sourceTypeFullName = null;
                    string sourceName = null;

                    if (kind == TestCaseSourceKind.MSTest || kind == TestCaseSourceKind.NUnit)
                    {
                        sourceTypeFullName = attribute
                            .ConstructorArguments
                            .Where(x => x.Kind == TypedConstantKind.Type && x.Value != null)
                            .Select(x => x.Value.ToString())
                            .FirstOrDefault();

                        if (string.IsNullOrEmpty(sourceTypeFullName))
                        {
                            sourceTypeFullName = methodSymbol.ContainingType.ToDisplayString();
                        }
                        sourceName = attribute
                                        .ConstructorArguments
                                        .Where(x => x.Kind == TypedConstantKind.Primitive && x.Value != null)
                                        .Select(x => x.Value.ToString())
                                        .FirstOrDefault();
                    }

                    if (kind == TestCaseSourceKind.XUnitNonClassData)
                    {
                        var memberType = attribute
                                                .NamedArguments
                                                .Where(x => x.Key == "MemberType")
                                                .Select(x => x.Value.Value)
                                                .SingleOrDefault()
                                                ?? methodSymbol.ContainingType;
                        if (memberType is ITypeSymbol typeSymbol)
                        {
                            sourceTypeFullName = typeSymbol.ToDisplayString();
                        }
                        sourceName = attribute
                                        .ConstructorArguments
                                        .Select(x => x.Value.ToString())
                                        .FirstOrDefault(); //MemberData is very first constructor argument despite its syntax position
                    }
                    if (string.IsNullOrEmpty(sourceTypeFullName) || string.IsNullOrEmpty(sourceName))
                    {
                        continue;
                    }
                    var methodNames = analysisState.SourceTypeFullNameToDataSourceMethodNames.GetOrAdd(sourceTypeFullName, _ => new ConcurrentBag<string>());
                    methodNames.Add(sourceName);
                }
                else
                {
                    if (attribute
                            .ConstructorArguments
                            .FirstOrDefault() //@class is very first constructor argument despite its syntax position
                            .Value is ITypeSymbol sourceType)
                    {
                        string sourceTypeFullName = sourceType.ToDisplayString();
                        analysisState.ClassDataSourceTypeFullName.Add(sourceTypeFullName);
                    }
                }
            }
        }
        private static void AnalyzeCompilationEnd(CompilationAnalysisContext compilationEndContext, AnalysisState analysisState)
        {
            var methodsForAnalysis = GetMethodsForAnalysis(compilationEndContext, analysisState);
            methodsForAnalysis
                .AsParallel()
                .Select(dataSourceMethod =>
                {
                    if (!TryParseMethodSyntax(dataSourceMethod, out var dataSourceMethodSyntax))
                    {
                        return null;
                    }
                    var isExpectedVarInvocationExpressionCandidate = MethodHelperCommon.IsExpectedVarInvocationExpressionCandidate(dataSourceMethodSyntax);
                    bool isUpdatable = false;
                    if (isExpectedVarInvocationExpressionCandidate)
                    {
                        var stormPetrelMethod = new InvocationExpressionHelperMethod().ToStormPetrelNode(dataSourceMethodSyntax, CancellationToken.None);
                        isUpdatable = stormPetrelMethod != default;
                        if (!isUpdatable)
                        {
                            const int veryFirstUpdatingIndex = 0;
                            var rewriter = new EnumerableResultRewriter(SyntaxNodeHelperCommon.GetValuePath(dataSourceMethodSyntax), veryFirstUpdatingIndex, veryFirstUpdatingIndex, "1");
                            var updated = rewriter.Visit(dataSourceMethodSyntax);
                            isUpdatable = updated != dataSourceMethodSyntax;
                        }
                    }
                    if (!isUpdatable)
                    {
                        var location = dataSourceMethodSyntax.GetLocation();
                        var d = IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers
                                    .CreateDiagnostic(dataSourceMethod.Name, location);
                        return d;
                    }
                    return null;
                })
                .Where(x => x != null)
                .AsSequential()
                .ToList()
                .ForEach(x => compilationEndContext.ReportDiagnostic(x));
        }
        private static List<IMethodSymbol> GetMethodsForAnalysis(CompilationAnalysisContext compilationEndContext, AnalysisState analysisState)
        {
            var methodsForAnalysis = new List<IMethodSymbol>();
            foreach (var kv in analysisState.SourceTypeFullNameToDataSourceMethodNames)
            {
                var classSymbol = compilationEndContext.Compilation.GetTypeByMetadataName(kv.Key);
                if (classSymbol == null)
                {
                    continue;
                }
                foreach (var methodName in kv.Value.Distinct())
                {
                    var dataSourceMethod = classSymbol
                                            .GetMembers(methodName)
                                            .OfType<IMethodSymbol>()
                                            .FirstOrDefault();
                    if (dataSourceMethod == null)
                    {
                        continue;
                    }
                    methodsForAnalysis.Add(dataSourceMethod);
                }
            }
            var iEnumerableSymbol = compilationEndContext.Compilation.GetTypeByMetadataName(typeof(IEnumerable).FullName);
            if (iEnumerableSymbol == null)
            {
                return methodsForAnalysis;
            }
            foreach (var className in analysisState.ClassDataSourceTypeFullName)
            {
                var classSymbol = compilationEndContext.Compilation.GetTypeByMetadataName(className);
                if (classSymbol == null)
                {
                    continue;
                }
                foreach (var member in classSymbol.GetMembers())
                {
                    if (!(member is IMethodSymbol method))
                    {
                        continue;
                    }
                    ITypeSymbol returnType = method.ReturnType;
                    if (returnType != null &&
                        (returnType.OriginalDefinition.Equals(iEnumerableSymbol, SymbolEqualityComparer.Default) ||
                            returnType.AllInterfaces.Any(i => i.OriginalDefinition.Equals(iEnumerableSymbol, SymbolEqualityComparer.Default))))
                    {
                        methodsForAnalysis.Add(method);
                    }
                }
            }
            return methodsForAnalysis;
        }
    }
}
