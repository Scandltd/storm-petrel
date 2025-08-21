using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Scand.StormPetrel.Generator.Common
{
    internal abstract class InvocationExpressionHelper<T> where T : SyntaxNode
    {
        public T ToStormPetrelNode(T syntaxNode, CancellationToken cancellationToken)
        {
            var nodesWithDescendantSwitchExpressionArm = new HashSet<SyntaxNode>();
            foreach (var node in syntaxNode.DescendantNodes())
            {
                if (node is SwitchExpressionArmSyntax)
                {
                    nodesWithDescendantSwitchExpressionArm.UnionWith(node.Ancestors());
                }
            }
            var newName = SyntaxFactory.Identifier(GetNodeIdentifierText(syntaxNode) + "StormPetrel");
            var nodeToNewNode = new Dictionary<SyntaxNode, SyntaxNode>();
            var kindToSkipCount = new Dictionary<SyntaxKind, int>();
            var _ = syntaxNode.DescendantNodes(node =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
                SyntaxNode newNode = null;
                if (node is ReturnStatementSyntax @return
                        && !nodesWithDescendantSwitchExpressionArm.Contains(@return))
                {
                    newNode = @return.WithExpression(NewExpression(@return));
                }
                else if (node is ArrowExpressionClauseSyntax arrow
                            && !nodesWithDescendantSwitchExpressionArm.Contains(arrow))
                {
                    var arrowBody = GetNodeExpressionBody(syntaxNode);
                    if (arrowBody != null)
                    {
                        if (!(arrowBody.Expression is ThrowExpressionSyntax))
                        {
                            newNode = SyntaxFactory.ArrowExpressionClause(NewExpression(arrow));
                        }
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }
                }
                else if (node is SwitchExpressionArmSyntax @switch
                            && !nodesWithDescendantSwitchExpressionArm.Contains(@switch))
                {
                    newNode = @switch.WithExpression(NewExpression(@switch));
                }
                if (newNode != null)
                {
                    nodeToNewNode.Add(node, newNode);
                }
                return newNode == null;
            }).Count();

            if (nodeToNewNode.Count != 0)
            {
                var newSyntaxNode = syntaxNode
                                        .ReplaceNodes(nodeToNewNode.Keys, (x, y) => nodeToNewNode[x])
                                        .WithoutTrivia();
                return AdjustNewNode(newSyntaxNode, newName);
            }

            return null;

            ExpressionSyntax NewExpression(SyntaxNode node)
            {
                var nodeKind = node.Kind();
                if (!kindToSkipCount.TryGetValue(nodeKind, out var skipCount))
                {
                    skipCount = -1;
                }
                skipCount++;
                kindToSkipCount[nodeKind] = skipCount;
                var tupleExpressionArgs = SyntaxFactory.SeparatedList<ArgumentSyntax>(
                                            new SyntaxNodeOrToken[]
                                            {
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal((int) nodeKind))),
                                                SyntaxFactory.Token(SyntaxKind.CommaToken),
                                                SyntaxFactory.Argument(
                                                    SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(skipCount))),
                                            });
                var tupleExpression = SyntaxFactory.TupleExpression(tupleExpressionArgs);
                return tupleExpression;
            }

        }

        protected abstract string GetNodeIdentifierText(T newNode);
        protected abstract ArrowExpressionClauseSyntax GetNodeExpressionBody(T node);
        protected abstract T AdjustNewNode(T newNode, SyntaxToken newNodeName);

        protected static TypeSyntax CreateNamedTupleType()
        {
            var tupleElements = new[]
            {
                SyntaxFactory
                    .TupleElement(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)))
                    .WithIdentifier(SyntaxFactory.Identifier("NodeKind")),
                SyntaxFactory
                    .TupleElement(SyntaxFactory.PredefinedType(SyntaxFactory.Token(SyntaxKind.IntKeyword)))
                    .WithIdentifier(SyntaxFactory.Identifier("NodeIndex")),
            };
            var tupleTypeSyntax = SyntaxFactory.TupleType(SyntaxFactory.SeparatedList(tupleElements));
            return tupleTypeSyntax;
        }
    }
}
