using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using Scand.StormPetrel.Generator.AssertExpressionDetector;
using Scand.StormPetrel.Generator.ExtraContextInternal;
using Scand.StormPetrel.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                    new AssertAreEqualDetector()
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
                                       var isExpressionOk = IsSupportedExpression(e)
                                                               || e is CastExpressionSyntax castExpression && IsSupportedExpression(castExpression.Expression);
                                       if (isExpressionOk && _expectedExpressionDetectors.Any(x => x.IsExpectedArgument(tmpArgument, actualIdentifier, out actualExpression)))
                                       {
                                           argument = tmpArgument;
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
                        ExpectedExpression = argument.Expression,
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
                    || e is CollectionExpressionSyntax
                    || e is ImplicitArrayCreationExpressionSyntax
                    || e is ImplicitObjectCreationExpressionSyntax
                    || e is LiteralExpressionSyntax
                    || e is ObjectCreationExpressionSyntax
                    || e is TupleExpressionSyntax
                    ;
        }
    }
}
