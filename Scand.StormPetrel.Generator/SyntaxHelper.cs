﻿using Microsoft.CodeAnalysis;
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
            ExpressionSyntax testCaseAttributeInfo = null;
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
                testCaseAttributeInfo = SyntaxFactory.ObjectCreationExpression(
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
                testCaseAttributeInfo = GetPropertyAssignment("TestCaseAttributeInfo", testCaseAttributeInfo);
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
                            testCaseAttributeInfo,
                        }
                        .Where(a => a != null))));

            var stormPetrelContextVarName = "stormPetrelContext" + (blockIndex == 0 ? "" : blockIndex.ToString(CultureInfo.InvariantCulture));
            var variableDeclaration = SyntaxFactory.VariableDeclaration(
                    SyntaxFactory.ParseTypeName("var"),
                    SyntaxFactory.SeparatedList(new[]
                    {
                                    SyntaxFactory
                                        .VariableDeclarator(stormPetrelContextVarName)
                                        .WithInitializer(SyntaxFactory.EqualsValueClause(contextCreationExpression))
                    }));

            return new List<StatementSyntax>
                {
                    SyntaxFactory
                        .LocalDeclarationStatement(variableDeclaration),
                    SyntaxFactory
                        .ParseStatement($"((Scand.StormPetrel.Generator.TargetProject.IGenerator) {_targetProjectGeneratorExpression}).GenerateBaseline({stormPetrelContextVarName});"),
                };
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
