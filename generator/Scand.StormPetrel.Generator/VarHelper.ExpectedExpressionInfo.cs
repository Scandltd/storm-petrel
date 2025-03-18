using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using Scand.StormPetrel.Generator.AssertExpressionDetector;
using Scand.StormPetrel.Generator.ExtraContextInternal;
using Scand.StormPetrel.Shared;

namespace Scand.StormPetrel.Generator
{
    partial class VarHelper
    {
        private class ExpectedExpressionInfo
        {
            private readonly List<VarPairInfo> _varPairInfoCollected;

            private readonly AbstractDetector[] _expectedExpressionDetectors;
            public ExpectedExpressionInfo()
            {
                _varPairInfoCollected = new List<VarPairInfo>();
                _expectedExpressionDetectors = new AbstractDetector[]
                {
                    new ShouldBeDetector(),
                    new AssertEqualDetector(),
                    new AssertThatDetector(),
                    new AssertAreEqualDetector(),
                    new ShouldlyDetector()
                };
            }
            public void TryCollectExpectedExpression(object statement, MethodDeclarationSyntax method, Regex actualRegex, int index, int indexOfBodyStatement)
            {
                if (!(statement is ExpressionStatementSyntax expressionStatement))
                {
                    return;
                }
                if (!(expressionStatement.Expression is InvocationExpressionSyntax)
                        && !(expressionStatement.Expression is ConditionalAccessExpressionSyntax))
                {
                    return;
                }
                IdentifierNameSyntax actualIdentifier = null;
                ExpressionSyntax actualExpression = null;
                ArgumentSyntax argument = null;
                ExpressionSyntax expectedExpression = null;
                var argumentToIndex = new Dictionary<ArgumentSyntax, int>();
                int argumentNodeLatestIndex = -1;
                var _ = expressionStatement
                            .DescendantNodes(nd =>
                            {
                                if (nd is ArgumentSyntax tmpArgument)
                                {
                                    argumentToIndex.Add(tmpArgument, ++argumentNodeLatestIndex);
                                }
                                if (nd is IdentifierNameSyntax identifier && actualRegex.IsMatch(identifier.Identifier.Text))
                                {
                                    actualIdentifier = identifier;
                                }
                                return true;
                            })
                            .Count();
                if (actualIdentifier == null)
                {
                    return;
                }
                var __ = expressionStatement
                           .DescendantNodes(nd =>
                           {
                               if (nd is ArgumentSyntax tmpArgument)
                               {
                                   if (actualExpression == null)
                                   {
                                       var e = tmpArgument.Expression;
                                       int arrayRankSpecifierCount = 0;
                                       var isExpressionOk = IsSupportedExpression(e)
                                                               || e is CastExpressionSyntax castExpression && IsSupportedExpression(castExpression.Expression)
                                                               || IsSupportedCollectionExpression(e, out arrayRankSpecifierCount);
                                       if (isExpressionOk && _expectedExpressionDetectors.Any(x => x.IsExpectedArgument(tmpArgument, actualIdentifier, out actualExpression)))
                                       {
                                           argument = tmpArgument;
                                           expectedExpression = arrayRankSpecifierCount <= 0
                                                                    ? argument.Expression
                                                                    : GetExpectedExpression(e, arrayRankSpecifierCount);
                                           return false;
                                       }
                                   }
                               }
                               return true;
                           })
                           .Count();
                if (argument == null || actualExpression == null)
                {
                    return;
                }
                _varPairInfoCollected.Add(new VarPairInfo
                {
                    ActualVarName = actualIdentifier.Identifier.Text,
                    ActualVarExpression = actualExpression,
                    ActualVarPath = SyntaxNodeHelper.GetValuePath(method),
                    StatementIndexForSubOrder = index - 1,
                    ExpectedVarExtraContextInternal = new InvocationExpressionWithEmbeddedExpectedContextInternal
                    {
                        ExpectedExpression = expectedExpression,
                        PartialExtraContext = new InvocationSourceContext
                        {
                            MethodInfo = new InvocationSourceMethodInfo
                            {
                                ArgsCount = method.ParameterList.Parameters.Count,
                                NodeIndex = argumentToIndex[argument],
                                NodeKind = argument.RawKind,
                            },
                            Path = new[]
                                    {
                                        $"experimental-method-body-statement-index:{indexOfBodyStatement}",
                                    }
                                    .Concat(SyntaxNodeHelper.GetValuePath(method))
                                    .ToArray(),
                        },
                    },
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
            private static bool IsSupportedExpression(ExpressionSyntax e)
                => e is AnonymousObjectCreationExpressionSyntax
                    || e is ArrayCreationExpressionSyntax
                    || e is ImplicitArrayCreationExpressionSyntax
                    || e is ImplicitObjectCreationExpressionSyntax
                    || e is LiteralExpressionSyntax
                    || e is ObjectCreationExpressionSyntax
                    || e is TupleExpressionSyntax
                    ;

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
