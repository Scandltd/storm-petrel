using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using Scand.StormPetrel.Generator.Common;
using Scand.StormPetrel.Generator.Common.ExtraContextInternal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Scand.StormPetrel.Generator
{
    internal class SyntaxHelper
    {
        private readonly string _syntaxTreeFilePath;
        private readonly string _targetProjectGeneratorExpression;
        private readonly Regex _ignoreInvocationExpressionRegex;
        private const string StormPetrelSharedContextVarName = "stormPetrelSharedContext";
        public SyntaxHelper(string syntaxTreeFilePath, string targetProjectGeneratorExpression, Regex ignoreInvocationExpressionRegex)
        {
            _syntaxTreeFilePath = syntaxTreeFilePath;
            _targetProjectGeneratorExpression = targetProjectGeneratorExpression;
            _ignoreInvocationExpressionRegex = ignoreInvocationExpressionRegex;
        }

        private static ExpressionSyntax ToStringLiteralExpression(string s) =>
            SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(s));
        private static ExpressionSyntax GetArrayInitializer<T>(T[] arr, Func<T, ExpressionSyntax> func, string typeNameForEmptyArray = null) =>
            GetArrayInitializer(arr.Select(x => func(x)).ToArray(), typeNameForEmptyArray);
        private static ExpressionSyntax GetArrayInitializer(ExpressionSyntax[] expressions, string typeNameForEmptyArray = null)
        {
            var arrayExpression = SyntaxFactory.InitializerExpression(
                SyntaxKind.ArrayInitializerExpression,
                SyntaxFactory.SeparatedList(expressions)
            );

            TypeSyntax typeSyntax;
            if (expressions.Length == 0)
            {
                if (string.IsNullOrEmpty(typeNameForEmptyArray))
                {
                    throw new ArgumentOutOfRangeException(nameof(typeNameForEmptyArray));
                }
                typeSyntax = SyntaxFactory.ParseTypeName(typeNameForEmptyArray);
            }
            else
            {
                typeSyntax = SyntaxFactory.OmittedTypeArgument();
            }

            ArrayTypeSyntax arrayType = SyntaxFactory.ArrayType(typeSyntax)
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

        public List<StatementSyntax> GetNewCodeBlock(string className, string methodName, VarPairInfo info, int blockIndex, int varPairsCount, SeparatedSyntaxList<ParameterSyntax> parameters)
        {
            ObjectCreationExpressionSyntax extraContextExpression = null;
            var result = new List<StatementSyntax>();
            ExpressionSyntax expectedExpression = null;
            var methodContextStatement = GetMethodContextStatement(className, methodName, blockIndex, varPairsCount, parameters);
            result.Add(methodContextStatement);
            if (info.ExpectedVarExtraContextInternal is InvocationExpressionContextInternal invocationSourceContext)
            {
                string stormPetrelMethodNodeVarName = null;
                ObjectCreationExpressionSyntax methodInfoExpression = null;
                if (invocationSourceContext.PartialExtraContext.MethodInfo != null)
                {
                    if (_ignoreInvocationExpressionRegex == null || !_ignoreInvocationExpressionRegex.IsMatch(invocationSourceContext.InvocationExpressionStormPetrel))
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
                    }

                    methodInfoExpression = SyntaxFactory.ObjectCreationExpression(
                        SyntaxFactory.IdentifierName($"{typeof(InvocationSourceMethodInfo).FullName}()"))
                        .WithInitializer(
                            SyntaxFactory.InitializerExpression(
                                SyntaxKind.ObjectInitializerExpression,
                                SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                                {
                                        stormPetrelMethodNodeVarName == null ? null : GetPropertyAssignment(nameof(InvocationSourceMethodInfo.NodeKind), SyntaxFactory.ParseExpression($"{stormPetrelMethodNodeVarName}.NodeKind")),
                                        stormPetrelMethodNodeVarName == null ? null : GetPropertyAssignment(nameof(InvocationSourceMethodInfo.NodeIndex), SyntaxFactory.ParseExpression($"{stormPetrelMethodNodeVarName}.NodeIndex")),
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.ArgsCount), SyntaxFactory.LiteralExpression(
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(invocationSourceContext.PartialExtraContext.MethodInfo.ArgsCount))),
                                }
                                .Where(a => a != null)
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
                                GetPropertyAssignment(nameof(InvocationSourceContext.Path), GetArrayInitializer(invocationSourceContext.GetPath(info.ExpectedVarPath), ToStringLiteralExpression)),
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
                var breakConditions = testCaseSourceContext
                                        .NonExpectedParameterNames
                                        .Select((x, i) => (Name: x, Condition:
$@"stormPetrelRow.Length > {i} && ({x} == ({testCaseSourceContext.NonExpectedParameterTypes[i]}) stormPetrelRow[{i}] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual({x}, stormPetrelRow[{i}]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements({x}, stormPetrelRow[{i}]))
    || stormPetrelRow.Length <= {i} && {x} == {testCaseSourceContext.TestMethodParameterDefaultValues[i]}"))
                                        .ToArray();
                var breakCondition = string.Join(") &&\n            (", breakConditions.Select(x => x.Condition));
                breakCondition = breakConditions.Length <= 1 ? breakCondition : $"({breakCondition})";
                var noEqualArgNames = GetBlockIndexVarName("stormPetrelNoEqualArgNames");
                var noEqualArgNamesInit = string.Join(", ", breakConditions.Select(x => $"\"{x.Name}\""));
                var detectNoEqualArgNamesCondition = string.Join("\n", breakConditions.Select(x => $"            if ({x.Condition}) {{ {noEqualArgNames}.Remove(\"{x.Name}\"); }}"));
                var testCaseSourceRowIndexVarName = GetBlockIndexVarName("stormPetrelTestCaseSourceRowIndex");
                var isTestCaseSourceRowExistVarName = GetBlockIndexVarName("stormPetrelIsTestCaseSourceRowExist");
                var stormPetrelRowsVarName = GetBlockIndexVarName("stormPetrelRows");
                var newCode = @"
private static void TempMethod()
{
    var " + testCaseSourceRowIndexVarName + @" = -1;
    var " + isTestCaseSourceRowExistVarName + @" = false;
    var " + stormPetrelRowsVarName + @" = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(" + testCaseSourceContext.TestCaseSourceExpression + @");
    foreach (var stormPetrelRow in " + stormPetrelRowsVarName + @")
    {
        " + testCaseSourceRowIndexVarName + @"++;
        if (" + breakCondition + @")
        {
            " + isTestCaseSourceRowExistVarName + @" = true;
            break;
        }
    }
    if (!" + isTestCaseSourceRowExistVarName + @")
    {
        var " + noEqualArgNames + @" = new System.Collections.Generic.List<string>(){ "+ noEqualArgNamesInit + @" };
        foreach (var stormPetrelRow in " + stormPetrelRowsVarName + @")
        {
            " + detectNoEqualArgNamesCondition + @"
        }
        throw new System.InvalidOperationException(""Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '"" + string.Join(""', '"", " + noEqualArgNames + @") + ""' argument(s)."");
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
                                GetPropertyAssignment(nameof(TestCaseSourceContext.RowIndex), SyntaxFactory.IdentifierName(testCaseSourceRowIndexVarName)),
                                GetPropertyAssignment(nameof(TestCaseSourceContext.Path), GetPathExpression(testCaseSourceContext)),
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
            else if (info.ExpectedVarExtraContextInternal is InvocationExpressionWithEmbeddedExpectedContextInternal embeddedExpectedContext)
            {
                var currentMethodInfo = embeddedExpectedContext.PartialExtraContext.MethodInfo;
                var methodInfoExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(InvocationSourceMethodInfo).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.NodeKind), SyntaxFactory.LiteralExpression(
                                                SyntaxKind.NumericLiteralExpression,
                                                SyntaxFactory.Literal(currentMethodInfo.NodeKind))),
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.NodeIndex), SyntaxFactory.LiteralExpression(
                                                SyntaxKind.NumericLiteralExpression,
                                                SyntaxFactory.Literal(currentMethodInfo.NodeIndex))),
                                        GetPropertyAssignment(nameof(InvocationSourceMethodInfo.ArgsCount), SyntaxFactory.LiteralExpression(
                                                SyntaxKind.StringLiteralExpression,
                                                SyntaxFactory.Literal(currentMethodInfo.ArgsCount))),
                            }
                        )
                    )
                );

                expectedExpression = embeddedExpectedContext.ExpectedExpression;
                extraContextExpression = SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(InvocationSourceContext).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(InvocationSourceContext.Path), GetArrayInitializer(embeddedExpectedContext.PartialExtraContext.Path, ToStringLiteralExpression)),
                                GetPropertyAssignment(nameof(InvocationSourceContext.MethodInfo), methodInfoExpression),
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
                            (ExpressionSyntax)GetPropertyAssignment(nameof(GenerationContext.Actual), info.ActualVarExpression ?? SyntaxFactory.IdentifierName(info.ActualVarName)),
                            GetPropertyAssignment(nameof(GenerationContext.ActualVariablePath), GetArrayInitializer(info.ActualVarPath, ToStringLiteralExpression)),
                            !string.IsNullOrEmpty(info.ExpectedVarName) && expectedExpression == null
                                ? GetPropertyAssignment(nameof(GenerationContext.Expected), SyntaxFactory.IdentifierName(info.ExpectedVarName))
                                : expectedExpression != null
                                    ? GetPropertyAssignment(nameof(GenerationContext.Expected), expectedExpression)
                                    : throw new InvalidOperationException($"Unexpected null in {nameof(expectedExpression)}"),
                            info.ExpectedVarPath != null
                                ? GetPropertyAssignment(nameof(GenerationContext.ExpectedVariablePath), GetArrayInitializer(info.ExpectedVarPath, ToStringLiteralExpression))
                                : null,
                            GetPropertyAssignment(nameof(GenerationContext.ExtraContext), extraContextExpression),
                            GetPropertyAssignment(nameof(GenerationContext.MethodSharedContext), SyntaxFactory.IdentifierName(StormPetrelSharedContextVarName)),
                        }
                        .Where(x => x != null)
                        )));

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

        private StatementSyntax GetMethodContextStatement(string className, string methodName, int varPairIndex, int varPairsCount, SeparatedSyntaxList<ParameterSyntax> parameters)
        {
            if (varPairIndex == 0)
            {
                var parametersExpression = GetArrayInitializer(parameters.ToArray(), x => SyntaxFactory.ObjectCreationExpression(
                    SyntaxFactory.IdentifierName($"{typeof(ParameterInfo).FullName}()"))
                    .WithInitializer(
                        SyntaxFactory.InitializerExpression(
                            SyntaxKind.ObjectInitializerExpression,
                            SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                            {
                                GetPropertyAssignment(nameof(ParameterInfo.Name), SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(x.Identifier.Text))),
                                GetPropertyAssignment(nameof(ParameterInfo.Value), SyntaxFactory.IdentifierName(x.Identifier.Text)),
                                GetPropertyAssignment(nameof(ParameterInfo.Attributes),
                                                        GetArrayInitializer(x.AttributeLists
                                                                                .ToArray()
                                                                                .SelectMany(y => y.Attributes.Where(z => z != null))
                                                                                .ToArray(),
                                                        y => SyntaxFactory
                                                                .ObjectCreationExpression(SyntaxFactory.IdentifierName($"{typeof(AttributeInfo).FullName}()"))
                                                                .WithInitializer(
                                                                    SyntaxFactory.InitializerExpression(
                                                                        SyntaxKind.ObjectInitializerExpression,
                                                                        SyntaxFactory.SeparatedList(new ExpressionSyntax[]
                                                                        {
                                                                            GetPropertyAssignment(nameof(AttributeInfo.Name), SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(y.Name.GetText().ToString()))),
                                                                        }
                                                                    )
                                                                )),
                                                        typeof(AttributeInfo).FullName)
                                                      ),
                            })
                        )
                    ),
                    typeof(ParameterInfo).FullName
                );

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
                                GetPropertyAssignment(nameof(MethodContext.Parameters), parametersExpression),
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

        private static InvocationExpressionSyntax GetPathExpression(TestCaseSourceContextInternal context)
        {
            var pathExpression = GetArrayInitializer(
            new[]
            {
                ToStringLiteralExpression($"experimental-parameter-default-values:{JsonSerializerForTargetProject.SerializeStringArray(context.TestMethodParameterDefaultValues)}")
            });

            var unionInvocation = SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    pathExpression,
                    SyntaxFactory.IdentifierName("Union")
                ),
                SyntaxFactory.ArgumentList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Argument(
                            SyntaxFactory.ParseExpression(context.TestCaseSourcePathExpression)
                        )
                    )
                )
            );

            return SyntaxFactory.InvocationExpression(
                SyntaxFactory.MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    unionInvocation,
                    SyntaxFactory.IdentifierName("ToArray")
                )
            );
        }
    }
}
