using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Scand.StormPetrel.Generator.AssertExpressionDetector
{
    internal abstract class ShouldBeAbstractDetector : AbstractDetector
    {
        protected abstract string[] SupportedMethodNames { get; }
        protected abstract string[] SupportedArgumentNames { get; }
        public override bool IsExpectedArgument(ArgumentSyntax argument, IdentifierNameSyntax actualIdentifier, out ExpressionSyntax actualExpression)
        {
            actualExpression = null;
            if (!IsAppropriateArgumentInTheList(argument, 0, SupportedArgumentNames, out var _))
            {
                return false;
            }
            var methodStatement = argument
                                    .Ancestors()
                                    .OfType<ExpressionStatementSyntax>()
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

            ExpressionStatementSyntax newStatement = null;
            if (expressionBeforeShould != null)
            {
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
            actualExpression = newStatement
                                .Expression
                                .WithoutLeadingTrivia()
                                .WithoutTrailingTrivia();
            return true;
        }

        protected abstract string GetBeIdentifier(SyntaxNode node, out ExpressionSyntax expressionBeforeShould);
    }
}
