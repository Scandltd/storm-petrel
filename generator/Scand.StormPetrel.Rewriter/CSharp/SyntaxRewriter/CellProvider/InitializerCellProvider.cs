using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal sealed class InitializerCellProvider : AbstractCellProvider<InitializerExpressionSyntax, ExpressionSyntax>
    {
        protected override IEnumerable<SyntaxNode> GetExistingCells(InitializerExpressionSyntax row) => row.ChildNodes();
        protected override ExpressionSyntax NewCell(string defaultExpression) =>
            ParseExpression(defaultExpression);
        protected override InitializerExpressionSyntax NewRow(InitializerExpressionSyntax row, IEnumerable<ExpressionSyntax> appendCells)
        {
            var updatedExpressions = row.Expressions.AddRange(appendCells);
            return row.WithExpressions(updatedExpressions);
        }
    }
}
