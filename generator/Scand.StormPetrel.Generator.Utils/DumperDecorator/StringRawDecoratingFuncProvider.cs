using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    internal class StringRawDecoratingFuncProvider : StringDecoratingFuncProviderAbstract
    {
        private readonly IReadOnlyDictionary<string, IEnumerable<RawStringProperty>> _decoratingTypeNameToProperties;
        public StringRawDecoratingFuncProvider(IReadOnlyDictionary<string, IEnumerable<RawStringProperty>> decoratingTypeNameToProperties)
        {
            _decoratingTypeNameToProperties = decoratingTypeNameToProperties;
        }
        protected override LiteralExpressionSyntax CreateLiteral(LiteralExpressionDumpContext context)
        {
            var value = context.TokenValueText;
            string leadingTriviaText = GetLeadingTriviaText(context);
            var rawText = value;
            var newLine = "\n";
            var (wrappingDoubleQuotes, newLineChars) = Shared.Utils.GetRawStringInfo(value);
            if (newLineChars.Length > 0)
            {
                if (leadingTriviaText != "")
                {
                    rawText = Shared.Utils.PrependForEachValueLine(rawText, leadingTriviaText);
                }
                newLine = new string(newLineChars);
                rawText = $"{newLine}{rawText}{newLine}{leadingTriviaText}";
            }
            rawText = $"{wrappingDoubleQuotes}{rawText}{wrappingDoubleQuotes}";
            var comment = _decoratingTypeNameToProperties?
                                .Where(x => x.Key == "" || x.Key.Equals(context.FirstAncestorName, StringComparison.Ordinal))
                                .SelectMany(x => x.Value)
                                .Where(x => x.Name == "" || (x.Name ?? "").Equals(context.AssignmentLeftName, StringComparison.Ordinal))
                                .FirstOrDefault()?
                                .Comment;
            if (comment.HasValue && comment.Value != RawStringPropertyComment.None)
            {
                var commentText = comment.Value == RawStringPropertyComment.LangRegex
                                    ? "// lang=regex"
                                    : comment.Value == RawStringPropertyComment.LangJson
                                        ? "// lang=json"
                                        : throw new InvalidOperationException($"Unexpected value: {comment.Value}");
                rawText = $"{commentText}{newLine}{leadingTriviaText}{rawText}";
            }

            var result = SyntaxFactory.LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                SyntaxFactory.Literal(
                    text: rawText,
                    value: value  // Actual unescaped value
                )
            );
            return result;
        }
        protected override IReadOnlyDictionary<string, IEnumerable<string>> GetDecoratingTypeNameToPropertyNames() =>
            _decoratingTypeNameToProperties?.ToDictionary(x => x.Key, x => x.Value.Select(y => y.Name));
        protected override bool ShouldSkip(LiteralExpressionDumpContext context)
        {
            var valueDoubleQuotesCount =
                context
                    .TokenValueText
                    .TakeWhile(x => x == '"')
                    .Count();
            return context
                        .TokenText
                        .TakeWhile(x => x == '"')
                        .Count() >= 3 + valueDoubleQuotesCount;
        }
        private static string GetLeadingTriviaText(LiteralExpressionDumpContext context)
        {
            SyntaxNode maxTriviaNode = null;
            foreach (var a in context.OriginalLiteralExpression.Ancestors())
            {
                if (maxTriviaNode == null)
                {
                    maxTriviaNode = a;
                }
                else
                {
                    maxTriviaNode = Shared.Utils.MaxTriviaNode(a, maxTriviaNode);
                }
            }
            var leadingTriviaText = Shared.Utils.GetLeadingWhitespace(maxTriviaNode).ToFullString() ?? "";
            return leadingTriviaText;
        }
    }
}
