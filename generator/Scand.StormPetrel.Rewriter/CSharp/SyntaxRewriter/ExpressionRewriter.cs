using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class ExpressionRewriter : AbstractValueRewriter
    {
        private readonly SyntaxKind _expressionKind;
        private readonly int _expressionIndex;
        private readonly int? _methodBodyStatementIndex;
        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="methodPath"></param>
        /// <param name="expressionKind"></param>
        /// <param name="expressionIndex"></param>
        /// <param name="valueNewCode"></param>
        /// <param name="methodBodyStatementIndex">If not null, then <paramref name="expressionIndex"/> is relative to the statement.
        /// Applicable only when <paramref name="expressionKind"/> is <see cref="SyntaxKind.Argument"/>.</param>
        public ExpressionRewriter(IEnumerable<string> methodPath, int expressionKind, int expressionIndex, string valueNewCode, int? methodBodyStatementIndex = null)
            : base(methodPath, valueNewCode)
        {
            _expressionKind = (SyntaxKind)expressionKind;
            if (_expressionKind == SyntaxKind.Argument && methodBodyStatementIndex == null)
            {
                throw new ArgumentNullException(nameof(methodBodyStatementIndex));
            }
            else if (_expressionKind != SyntaxKind.Argument && methodBodyStatementIndex != null)
            {
                throw new ArgumentOutOfRangeException(nameof(methodBodyStatementIndex));
            }
            _expressionIndex = expressionIndex <= 0
                                    ? 0
                                    : expressionIndex;
            _methodBodyStatementIndex = methodBodyStatementIndex;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (!IsMatchWithValuePath(node))
            {
                return base.VisitMethodDeclaration(node);
            }
            SyntaxNode newNode = null;
            SyntaxNode nodeToChange = null;
            bool isExpressionBody = false;
            var expressionIndexCurrent = -1;
            Dictionary<StatementSyntax, (int Index, bool IsExpressionIndexReset)> methodBodyStatementToInfo = null;
            if (_methodBodyStatementIndex.HasValue && node.Body != null)
            {
                methodBodyStatementToInfo = new Dictionary<StatementSyntax, (int Index, bool IsIndexReset)>();
                int i = 0;
                foreach (var statement in node.Body.Statements)
                {
                    methodBodyStatementToInfo.Add(statement, (i++, false));
                }
            }
            var _ = node.DescendantNodes(nd =>
            {
                if (newNode != null || isExpressionBody)
                {
                    return false;
                }
                if (!nd.IsKind(_expressionKind))
                {
                    return true;
                }
                if (methodBodyStatementToInfo != null)
                {
                    (int Index, bool IsIndexReset) info = default;
                    var statement = nd.FirstAncestorOrSelf<StatementSyntax>(x => methodBodyStatementToInfo.TryGetValue(x, out info))
                                        ?? throw new InvalidOperationException("Cannot find body statement");
                    if (info.Index == _methodBodyStatementIndex)
                    {
                        if (!info.IsIndexReset)
                        {
                            expressionIndexCurrent = -1;
                            methodBodyStatementToInfo[statement] = (info.Index, true);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                expressionIndexCurrent++;
                if (expressionIndexCurrent < _expressionIndex)
                {
                    return true;
                }
                else if (expressionIndexCurrent > _expressionIndex)
                {
                    return false;
                }
                nodeToChange = nd;
                if (nd is AssignmentExpressionSyntax assignment)
                {
                    newNode = WithRight(assignment);
                }
                else if (nd is LocalDeclarationStatementSyntax localDeclaration)
                {
                    newNode = WithInitializer(localDeclaration);
                }
                else if (nd is ReturnStatementSyntax @return)
                {
                    var newExpression = CreateInitializeExpressionSyntax(@return);
                    newNode = @return.WithExpression(newExpression);
                }
                else if (nd is ArrowExpressionClauseSyntax)
                {
                    if (node.ExpressionBody != null)
                    {
                        isExpressionBody = true;
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else if (nd is SwitchExpressionArmSyntax @switch)
                {
                    var newExpression = CreateInitializeExpressionSyntax(@switch);
                    newNode = @switch.WithExpression(newExpression);
                }
                else if (nd is ArgumentSyntax argument)
                {
                    var topAncestor = nd;
                    foreach (var ancestor in nd.Ancestors())
                    {
                        if (ancestor.Parent == node || ancestor.Parent == node.Body || ancestor == node.ExpressionBody)
                        {
                            //stop on method statement
                            break;
                        }
                        topAncestor = ancestor;
                    }
                    var triviaSources = topAncestor
                                            .DescendantNodesAndTokens()
                                            .Where(x =>
                                            {
                                                var isArgument = x.AsNode()?.IsKind(SyntaxKind.Argument) == true;
                                                if (isArgument)
                                                {
                                                    return false;
                                                }
                                                var isArgumentDescendant = x.Parent.AncestorsAndSelf().Any(y => y.IsKind(SyntaxKind.Argument));
                                                return !isArgumentDescendant;
                                            });
                    var maxTrivia = Utils.GetLeadingWhitespace(argument);
                    var maxTriviaLength = Utils.GetTriviaLength(maxTrivia);
                    foreach (var triviaSource in triviaSources)
                    {
                        var trivia = Utils.GetLeadingWhitespace(triviaSource);
                        var triviaLength = Utils.GetTriviaLength(trivia);
                        if (triviaLength > maxTriviaLength)
                        {
                            maxTrivia = trivia;
                            maxTriviaLength = triviaLength;
                        }
                    }
                    var expression = CreateInitializeExpressionSyntax(maxTrivia);
                    newNode = SyntaxFactory
                                .Argument(expression)
                                .WithNameColon(argument.NameColon)
                                .WithTriviaFrom(argument);
                }
                else
                {
                    newNode = CreateInitializeExpressionSyntax(nd);
                }
                return false;
            }).Count();
            if (isExpressionBody)
            {
                return WithExpressionBody(node);
            }
            if (newNode != null)
            {
                return node.ReplaceNode(nodeToChange, newNode);
            }
            return base.VisitMethodDeclaration(node);
        }
    }
}
