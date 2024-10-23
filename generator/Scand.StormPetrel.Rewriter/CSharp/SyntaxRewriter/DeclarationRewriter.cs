using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class DeclarationRewriter : AbstractValueRewriter
    {
        public DeclarationRewriter(IEnumerable<string> declarationPath, string declarationNewCode)
            : base(declarationPath, declarationNewCode)
        {
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (node.Declaration.Variables.Count > 1)
            {
                return base.VisitLocalDeclarationStatement(node);
            }
            if (node.Declaration.Variables[0].Initializer == null)
            {
                return base.VisitLocalDeclarationStatement(node);
            }
            var isMatch = IsMatchWithValuePath(node);
            if (isMatch)
            {
                return WithInitializer(node);
            }
            return base.VisitLocalDeclarationStatement(node);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            AccessorDeclarationSyntax getAccessor = null;
            foreach (var a in node.AccessorList?.Accessors ?? Enumerable.Empty<AccessorDeclarationSyntax>())
            {
                if (a.IsKind(SyntaxKind.GetAccessorDeclaration))
                {
                    getAccessor = a;
                    break;
                }
            }
            if (node.Initializer == null
                && node.ExpressionBody == null
                && getAccessor == null
                    || !IsMatchWithValuePath(node))
            {
                return base.VisitPropertyDeclaration(node);
            }
            if (node.Initializer != null)
            {
                var newExpression = CreateInitializeExpressionSyntax(node);
                var newInitializer = node.Initializer.WithValue(newExpression);
                return node.ReplaceNode(node.Initializer, newInitializer);
            }
            var getAccessorReturns = getAccessor?
                                        .DescendantNodes(x => !(x is ReturnStatementSyntax))
                                        .Where(x => x is ReturnStatementSyntax)
                                        .Cast<ReturnStatementSyntax>();
            if (getAccessorReturns?.Any() == true)
            {
                var newExpression = CreateInitializeExpressionSyntax(node)
                                        .WithoutTrailingTrivia();
                //Replace all `returns` (even some of replaces might be redundant) so that it is visible for end user
                return node.ReplaceNodes(getAccessorReturns, (x, _) => x.WithExpression(newExpression));
            }
            if (getAccessor?.ExpressionBody != null)
            {
                var newGetAccessor = WithExpressionBody(getAccessor);
                return node.ReplaceNode(getAccessor, newGetAccessor);
            }
            if (node.ExpressionBody == null)
            {
                var newExpression = CreateInitializeExpressionSyntax(node)
                                        .WithoutTrailingTrivia();
                var equalsToken = SyntaxFactory
                                    .Token(SyntaxKind.EqualsToken)
                                    .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                                    .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                var equalsValue = SyntaxFactory
                                    .EqualsValueClause(equalsToken, newExpression);
                return node
                        .WithoutTrailingTrivia()
                        .WithInitializer(equalsValue)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        .WithTrailingTrivia(node.GetTrailingTrivia());
            }

            return WithExpressionBody(node);
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
            return WithExpressionBody(node);
        }
    }
}
