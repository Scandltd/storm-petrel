using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Scand.StormPetrel.Generator.TargetProject;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Scand.StormPetrel.Generator
{
    [Generator]
    public sealed partial class StormPetrelGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var jsonConfigFile = context
                                    .AdditionalTextsProvider
                                    .Where(file => file.Path.EndsWith("appsettings.StormPetrel.json", StringComparison.OrdinalIgnoreCase))
                                    .Collect()
                                    .Select((a, _) => a.FirstOrDefault());

            IncrementalValuesProvider<SyntaxTree> syntaxProvider = default;
            foreach (var attributeFullName in SupportedMethodInfo.AttributeFullNames)
            {
                var sp = context
                            .SyntaxProvider
                            .ForAttributeWithMetadataName(
                                attributeFullName,
                                predicate: (node, _) =>
                                {
                                    return (node.Parent as TypeDeclarationSyntax) != null;
                                },
                                transform: (syntaxContext, _) =>
                                {
                                    var semanticModel = syntaxContext.SemanticModel;
                                    var type = (ITypeSymbol)semanticModel.GetDeclaredSymbol(syntaxContext.TargetNode.Parent, CancellationToken.None);
                                    return type
                                            .Locations
                                            .Select(x => x.SourceTree);
                                })
                            .SelectMany((x, _) => x.Where(y => y != null))
                            .Collect()
                            .SelectMany((x, _) => x.Distinct());
                if (syntaxProvider.Equals(default(IncrementalValuesProvider<SyntaxTree>)))
                {
                    syntaxProvider = sp;
                }
                else
                {
                    var collectedOne = syntaxProvider.Collect();
                    var collectedTwo = sp.Collect();
                    syntaxProvider = collectedOne
                                        .Combine(collectedTwo)
                                        .SelectMany((pair, cancellationToken) => pair.Left.AddRange(pair.Right))
                                        .Collect()
                                        .SelectMany((x, _) => x.Distinct());
                }
            }

            var combinedProviderFirst = jsonConfigFile
                                            .Combine(syntaxProvider.Collect())
                                            .Select((x, _) => (SyntaxTree: x.Right.FirstOrDefault(), AdditionalText: x.Left));

            context.RegisterImplementationSourceOutput(combinedProviderFirst, (syntaxContext, combined) =>
            {
                var filePath = combined.AdditionalText != null
                                ? combined.AdditionalText.Path
                                : combined.SyntaxTree?.FilePath;
                if (filePath == null)
                {
                    return;
                }
                var (config, _) = GeneratorInfoCache.Get(combined.AdditionalText, filePath, true);
                SourceGenerator.AddCommonStormPetrelSourceToContext(config.GeneratorConfig, syntaxContext);
            });

            var combinedProvider = jsonConfigFile
                                        .Combine(syntaxProvider.Collect())
                                        .SelectMany((x, _) => x.Right.Select(y => (SyntaxTree: y, AdditionalText: x.Left)));

            context.RegisterImplementationSourceOutput(combinedProvider, (syntaxContext, combined) =>
            {
                var filePath = combined.SyntaxTree.FilePath;
                var (config, logger) = GeneratorInfoCache.Get(combined.AdditionalText, filePath);
                if (config.IsDisabled)
                {
                    logger.Information("Source Generator is disabled. No StormPetrel tests will be generated");
                    return;
                }
                if (config.IsMatchToIgnoreFilePathRegex(filePath))
                {
                    logger.Information("Ignore `{FilePath}` file", filePath);
                    return;
                }
                logger.Information("Regenerating tests for `{FilePath}` file", filePath);
                var newSource = SourceGenerator.CreateNewSourceAsSourceText(filePath, combined.SyntaxTree, config, logger, syntaxContext.CancellationToken);
                if (newSource != null)
                {
                    var sourcePath = GeneratorInfoCache.ToSourcePath(combined.AdditionalText?.Path, filePath);
                    syntaxContext.AddSource($"{sourcePath}.g.cs", newSource);
                }
            });

            var syntaxProviderForClassWithStatic = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: (node, cancellationToken) =>
                {
                    //Ignore auto generated file from xUnit v3
                    if (node.SyntaxTree.FilePath.EndsWith("XunitAutoGeneratedEntryPoint.cs", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                    MethodDeclarationSyntax methodDeclaration = node as MethodDeclarationSyntax;
                    PropertyDeclarationSyntax propertyDeclaration = node as PropertyDeclarationSyntax;
                    if (methodDeclaration != null || propertyDeclaration != null)
                    {
                        if (methodDeclaration != null
                           ? MethodHelper.IsExpectedVarInvocationExpressionCandidate(methodDeclaration)
                           : MethodHelper.IsExpectedVarInvocationExpressionCandidate(propertyDeclaration))
                        {
                            return (node.Parent as ClassDeclarationSyntax) != null;
                        }
                    }
                    return false;
                },
                transform: (syntaxContext, cancellationToken) =>
                {
                    var semanticModel = syntaxContext.SemanticModel;
                    var type = (ITypeSymbol)semanticModel.GetDeclaredSymbol(syntaxContext.Node.Parent, cancellationToken);
                    return type
                            .Locations
                            .Select(x => x.SourceTree);
                })
                .SelectMany((x, _) => x.Where(y => y != null).Distinct())
                .Collect();

            var combinedProviderForClassWithStatic = jsonConfigFile
                                                        .Combine(syntaxProviderForClassWithStatic)
                                                        .Select((pair, cancellationToken) => (AdditionalText: pair.Left, SyntaxTrees: pair.Right));

            context.RegisterImplementationSourceOutput(combinedProviderForClassWithStatic, (syntaxContext, combined) =>
            {
                MainConfig config = null;
                ILogger logger = Logger.None;
                if (combined.AdditionalText != null || combined.SyntaxTrees.Any())
                {
                    (config, logger) = GeneratorInfoCache.Get(combined.AdditionalText, combined.SyntaxTrees.FirstOrDefault()?.FilePath);
                }
                bool isConfigNullOrDisabled = config == null || config.IsDisabled;
                var invocationInfo = isConfigNullOrDisabled
                                        ? Enumerable.Empty<(SyntaxNode NewSource, IEnumerable<(string[] Path, int ParametersCount)> MethodInfo, IEnumerable<string[]> PropertyPaths, string FilePath)>()
                                        : combined
                                            .SyntaxTrees
                                            .Distinct()
                                            .Where(a => config == null || !config.IsMatchToIgnoreFilePathRegex(a.FilePath))
                                            .AsParallel()
                                            .Select(a =>
                                            {
                                                var (newSource, methodInfo, propertyPaths) = SourceGenerator.CreateNewSourceForStaticStuff(a.FilePath, a, config, logger, syntaxContext.CancellationToken);
                                                return (NewSource: newSource, MethodInfo: methodInfo, PropertyPaths: propertyPaths, a.FilePath);
                                            })
                                            .AsSequential();

                var methodsInfo = invocationInfo
                                    .SelectMany(a => a.MethodInfo.Select(b => (MethodPath: b.Path, MethodParametersCount: b.ParametersCount, a.FilePath)));
                var newMethodsCode = SourceGenerator.GetStaticMethodInfoCode(methodsInfo, syntaxContext.CancellationToken);
                syntaxContext.AddSource($"Scand.StormPetrel.Generator.TargetProject.{ResourceHelper.StaticMethodInfoResourceFileName}.g.cs", newMethodsCode);

                var propertiesInfo = invocationInfo
                                        .SelectMany(a => a.PropertyPaths.Select(b => (Properties: b, a.FilePath)));
                var newPropertiesCode = SourceGenerator.GetStaticPropertyInfoCode(propertiesInfo, syntaxContext.CancellationToken);
                syntaxContext.AddSource($"Scand.StormPetrel.Generator.TargetProject.{ResourceHelper.StaticPropertyInfoResourceFileName}.g.cs", newPropertiesCode);

                if (isConfigNullOrDisabled)
                {
                    return;
                }

                var info = invocationInfo
                            .Where(a => a.NewSource != null); //Files with test methods are already taken into account in another place
                foreach (var (newSource, _, _, filePath) in info)
                {
                    var sourcePath = GeneratorInfoCache.ToSourcePath(combined.AdditionalText?.Path, filePath);
                    //Add "StormPetrel" classes for methods which can be called from test methods
                    syntaxContext.AddSource($"{sourcePath}.g.cs", newSource.GetText(Encoding.UTF8));
                }
            });
        }
    }
}
