using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal sealed class ImplicitArrayCreationCellProvider : AbstractCellProvider<ImplicitArrayCreationExpressionSyntax, ExpressionSyntax>
    {
        protected override IEnumerable<SyntaxNode> GetExistingCells(ImplicitArrayCreationExpressionSyntax row) =>
            CellProviderHelper.GetChildNodesOfChildNodes(row);
        protected override ExpressionSyntax NewCell(string defaultExpression) =>
            ParseExpression(defaultExpression);
        protected override ImplicitArrayCreationExpressionSyntax NewRow(ImplicitArrayCreationExpressionSyntax row, IEnumerable<ExpressionSyntax> appendCells)
        {
            var newInitializer = InitializerWithAppendCells(row.Initializer, appendCells);
            return row.WithInitializer(newInitializer);
        }
    }
}
