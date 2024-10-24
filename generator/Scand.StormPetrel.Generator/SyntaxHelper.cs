using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using Scand.StormPetrel.Generator.ExtraContextInternal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    internal class SyntaxHelper
    {
        private readonly string _syntaxTreeFilePath;
        private readonly string _targetProjectGeneratorExpression;
        private const string StormPetrelSharedContextVarName = "stormPetrelSharedContext";
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

        public List<StatementSyntax> GetNewCodeBlock(string className, string methodName, VarPairInfo info, int blockIndex, int varPairsCount)
        {
            ObjectCreationExpressionSyntax extraContextExpression = null;
            var result = new List<StatementSyntax>();
            var methodContextStatement = GetMethodContextStatement(className, methodName, blockIndex, varPairsCount);
            result.Add(methodContextStatement);
            if (info.ExpectedVarExtraContextInternal is InvocationExpressionContextInternal invocationSourceContext)
            {
                string stormPetrelMethodNodeVarName = null;
                ObjectCreationExpressionSyntax methodInfoExpression = null;
                if (invocationSourceContext.PartialExtraContext.MethodInfo != null)
                {
                    stormPetrelMethodNodeVarName = GetBlockIndexVarName("stormPetrelMethodNode");
                    var stormPetrelMethodNodeClause = GetStormPetrelMethodNodeClause(invocationSourceContext.InvocationExpressionStormPetrel, invocationSourceContext.MethodArgs);
                    var stormPetrelMethodNodeVarDeclaration = SyntaxFactory.VariableDeclaration(
                            SyntaxFactory.ParseTypeName("var"),
                            SyntaxFactory.SeparatedList(new[]
                            {
                                    SyntaxFactory
                                        .VariableDeclarator(SyntaxFactory.Identifier(stormPetrelMethodNodeVarName), null, stormPetrelMethodNodeClause)
                            }));

                    result.Add(SyntaxFactory
                                .LocalDeclarationStatement(stormPetrelMethodNodeVarDeclaration));
                    methodInfoExpression = SyntaxFactory.ObjectCreationExpression(
                        SyntaxFactory.IdentifierName($"{typeof(InvocationSourceMethodInfo).FullName}()"))
                        .WithInitializer(
                            SyntaxFactory.InitializerExpression(
                                SyntaxKind.ObjectInitializerExpression,
                                SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                                {
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.NodeKind), SyntaxFactory.ParseExpression($"{stormPetrelMethodNodeVarName}.NodeKind")),
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.NodeIndex), SyntaxFactory.ParseExpression($"{stormPetrelMethodNodeVarName}.NodeIndex")),
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.ArgsCount), SyntaxFactory.LiteralExpression(
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(invocationSourceContext.PartialExtraContext.MethodInfo.ArgsCount))),
                                }
                            )
                        )
                    );
                }
                extraContextExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(InvocationSourceContext).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(InvocationSourceContext.Path), GetArrayInitializer(invocationSourceContext.GetPath(info.ExpectedVarPath))),
                                methodInfoExpression == null
                                    ? null
                                    : GetPropertyAssignment(nameof(InvocationSourceContext.MethodInfo), methodInfoExpression),
                            }
                            .Where(a => a != null)
                        )
                    )
                );
            }
            else if (info.ExpectedVarExtraContextInternal is AttributeContextInternal attributeContext)
            {
                extraContextExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(AttributeContext).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(AttributeContext.Index), SyntaxFactory.IdentifierName(MethodHelper.StormPetrelUseCaseIndexParameterName)),
                                GetPropertyAssignment(nameof(AttributeContext.Name), SyntaxFactory.LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            SyntaxFactory.Literal(attributeContext.PartialExtraContext.Name)
                                          )),
                                GetPropertyAssignment(nameof(AttributeContext.ParameterIndex), SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            SyntaxFactory.Literal(attributeContext.PartialExtraContext.ParameterIndex)
                                          )),
                            }
                        )
                    )
                );
            }
            else if (info.ExpectedVarExtraContextInternal is TestCaseSourceContextInternal testCaseSourceContext)
            {
                var breakCondition = string.Join(" && ", testCaseSourceContext
                                                            .NonExpectedParameterNames
                                                            .Select((x, i) => x + " == (" + testCaseSourceContext.NonExpectedParameterTypes[i] + ") stormPetrelRow[" + i + "]"));
                var stormPetrelTestCaseSourceRowIndexVarName = GetBlockIndexVarName("stormPetrelTestCaseSourceRowIndex");
                var newCode = @"
private static void TempMethod()
{
    var " + stormPetrelTestCaseSourceRowIndexVarName + @" = -1;
    foreach (var stormPetrelRow in " + testCaseSourceContext.TestCaseSourceExpression + @")
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

                extraContextExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(TestCaseSourceContext).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(TestCaseSourceContext.ColumnIndex), SyntaxFactory.LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            SyntaxFactory.Literal(testCaseSourceContext.PartialExtraContext.ColumnIndex)
                                )),
                                GetPropertyAssignment(nameof(TestCaseSourceContext.RowIndex), SyntaxFactory.IdentifierName(stormPetrelTestCaseSourceRowIndexVarName)),
                                GetPropertyAssignment(nameof(TestCaseSourceContext.Path), SyntaxFactory.ParseExpression(testCaseSourceContext.TestCaseSourcePathExpression)),
                            }
                        )
                    )
                );
            }
            else if (info.ExpectedVarExtraContextInternal is InitializerContextInternal initializerContext)
            {
                extraContextExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(InitializerContext).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(InitializerContext.Kind), SyntaxFactory.ParseExpression($"{typeof(InitializerContextKind).FullName}.{initializerContext.PartialExtraContext.Kind}")),
                            }
                        )
                    )
                );
            }
            else
            {
                throw new InvalidOperationException("Unexpected case");
            }
            var contextCreationExpression = SyntaxFactory.ObjectCreationExpression(
                SyntaxFactory.IdentifierName($"{typeof(GenerationContext).FullName}()"))
                .WithInitializer(
                    SyntaxFactory.InitializerExpression(
                        SyntaxKind.ObjectInitializerExpression,
                        SyntaxFactory.SeparatedList(new[]
                        {
                            (ExpressionSyntax)GetPropertyAssignment(nameof(GenerationContext.Actual), SyntaxFactory.IdentifierName(info.ActualVarName)),
                            GetPropertyAssignment(nameof(GenerationContext.ActualVariablePath), GetArrayInitializer(info.ActualVarPath)),
                            GetPropertyAssignment(nameof(GenerationContext.Expected), SyntaxFactory.IdentifierName(info.ExpectedVarName)),
                            GetPropertyAssignment(nameof(GenerationContext.ExpectedVariablePath), GetArrayInitializer(info.ExpectedVarPath)),
                            GetPropertyAssignment(nameof(GenerationContext.ExtraContext), extraContextExpression),
                            GetPropertyAssignment(nameof(GenerationContext.MethodSharedContext), SyntaxFactory.IdentifierName(StormPetrelSharedContextVarName)),
                        })));

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
                        .ParseStatement($"(({typeof(IGenerator).FullName}) {_targetProjectGeneratorExpression}).GenerateBaseline({stormPetrelContextVarName});"));
            return result;

            string GetBlockIndexVarName(string varName) => blockIndex == 0 ? varName : varName + blockIndex.ToString(CultureInfo.InvariantCulture);
        }

        private StatementSyntax GetMethodContextStatement(string className, string methodName, int varPairIndex, int varPairsCount)
        {
            if (varPairIndex == 0)
            {
                var sharedContextExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(MethodContext).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(MethodContext.FilePath), SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(_syntaxTreeFilePath))),
                                GetPropertyAssignment(nameof(MethodContext.ClassName), SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(className))),
                                GetPropertyAssignment(nameof(MethodContext.MethodName), SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(methodName))),
                                GetPropertyAssignment(nameof(MethodContext.VariablePairCurrentIndex), SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0))),
                                GetPropertyAssignment(nameof(MethodContext.VariablePairsCount), SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(varPairsCount))),
                            }
                        )
                    )
                );
                var sharedContextVariableDeclaration = SyntaxFactory.VariableDeclaration(
                        SyntaxFactory.ParseTypeName("var"),
                        SyntaxFactory.SeparatedList(new[]
                        {
                                    SyntaxFactory
                                        .VariableDeclarator(StormPetrelSharedContextVarName)
                                        .WithInitializer(SyntaxFactory.EqualsValueClause(sharedContextExpression))
                        }));
                return SyntaxFactory
                            .LocalDeclarationStatement(sharedContextVariableDeclaration);
            }
            var memberAccess = SyntaxFactory.MemberAccessExpression(
                                                SyntaxKind.SimpleMemberAccessExpression,
                                                SyntaxFactory.IdentifierName(StormPetrelSharedContextVarName),
                                                SyntaxFactory.IdentifierName(nameof(MethodContext.VariablePairCurrentIndex)));
            var postfixIncrementExpression = SyntaxFactory.PostfixUnaryExpression(
                                                SyntaxKind.PostIncrementExpression,
                                                memberAccess);
            return SyntaxFactory.ExpressionStatement(postfixIncrementExpression);
        }

        private static AssignmentExpressionSyntax GetPropertyAssignment(string name, ExpressionSyntax value)
        {
            var retCode = SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName(name),
                            value);

            return retCode;
        }

        private static EqualsValueClauseSyntax GetStormPetrelMethodNodeClause(string invocationExpressionStormPetrel, ArgumentListSyntax args)
        {
            var pe = SyntaxFactory.ParseExpression(invocationExpressionStormPetrel);
            var ie = SyntaxFactory.InvocationExpression(pe, args);
            return SyntaxFactory.EqualsValueClause(ie);
        }
    }
}
