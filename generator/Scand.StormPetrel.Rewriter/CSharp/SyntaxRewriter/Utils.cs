using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    internal static class Utils
    {
        private static readonly SyntaxTrivia EmptyTrivia = SyntaxFactory.Whitespace("");

        public static ExpressionSyntax CreateInitializeExpressionSyntax(string initializeCode, SyntaxNode leadingTriviaDonor)
            => CreateInitializeExpressionSyntax(initializeCode, GetLeadingWhitespace(leadingTriviaDonor));
        public static ExpressionSyntax CreateInitializeExpressionSyntax(string initializeCode, SyntaxTrivia leadingWhitespace)
        {
            ExpressionSyntax expression = SyntaxFactory.ParseExpression(initializeCode);
            var eolTrivias = expression
                                .DescendantTrivia()
                                .Where(trivia => trivia.IsKind(SyntaxKind.EndOfLineTrivia));
            expression = expression.ReplaceTrivia(eolTrivias, (originalTrivia, _) =>
            {
                var triviaListAsString = SyntaxFactory.TriviaList(originalTrivia, leadingWhitespace).ToFullString();
                return SyntaxFactory.SyntaxTrivia(originalTrivia.Kind(), triviaListAsString);
            });
            return expression;
        }

        public static SyntaxNode MaxTriviaNode(SyntaxNode a, SyntaxNode b)
        {
            var aLength = GetLeadingWhitespaceLength(a);
            var bLength = GetLeadingWhitespaceLength(b);
            return aLength >= bLength ? a : b;
        }

        public static SyntaxTrivia GetLeadingWhitespace(SyntaxNode syntaxNode)
            => GetLeadingWhitespaceImplementation(syntaxNode.GetLeadingTrivia());

        public static SyntaxTrivia GetLeadingWhitespace(SyntaxNodeOrToken nodeOrToken)
            => GetLeadingWhitespaceImplementation(nodeOrToken.GetLeadingTrivia());

        public static int GetLeadingWhitespaceLength(SyntaxNode syntaxNode)
            => GetTriviaLength(GetLeadingWhitespace(syntaxNode));

        public static int GetTriviaLength(SyntaxTrivia trivia)
            => trivia.FullSpan.Length;

        private static SyntaxTrivia GetLeadingWhitespaceImplementation(SyntaxTriviaList trivias)
        {
            var triviasReversed = trivias.Reverse();
            SyntaxTrivia? syntaxNodeLineLeadingWhitespace = null; // leading trivia of the same line where syntaxNode is located
            foreach (var trivia in triviasReversed)
            {
                if (trivia.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    //Another line found
                    break;
                }
                if (trivia.IsKind(SyntaxKind.WhitespaceTrivia))
                {
                    syntaxNodeLineLeadingWhitespace = trivia;
                }
            }
            if (syntaxNodeLineLeadingWhitespace == null)
            {
                return EmptyTrivia;
            }
            return syntaxNodeLineLeadingWhitespace.Value;
        }
    }
}
