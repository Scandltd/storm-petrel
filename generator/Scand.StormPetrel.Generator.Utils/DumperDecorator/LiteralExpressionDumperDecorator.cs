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
        /// Creates new function to prepend verbatim text token (@) to string literal expression value if both:
        /// - The value length is greater than <paramref name="maxPreservedStringLength"/>.
        /// - The value contains double quote or line break characters.
        /// </summary>
        /// <param name="typeNameToVerbatimStringPropertyNames">A dictionary where:
        /// - Key is object constructor token. Use empty string for anonymous or target-typed objects or any constructor token or root expression;
        /// - Value is a list of assignment (e.g. property or field) names to apply verbatim string decoration. Use empty string to decorate any assignment in context of the key.
        /// Use `null` to decorate any string longer than <paramref name="maxPreservedStringLength"/>.</param>
        /// <param name="maxPreservedStringLength">Max length of strings where verbatim token is not applied to.</param>
        /// <returns>New function returning new <see cref="SyntaxNode"/> with verbatim token or <see cref="null"/> if verbatim text token is not applicable for input <see cref="LiteralExpressionDumpContext"/> instance.</returns>
        public static Func<LiteralExpressionDumpContext, SyntaxNode> GetVerbatimStringDecoratingFunc(
                                                                        IReadOnlyDictionary<string, IEnumerable<string>> typeNameToVerbatimStringPropertyNames = null,
                                                                        int maxPreservedStringLength = MaxPreservedStringLength)
        {
            var funcProvider = new StringVerbatimDecoratingFuncProvider(maxPreservedStringLength, typeNameToVerbatimStringPropertyNames);
            return funcProvider.GetDecoratingFunc();
        }
        /// <summary>
        /// Creates new function to replace string regular values with raw string literals.
        /// </summary>
        /// <param name="typeNameToRawStringProperties">Configuration object similar to <see cref="GetVerbatimStringDecoratingFunc(IReadOnlyDictionary{string, IEnumerable{string}}, int)"/> corresponding parameter.</param>
        /// <returns>New function returning new <see cref="SyntaxNode"/> with raw string literal or <see cref="null"/> if raw string literal is not applicable for input <see cref="LiteralExpressionDumpContext"/> instance.</returns>
        public static Func<LiteralExpressionDumpContext, SyntaxNode> GetRawStringDecoratingFunc(
                                                                        IReadOnlyDictionary<string, IEnumerable<RawStringProperty>> typeNameToRawStringProperties = null)
        {
            var funcProvider = new StringRawDecoratingFuncProvider(typeNameToRawStringProperties);
            return funcProvider.GetDecoratingFunc();
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
    }
}
