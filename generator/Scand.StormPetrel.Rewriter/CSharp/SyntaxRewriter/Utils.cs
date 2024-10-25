using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    internal static class Utils
    {
        public static ExpressionSyntax CreateInitializeExpressionSyntax(string initializeCode, SyntaxNode leadingTriviaDonor)
        {
            var leadingWhitespace = GetLeadingWhitespace(leadingTriviaDonor);
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

            SyntaxTrivia GetLeadingWhitespace(SyntaxNode syntaxNode)
            {
                var trivias = syntaxNode.GetLeadingTrivia().Reverse();
                SyntaxTrivia? syntaxNodeLineLeadingWhitespace = null; // leading trivia of the same line where syntaxNode is located
                foreach (var trivia in trivias)
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
                    return SyntaxFactory.Whitespace("");
                }
                return syntaxNodeLineLeadingWhitespace.Value;
            }
        }
    }
}
