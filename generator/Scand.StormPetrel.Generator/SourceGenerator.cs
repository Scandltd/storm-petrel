using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Scand.StormPetrel.Generator.Common;
using Scand.StormPetrel.Generator.Common.ExtraContextInternal;
using Scand.StormPetrel.Generator.Common.TargetProject;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Scand.StormPetrel.Generator
{
    internal partial class SourceGenerator
    {
        private const SyntaxRemoveOptions NodeRemoveOptions = SyntaxRemoveOptions.KeepNoTrivia | SyntaxRemoveOptions.KeepDirectives;
        public static SourceText CreateNewSourceAsSourceText(string syntaxTreeFilePath, SyntaxTree syntaxTree, MainConfigParsed config, ILogger logger, CancellationToken cancellationToken)
            => CreateNewSource(syntaxTreeFilePath, syntaxTree, config, logger, cancellationToken)?.GetText(Encoding.UTF8);
        public static SyntaxNode CreateNewSource(string syntaxTreeFilePath, SyntaxTree syntaxTree, MainConfigParsed config, ILogger logger, CancellationToken cancellationToken)
        {
            var collector = new TestInfoCollector();
            var newSource = CreateNewSourceImplementation(syntaxTreeFilePath, syntaxTree, config, collector, logger, cancellationToken);
            if (!collector.IsTestMethodCollected)
            {
                return null;
            }
            return newSource;
        }
        public static (SyntaxNode, IEnumerable<(string[] Path, int ParametersCount)> MethodInfo, IEnumerable<string[]> PropertyPaths) CreateNewSourceForStaticStuff(string syntaxTreeFilePath, SyntaxTree syntaxTree, MainConfigParsed config, ILogger logger, CancellationToken cancellationToken)
        {
            var collector = new StaticInfoCollector();
            var newSource = CreateNewSourceImplementation(syntaxTreeFilePath, syntaxTree, config, collector, logger, cancellationToken);
            if (collector.IsTestMethodCollected
                    || collector.CollectedMethodInfo.Count == 0 && collector.CollectedPropertyPaths.Count == 0)
            {
                newSource = null;
            }
            return (newSource, collector.CollectedMethodInfo, collector.CollectedPropertyPaths);
        }
        private static SyntaxNode CreateNewSourceImplementation(string syntaxTreeFilePath, SyntaxTree syntaxTree, MainConfigParsed config, AbstractInfoCollector infoCollector, ILogger logger, CancellationToken cancellationToken)
        {
            var syntaxTreeRoot = syntaxTree.GetRoot(CancellationToken.None);
            var classes = MethodHelper.GetDescendantNodesOptimized(syntaxTreeRoot, a => a is ClassDeclarationSyntax c
                                                                                            ? (c, false)
                                                                                            : (null, true));
            SyntaxNode newRoot;
            var varHelper = new VarHelper(config.TestVariablePairConfigs);
            var syntaxHelper = new SyntaxHelper(syntaxTreeFilePath, config.TargetProjectGeneratorExpression, config.IgnoreInvocationExpressionRegex);
            newRoot = syntaxTreeRoot.ReplaceNodes(classes, (_, @class) =>
            {
                infoCollector.CollectClassDeclaration(@class);
                int skipClassChildMethodCount = -1;
                int skipNewClassChildMethodCount = -1;
                var newClass = @class;
                var csharp14ExtensionChildrenToRemove =
                    newClass
                        .ChildNodes()
                        .Where(x => x.RawKind == 9079 /* ExtensionDeclaration */)
                        .SelectMany(x => x.ChildNodes()
                                            .Where(y => y.IsKind(SyntaxKind.MethodDeclaration) && !MethodHelperCommon.HasPrivateOrNoAccessModifiers((MethodDeclarationSyntax)y)
                                                        || y.IsKind(SyntaxKind.PropertyDeclaration) && !MethodHelperCommon.HasPrivateOrNoAccessModifiers(((PropertyDeclarationSyntax)y).Modifiers)))
                        .ToArray();
                if (csharp14ExtensionChildrenToRemove.Length > 0)
                {
                    newClass = newClass.RemoveNodes(csharp14ExtensionChildrenToRemove, NodeRemoveOptions);
                }
                newClass = RenameChildConstructorsAndOtherTokensIfNeed(newClass);
                var newTypeName = @class.Identifier.Text + "StormPetrel";
                newClass = newClass.WithIdentifier(SyntaxFactory.ParseToken(newTypeName));
                var newClassCopyBeforeMethodChanges = newClass;
                do
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return @class;
                    }
                    skipClassChildMethodCount++;
                    skipNewClassChildMethodCount++;
                    var method = GetFirstOrDefaultMethod(newClass, skipNewClassChildMethodCount);
                    if (method == null)
                    {
                        break;
                    }
                    var hasTestAttributeNames = MethodHelperCommon.GetTestAttributeNames(method).Any();
                    if (!hasTestAttributeNames)
                    {
                        var isExpectedVarInvocationExpressionCandidate = MethodHelperCommon.IsExpectedVarInvocationExpressionCandidate(method);
                        var isExtensionMethodAndNotPrivate = false;
                        if (isExpectedVarInvocationExpressionCandidate)
                        {
                            var original = GetFirstOrDefaultMethod(@class, skipClassChildMethodCount);
                            infoCollector.CollectExpectedVarInvocationExpressionCandidate(original);
                            var extraMethod = new InvocationExpressionHelperMethod().ToStormPetrelNode(method, cancellationToken);
                            if (extraMethod != null)
                            {
                                skipNewClassChildMethodCount++;
                                newClass = newClass.InsertNodesAfter(method, new[] { extraMethod });
                            }
                        }
                        else if (MethodHelperCommon.IsExtensionMethod(method) && !MethodHelperCommon.HasPrivateOrNoAccessModifiers(method))
                        {
                            isExtensionMethodAndNotPrivate = true;
                            newClass = newClass.RemoveNode(method, NodeRemoveOptions);
                            skipNewClassChildMethodCount--;
                        }
                        logger.Information("No test attributes for {MethodName} method, isExpectedVarInvocationExpressionCandidate={isExpectedVarInvocationExpressionCandidate}, isExtensionMethodAndNotPrivate={isExtensionMethodAndNotPrivate}", method.Identifier.Text, isExpectedVarInvocationExpressionCandidate, isExtensionMethodAndNotPrivate);
                        continue;
                    }
                    infoCollector.IsTestMethodCollected = true;
                    if (!infoCollector.NeedStormPetrelTestMethods)
                    {
                        //for performance reasons
                        continue;
                    }
                    var methodOriginal = GetFirstOrDefaultMethod(@class, skipClassChildMethodCount);
                    var varPairInfoList = varHelper.GetVarPairs(methodOriginal);
                    var newMethod = method;
                    int i = -1;
                    bool addStormPetrelUseCaseIndex = varPairInfoList.Any(x => x.ExpectedVarExtraContextInternal is AttributeContextInternal);
                    foreach (var info in varPairInfoList
                                            .OrderByDescending(a => a.StatementIndex)
                                            .ThenByDescending(a => a.StatementIndexForSubOrder))
                    {
                        i++;
                        var oldMethodName = i == 0
                                                ? method.Identifier.Text
                                                : method.Identifier.Text + "StormPetrel";
                        var blocks = syntaxHelper.GetNewCodeBlock(@class.Identifier.ValueText, method.Identifier.ValueText, info, varPairInfoList.Count - i - 1, varPairInfoList.Count, method.ParameterList.Parameters, config.IsAddNullableEnable);

                        if (method.Body != null)
                        {
                            var newStatements = newMethod.Body.Statements.InsertRange(info.StatementIndex + 1, blocks);
                            newMethod = newMethod
                                            .WithBody(newMethod.Body.WithStatements(newStatements));
                        }
                        else
                        {
                            //Ignore newMethod return type because it is void or Task. Do not support other types because it is extremely rare scenario.
                            blocks.Add(SyntaxFactory.ExpressionStatement(method.ExpressionBody.Expression));
                            newMethod = newMethod
                                            .WithExpressionBody(null)
                                            .WithSemicolonToken(SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken))
                                            .WithBody(SyntaxFactory.Block(SyntaxFactory.List(blocks)));
                        }
                        newMethod = newMethod
                                        .WithIdentifier(SyntaxFactory.Identifier(method.Identifier.Text + "StormPetrel"));
                        var oldMethod = newClass
                                            .ChildNodes()
                                            .OfType<MethodDeclarationSyntax>()
                                            .Single(a => a.Identifier.Text == oldMethodName);
                        if (addStormPetrelUseCaseIndex)
                        {
                            newMethod = MethodHelper.WithStormPetrelTestCaseRelatedStuff(newMethod);
                            addStormPetrelUseCaseIndex = false;
                        }
                        newClass = newClass.ReplaceNode(oldMethod, newMethod);
                    }
                    logger.Information("Test variable pairs count for {MethodName}: {Count}", method.Identifier.Text, varPairInfoList.Count);
                }
                while (true);
                if (newClass == newClassCopyBeforeMethodChanges)
                {
                    return @class;
                }
                newClass = GeneratedCodeAttribute.WithAttribute(newClass);
                return newClass;
            });
            if (newRoot == syntaxTreeRoot)
            {
                logger.Information("No classes to replace in `{syntaxTreeFilePath}`", syntaxTreeFilePath);
                return null;
            }

            var nodesToRemove = GetNodesToRemove();
            logger.Information("Remove nodes count for `{syntaxTreeFilePath}` file: {Length}", syntaxTreeFilePath, nodesToRemove.Length);
            newRoot = newRoot.RemoveNodes(nodesToRemove, SyntaxRemoveOptions.KeepNoTrivia);
            IEnumerable<SyntaxTrivia> newTrivias = new[]
            {
                    SyntaxFactory.Comment("// <auto-generated />"),
                    SyntaxFactory.ElasticCarriageReturnLineFeed,
            };
            if (config.IsAddNullableEnable)
            {
                newTrivias = newTrivias.Concat(new[]
                {
                    SyntaxHelper.IfDirectiveTriviaForNullableEnable(),
                    SyntaxFactory.ElasticCarriageReturnLineFeed,
                    SyntaxFactory.Trivia(
                        SyntaxFactory.NullableDirectiveTrivia(
                            SyntaxFactory.Token(SyntaxKind.EnableKeyword),
                            true
                        )),
                    SyntaxFactory.ElasticCarriageReturnLineFeed,
                    SyntaxHelper.EndIfDirectiveTrivia(),
                    SyntaxFactory.ElasticCarriageReturnLineFeed,
                });
            }
            var newLeadingTrivia = newRoot
                                    .GetLeadingTrivia()
                                    .InsertRange(0, newTrivias);
            newRoot = newRoot.WithLeadingTrivia(newLeadingTrivia);
            return newRoot.NormalizeWhitespace();

            ClassDeclarationSyntax RenameChildConstructorsAndOtherTokensIfNeed(ClassDeclarationSyntax @class)
            {
                if (@class.Identifier.Text.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase))
                {
                    return @class; //no need to rename, already renamed
                }

                var oldName = @class.Identifier.Text;
                var newName = oldName + "StormPetrel";

                // Replace class identifier first preserving trivia
                var newClass = @class.WithIdentifier(SyntaxFactory.Identifier(@class.Identifier.LeadingTrivia, newName, @class.Identifier.TrailingTrivia));

                // Replace any identifier tokens inside the class that match the old name (constructors, type references, etc.)
                // But do not rename tokens that are part of a qualified name (e.g. Namespace.MyType) or member access where
                // the identifier is qualified by a namespace-like segment.
                var tokensToReplace = newClass
                    .DescendantTokens()
                    .Where(x => x.IsKind(SyntaxKind.IdentifierToken) && x.ValueText == oldName)
                    .Where(x =>
                    {
                        var parent = x.Parent;
                        if (parent == null)
                        {
                            return false;
                        }

                        // Do not rename identifiers that are part of attribute usages because they are definitely do not match the class we rename
                        if (parent.AncestorsAndSelf().OfType<AttributeSyntax>().Any())
                        {
                            return false;
                        }

                        // Allow constructor declarations (e.g. `public MyClass() { }`)
                        if (parent is ConstructorDeclarationSyntax)
                        {
                            return true;
                        }

                        // Only allow renames for a few specific SimpleName usages:
                        // - object creation expressions (constructor calls): `new MyClass()`
                        // - type names in variable/field declarations: `MyClass x;` (covers local and field declarations)
                        // - type names in property declarations: `public MyClass Prop { get; set; }`
                        if (parent is SimpleNameSyntax simpleName)
                        {
                            var pp = simpleName.Parent;
                            // Skip qualified/alias/member access (Namespace.MyType, global::MyType, obj.Type)
                            if (pp is QualifiedNameSyntax q && q.Right == simpleName
                                || pp is AliasQualifiedNameSyntax a && a.Name == simpleName
                                // Member access can be used in expressions (obj.Type) or in some type references; treat as qualified and skip.
                                || pp is MemberAccessExpressionSyntax m && m.Name == simpleName)
                            {
                                return false;
                            }

                            if (pp is NullableTypeSyntax)
                            {
                                pp = pp.Parent;
                            }

                            if (
                                // Constructor call: `new MyClass()`
                                pp is ObjectCreationExpressionSyntax
                                // Variable/field declaration: `MyClass x;` (covers local declarations and fields)
                                || pp is VariableDeclarationSyntax
                                // Property declaration: `public MyClass Prop { get; set; }`
                                || pp is PropertyDeclarationSyntax
                                // Method declaration: `public MyClass Method()`
                                || pp is MethodDeclarationSyntax
                                // Named tuple element type: `(MyClass x, int y)`
                                || pp is DeclarationExpressionSyntax
                                // Generic type argument: `MyGenericType<MyClass>`
                                || pp is TypeArgumentListSyntax)
                            {
                                return true;
                            }

                            return false;
                        }

                        return false;
                    });
                newClass = newClass.ReplaceTokens(tokensToReplace, (x, _) => SyntaxFactory.Identifier(x.LeadingTrivia, newName, x.TrailingTrivia));
                return newClass;
            }

            SyntaxNode CandidateToRemove(SyntaxNode x) =>
                x is ClassDeclarationSyntax
                    || x is EnumDeclarationSyntax
                    || x is StructDeclarationSyntax
                    || x is RecordDeclarationSyntax
                    || x is InterfaceDeclarationSyntax
                    ? x
                    : null;

            MethodDeclarationSyntax GetFirstOrDefaultMethod(ClassDeclarationSyntax classDeclarationSyntax, int skipCount) =>
                MethodHelperCommon
                    .GetMethods(classDeclarationSyntax)
                    .Skip(skipCount)
                    .FirstOrDefault();

            SyntaxNode[] GetNodesToRemove()
            {
                var candidatesToRemove = MethodHelper
                                            .GetDescendantNodesOptimized(newRoot, x =>
                                            {
                                                var c = CandidateToRemove(x);
                                                return (c, c == null);
                                            });
                var stormPetrelClasses = candidatesToRemove
                                            .Where(x => x is ClassDeclarationSyntax classDeclaration
                                                            && (classDeclaration.Identifier.ValueText.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase)
                                                                    || MethodHelper
                                                                            .GetDescendantNodesOptimized(classDeclaration, y =>
                                                                            {
                                                                                if (y is MethodDeclarationSyntax m)
                                                                                {
                                                                                    return (m.Identifier.ValueText.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase) ? m : null, false);
                                                                                }
                                                                                return (null, true);
                                                                            }, true)
                                                                            .Count != 0))
                                            .ToImmutableHashSet();
                var neededNodes = candidatesToRemove
                                        .Where(x => stormPetrelClasses.Contains(x)
                                                        || x.Ancestors().Any(y => stormPetrelClasses.Contains(y))
                                                        || x.DescendantNodes(y => CandidateToRemove(y) == null)
                                                            .Any(y => stormPetrelClasses.Contains(y)))
                                        .ToImmutableHashSet();
                return candidatesToRemove
                            .Where(x => !neededNodes.Contains(x))
                            .ToArray();
            }
        }

        public static string GetStaticPropertyInfoCode(IEnumerable<(string[] PropertyPath, string FilePath)> propertiesInfo, CancellationToken cancellationToken)
        {
            var newCode = ResourceHelper.ReadTargetProjectResource(ResourceHelper.StaticPropertyInfoResourceFileName);
            var sb = new StringBuilder();
            sb.AppendLine(propertiesInfo.Any() ? "new[]" : "new StaticPropertyInfo[]");
            sb.AppendLine("{");
            foreach (var (propertyPath, filePath) in propertiesInfo)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                sb.AppendLine("    new StaticPropertyInfo()");
                sb.AppendLine("    {");
                sb.AppendLine(@"        PropertyPath = new[] {""" + string.Join(@""", """, propertyPath) + @"""},");
                sb.AppendLine(@"        FilePath = @""" + filePath + @"""");
                sb.AppendLine("    },");
            }
            sb.Append('}');
            newCode = newCode.Replace("_staticPropertyInfoArray = null", "_staticPropertyInfoArray = " + sb.ToString());

            return newCode;
        }

        public static string GetStaticMethodInfoCode(IEnumerable<(string[] MethodPath, int MethodParametersCount, string FilePath)> methodsInfo, CancellationToken cancellationToken)
        {
            var newCode = ResourceHelper.ReadTargetProjectResource(ResourceHelper.StaticMethodInfoResourceFileName);
            var sb = new StringBuilder();
            sb.AppendLine(methodsInfo.Any() ? "new[]" : "new StaticMethodInfo[]");
            sb.AppendLine("{");
            foreach (var (methodPath, methodParametersCount, filePath) in methodsInfo)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var methodPathStr = methodPath.ToArray(); //copy to modify
                var methodNameSegment = methodPathStr[methodPathStr.Length - 1];
                if (methodNameSegment.IndexOf('[') > -1)
                {
                    methodNameSegment = methodNameSegment.Substring(0, methodNameSegment.IndexOf('['));
                }
                //Select any method. Appropriate overload should be selected by method arguments
                methodPathStr[methodPathStr.Length - 1] = methodNameSegment + "[*]";
                sb.AppendLine("    new StaticMethodInfo()");
                sb.AppendLine("    {");
                sb.AppendLine(@"        MethodPath = new[] {""" + string.Join(@""", """, methodPathStr) + @"""},");
                sb.AppendLine(@"        MethodArgsCount = " + methodParametersCount + @",");
                sb.AppendLine(@"        FilePath = @""" + filePath + @"""");
                sb.AppendLine("    },");
            }
            sb.Append('}');
            newCode = newCode.Replace("_staticMethodInfoArray = null", "_staticMethodInfoArray = " + sb.ToString());
            return newCode;
        }

        public static void AddCommonStormPetrelSourceToContext(GeneratorConfig generatorConfig, SourceProductionContext context)
        {
            string dumperFileName = null;
            if (MainConfig.IsFromTargetProjectNamespace(generatorConfig.DumperExpression))
            {
                if (generatorConfig.DumperExpression.Contains("GeneratorDumper"))
                {
                    dumperFileName = "GeneratorDumper";
                }
                else if (generatorConfig.DumperExpression.Contains("GeneratorObjectDumper"))
                {
                    dumperFileName = "GeneratorObjectDumper";
                }
            }
            var fileNames = new[]
            {
                    "Generator",
                    "GeneratorBackuper",
                    dumperFileName,
                    "GeneratorRewriter",
            };
            foreach (var fileName in fileNames.Where(a => a != null))
            {
                var newCode = ResourceHelper.ReadTargetProjectResource(fileName);
                if (fileName == "Generator")
                {
                    newCode = newCode.Replace("(IGeneratorDumper)null", $"(IGeneratorDumper){generatorConfig.DumperExpression}");
                    newCode = newCode.Replace("(IGeneratorRewriter)null", $"(IGeneratorRewriter){generatorConfig.RewriterExpression}");
                }
                else if (fileName == "GeneratorRewriter")
                {
                    string backuperExpression = string.IsNullOrEmpty(generatorConfig.BackuperExpression) ? "null" : generatorConfig.BackuperExpression;
                    newCode = newCode.Replace("(IGeneratorBackuper)null", $"(IGeneratorBackuper){backuperExpression}");
                }
                newCode = newCode.Replace("// {GeneratedCodeAttribute}", GeneratedCodeAttribute.GetAttributeFullString());

                //Mark as "public" to indicate its meaning and thus carefully change public stuff,
                //but eventually make them "internal" within target project to not expose them.
                newCode = newCode.Replace("public sealed class", "internal sealed class");
                newCode = newCode.Replace("public sealed partial class", "internal sealed partial class");
                newCode = newCode.Replace("public interface", "internal interface");
                newCode = newCode.Replace("public enum", "internal enum");
                context.AddSource($"Scand.StormPetrel.Generator.TargetProject.{fileName}.g.cs", newCode);
            }
        }
    }
}
