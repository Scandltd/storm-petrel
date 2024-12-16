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
        private int _currentRowIndex = -1;
        private bool _isRootStarted;
        private bool _isRootObjectCreation;
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
            return VisitMemberDeclaration(node, x => base.VisitMethodDeclaration(x));
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            return VisitMemberDeclaration(node, x => base.VisitPropertyDeclaration(x));
        }

        public override SyntaxNode VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            return VisitMemberDeclaration(node, x => base.VisitPropertyPatternClause(x));
        }

        private SyntaxNode VisitMemberDeclaration<T>(T node, Func<T, SyntaxNode> baseVisit) where T : CSharpSyntaxNode
        {
            if (IsMatchWithValuePath(node))
            {
                _isMatched = true;
            }
            var result = baseVisit(node);
            _isMatched = false;
            return result;
        }

        public override SyntaxNode VisitYieldStatement(YieldStatementSyntax node)
        {
            if (_isMatched)
            {
                _isRootStarted = true;
            }
            return base.VisitYieldStatement(node);
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
            VisitObjectCreationImplementation(node, node?.Initializer?.Expressions, base.VisitImplicitObjectCreationExpression);

        public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node) =>
            VisitObjectCreationImplementation(node, node?.Initializer?.Expressions, base.VisitObjectCreationExpression);

        private SyntaxNode VisitObjectCreationImplementation<T>(T node, IEnumerable<SyntaxNode> initializerExpressions, Func<T, SyntaxNode> baseVisit)
            where T : SyntaxNode
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (initializerExpressions == null)
            {
                return baseVisit(node);
            }
            if (!_isMatched || _isRootStarted || _isRootObjectCreation)
            {
                return baseVisit(node);
            }
            _isRootObjectCreation = true;
            var row = initializerExpressions
                        .Skip(_resultRowIndex)
                        .FirstOrDefault();
            if (row == null)
            {
                return baseVisit(node);
            }
            var cell = row
                        .ChildNodes()
                        .Skip(_resultColumnIndex)
                        .FirstOrDefault();
            if (cell == null)
            {
                return baseVisit(node);
            }
            var maxTriviaDonor = MaxTriviaNode(row, cell, initializerExpressions.First());
            var initExpression = CreateInitializeExpressionSyntax(maxTriviaDonor);
            initExpression = initExpression
                                .WithLeadingTrivia(cell.GetLeadingTrivia())
                                .WithTrailingTrivia(cell.GetTrailingTrivia());
            return node.ReplaceNode(cell, initExpression);
        }

        private SyntaxNode VisitImplementation<T>(T node, IEnumerable<SyntaxNode> nodeArrayElement, Func<T, SyntaxNode> baseVisit)
            where T : SyntaxNode
        {
            if (!_isMatched || _isRootObjectCreation)
            {
                return baseVisit(node);
            }
            if (!_isRootStarted)
            {
                _isRootStarted = true;
                return baseVisit(node);
            }
            _currentRowIndex++;
            if (_currentRowIndex != _resultRowIndex)
            {
                return baseVisit(node);
            }
            var nodeArrayElementFiltered = nodeArrayElement
                                            .Where(x => !(
                                                            x.IsKind(SyntaxKind.ArrayRankSpecifier)
                                                            || x.IsKind(SyntaxKind.PredefinedType)
                                                        ));
            var toReplace = nodeArrayElementFiltered
                                .Skip(_resultColumnIndex)
                                .FirstOrDefault();
            if (toReplace == null)
            {
                return baseVisit(node);
            }
            var maxTriviaDonor = MaxTriviaNode(toReplace, toReplace.Parent, nodeArrayElementFiltered.First());
            var initExpression = CreateInitializeExpressionSyntax(maxTriviaDonor);
            initExpression = initExpression
                                .WithLeadingTrivia(toReplace.GetLeadingTrivia())
                                .WithTrailingTrivia(toReplace.GetTrailingTrivia());
            SyntaxNode newNode;
            if (toReplace is ExpressionSyntax)
            {
                newNode = initExpression;
            }
            else if (toReplace is ExpressionElementSyntax)
            {
                newNode = SyntaxFactory.ExpressionElement(initExpression);
            }
            else if (toReplace is ArgumentSyntax)
            {
                newNode = SyntaxFactory.Argument(initExpression);
            }
            else
            {
                return baseVisit(node);
            }
            return node.ReplaceNode(toReplace, newNode);
        }

        private static IEnumerable<SyntaxNode> GetChildNodesOfChildNodes(SyntaxNode node) =>
            node.ChildNodes().SelectMany(x => x.ChildNodes());

        private static SyntaxNode MaxTriviaNode(SyntaxNode a, SyntaxNode b, SyntaxNode c)
        {
            var max = MaxTriviaNode(a, b);
            return MaxTriviaNode(max, c);
        }

        private static SyntaxNode MaxTriviaNode(SyntaxNode a, SyntaxNode b)
        {
            var aLength = Utils.GetLeadingWhitespace(a).FullSpan.Length;
            var bLength = Utils.GetLeadingWhitespace(b).FullSpan.Length;
            return aLength >= bLength ? a : b;
        }
    }
}
