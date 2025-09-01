using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider
{
    internal abstract class AbstractCellProvider
    {
        public abstract bool TryGetCells(SyntaxNode row, int columnIndex, string[] rowDefaultExpressions, out SyntaxNode rowUpdatedOrSelf, out IEnumerable<SyntaxNode> cells);
    }
    internal abstract class AbstractCellProvider<TRow, TCell> : AbstractCellProvider
        where TRow : SyntaxNode
        where TCell : CSharpSyntaxNode
    {
        public override bool TryGetCells(SyntaxNode row, int columnIndex, string[] rowDefaultExpressions, out SyntaxNode rowUpdatedOrSelf, out IEnumerable<SyntaxNode> cells)
        {
            rowUpdatedOrSelf = row;
            cells = null;
            if (!TryCastRow(row, out var rowCasted))
            {
                return false;
            }
            var cellsArray = GetExistingCells(rowCasted).ToArray();
            var count = cellsArray.Length;
            if (columnIndex >= count
                    && count < rowDefaultExpressions.Length)
            {
                var appendCells = rowDefaultExpressions
                                        .Skip(count)
                                        .Take(columnIndex - count + 1)
                                        .Select(x => NewCell(x))
                                        .Select((x, i) => count == 0 && i == 0
                                                            ? x
                                                            : x.WithLeadingTrivia(SyntaxFactory.Whitespace(" ")));
                rowCasted = NewRow(rowCasted, appendCells);
                rowUpdatedOrSelf = rowCasted;
                cells = GetExistingCells(rowCasted);
            }
            else
            {
                cells = cellsArray;
            }
            return true;
        }
        private static bool TryCastRow(SyntaxNode row, out TRow rowCasted) => TryCast(row, out rowCasted);
        private static bool TryCast<T1, T2>(T1 inObj, out T2 outObj)
            where T1 : class
            where T2 : class
        {
            if (inObj is T2 temp)
            {
                outObj = temp;
                return true;
            }
            outObj = default;
            return false;
        }
        protected abstract IEnumerable<SyntaxNode> GetExistingCells(TRow row);
        protected abstract TCell NewCell(string defaultExpression);
        protected abstract TRow NewRow(TRow row, IEnumerable<TCell> appendCells);
        #region Helper Methods
        protected static ExpressionSyntax ParseExpression(string expression) => SyntaxFactory.ParseExpression(expression);
        protected static InitializerExpressionSyntax InitializerWithAppendCells(InitializerExpressionSyntax initializer, IEnumerable<ExpressionSyntax> appendCells)
        {
            var combinedExpressions = initializer.Expressions.AddRange(appendCells);
            return initializer.WithExpressions(combinedExpressions);
        }
        protected static ArgumentSyntax ParseAsArgumentSyntax(string expression) =>
            SyntaxFactory.ParseArgumentList($"({expression})").Arguments[0];
        #endregion
    }
}
