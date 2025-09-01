using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal sealed class ArrayCreationCellProvider : AbstractCellProvider<ArrayCreationExpressionSyntax, ExpressionSyntax>
    {
        protected override IEnumerable<SyntaxNode> GetExistingCells(ArrayCreationExpressionSyntax row) =>
            CellProviderHelper.GetChildNodesOfChildNodes(row);
        protected override ExpressionSyntax NewCell(string defaultExpression) =>
            ParseExpression(defaultExpression);
        protected override ArrayCreationExpressionSyntax NewRow(ArrayCreationExpressionSyntax row, IEnumerable<ExpressionSyntax> appendCells)
        {
            var newInitializer = InitializerWithAppendCells(row.Initializer, appendCells);
            return row.WithInitializer(newInitializer);
        }
    }
}
