using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal sealed class BaseObjectCreationCellProvider : AbstractCellProvider<BaseObjectCreationExpressionSyntax, ArgumentSyntax>
    {
        protected override IEnumerable<SyntaxNode> GetExistingCells(BaseObjectCreationExpressionSyntax row) =>
            CellProviderHelper.GetObjectChilds(row.ArgumentList, row.Initializer?.Expressions);
        protected override ArgumentSyntax NewCell(string defaultExpression) => ParseAsArgumentSyntax(defaultExpression);
        protected override BaseObjectCreationExpressionSyntax NewRow(BaseObjectCreationExpressionSyntax row, IEnumerable<ArgumentSyntax> appendCells)
        {
            var updatedArgumentList = appendCells.Aggregate(row.ArgumentList, (ceed, x) => ceed.AddArguments(x));
            return row.WithArgumentList(updatedArgumentList);
        }
    }
}
