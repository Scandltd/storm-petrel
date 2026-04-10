using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public abstract class AbstractValueRewriter : AbstractRewriter
    {
        private protected readonly string[] _valuePath;
        private protected readonly string _valueNewCode;
        private protected readonly string[] _invocationPath;

        private protected AbstractValueRewriter(IEnumerable<string> valuePath, string valueNewCode, IEnumerable<string> invocationPath)
        {
            var valuePathArray = valuePath?.ToArray();
            if (valuePathArray == null || valuePathArray.Length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(valuePath), "Must have at least 1 element");
            }
            if (invocationPath == null)
            {
                throw new ArgumentNullException(nameof(invocationPath));
            }
            _valuePath = valuePathArray;
            _valueNewCode = valueNewCode;
            _invocationPath = invocationPath.ToArray();
        }

        private protected ExpressionSyntax CreateInitializeExpressionSyntax(SyntaxNode leadingTriviaDonor)
            => Utils.CreateInitializeExpressionSyntax(_valueNewCode, leadingTriviaDonor);

        private protected ExpressionSyntax CreateInitializeExpressionSyntax(SyntaxTrivia leadingTrivia)
            => Utils.CreateInitializeExpressionSyntax(_valueNewCode, leadingTrivia);

        private protected bool IsMatchWithValuePath(CSharpSyntaxNode node)
            => SyntaxNodeHelper.IsMatchWithValuePath(node, _valuePath);

        private ArrowExpressionClauseSyntax GetExpressionBody(SyntaxNode node, ArrowExpressionClauseSyntax expressionBody)
        {
            var newExpression = CreateInitializeExpressionSyntax(node);
            var newExpressionWithTrivia = newExpression.WithTriviaFrom(expressionBody.Expression);
            var arrowExpression = SyntaxFactory.ArrowExpressionClause(expressionBody.ArrowToken, newExpressionWithTrivia);
            return arrowExpression.WithTriviaFrom(expressionBody);
        }

        private protected SyntaxNode WithExpressionBody(MethodDeclarationSyntax node)
        {
            if (_invocationPath.Length == 0)
            {
                var newBody = GetExpressionBody(node, node.ExpressionBody);
                return node.WithExpressionBody(newBody);
            }
            return WithInvocationPathHandling(node, node.ExpressionBody);
        }

        private protected PropertyDeclarationSyntax WithExpressionBody(PropertyDeclarationSyntax node, AccessorDeclarationSyntax nodeGetAccessor)
        {
            AccessorDeclarationSyntax newAccessor;
            if (_invocationPath.Length == 0)
            {
                var newBody = GetExpressionBody(nodeGetAccessor, nodeGetAccessor.ExpressionBody);
                newAccessor = nodeGetAccessor.WithExpressionBody(newBody);
            }
            else
            {
                newAccessor = WithInvocationPathHandling(nodeGetAccessor, nodeGetAccessor.ExpressionBody);
            }
            return nodeGetAccessor == newAccessor ? node : node.ReplaceNode(nodeGetAccessor, newAccessor);
        }

        private protected SyntaxNode WithExpressionBody(PropertyDeclarationSyntax node)
        {
            if (node.ExpressionBody == null)
            {
                if (_invocationPath.Length != 0)
                {
                    return node;
                }
                var newExpression = CreateInitializeExpressionSyntax(node)
                                        .WithoutTrailingTrivia();
                var equalsToken = SyntaxFactory
                                    .Token(SyntaxKind.EqualsToken)
                                    .WithLeadingTrivia(SyntaxFactory.Whitespace(" "))
                                    .WithTrailingTrivia(SyntaxFactory.Whitespace(" "));
                var equalsValue = SyntaxFactory
                                    .EqualsValueClause(equalsToken, newExpression);
                return node
                        .WithoutTrailingTrivia()
                        .WithInitializer(equalsValue)
                        .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        .WithTrailingTrivia(node.GetTrailingTrivia());
            }
            if (_invocationPath.Length == 0)
            {
                var newBody = GetExpressionBody(node, node.ExpressionBody);
                return node.WithExpressionBody(newBody);
            }
            return WithInvocationPathHandling(node, node.ExpressionBody);
        }

        private protected SyntaxNode WithRight(AssignmentExpressionSyntax node)
        {
            if (_invocationPath.Length == 0)
            {
                var newExpression = CreateInitializeExpressionSyntax(node);
                return node.WithRight(newExpression);
            }
            return WithInvocationPathHandling(node, node.Right);
        }
        private protected SyntaxNode WithInitializer(LocalDeclarationStatementSyntax node)
        {
            var declarator = node.Declaration.Variables.First();
            if (_invocationPath.Length == 0)
            {
                var newExpression = CreateInitializeExpressionSyntax(declarator.Parent);
                var newInitializer = declarator.Initializer.WithValue(newExpression);
                var newNode = node.ReplaceNode(declarator.Initializer, newInitializer);
                return newNode;
            }
            return WithInvocationPathHandling(node, declarator.Initializer);
        }

        private protected AssignmentExpressionSyntax GetAssignmentByInvocationPath(SyntaxNode node)
        {
            var current = node;
            AssignmentExpressionSyntax lastAssignment = null;
            int assignmentsCount = 0;
            foreach (var segment in _invocationPath)
            {
                if (current == null)
                {
                    return null;
                }
                var prevAssignment = lastAssignment;
                while (true)
                {
                    var prevCurrent = current;
                    if (TryExtractAppropriateChild(current, ref current))
                    {
                        continue;
                    }
                    foreach (var child in current.ChildNodes())
                    {
                        if (TryExtractAppropriateChild(child, ref current))
                        {
                            break;
                        }
                        if (child is AssignmentExpressionSyntax a
                                && a.Left is IdentifierNameSyntax identifier
                                && identifier.Identifier.ValueText == segment)
                        {
                            lastAssignment = a;
                            assignmentsCount++;
                            if (_invocationPath.Length == assignmentsCount)
                            {
                                break;
                            }
                            var _ = TryExtractAppropriateChild(a.Right, ref current);
                            break;
                        }
                    }
                    if (prevCurrent == current)
                    {
                        break;
                    }
                }
                if (prevAssignment == lastAssignment)
                {
                    break;
                }
            }
            return _invocationPath.Length == assignmentsCount ? lastAssignment : null;

            bool TryExtractAppropriateChild(SyntaxNode nd, ref SyntaxNode outNode)
            {
                var originalOutNode = outNode;
                if (nd is ImplicitObjectCreationExpressionSyntax objImplicit)
                {
                    outNode = objImplicit.Initializer;
                }
                else if (nd is ObjectCreationExpressionSyntax obj)
                {
                    outNode = obj.Initializer;
                }
                return originalOutNode != outNode;
            }
        }
        private protected T WithInvocationPathHandling<T>(T node, SyntaxNode assignmentRootNode) where T : SyntaxNode
            => WithInvocationPathHandling(node, Enumerable.Repeat(assignmentRootNode, 1));
        private protected T WithInvocationPathHandling<T>(T node, IEnumerable<SyntaxNode> assignmentRootNodes) where T : SyntaxNode
        {
            if (_invocationPath.Length == 0)
            {
                throw new InvalidOperationException("Invocation path must have at least 1 element");
            }
            var lastAssignmentToNewInitializer = assignmentRootNodes
                .Select(x => GetAssignmentByInvocationPath(x))
                .Where(x => x?.Right != null)
                .ToDictionary(x => x.Right, x => CreateInitializeExpressionSyntax(x));
            if (lastAssignmentToNewInitializer.Count == 0)
            {
                return node;
            }
            return node.ReplaceNodes(lastAssignmentToNewInitializer.Keys, (x, _) => lastAssignmentToNewInitializer[x]);
        }
    }
}
