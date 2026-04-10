using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSource;
using Scand.StormPetrel.Generator.Common.AssertExpressionDetector;
using Scand.StormPetrel.Generator.Common.ExtraContextInternal;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;

namespace Scand.StormPetrel.Generator.Common
{
    partial class VarHelper
    {
        private class ExpectedExpressionInfo
        {
            private readonly List<VarPairInfo> _varPairInfoCollected;
            private readonly Dictionary<string, InitializerContextKind> _localVarNameToInitializerContextKind;

            private readonly AbstractDetector[] _expectedExpressionDetectors;
            public ExpectedExpressionInfo()
            {
                _varPairInfoCollected = new List<VarPairInfo>();
                _localVarNameToInitializerContextKind = new Dictionary<string, InitializerContextKind>();
                _expectedExpressionDetectors = new AbstractDetector[]
                {
                    new ShouldBeDetector(),
                    new AssertEqualDetector(),
                    new AssertThatDetector(),
                    new AssertAreEqualDetector(),
                    new ShouldlyDetector()
                };
            }

            private static bool IsSupportedStatementExpression(ExpressionSyntax statementExpression) =>
                statementExpression is InvocationExpressionSyntax
                    || statementExpression is ConditionalAccessExpressionSyntax;

            public void TryCollectExpectedExpression(
                SyntaxNode statement,
                MethodDeclarationSyntax method,
                Regex actualRegex,
                int indexOfBodyStatement,
                ImmutableHashSet<string> expectedVarNamesToSkip,
                IReadOnlyDictionary<ParameterSyntax, VarInfo> parameterToVarInfo)
            {
                if (TryDetectInitializerContext(statement, out var varName, out var contextKind)
                        && contextKind.HasValue)
                {
                    _localVarNameToInitializerContextKind[varName] = contextKind.Value;
                }
                SyntaxNode expressionStatement;
                if (statement is ExpressionStatementSyntax expression
                        && IsSupportedStatementExpression(expression.Expression))
                {
                    expressionStatement = expression;
                }
                else if (statement is ArrowExpressionClauseSyntax arrowExpressionStatement
                            && IsSupportedStatementExpression(arrowExpressionStatement.Expression))
                {
                    expressionStatement = arrowExpressionStatement;
                }
                else
                {
                    return;
                }
                ExpressionSyntax actualExpression = null;
                (ArgumentSyntax Argument, int Index) argumentInfo = default;
                ExpressionSyntax expectedExpression = null;
                int argumentNodeLatestIndex = -1;
                var _ = expressionStatement
                           .DescendantNodes(nd =>
                           {
                               if (nd is ArgumentSyntax tmpArgument)
                               {
                                   argumentNodeLatestIndex++;
                                   if (actualExpression == null)
                                   {
                                       var e = tmpArgument.Expression;
                                       int arrayRankSpecifierCount = 0;
                                       var isExpressionOk = IsSupportedExpression(e, expectedVarNamesToSkip)
                                                               || TryGetPropertyOrFieldInvocationPath(e, out var memberAccessVarName, out var _)
                                                                    && !expectedVarNamesToSkip.Contains(memberAccessVarName)
                                                               || e is CastExpressionSyntax castExpression && IsSupportedExpression(castExpression.Expression, expectedVarNamesToSkip)
                                                               || IsSupportedCollectionExpression(e, out arrayRankSpecifierCount);
                                       if (isExpressionOk && _expectedExpressionDetectors.Any(x => x.IsExpectedArgument(tmpArgument, out actualExpression)))
                                       {
                                           var actualExpressionFullString = actualExpression.ToFullString();
                                           if (actualRegex != null && !actualRegex.IsMatch(actualExpressionFullString))
                                           {
                                               actualExpression = null;
                                           }
                                           else
                                           {
                                               argumentInfo = (tmpArgument, argumentNodeLatestIndex);
                                               expectedExpression = arrayRankSpecifierCount <= 0
                                                                        ? tmpArgument.Expression
                                                                        : GetExpectedExpression(e, arrayRankSpecifierCount);
                                           }
                                           return false;
                                       }
                                   }
                               }
                               return true;
                           })
                           .Count();
                if (argumentInfo == default || actualExpression == null)
                {
                    return;
                }
                string[] expectedVarPath = null;
                AbstractExtraContextInternal extraContext = null;
                if (expectedExpression != null
                    && TryGetPropertyOrFieldInvocationPath(expectedExpression, out var expectedExpressionVariableName, out var invocationPath))
                {
                    if (_localVarNameToInitializerContextKind.TryGetValue(expectedExpressionVariableName, out var initializerContextKind))
                    {
                        expectedVarPath = SyntaxNodeHelper
                                            .GetValuePath(method)
                                            .Union(Enumerable.Repeat(expectedExpressionVariableName, 1))
                                            .ToArray();
                        extraContext = new InvocationExpressionWithEmbeddedExpectedMemberAccessInternal
                        {
                            ExpectedExpression = expectedExpression,
                            ExpectedExpressionVariableName = expectedExpressionVariableName,
                            ExpectedVarInvocationPath = invocationPath,
                            InitializerContextKind = initializerContextKind,
                        };
                    }
                    // Use test case source only: ignore attributes because no suitable updates are possible due to c# attribute syntax limitations
                    else if (parameterToVarInfo
                                .Where(x => x.Key.Identifier.ValueText == expectedExpressionVariableName)
                                .Select(x => x.Value)
                                .SingleOrDefault()?
                                .ExtraContextInternal is TestCaseSourceContextInternal testCaseSource)
                    {
                        extraContext = new TestCaseSourceContextInternal
                        {
                            ExpectedExpression = expectedExpression,
                            ExpectedExpressionVariableName = expectedExpressionVariableName,
                            TestCaseSourceExpression = testCaseSource.TestCaseSourceExpression,
                            TestCaseSourcePathExpression = testCaseSource.TestCaseSourcePathExpression,
                            PartialExtraContext = new TestCaseSourceContext
                            {
                                Path = testCaseSource.PartialExtraContext.Path,
                                ColumnIndex = testCaseSource.PartialExtraContext.ColumnIndex,
                                InvocationPath = invocationPath,
                            },
                        };
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    extraContext = new InvocationExpressionWithEmbeddedExpectedContextInternal
                    {
                        ExpectedExpression = expectedExpression,
                        MethodBodyStatementInfo = new MethodBodyStatementInfo
                        {
                            StatementNodeIndex = argumentInfo.Index,
                            StatementNodeKind = argumentInfo.Argument.RawKind,
                            StatementIndex = indexOfBodyStatement,
                        },
                        Path = SyntaxNodeHelper.GetValuePath(method),
                    };
                }
                _varPairInfoCollected.Add(new VarPairInfo
                {
                    ActualVarExpression = actualExpression,
                    StatementIndexForSubOrder = indexOfBodyStatement - 1,
                    ExpectedVarExtraContextInternal = extraContext,
                    ExpectedVarPath = expectedVarPath,
                });

            }
            public List<VarPairInfo> GetCollectedPairInfo()
            {
                if (_varPairInfoCollected.Count == 0)
                {
                    return _varPairInfoCollected;
                }
                var minIndex = _varPairInfoCollected.Min(x => x.StatementIndexForSubOrder);
                foreach (var info in _varPairInfoCollected)
                {
                    info.StatementIndex = minIndex;
                }
                return _varPairInfoCollected;
            }

            /// <summary>
            /// Expression types list comes from https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax?view=roslyn-dotnet-4.9.0
            /// </summary>
            /// <param name="e"></param>
            /// <returns></returns>
            private static bool IsSupportedExpression(ExpressionSyntax e, ImmutableHashSet<string> expectedVarNamesToSkip)
                => e is AnonymousObjectCreationExpressionSyntax
                    || e is IdentifierNameSyntax identifierSyntax && !expectedVarNamesToSkip.Contains(identifierSyntax.Identifier.Text)
                    || e is ArrayCreationExpressionSyntax
                    || e is ImplicitArrayCreationExpressionSyntax
                    || e is ImplicitObjectCreationExpressionSyntax
                    || e is LiteralExpressionSyntax
                    || e is ObjectCreationExpressionSyntax
                    || e is PrefixUnaryExpressionSyntax
                    || e is InterpolatedStringExpressionSyntax
                    || e is TupleExpressionSyntax
                    || e is DefaultExpressionSyntax
                    ;

            private static bool TryGetPropertyOrFieldInvocationPath(ExpressionSyntax node, out string variableName, out string[] path)
            {
                var names = new List<string>();
                SyntaxNode current = node;
                variableName = null;
                path = null;

                while (true)
                {
                    // unwrap null-forgiving postfix operator at the current level (e.g. a!.b or (a!).b)
                    if (current is PostfixUnaryExpressionSyntax topPostfix && topPostfix.IsKind(SyntaxKind.SuppressNullableWarningExpression))
                    {
                        current = topPostfix.Operand;
                    }

                    if (IsConditionalExpression(current, out var isBreak, out bool isContinue))
                    {
                        if (isBreak)
                        {
                            break;
                        }
                        else if (isContinue)
                        {
                            continue;
                        }
                        return false;
                    }

                    if (!(current is MemberAccessExpressionSyntax ma))
                    {
                        return false;
                    }

                    // extract rightmost name (could be IdentifierName or GenericName)
                    if (!(ma.Name is SimpleNameSyntax simpleName))
                    {
                        return false;
                    }
                    names.Add(simpleName.Identifier.Text);

                    // move to the left part of the member access
                    var expr = ma.Expression;

                    // unwrap null-forgiving postfix operator (e.g. a!.b)
                    if (expr is PostfixUnaryExpressionSyntax postfix && postfix.IsKind(SyntaxKind.SuppressNullableWarningExpression))
                    {
                        expr = postfix.Operand;
                    }

                    // If left side is identifier - add and finish
                    if (expr is IdentifierNameSyntax id)
                    {
                        names.Add(id.Identifier.Text);
                        break;
                    }

                    if (IsConditionalExpression(expr, out var isBreak2, out bool isContinue2))
                    {
                        if (isBreak2)
                        {
                            break;
                        }
                        else if (isContinue2)
                        {
                            continue;
                        }
                        return false;
                    }

                    // if left side is another member access - continue loop
                    if (expr is MemberAccessExpressionSyntax leftMa)
                    {
                        current = leftMa;
                        continue;
                    }

                    // unsupported expression on the left side (method calls, element access etc.)
                    return false;
                }

                if (names.Count <= 1)
                {
                    throw new InvalidOperationException("Unexpected names count: " + names.Count);
                }
                // names are collected from right to left - reverse them
                names.Reverse();
                path = names.Skip(1).ToArray();
                variableName = names[0];
                return true;

                bool IsConditionalExpression(SyntaxNode expr, out bool isBreak, out bool isContinue)
                {
                    isBreak = false;
                    isContinue = false;
                    // If current is a conditional access expression like `a?.b`
                    if (expr is ConditionalAccessExpressionSyntax cond)
                    {
                        // WhenNotNull may be MemberBindingExpressionSyntax (?.Name)
                        if (cond.WhenNotNull is MemberBindingExpressionSyntax mb && mb.Name is SimpleNameSyntax mbName)
                        {
                            // include the member from the binding (the one after ?.)
                            names.Add(mbName.Identifier.Text);

                            // continue walking from the conditional's expression (the leftmost part)
                            var left = cond.Expression;
                            while (left is ParenthesizedExpressionSyntax par2) left = par2.Expression;
                            if (left is PostfixUnaryExpressionSyntax pf && pf.IsKind(SyntaxKind.SuppressNullableWarningExpression))
                            {
                                left = pf.Operand;
                            }
                            if (left is IdentifierNameSyntax id2)
                            {
                                names.Add(id2.Identifier.Text);
                                isBreak = true;
                            }
                            else if (left is MemberAccessExpressionSyntax)
                            {
                                current = left;
                                isContinue = true;
                            }
                        }
                        return true;
                    }
                    return false;
                }
            }

            #region Collection Initializer Handling
            private static CastExpressionSyntax GetExpectedExpression(ExpressionSyntax expression, int arrayRankSpecifierCount)
            {
                if (arrayRankSpecifierCount <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(arrayRankSpecifierCount));
                }
                var castSyntax = SyntaxFactory.ArrayType(
                    SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.ObjectKeyword)),
                    SyntaxFactory.SingletonList(SyntaxFactory.ArrayRankSpecifier()));
                arrayRankSpecifierCount--;
                while (arrayRankSpecifierCount > 0)
                {
                    castSyntax = SyntaxFactory.ArrayType(
                        castSyntax,
                        SyntaxFactory.SingletonList(SyntaxFactory.ArrayRankSpecifier()));
                    arrayRankSpecifierCount--;
                }
                var castExpression = SyntaxFactory.CastExpression(castSyntax, expression);
                return castExpression;
            }

            private static IEnumerable<CollectionElementSyntax> GetCollectionElements(CollectionElementSyntax elementSyntax)
            {
                if (elementSyntax is ExpressionElementSyntax elementExpression)
                {
                    if (elementExpression.Expression is CollectionExpressionSyntax collectionElementSyntax)
                    {
                        return collectionElementSyntax.Elements;
                    }
                    else if (elementExpression.Expression is CastExpressionSyntax castElementExpressionSyntax
                        && castElementExpressionSyntax.Expression is CollectionExpressionSyntax castCollectionElementSyntax)
                    {
                        return castCollectionElementSyntax.Elements;
                    }
                }
                return Enumerable.Empty<CollectionElementSyntax>();
            }

            private static bool CheckCollectionElements(IEnumerable<CollectionElementSyntax> collection, ref int arrayRankSpecifierCount, ref bool calculateRankSpecifier)
            {
                var elements = collection.ToArray();
                if (elements.Length == 0)
                {
                    arrayRankSpecifierCount++;
                    return true;
                }
                var firstElement = elements[0];
                if (firstElement is ExpressionElementSyntax firstElementExpression)
                {
                    if (firstElementExpression.Expression.IsKind(SyntaxKind.CollectionExpression)
                        && elements.Any(e => !(e is ExpressionElementSyntax elementExpression
                                                && elementExpression.Expression.IsKind(SyntaxKind.CollectionExpression))))
                    {
                        return false;
                    }
                    else if (firstElementExpression.Expression.IsKind(SyntaxKind.CastExpression)
                                && elements.Any(e => !(e is ExpressionElementSyntax elementExpression
                                                        && elementExpression.Expression is CastExpressionSyntax castExpression
                                                        && castExpression.Expression.IsKind(SyntaxKind.CollectionExpression))))
                    {
                        return false;
                    }

                    if (calculateRankSpecifier && firstElementExpression.Expression.IsKind(SyntaxKind.CollectionExpression))
                    {
                        arrayRankSpecifierCount++;
                    }
                    else
                    {
                        calculateRankSpecifier = false;
                    }
                }
                var nextLevel = elements.SelectMany(e => GetCollectionElements(e));
                return CheckCollectionElements(nextLevel, ref arrayRankSpecifierCount, ref calculateRankSpecifier);
            }

            private static bool IsSupportedCollectionExpression(ExpressionSyntax expression, out int arrayRankSpecifierCount)
            {
                arrayRankSpecifierCount = 0;
                bool calculateRankSpecifier = true;
                if (expression is CollectionExpressionSyntax collectionExpression)
                {
                    return CheckCollectionElements(collectionExpression.Elements, ref arrayRankSpecifierCount, ref calculateRankSpecifier);
                }
                else if (expression is CastExpressionSyntax castExpression
                    && castExpression.Expression is CollectionExpressionSyntax castCollectionExpression)
                {
                    var result = CheckCollectionElements(castCollectionExpression.Elements, ref arrayRankSpecifierCount, ref calculateRankSpecifier);
                    arrayRankSpecifierCount = 0; //we don't need rank specifier for this case because the original cast is acceptable
                    return result;
                }
                return false;
            }
            #endregion
        }
    }
}
