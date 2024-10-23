using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    internal static class Utils
    {
        public static ExpressionSyntax CreateInitializeExpressionSyntax(string initializeCode, SyntaxNode leadingTriviaDonor)
        {
            var endOfLines = new List<(int Start, int Length)>();
            int i = 0;
            while (i < initializeCode.Length)
            {
                var c = initializeCode[i];
                if (c == '\r' && i < initializeCode.Length - 1 && initializeCode[i + 1] == '\n')
                {
                    endOfLines.Add((i, 2));
                    i += 1;
                }
                else if (c == '\r' || c == '\n')
                {
                    endOfLines.Add((i, 1));
                }
                i++;
            }
            var leadingWhitespace = GetLeadingWhitespace(leadingTriviaDonor);
            string leadingWhitespaceAsString;
            using (var w = new StringWriter())
            {
                leadingWhitespace.WriteTo(w);
                w.Flush();
                leadingWhitespaceAsString = w.ToString();
            }
            var sb = new StringBuilder();
            int currentStartIndex = 0;
            for (i = 0; i <= endOfLines.Count; i++)
            {
                int length = i < endOfLines.Count
                                ? endOfLines[i].Start - currentStartIndex + endOfLines[i].Length
                                : initializeCode.Length - currentStartIndex;
                var line = initializeCode.Substring(currentStartIndex, length);
                if (i != 0)
                {
                    sb.Append(leadingWhitespaceAsString);
                }
                sb.Append(line);
                currentStartIndex += length;
            }
            var combinedExpression = SyntaxFactory.ParseExpression(sb.ToString());
            return combinedExpression;

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
