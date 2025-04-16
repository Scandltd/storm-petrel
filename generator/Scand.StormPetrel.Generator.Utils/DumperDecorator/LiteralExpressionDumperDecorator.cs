using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    /// <summary>
    /// Decorates literal expressions.
    /// </summary>
    public sealed class LiteralExpressionDumperDecorator : AbstractDumperDecorator
    {
        private readonly Func<LiteralExpressionDumpContext, SyntaxNode> _decoratingFunc;
        private const int MaxPreservedStringLength = 64;
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dumper">A dumper to be decorated.</param>
        /// <param name="decoratingFunc">A function returning new <see cref="SyntaxNode"/> to replace input <see cref="LiteralExpressionDumpContext.OriginalLiteralExpression"/>.
        /// Return null to avoid the replacement.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LiteralExpressionDumperDecorator(
            IGeneratorDumper dumper,
            Func<LiteralExpressionDumpContext, SyntaxNode> decoratingFunc)
                : base(dumper)
        {
            _decoratingFunc = decoratingFunc ?? throw new ArgumentNullException(nameof(decoratingFunc));
        }
        /// <summary>
        /// Creates new instance using <see cref="GetVerbatimStringDecoratingFunc"/>.
        /// </summary>
        /// <param name="dumper">A dumper to be decorated.</param>
        /// <param name="typeNameToVerbatimStringPropertyNames">Parameter value for <see cref="GetVerbatimStringDecoratingFunc"/>.</param>
        /// <param name="maxPreservedStringLength">Parameter value for <see cref="GetVerbatimStringDecoratingFunc"/>.</param>
        public LiteralExpressionDumperDecorator(
            IGeneratorDumper dumper,
            Dictionary<string, IEnumerable<string>> typeNameToVerbatimStringPropertyNames,
            int maxPreservedStringLength = MaxPreservedStringLength)
                : this(dumper,
                        GetVerbatimStringDecoratingFunc(typeNameToVerbatimStringPropertyNames, maxPreservedStringLength))
        {
        }
        /// <summary>
        /// Creates new funtion to prepend verbatim text token (@) to string literal expression value if both:
        /// - The value length is greater than <paramref name="maxPreservedStringLength"/>.
        /// - The value contains double quote or line break characters.
        /// </summary>
        /// <param name="typeNameToVerbatimStringPropertyNames">A dictionary where:
        /// - Key is object constructor token. Use empty string for anonymous or target-typed objects or any constructor token or root expression;
        /// - Value is a list of assigment (e.g. property or field) names to apply verbatim string decoration. Use empty string to decorate any assignment in context of the key.</param>
        /// <param name="maxPreservedStringLength"></param>
        /// <returns>New function returning new <see cref="SyntaxNode"/> or <see cref="null"/> if verbatim text token is not applicable.</returns>
        public static Func<LiteralExpressionDumpContext, SyntaxNode> GetVerbatimStringDecoratingFunc(
                                                                        Dictionary<string, IEnumerable<string>> typeNameToVerbatimStringPropertyNames,
                                                                        int maxPreservedStringLength = MaxPreservedStringLength)
        {
            return (LiteralExpressionDumpContext context) =>
            {
                var nd = context.OriginalLiteralExpression;
                if (!nd.IsKind(SyntaxKind.StringLiteralExpression)
                        || context.TokenValueText == null
                        || context.TokenValueText.Length <= maxPreservedStringLength
                        || context.TokenText == null
                        || context.TokenText.StartsWith("@"))
                {
                    return null;
                }
                if (context.FirstAncestorName != null
                        && typeNameToVerbatimStringPropertyNames.TryGetValue(context.FirstAncestorName, out var verbatimStringPropertyNames)
                            && (verbatimStringPropertyNames.Contains("") || verbatimStringPropertyNames.Contains(context.AssignmentLeftName))
                    || typeNameToVerbatimStringPropertyNames.TryGetValue("", out var verbatimStringPropertyNamesDefault)
                        && (verbatimStringPropertyNamesDefault.Contains("") || verbatimStringPropertyNamesDefault.Contains(context.AssignmentLeftName)))
                {
                    return CreateVerbatimStringLiteral(context.TokenValueText)
                            ?.WithTriviaFrom(nd);
                }
                return null;
            };
        }

        /// <summary>
        /// Creates new function to convert all numeric values from integer range to hex format.
        /// The method has naive implementation developed mostly for demonstrative purposes.
        /// </summary>
        /// <returns></returns>
        public static Func<LiteralExpressionDumpContext, SyntaxNode> GetNumberToHexDecoratingFunc()
        {
            return (LiteralExpressionDumpContext context) =>
            {
                if (!context.OriginalLiteralExpression.IsKind(SyntaxKind.NumericLiteralExpression)
                        || !int.TryParse(context.TokenText, out var number))
                {
                    return null;
                }
                return SyntaxFactory.LiteralExpression(
                    SyntaxKind.NumericLiteralExpression,
                    SyntaxFactory.Literal($"0x{number:x}", number)
                );
            };
        }

        /// <summary>
        /// Decorates the <paramref name="node"/> based on instance parameters.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public SyntaxNode Decorate(SyntaxNode node) => DumpImplementation(node);
        protected override SyntaxNode DumpImplementation(SyntaxNode node)
        {
            var originalNodeToReplacement = node
                                                .DescendantNodesAndSelf()
                                                .OfType<LiteralExpressionSyntax>()
                                                .Select(x => (OriginalNode: x, Replacement: _decoratingFunc(GetLiteralExpressionDumpContext(x))))
                                                .Where(x => x.Replacement != null)
                                                .ToDictionary(x => x.OriginalNode, x => x.Replacement);
            var updatedNode = node.ReplaceNodes(originalNodeToReplacement.Keys, (nd, _) => originalNodeToReplacement[nd]);
            return updatedNode;
        }
        private static LiteralExpressionDumpContext GetLiteralExpressionDumpContext(LiteralExpressionSyntax literalExpression)
        {
            var ancestorAssignment = literalExpression
                                        .Ancestors()
                                        .FirstOrDefault(x => x.IsKind(SyntaxKind.SimpleAssignmentExpression)
                                                                || x.IsKind(SyntaxKind.AnonymousObjectMemberDeclarator));
            var nodeInfo = ancestorAssignment != null
                                ? GetAssignmentNodeInfo(ancestorAssignment)
                                : null;

            return new LiteralExpressionDumpContext
            {
                AssignmentLeftName = nodeInfo.HasValue ? nodeInfo.Value.AssignmentLeftName : null,
                FirstAncestorName = nodeInfo.HasValue ? nodeInfo.Value.FirstAncestorName : null,
                OriginalLiteralExpression = literalExpression,
                TokenText = literalExpression.Token.Text,
                TokenValueText = literalExpression.Token.ValueText,
            };
        }
        private static LiteralExpressionSyntax CreateVerbatimStringLiteral(string value)
        {
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
    }
}
