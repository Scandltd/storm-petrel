using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    public sealed class LiteralExpressionDumpContext
    {
        public LiteralExpressionSyntax OriginalLiteralExpression { get; set; }
        public string AssignmentLeftName { get; set; }
        public string FirstAncestorName { get; set; }
        /// <summary>
        /// Original source text (includes @, quotes, and escape sequences).
        /// </summary>
        public string TokenText { get; set; }
        /// <summary>
        /// "Decoded" content (no quotes, unescaped).
        /// </summary>
        public string TokenValueText { get; set; }
    }
}
