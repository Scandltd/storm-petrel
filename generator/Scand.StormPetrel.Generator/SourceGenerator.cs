using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Scand.StormPetrel.Generator.ExtraContextInternal;
using Scand.StormPetrel.Generator.TargetProject;
using Scand.StormPetrel.Shared;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;

namespace Scand.StormPetrel.Generator
{
    internal class SourceGenerator
    {
        public static SourceText CreateNewSourceAsSourceText(string syntaxTreeFilePath, SyntaxTree syntaxTree, MainConfig config, ILogger logger, CancellationToken cancellationToken)
            => CreateNewSource(syntaxTreeFilePath, syntaxTree, config, logger, cancellationToken)?.GetText(Encoding.UTF8);
        public static SyntaxNode CreateNewSource(string syntaxTreeFilePath, SyntaxTree syntaxTree, MainConfig config, ILogger logger, CancellationToken cancellationToken)
        {
            var syntaxTreeRoot = syntaxTree.GetRoot(CancellationToken.None);
            var actualClasses = MethodHelper.GetDescendantNodesOptimized(syntaxTreeRoot, a => a is ClassDeclarationSyntax c
                                                                                            ? (c, false)
                                                                                            : (null, true));
            var actualClassToNewClass = new Dictionary<ClassDeclarationSyntax, ClassDeclarationSyntax>();
            var varHelper = new VarHelper(config.TestVariablePairConfigs);
            var syntaxHelper = new SyntaxHelper(syntaxTreeFilePath, config.TargetProjectGeneratorExpression, config.IgnoreInvocationExpressionRegex);
            foreach (var actualClass in actualClasses)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }
                int skipActualClassChildMethodCount = -1;
                int skipNewActualClassChildMethodCount = -1;
                var newActualClass = actualClass;
                do
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return null;
                    }
                    skipActualClassChildMethodCount++;
                    skipNewActualClassChildMethodCount++;
                    var method = GetFirstOrDefaultMethod(newActualClass, skipNewActualClassChildMethodCount);
                    if (method == null)
                    {
                        break;
                    }
                    var testAttributeNames = MethodHelper.GetTestAttributeNames(method).ToArray();
                    if (testAttributeNames.Length == 0)
                    {
                        var isExpectedVarInvocationExpressionCandidate = MethodHelper.IsExpectedVarInvocationExpressionCandidate(method);
                        if (isExpectedVarInvocationExpressionCandidate)
                        {
                            var extraMethod = new InvocationExpressionHelperMethod().ToStormPetrelNode(method, cancellationToken);
                            if (extraMethod != null)
                            {
                                skipNewActualClassChildMethodCount++;
                                newActualClass = newActualClass.InsertNodesAfter(method, new[] { extraMethod });
                                newActualClass = RenameChildConstructorsIfNeed(newActualClass);
                                var newTypeName = actualClass.Identifier.Text + "StormPetrel";
                                newActualClass = newActualClass.WithIdentifier(SyntaxFactory.ParseToken(newTypeName));
                                actualClassToNewClass[actualClass] = newActualClass;
                            }
                        }
                        logger.Information("No test attributes for {MethodName} method, isExpectedVarInvocationExpressionCandidate={isExpectedVarInvocationExpressionCandidate}", method.Identifier.Text, isExpectedVarInvocationExpressionCandidate);
                        continue;
                    }

                    var methodOriginal = GetFirstOrDefaultMethod(actualClass, skipActualClassChildMethodCount);
                    var varPairInfoList = methodOriginal.Body != null
                                            ? varHelper.GetVarPairs(methodOriginal)
                                            : new List<VarPairInfo>();
                    var newMethod = method;
                    int i = -1;
                    foreach (var info in varPairInfoList.OrderByDescending(a => a.StatementIndex))
                    {
                        i++;
                        var oldMethodName = i == 0
                                                ? method.Identifier.Text
                                                : method.Identifier.Text + "StormPetrel";
                        var blocks = syntaxHelper.GetNewCodeBlock(actualClass.Identifier.ValueText, method.Identifier.ValueText, info, varPairInfoList.Count - i - 1, varPairInfoList.Count);
                        var newStatements = newMethod.Body.Statements.InsertRange(info.StatementIndex + 1, blocks);
                        newMethod = newMethod
                                        .WithBody(newMethod.Body.WithStatements(newStatements))
                                        .WithIdentifier(SyntaxFactory.Identifier(method.Identifier.Text + "StormPetrel"));
                        var oldMethod = newActualClass
                                            .ChildNodes()
                                            .OfType<MethodDeclarationSyntax>()
                                            .Single(a => a.Identifier.Text == oldMethodName);
                        if (i == 0 && info.ExpectedVarExtraContextInternal is AttributeContextInternal)
                        {
                            newMethod = MethodHelper.WithStormPetrelTestCaseRelatedStuff(newMethod);
                        }
                        newActualClass = newActualClass.ReplaceNode(oldMethod, newMethod);
                        newActualClass = RenameChildConstructorsIfNeed(newActualClass);
                        var newTypeName = actualClass.Identifier.Text + "StormPetrel";
                        newActualClass = newActualClass.WithIdentifier(SyntaxFactory.ParseToken(newTypeName));
                        actualClassToNewClass[actualClass] = newActualClass;
                    }
                    logger.Information("Test variable pairs count for {MethodName}: {Count}", method.Identifier.Text, varPairInfoList.Count);
                }
                while (true);
            }
            if (actualClassToNewClass.Count == 0)
            {
                logger.Information("No classes to replace in `{syntaxTreeFilePath}`", syntaxTreeFilePath);
                return null;
            }

            SyntaxNode newRoot;
            try
            {
                newRoot = syntaxTreeRoot.ReplaceNodes(actualClassToNewClass.Keys, (x, y) =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    return actualClassToNewClass[x];
                });
            }
            catch (OperationCanceledException)
            {
                return null;
            }

            var nodesToRemove = GetNodesToRemove();
            logger.Information("Remove nodes count for `{syntaxTreeFilePath}` file: {Length}", syntaxTreeFilePath, nodesToRemove.Length);
            newRoot = newRoot.RemoveNodes(nodesToRemove, SyntaxRemoveOptions.KeepNoTrivia);
            return newRoot.NormalizeWhitespace();

            ClassDeclarationSyntax RenameChildConstructorsIfNeed(ClassDeclarationSyntax @class) =>
                @class.Identifier.Text.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase)
                    ? @class //no need to rename, already renamed
                    : @class.ReplaceNodes(@class.ChildNodes().OfType<ConstructorDeclarationSyntax>(), (x, _) => x.WithIdentifier(SyntaxFactory.Identifier(x.Identifier.Text + "StormPetrel")));

            SyntaxNode CandidateToRemove(SyntaxNode x)
            {
                return x is ClassDeclarationSyntax
                        || x is EnumDeclarationSyntax
                        || x is StructDeclarationSyntax
                        || x is RecordDeclarationSyntax
                        || x is InterfaceDeclarationSyntax
                        ? x
                        : null;
            }

            MethodDeclarationSyntax GetFirstOrDefaultMethod(ClassDeclarationSyntax classDeclarationSyntax, int skipCount)
            {
                return classDeclarationSyntax
                            .ChildNodes()
                            .OfType<MethodDeclarationSyntax>()
                            .Skip(skipCount)
                            .FirstOrDefault();
            }

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

        public static string GetStaticPropertyInfoCode(IEnumerable<(PropertyDeclarationSyntax Property, string FilePath)> propertiesInfo, CancellationToken cancellationToken)
        {
            var newCode = ResourceHelper.ReadTargetProjectResource(ResourceHelper.StaticPropertyInfoResourceFileName);
            var sb = new StringBuilder();
            sb.AppendLine(propertiesInfo.Any() ? "new[]" : "new StaticPropertyInfo[]");
            sb.AppendLine("{");
            foreach (var (property, filePath) in propertiesInfo)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var propertyPathStr = SyntaxNodeHelper.GetValuePath(property);
                var propertyNameSegment = propertyPathStr[propertyPathStr.Length - 1];

                propertyPathStr[propertyPathStr.Length - 1] = propertyNameSegment;
                sb.AppendLine("    new StaticPropertyInfo()");
                sb.AppendLine("    {");
                sb.AppendLine(@"        PropertyPath = new[] {""" + string.Join(@""", """, propertyPathStr) + @"""},");
                sb.AppendLine(@"        FilePath = @""" + filePath + @"""");
                sb.AppendLine("    },");
            }
            sb.Append('}');
            newCode = newCode.Replace("_staticPropertyInfoArray = null", "_staticPropertyInfoArray = " + sb.ToString());

            return newCode;
        }

        public static string GetStaticMethodInfoCode(IEnumerable<(MethodDeclarationSyntax Method, bool MethodClassHasTestMethod, string FilePath)> methodsInfo, CancellationToken cancellationToken)
        {
            var newCode = ResourceHelper.ReadTargetProjectResource(ResourceHelper.StaticMethodInfoResourceFileName);
            var sb = new StringBuilder();
            sb.AppendLine(methodsInfo.Any() ? "new[]" : "new StaticMethodInfo[]");
            sb.AppendLine("{");
            foreach (var (method, _, filePath) in methodsInfo)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                var methodPathStr = SyntaxNodeHelper.GetValuePath(method);
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
                sb.AppendLine(@"        MethodArgsCount = " + method.ParameterList.Parameters.Count + @",");
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
