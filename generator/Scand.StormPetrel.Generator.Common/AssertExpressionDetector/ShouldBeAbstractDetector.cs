using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Scand.StormPetrel.Generator.Common.AssertExpressionDetector
{
    internal abstract class ShouldBeAbstractDetector : AbstractDetector
    {
        protected abstract string[] SupportedMethodNames { get; }
        protected abstract string[] SupportedArgumentNames { get; }
        public override bool IsExpectedArgument(ArgumentSyntax argument, out ExpressionSyntax actualExpression)
        {
            actualExpression = null;
            if (!IsAppropriateArgumentInTheList(argument, 0, SupportedArgumentNames, out var _))
            {
                return false;
            }
            var methodStatement = argument
                                    .Ancestors()
                                    .Where(x => x.IsKind(SyntaxKind.ExpressionStatement) || x.IsKind(SyntaxKind.ArrowExpressionClause))
                                    .FirstOrDefault();
            if (methodStatement == null)
            {
                return false;
            }

            ExpressionSyntax expressionBeforeShould = null;
            var shouldBeInvocation = methodStatement
                                        .DescendantNodes()
                                        .FirstOrDefault(x => SupportedMethodNames.Contains(GetBeIdentifier(x, out expressionBeforeShould)));
            if (shouldBeInvocation == null)
            {
                return false;
            }

            SyntaxNode newStatement = null;
            if (expressionBeforeShould != null)
            {
                var isBeforeShouldArg = expressionBeforeShould
                                            .DescendantNodes()
                                            .OfType<ArgumentSyntax>()
                                            .Any(x => x == argument);
                if (isBeforeShouldArg)
                {
                    return false;
                }
                newStatement = methodStatement.ReplaceNode(shouldBeInvocation, expressionBeforeShould);
            }
            else if (shouldBeInvocation.Parent is ConditionalAccessExpressionSyntax conditional)
            {
                newStatement = methodStatement.ReplaceNode(conditional, conditional.Expression);
            }
            if (newStatement == null)
            {
                return false;
            }

            if (newStatement is ArrowExpressionClauseSyntax arrowExpressionStatement)
            {
                actualExpression = arrowExpressionStatement.Expression;
            }
            else if (newStatement is ExpressionStatementSyntax expressionStatement)
            {
                actualExpression = expressionStatement.Expression;
            }
            else
            {
                throw new InvalidOperationException($"This case should not be possible due to {nameof(methodStatement)} kind filtering above");
            }
            actualExpression = actualExpression
                                .WithoutLeadingTrivia()
                                .WithoutTrailingTrivia();
            return true;
        }

        protected abstract string GetBeIdentifier(SyntaxNode node, out ExpressionSyntax expressionBeforeShould);
    }
}
