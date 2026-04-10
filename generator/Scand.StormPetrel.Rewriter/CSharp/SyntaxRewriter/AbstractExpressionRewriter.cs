using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public abstract class AbstractExpressionRewriter : AbstractValueRewriter
    {
        private protected readonly SyntaxKind _expressionKind;
        private readonly int _expressionIndex;
        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="methodPath"></param>
        /// <param name="expressionKind"></param>
        /// <param name="expressionIndex"></param>
        /// <param name="valueNewCode"></param>
        private protected AbstractExpressionRewriter(IEnumerable<string> methodPath, int expressionKind, int expressionIndex, string valueNewCode, IEnumerable<string> invocationPath) :
            base(methodPath, valueNewCode, invocationPath)
        {
            _expressionKind = (SyntaxKind)expressionKind;
            _expressionIndex = expressionIndex <= 0
                                    ? 0
                                    : expressionIndex;
        }
        protected abstract object OnBeforeVisitDescendantNodesState(MethodDeclarationSyntax node);
        protected abstract bool IsDescendantNodeVisitable(object onBeforeVisitDescendantNodesState, SyntaxNode descendantNode, out bool isResetExpressionIndex);
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
            var state = OnBeforeVisitDescendantNodesState(node);
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
                var isVisitable = IsDescendantNodeVisitable(state, nd, out var isResetExpressionIndex);
                if (!isVisitable)
                {
                    return false;
                }
                if (isResetExpressionIndex)
                {
                    expressionIndexCurrent = 0;
                }
                else
                {
                    expressionIndexCurrent++;
                }
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
                    if (_invocationPath.Length == 0)
                    {
                        var newExpression = CreateInitializeExpressionSyntax(@return);
                        newNode = @return.WithExpression(newExpression);
                    }
                    else
                    {
                        newNode = WithInvocationPathHandling(@return, @return.Expression);
                    }
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
                    if (_invocationPath.Length == 0)
                    {
                        var newExpression = CreateInitializeExpressionSyntax(@switch);
                        newNode = @switch.WithExpression(newExpression);
                    }
                    else
                    {
                        newNode = WithInvocationPathHandling(@switch, @switch.Expression);
                    }
                }
                else if (nd is ArgumentSyntax argument)
                {
                    // Invocation path is not applicable for this case
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
                    // Invocation path is not applicable for this case
                    newNode = CreateInitializeExpressionSyntax(nd);
                }
                return false;
            }).Count();
            if (isExpressionBody)
            {
                return WithExpressionBody(node);
            }
            if (newNode != null && newNode != nodeToChange)
            {
                return node.ReplaceNode(nodeToChange, newNode);
            }
            return base.VisitMethodDeclaration(node);
        }
    }
}
