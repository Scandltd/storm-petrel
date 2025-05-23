using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class EnumerableResultRewriter : AbstractValueRewriter
    {
        private readonly int _resultRowIndex;
        private readonly int _resultColumnIndex;
        private bool _isMatched;
        private bool _isCellReplaced;
        public EnumerableResultRewriter(IEnumerable<string> methodPath, int resultRowIndex, int resultColumnIndex, string valueNewCode)
            : base(methodPath, valueNewCode)
        {
            _resultRowIndex = resultRowIndex;
            _resultColumnIndex = resultColumnIndex;
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
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), GetChildNodesOfChildNodes(node), base.VisitArrayCreationExpression);

        public override SyntaxNode VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), GetChildNodesOfChildNodes(node), base.VisitImplicitArrayCreationExpression);

        public override SyntaxNode VisitCollectionExpression(CollectionExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), node.ChildNodes(), base.VisitCollectionExpression);

        public override SyntaxNode VisitTupleExpression(TupleExpressionSyntax node) =>
            VisitImplementation(node ?? throw new ArgumentNullException(nameof(node)), node.ChildNodes(), base.VisitTupleExpression);

        public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node) =>
            VisitImplementation(node, GetChilds(node), base.VisitImplicitObjectCreationExpression);

        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
         => VisitImplementation(node, GetChilds(node), base.VisitObjectCreationExpression);

        private static IEnumerable<SyntaxNode> GetChilds(ImplicitObjectCreationExpressionSyntax node)
            => GetChilds(node?.ArgumentList, node?.Initializer?.Expressions);

        private static IEnumerable<SyntaxNode> GetChilds(ObjectCreationExpressionSyntax node)
            => GetChilds(node?.ArgumentList, node?.Initializer?.Expressions);

        private static IEnumerable<SyntaxNode> GetChilds(ArgumentListSyntax argumentList, SeparatedSyntaxList<ExpressionSyntax>? expressions)
            => argumentList?.Arguments.Count > 0 == true
                ? argumentList.Arguments
                : expressions ?? Enumerable.Empty<SyntaxNode>();

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
            IEnumerable<SyntaxNode> cells = GetCells(row);
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
            return node.ReplaceNode(cell, newNode);
        }

        private static IEnumerable<SyntaxNode> GetCells(SyntaxNode row)
        {
            IEnumerable<SyntaxNode> cells = null;
            if (row is BaseObjectCreationExpressionSyntax baseObjectCreation)
            {
                cells = GetChilds(baseObjectCreation.ArgumentList, baseObjectCreation.Initializer?.Expressions);
            }
            else if (row is ArrayCreationExpressionSyntax || row is ImplicitArrayCreationExpressionSyntax)
            {
                cells = GetChildNodesOfChildNodes(row);
            }
            else if (row is ImplicitObjectCreationExpressionSyntax implicitObjectCreation)
            {
                cells = GetChilds(implicitObjectCreation);
            }
            else if (row is ObjectCreationExpressionSyntax objectCreation)
            {
                cells = GetChilds(objectCreation);
            }
            else if (row is ExpressionElementSyntax expression)
            {
                var e = expression.Expression;
                if (e is TupleExpressionSyntax tupleExpression)
                {
                    cells = tupleExpression.Arguments;
                }
                else if (e is CollectionExpressionSyntax collection)
                {
                    cells = collection.Elements;
                }
                else if (e is BaseObjectCreationExpressionSyntax expressionBaseObjectCreation)
                {
                    cells = GetChilds(expressionBaseObjectCreation.ArgumentList, expressionBaseObjectCreation.Initializer?.Expressions);
                }
                else if (e is ImplicitArrayCreationExpressionSyntax implicitArray)
                {
                    cells = GetChildNodesOfChildNodes(implicitArray);
                }
            }
            else if (row is YieldStatementSyntax yieldSyntax)
            {
                cells = GetCells(yieldSyntax.Expression);
            }
            if (cells == null)
            {
                cells = row.ChildNodes();
            }
            return FilterOutRedundantNodes(cells);
        }

        private static IEnumerable<SyntaxNode> FilterOutRedundantNodes(IEnumerable<SyntaxNode> nodes)
            => nodes.Where(x => !(
                                    x.IsKind(SyntaxKind.ArrayRankSpecifier)
                                    || x.IsKind(SyntaxKind.PredefinedType)
                                ));

        private static IEnumerable<SyntaxNode> GetChildNodesOfChildNodes(SyntaxNode node) =>
            node.ChildNodes().SelectMany(x => FilterOutRedundantNodes(x.ChildNodes()));

        private static SyntaxNode MaxTriviaNode(SyntaxNode a, SyntaxNode b, SyntaxNode c, SyntaxNode d)
        {
            var max = MaxTriviaNode(a, b);
            var max2 = MaxTriviaNode(c, d);
            return MaxTriviaNode(max, max2);
        }

        private static SyntaxNode MaxTriviaNode(SyntaxNode a, SyntaxNode b)
        {
            var aLength = Utils.GetLeadingWhitespace(a).FullSpan.Length;
            var bLength = Utils.GetLeadingWhitespace(b).FullSpan.Length;
            return aLength >= bLength ? a : b;
        }
    }
}
