using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.TargetProject;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    internal class SyntaxHelper
    {
        private readonly string _syntaxTreeFilePath;
        private readonly string _targetProjectGeneratorExpression;
        public SyntaxHelper(string syntaxTreeFilePath, string targetProjectGeneratorExpression)
        {
            _syntaxTreeFilePath = syntaxTreeFilePath;
            _targetProjectGeneratorExpression = targetProjectGeneratorExpression;
        }
        private static ExpressionSyntax GetArrayInitializer(string[] arr)
        {
            var arrayExpression = SyntaxFactory.InitializerExpression(
                SyntaxKind.ArrayInitializerExpression,
                SyntaxFactory.SeparatedList<ExpressionSyntax>(
                    arr.Select(a => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(a)))
                )
            );

            ArrayTypeSyntax arrayType = SyntaxFactory.ArrayType(
                SyntaxFactory.OmittedTypeArgument()
            )
            .WithRankSpecifiers(
                SyntaxFactory.SingletonList(
                    SyntaxFactory.ArrayRankSpecifier(
                        SyntaxFactory.SingletonSeparatedList<ExpressionSyntax>(
                            SyntaxFactory.OmittedArraySizeExpression()
                        )
                    )
                )
            );

            ExpressionSyntax completeArrayCreationExpression = SyntaxFactory.ArrayCreationExpression(
                arrayType,
                arrayExpression
            );
            return completeArrayCreationExpression;
        }

        public List<StatementSyntax> GetNewCodeBlock(string className, string methodName, string[] testAttributeNames, VarPairInfo info, int blockIndex, bool isLastVariablePair)
        {
            ExpressionSyntax expectedVariableInvocationExpressionInfo = null;
            ExpressionSyntax testCaseInfo = null;
            var result = new List<StatementSyntax>();
            if (info.ExpectedVarInvocationExpressionPath != null)
            {
                expectedVariableInvocationExpressionInfo = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName("Scand.StormPetrel.Generator.TargetProject.VariableInvocationExpressionInfo()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment("Path", GetArrayInitializer(info.ExpectedVarInvocationExpressionPath)),
                                info.RewriterKind != RewriterKind.PropertyExpression
                                    ? GetPropertyAssignment("NodeInfo",
                                        GetMethodsInvocationExpressionStormPetrel(info.ExpectedVarInvocationExpressionStormPetrel, info.ExpectedVariableInvocationExpressionArgs))
                                    : null,
                                info.RewriterKind != RewriterKind.PropertyExpression
                                    ? GetPropertyAssignment("ArgsCount"
                                        , SyntaxFactory.LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(
                                                info.ExpectedVariableInvocationExpressionArgs != null
                                                    ? info.ExpectedVariableInvocationExpressionArgs.Arguments.Count
                                                    : 0
                                            )
                                          )
                                      )
                                    : null
                            }
                            .Where(a => a != null)
                        )
                    )
                );
                expectedVariableInvocationExpressionInfo = GetPropertyAssignment("ExpectedVariableInvocationExpressionInfo", expectedVariableInvocationExpressionInfo);
            }
            else if (info.ExpectedVarParameterInfo != null)
            {
                testCaseInfo = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName("Scand.StormPetrel.Generator.TargetProject.TestCaseAttributeInfo()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment("Index", SyntaxFactory.IdentifierName(MethodHelper.StormPetrelUseCaseIndexParameterName)),
                                GetPropertyAssignment("Name", SyntaxFactory.LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(info.ExpectedVarParameterInfo.TestCaseAttributeName)
                                          )),
                                GetPropertyAssignment("ParameterIndex", SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            SyntaxFactory.Literal(info.ExpectedVarParameterInfo.ParameterIndex)
                                          )),
                            }
                        )
                    )
                );
                testCaseInfo = GetPropertyAssignment("TestCaseAttributeInfo", testCaseInfo);
            }
            else if (info.ExpectedVarParameterTestCaseSourceInfo != null)
            {
                var types = info.ExpectedVarParameterTestCaseSourceInfo.NonExpectedParameterTypes;
                var breakCondition = string.Join(" && ", info
                                                            .ExpectedVarParameterTestCaseSourceInfo
                                                            .NonExpectedParameterNames
                                                            .Select((x, i) => x + " == (" + types[i] + ") stormPetrelRow[" + i + "]"));
                var stormPetrelTestCaseSourceRowIndexVarName = GetBlockIndexVarName("stormPetrelTestCaseSourceRowIndex");
                var newCode = @"
private static void TempMethod()
{
    var " + stormPetrelTestCaseSourceRowIndexVarName + @" = -1;
    foreach (var stormPetrelRow in " + info.ExpectedVarParameterTestCaseSourceInfo.TestCaseSourceExpression + @")
    {
        " + stormPetrelTestCaseSourceRowIndexVarName + @"++;
        if (" + breakCondition + @")
        {
            break;
        }
    }
}
";
                var syntaxTree = CSharpSyntaxTree.ParseText(newCode);
                var rootMethod = syntaxTree
                                    .GetCompilationUnitRoot()
                                    .ChildNodes()
                                    .First()
                                    .ChildNodes()
                                    .OfType<LocalFunctionStatementSyntax>()
                                    .Single();
                result.AddRange(rootMethod.Body.Statements);

                testCaseInfo = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName("Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment("ColumnIndex", SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            SyntaxFactory.Literal(info.ExpectedVarParameterTestCaseSourceInfo.ParameterIndex)
                                )),
                                GetPropertyAssignment("RowIndex", SyntaxFactory.IdentifierName(stormPetrelTestCaseSourceRowIndexVarName)),
                                GetPropertyAssignment("Path", SyntaxFactory.ParseExpression(info.ExpectedVarParameterTestCaseSourceInfo.TestCaseSourcePathExpression)),
                            }
                        )
                    )
                );
                testCaseInfo = GetPropertyAssignment("TestCaseSourceInfo", testCaseInfo);
            }
            var contextCreationExpression = SyntaxFactory.ObjectCreationExpression(
                SyntaxFactory.IdentifierName("Scand.StormPetrel.Generator.TargetProject.GenerationContext()"))
                .WithInitializer(
                    SyntaxFactory.InitializerExpression(
                        SyntaxKind.ObjectInitializerExpression,
                        SyntaxFactory.SeparatedList(new[]
                        {
                            GetPropertyAssignment("FilePath", SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(_syntaxTreeFilePath))),
                            GetPropertyAssignment("ClassName", SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(className))),
                            GetPropertyAssignment("MethodName", SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(methodName))),
                            GetPropertyAssignment("MethodTestAttributeNames", GetArrayInitializer(testAttributeNames)),
                            GetPropertyAssignment("Actual", SyntaxFactory.IdentifierName(info.ActualVarName)),
                            GetPropertyAssignment("ActualVariablePath", GetArrayInitializer(info.ActualVarPath)),
                            GetPropertyAssignment("Expected", SyntaxFactory.IdentifierName(info.ExpectedVarName)),
                            GetPropertyAssignment("ExpectedVariablePath", GetArrayInitializer(info.ExpectedVarPath)),
                            expectedVariableInvocationExpressionInfo,
                            GetPropertyAssignment("IsLastVariablePair", SyntaxFactory.IdentifierName(isLastVariablePair.ToString(CultureInfo.InvariantCulture).ToLowerInvariant())),
                            GetPropertyAssignment("RewriterKind", SyntaxFactory.IdentifierName($"{typeof(RewriterKind).FullName}.{info.RewriterKind}")),
                            testCaseInfo,
                        }
                        .Where(a => a != null))));

            var stormPetrelContextVarName = GetBlockIndexVarName("stormPetrelContext");
            var variableDeclaration = SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName("var"),
                    SyntaxFactory.SeparatedList(new[]
                    {
                                    SyntaxFactory
                                        .VariableDeclarator(stormPetrelContextVarName)
                                        .WithInitializer(SyntaxFactory.EqualsValueClause(contextCreationExpression))
                    }));

            result.Add(SyntaxFactory
                        .LocalDeclarationStatement(variableDeclaration));
            result.Add(SyntaxFactory
                        .ParseStatement($"((Scand.StormPetrel.Generator.TargetProject.IGenerator) {_targetProjectGeneratorExpression}).GenerateBaseline({stormPetrelContextVarName});"));
            return result;

            string GetBlockIndexVarName(string varName) => blockIndex == 0 ? varName : varName + blockIndex.ToString(CultureInfo.InvariantCulture);
        }

        private static AssignmentExpressionSyntax GetPropertyAssignment(string name, ExpressionSyntax value)
        {
            var retCode = SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName(name),
                            value);

            return retCode;
        }

        private static InvocationExpressionSyntax GetMethodsInvocationExpressionStormPetrel(string invocationExpressionStormPetrel, ArgumentListSyntax args)
        {
            var pe = SyntaxFactory.ParseExpression(invocationExpressionStormPetrel);
            var ie = SyntaxFactory.InvocationExpression(pe, args);
            return ie;
        }
    }
}
