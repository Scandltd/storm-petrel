using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils
{
    /// <summary>
    /// Decorates input <see cref="IGeneratorDumper"/> with <see cref="CollectionExpressionSyntax"/> nodes
    /// where possible.
    /// </summary>
    public sealed class CollectionExpressionDumperDecorator : AbstractDumperDecorator
    {
        public CollectionExpressionDumperDecorator(IGeneratorDumper dumper) : base(dumper)
        {
        }
        private protected override SyntaxNode DumpImplementation(SyntaxNode node)
            => DecorateByCollectionExpression(node);

        public static IEnumerable<string> DefaultFullSupportCollections = new string[]
        {
            "List",
            "HashSet",
            "StringCollection",
        };

        public static IEnumerable<string> DefaultPartialSupportCollections = new string[]
        {
            "Stack",
            "NameValueCollection",
            "Dictionary",
            "ConcurrentDictionary",
            "Hashtable",
            "Queue",
            "ArrayList",
            "LinkedList",
            "SortedList",
            "ConcurrentQueue",
            "ConcurrentStack",
            "BlockingCollection",
        };

        /// <summary>
        /// Decorates input <paramref name="node"/> with <see cref="CollectionExpressionSyntax"/> nodes
        /// where possible.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="fullSupportCollections"></param>
        /// <param name="partialSupportCollections"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static SyntaxNode DecorateByCollectionExpression(
            SyntaxNode node,
            IEnumerable<string> fullSupportCollections = null,
            IEnumerable<string> partialSupportCollections = null)
        {
            var initializerNodes = node
                                        .DescendantNodes()
                                        .Where(x => x.IsKind(SyntaxKind.ArrayCreationExpression)
                                                        || x.IsKind(SyntaxKind.ImplicitArrayCreationExpression)
                                             );

            node = node.ReplaceNodes(initializerNodes, (_, nodeNew) =>
            {
                CollectionExpressionSyntax collectionExpression = null;
                if (nodeNew.IsKind(SyntaxKind.ArrayCreationExpression))
                {
                    collectionExpression = GetCollectionExpression<ArrayCreationExpressionSyntax>(nodeNew, x => x.Initializer);
                }
                else if (nodeNew.IsKind(SyntaxKind.ImplicitArrayCreationExpression))
                {
                    collectionExpression = GetCollectionExpression<ImplicitArrayCreationExpressionSyntax>(nodeNew, x => x.Initializer);
                }
                else
                {
                    throw new InvalidOperationException(nodeNew.Kind().ToString());
                }
                return collectionExpression;
            });

            if (fullSupportCollections == null)
            {
                fullSupportCollections = DefaultFullSupportCollections;
            }

            if (partialSupportCollections == null)
            {
                partialSupportCollections = DefaultPartialSupportCollections;
            }

            var initializers = node
                                .DescendantNodes()
                                .OfType<ObjectCreationExpressionSyntax>()
                                .Where(x => x.ArgumentList == null || x.ArgumentList.Arguments.Count == 0)
                                .Select(x => x.Type)
                                .OfType<SimpleNameSyntax>()
                                .Where(x => x.Identifier != null
                                            && (fullSupportCollections.Contains(x.Identifier.ValueText)
                                                    || (((ObjectCreationExpressionSyntax)x.Parent).Initializer == null
                                                            && partialSupportCollections.Contains(x.Identifier.ValueText))))
                                .Select(x => x.Parent);

            node = node.ReplaceNodes(initializers, (_, nodeNew) =>
            {
                var collectionExpression = GetCollectionExpression<ObjectCreationExpressionSyntax>(nodeNew, x => x.Initializer);

                var genericNameTriviaList = nodeNew
                                                .ChildNodes()
                                                .Where(x => !x.IsKind(SyntaxKind.CollectionInitializerExpression))
                                                .Select(x => x.GetTrailingTrivia())
                                                .LastOrDefault();

                if (genericNameTriviaList != null)
                {
                    var triviaList = new List<SyntaxTrivia>();
                    triviaList.AddRange(genericNameTriviaList.Where(x => !x.IsKind(SyntaxKind.WhitespaceTrivia)));
                    triviaList.AddRange(collectionExpression.GetLeadingTrivia());
                    return collectionExpression.WithLeadingTrivia(triviaList);
                }

                return collectionExpression;
            });

            var operatorTokens = node
                                .DescendantNodes()
                                .OfType<CollectionExpressionSyntax>()
                                .Where(c => c.HasLeadingTrivia && c.GetLeadingTrivia()
                                                                   .Any(t => t.IsKind(SyntaxKind.EndOfLineTrivia))
                                                                          && c.Parent.IsKind(SyntaxKind.SimpleAssignmentExpression))
                                .Select(x => ((AssignmentExpressionSyntax)x.Parent).OperatorToken);

            node = node.ReplaceTokens(operatorTokens, (_, token) =>
            {
                return token.WithTrailingTrivia();
            });

            return node;
        }

        private static CollectionExpressionSyntax GetCollectionExpression<T>(SyntaxNode initializerExpression, Func<T, InitializerExpressionSyntax> callback) where T : ExpressionSyntax
        {
            var initializer = callback((T)initializerExpression);
            if (initializer == null)
            {
                return SyntaxFactory.CollectionExpression().WithTriviaFrom(initializerExpression);
            }
            var expressions = initializer.Expressions;
            var elements = expressions
                                .Select(expr => (CollectionElementSyntax)SyntaxFactory.ExpressionElement(expr));
            var separators = expressions
                                .GetSeparators();
            var openBracket = SyntaxFactory
                                .Token(SyntaxKind.OpenBracketToken)
                                .WithLeadingTrivia(initializer.OpenBraceToken.LeadingTrivia)
                                .WithTrailingTrivia(initializer.OpenBraceToken.TrailingTrivia);
            var closeBracket = SyntaxFactory
                                .Token(SyntaxKind.CloseBracketToken)
                                .WithLeadingTrivia(initializer.CloseBraceToken.LeadingTrivia)
                                .WithTrailingTrivia(initializer.CloseBraceToken.TrailingTrivia);
            var separatedList = SyntaxFactory.SeparatedList(elements, separators);
            return SyntaxFactory.CollectionExpression(openBracket, separatedList, closeBracket);
        }
    }
}
