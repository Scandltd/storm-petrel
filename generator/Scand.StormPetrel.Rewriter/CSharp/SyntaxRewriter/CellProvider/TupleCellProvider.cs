using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal sealed class TupleCellProvider : AbstractCellProvider<TupleExpressionSyntax, ArgumentSyntax>
    {
        protected override IEnumerable<SyntaxNode> GetExistingCells(TupleExpressionSyntax row) => row.Arguments;
        protected override ArgumentSyntax NewCell(string defaultExpression) => ParseAsArgumentSyntax(defaultExpression);
        protected override TupleExpressionSyntax NewRow(TupleExpressionSyntax row, IEnumerable<ArgumentSyntax> appendCells)
        {
            var updatedArguments = row.Arguments.AddRange(appendCells);
            return row.WithArguments(updatedArguments);
        }
    }
}
