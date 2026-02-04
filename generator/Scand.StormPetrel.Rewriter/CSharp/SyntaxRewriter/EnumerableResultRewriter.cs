using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.CellProvider;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class EnumerableResultRewriter : AbstractValueRewriter
    {
        private readonly int _resultRowIndex;
        private readonly int _resultColumnIndex;
        private readonly string[] _rowDefaultExpressions;
        private bool _isMatched;
        private bool _isCellReplaced;
        public EnumerableResultRewriter(IEnumerable<string> methodPath, int resultRowIndex, int resultColumnIndex, string valueNewCode, string[] rowDefaultExpressions = null)
            : base(methodPath, valueNewCode)
        {
            _resultRowIndex = resultRowIndex;
            _resultColumnIndex = resultColumnIndex;
            _rowDefaultExpressions = rowDefaultExpressions ?? Array.Empty<string>();
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            return VisitMemberDeclaration(node, node.Body, base.VisitMethodDeclaration);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            var getAccessor = node.AccessorList?.Accessors.FirstOrDefault(x => (x.Keyword.Value as string) == "get");
            var body = (SyntaxNode)node.ExpressionBody ?? (SyntaxNode)getAccessor?.Body ?? getAccessor?.ExpressionBody;
            return VisitMemberDeclaration(node, body, base.VisitPropertyDeclaration);
        }

        public override SyntaxNode VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            return VisitMemberDeclaration(node, null, base.VisitPropertyPatternClause);
        }

        private SyntaxNode VisitMemberDeclaration<T>(T node, SyntaxNode nodeBody, Func<T, SyntaxNode> baseVisit) where T : CSharpSyntaxNode
        {
            if (IsMatchWithValuePath(node))
            {
                _isMatched = true;
            }
            var yieldReturnChilds = nodeBody?.ChildNodes().OfType<YieldStatementSyntax>();
            SyntaxNode result;
            if (yieldReturnChilds?.Any() == true)
            {
                result = VisitImplementation(node, yieldReturnChilds, x => x);
            }
            else
            {
                result = baseVisit(node);
            }
            _isMatched = false;
            return result;
        }

        public override SyntaxNode VisitArrayCreationExpression(ArrayCreationExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), CellProviderHelper.GetChildNodesOfChildNodes(node), base.VisitArrayCreationExpression);

        public override SyntaxNode VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), CellProviderHelper.GetChildNodesOfChildNodes(node), base.VisitImplicitArrayCreationExpression);

        public override SyntaxNode VisitCollectionExpression(CollectionExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), node.ChildNodes(), base.VisitCollectionExpression);

        public override SyntaxNode VisitTupleExpression(TupleExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), node.ChildNodes(), base.VisitTupleExpression);

        public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node) =>
            VisitImplementation(node, CellProviderHelper.GetObjectChilds(node), base.VisitImplicitObjectCreationExpression);

        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
         => VisitImplementation(node, CellProviderHelper.GetObjectChilds(node), base.VisitObjectCreationExpression);

        private SyntaxNode VisitImplementation<T>(T node, IEnumerable<SyntaxNode> rows, Func<T, SyntaxNode> baseVisit)
            where T : SyntaxNode
        {
            if (!_isMatched)
            {
                return node;
            }

            if (_isCellReplaced)
            {
                return node;
            }
            _isCellReplaced = true;

            var row = rows
                        .Skip(_resultRowIndex)
                        .FirstOrDefault();
            if (row == null)
            {
                return node;
            }
            IEnumerable<SyntaxNode> cells = GetCells(row, out var rowUpdatedOrSelf);
            if (cells == null)
            {
                return node;
            }
            var cell = cells
                        .Skip(_resultColumnIndex)
                        .FirstOrDefault();
            if (cell == null)
            {
                return baseVisit(node);
            }
            var maxTriviaDonor = MaxTriviaNode(cell, cell.Parent, rows.First(), cells.First());
            var initExpression = CreateInitializeExpressionSyntax(maxTriviaDonor);
            initExpression = initExpression
                                .WithLeadingTrivia(cell.GetLeadingTrivia())
                                .WithTrailingTrivia(cell.GetTrailingTrivia());
            SyntaxNode newNode;
            if (cell is ExpressionSyntax)
            {
                newNode = initExpression;
            }
            else if (cell is ExpressionElementSyntax)
            {
                newNode = SyntaxFactory.ExpressionElement(initExpression);
            }
            else if (cell is ArgumentSyntax)
            {
                newNode = SyntaxFactory.Argument(initExpression);
            }
            else
            {
                return baseVisit(node);
            }
            rowUpdatedOrSelf = rowUpdatedOrSelf.ReplaceNode(cell, newNode);
            return node.ReplaceNode(row, rowUpdatedOrSelf);
        }

        private IEnumerable<SyntaxNode> GetCells(SyntaxNode row, out SyntaxNode rowUpdatedOrSelf)
        {
            rowUpdatedOrSelf = row;
            IEnumerable<SyntaxNode> cells = null;
            if (row is ExpressionElementSyntax)
            {
                cells = GetCellsFromExpression(row, x => ((ExpressionElementSyntax)x).Expression, out rowUpdatedOrSelf);
            }
            else if (row is YieldStatementSyntax)
            {
                cells = GetCellsFromExpression(row, x => ((YieldStatementSyntax)x).Expression, out rowUpdatedOrSelf);
            }
            else
            {
                var (_, rowFromProviders, cellsFromProviders) =
                    GetCellProviders()
                        .Select(x => (IsCellsOk: x.TryGetCells(row, _resultColumnIndex, _rowDefaultExpressions, out var tempRow, out var tempCells), Row: tempRow, Cells: tempCells))
                        .FirstOrDefault(x => x.IsCellsOk);
                if (rowFromProviders != null)
                {
                    rowUpdatedOrSelf = rowFromProviders;
                    cells = cellsFromProviders;
                }
            }
            return cells;
        }

        private IEnumerable<SyntaxNode> GetCellsFromExpression(SyntaxNode row, Func<SyntaxNode, ExpressionSyntax> expressionSelector, out SyntaxNode rowUpdatedOrSelf)
        {
            rowUpdatedOrSelf = row;
            var rowExpression = expressionSelector(row);
            var cells = GetCells(expressionSelector(row), out var rowExpressionUpdatedOrSelf);
            if (rowExpression != rowExpressionUpdatedOrSelf)
            {
                rowUpdatedOrSelf = row.ReplaceNode(rowExpression, rowExpressionUpdatedOrSelf);
                cells = GetCells(expressionSelector(rowUpdatedOrSelf), out var _);
            }
            return cells;
        }

        private static IEnumerable<AbstractCellProvider> GetCellProviders()
        {
            yield return new BaseObjectCreationCellProvider();
            yield return new ArrayCreationCellProvider();
            yield return new ImplicitArrayCreationCellProvider();
            yield return new TupleCellProvider();
            yield return new CollectionCellProvider();
            yield return new InitializerCellProvider();
        }

        private static SyntaxNode MaxTriviaNode(SyntaxNode a, SyntaxNode b, SyntaxNode c, SyntaxNode d)
        {
            var max = Utils.MaxTriviaNode(a, b);
            var max2 = Utils.MaxTriviaNode(c, d);
            return Utils.MaxTriviaNode(max, max2);
        }
    }
}
