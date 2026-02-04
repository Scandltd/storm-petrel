using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scand.StormPetrel.Shared
{
    internal static class Utils
    {
        private static readonly SyntaxTrivia EmptyTrivia = SyntaxFactory.Whitespace("");

        public static ExpressionSyntax CreateInitializeExpressionSyntax(string initializeCode, SyntaxNode leadingTriviaDonor)
            => CreateInitializeExpressionSyntax(initializeCode, GetLeadingWhitespace(leadingTriviaDonor));
        public static ExpressionSyntax CreateInitializeExpressionSyntax(string initializeCode, SyntaxTrivia leadingWhitespace)
        {
            ExpressionSyntax expression = SyntaxFactory.ParseExpression(initializeCode);
            var leadingWhitespaceString = leadingWhitespace.ToFullString();
            if (!string.IsNullOrEmpty(leadingWhitespaceString))
            {
                var rawStringLiterals = expression
                                            .DescendantNodesAndSelf()
                                            .OfType<LiteralExpressionSyntax>()
                                            .Where(x => x.Token.Value is string s && !string.IsNullOrEmpty(s))
                                            .Where(x =>
                                            {
                                                var (wrappingDoubleQuotes, _) = GetRawStringInfo(x.Token.ValueText);
                                                var isRawString = x.Token.Text.StartsWith(wrappingDoubleQuotes, StringComparison.Ordinal);
                                                return isRawString == true;
                                            });
                expression = expression.ReplaceNodes(rawStringLiterals, (originalNode, _) =>
                    originalNode
                        .WithToken(SyntaxFactory.Literal(
                            text: PrependForEachValueLine(originalNode.Token.Text, leadingWhitespace.ToFullString(), true),
                            value: (string)originalNode.Token.Value
                        ))
                        .WithTriviaFrom(originalNode));
            }
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
        public static (string WrappingDoubleQuotes, char[] NewLine) GetRawStringInfo(string value)
        {
            int maxConsecutiveDoubleQuotesCount = 0;
            int consecutiveDoubleQuotesCount = 0;
            char prevChar = ' ';
            var newLineChars = new List<(int Index, char Char)>(2);
            for (int i = 0; i < value.Length; i++)
            {
                var c = value[i];
                if (prevChar != '"')
                {
                    consecutiveDoubleQuotesCount = 0;
                }
                if (c == '"')
                {
                    consecutiveDoubleQuotesCount++;
                }
                if (consecutiveDoubleQuotesCount > maxConsecutiveDoubleQuotesCount)
                {
                    maxConsecutiveDoubleQuotesCount = consecutiveDoubleQuotesCount;
                }
                if ((c == '\r' || c == '\n')
                        && (newLineChars.Count == 0 || newLineChars.Count == 1
                                                        && newLineChars[0].Index == (i - 1)
                                                        && newLineChars[0].Char == '\r'
                                                        && c == '\n'))
                {
                    newLineChars.Add((i, c));
                }
                prevChar = c;
            }
            var wrappingDoubleQuotes = string.Join("", Enumerable.Repeat('"', Math.Max(3, maxConsecutiveDoubleQuotesCount + 1)));
            return (wrappingDoubleQuotes, newLineChars.Select(x => x.Char).ToArray());
        }
        public static string PrependForEachValueLine(string value, string triviaText, bool skipOnStartLine = false)
        {
            var sb = new StringBuilder();
            if (!skipOnStartLine)
            {
                sb.Append(triviaText);
            }
            for (int i = 0; i < value.Length; i++)
            {
                var c = value[i];
                sb.Append(c);
                if (c == '\n'
                        || c == '\r' && (i == value.Length - 1 || value[i + 1] != '\n'))
                {
                    sb.Append(triviaText);
                }
            }
            return sb.ToString();
        }
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
