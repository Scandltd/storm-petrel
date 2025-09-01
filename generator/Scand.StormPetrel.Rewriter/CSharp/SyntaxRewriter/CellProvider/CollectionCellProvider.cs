using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal sealed class CollectionCellProvider : AbstractCellProvider<CollectionExpressionSyntax, CollectionElementSyntax>
    {
        protected override IEnumerable<SyntaxNode> GetExistingCells(CollectionExpressionSyntax row) => row.Elements;
        protected override CollectionElementSyntax NewCell(string defaultExpression) =>
            (ParseExpression($"[{defaultExpression}]") as CollectionExpressionSyntax)
                .Elements
                .Single();
        protected override CollectionExpressionSyntax NewRow(CollectionExpressionSyntax row, IEnumerable<CollectionElementSyntax> appendCells)
        {
            var updatedElements = row
                                    .Elements
                                    .AddRange(appendCells);
            return row.WithElements(updatedElements);
        }
    }
}
