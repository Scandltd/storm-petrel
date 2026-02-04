using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    internal class StringVerbatimDecoratingFuncProvider : StringDecoratingFuncProviderAbstract
    {
        private readonly int _maxPreservedStringLength;
        private readonly IReadOnlyDictionary<string, IEnumerable<string>> _decoratingTypeNameToPropertyNames;
        public StringVerbatimDecoratingFuncProvider(int maxPreservedStringLength, IReadOnlyDictionary<string, IEnumerable<string>> decoratingTypeNameToPropertyNames)
        {
            _maxPreservedStringLength = maxPreservedStringLength;
            _decoratingTypeNameToPropertyNames = decoratingTypeNameToPropertyNames;
        }
        protected override LiteralExpressionSyntax CreateLiteral(LiteralExpressionDumpContext context)
        {
            var value = context.TokenValueText;
            const string doubleQuotes = "\"";
            if (value.All(x => x != doubleQuotes[0]
                                && x != '\r'
                                && x != '\n'))
            {
                return null;
            }
            // Escape quotes (replace " with "")
            string escapedContent = value.Replace(doubleQuotes, "\"\"");

            // Format as verbatim string (@"...")
            string verbatimText = "@\"" + escapedContent + "\"";

            return SyntaxFactory.LiteralExpression(
                SyntaxKind.StringLiteralExpression,
                SyntaxFactory.Literal(
                    text: verbatimText,
                    value: value  // Actual unescaped value
                )
            );
        }
        protected override IReadOnlyDictionary<string, IEnumerable<string>> GetDecoratingTypeNameToPropertyNames() => _decoratingTypeNameToPropertyNames;
        protected override bool ShouldSkip(LiteralExpressionDumpContext context) =>
            context.TokenValueText.Length <= _maxPreservedStringLength
                || context.TokenText.StartsWith("@");
    }
}
