using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class ExpressionRewriter : AbstractValueRewriter
    {
        private readonly SyntaxKind _expressionKind;
        private readonly int _expressionIndex;
        public ExpressionRewriter(IEnumerable<string> methodPath, int expressionKind, int expressionIndex, string valueNewCode)
            : base(methodPath, valueNewCode)
        {
            _expressionKind = (SyntaxKind)expressionKind;
            _expressionIndex = expressionIndex <= 0
                                    ? 0
                                    : expressionIndex;
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
                expressionIndexCurrent++;
                if (expressionIndexCurrent < _expressionIndex)
                {
                    return true;
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
