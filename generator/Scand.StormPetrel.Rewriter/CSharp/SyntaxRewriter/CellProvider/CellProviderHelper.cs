using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal static class CellProviderHelper
    {
        private static IEnumerable<SyntaxNode> FilterOutRedundantNodes(IEnumerable<SyntaxNode> nodes)
            => nodes.Where(x => !(
                                    x.IsKind(SyntaxKind.ArrayRankSpecifier)
                                    || x.IsKind(SyntaxKind.PredefinedType)
                                ));
        public static IEnumerable<SyntaxNode> GetChildNodesOfChildNodes(SyntaxNode node) =>
            node.ChildNodes().SelectMany(x => FilterOutRedundantNodes(x.ChildNodes()));
        public static IEnumerable<SyntaxNode> GetObjectChilds(ImplicitObjectCreationExpressionSyntax node)
            => GetObjectChilds(node?.ArgumentList, node?.Initializer?.Expressions);
        public static IEnumerable<SyntaxNode> GetObjectChilds(ObjectCreationExpressionSyntax node)
            => GetObjectChilds(node?.ArgumentList, node?.Initializer?.Expressions);
        public static IEnumerable<SyntaxNode> GetObjectChilds(ArgumentListSyntax argumentList, SeparatedSyntaxList<ExpressionSyntax>? expressions)
        {
            if (argumentList?.Ancestors().Any(x => x is ThrowExpressionSyntax || x is ThrowStatementSyntax) == true)
            {
                return Enumerable.Empty<SyntaxNode>();
            }
            return argumentList?.Arguments.Count > 0 == true
                    ? argumentList.Arguments
                    : expressions ?? Enumerable.Empty<SyntaxNode>();
        }
    }
}
