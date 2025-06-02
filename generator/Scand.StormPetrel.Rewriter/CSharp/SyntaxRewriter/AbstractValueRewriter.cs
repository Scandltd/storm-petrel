using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public abstract class AbstractValueRewriter : AbstractRewriter
    {
        private protected readonly string[] _valuePath;
        private protected readonly string _valueNewCode;

        private protected AbstractValueRewriter(IEnumerable<string> valuePath, string valueNewCode)
        {
            var valuePathArray = valuePath?.ToArray();
            if (valuePathArray == null || valuePathArray.Length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(valuePath), "Must have at least 1 element");
            }
            _valuePath = valuePathArray;
            _valueNewCode = valueNewCode;
        }

        private protected ExpressionSyntax CreateInitializeExpressionSyntax(SyntaxNode leadingTriviaDonor)
            => Utils.CreateInitializeExpressionSyntax(_valueNewCode, leadingTriviaDonor);

        private protected ExpressionSyntax CreateInitializeExpressionSyntax(SyntaxTrivia leadingTrivia)
            => Utils.CreateInitializeExpressionSyntax(_valueNewCode, leadingTrivia);

        private protected bool IsMatchWithValuePath(CSharpSyntaxNode node)
            => SyntaxNodeHelper.IsMatchWithValuePath(node, _valuePath);

        private ArrowExpressionClauseSyntax GetExpressionBody(SyntaxNode node, ArrowExpressionClauseSyntax expressionBody)
        {
            var newExpression = CreateInitializeExpressionSyntax(node);
            var newExpressionWithTrivia = newExpression.WithTriviaFrom(expressionBody.Expression);
            var arrowExpression = SyntaxFactory.ArrowExpressionClause(expressionBody.ArrowToken, newExpressionWithTrivia);
            return arrowExpression.WithTriviaFrom(expressionBody);
        }

        private protected SyntaxNode WithExpressionBody(MethodDeclarationSyntax node)
        {
            var newBody = GetExpressionBody(node, node.ExpressionBody);
            return node.WithExpressionBody(newBody);
        }

        private protected SyntaxNode WithExpressionBody(AccessorDeclarationSyntax node)
        {
            var newBody = GetExpressionBody(node, node.ExpressionBody);
            return node.WithExpressionBody(newBody);
        }

        private protected SyntaxNode WithExpressionBody(PropertyDeclarationSyntax node)
        {
            var newBody = GetExpressionBody(node, node.ExpressionBody);
            return node.WithExpressionBody(newBody);
        }

        private protected SyntaxNode WithRight(AssignmentExpressionSyntax node)
        {
            var newExpression = CreateInitializeExpressionSyntax(node);
            var newNode = node.WithRight(newExpression);
            return newNode;
        }

        private protected SyntaxNode WithInitializer(LocalDeclarationStatementSyntax node)
        {
            var declarator = node.Declaration.Variables.First();
            var newExpression = CreateInitializeExpressionSyntax(declarator.Parent);
            var newInitializer = declarator.Initializer.WithValue(newExpression);
            var newNode = node.ReplaceNode(declarator.Initializer, newInitializer);
            return newNode;
        }
    }
}
