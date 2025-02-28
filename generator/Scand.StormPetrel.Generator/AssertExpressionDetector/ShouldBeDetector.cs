using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;

namespace Scand.StormPetrel.Generator.AssertExpressionDetector
{
    internal class ShouldBeDetector : AbstractDetector
    {
        private static readonly string[] SupportedMethodNames = new[]
        {
            "Be",
            "BeEquivalentTo",
        };
        private static readonly string[] SupportedArgumentNames = new[]
        {
            "expected", //for Be
            "expectation", //for BeEquivalentTo
        };
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

            MemberAccessExpressionSyntax shouldBeMemberAccess = null;
            var shouldBeInvocation = methodStatement
                                        .DescendantNodes()
                                        .FirstOrDefault(x =>
                                            HasMemberAccess(x, out shouldBeMemberAccess)
                                                && SupportedMethodNames.Contains(shouldBeMemberAccess.Name.Identifier.Text)
                                                && shouldBeMemberAccess.Expression is InvocationExpressionSyntax tempShouldInvocation
                                                && GetName(tempShouldInvocation)?.Identifier.Text == "Should");
            if (shouldBeInvocation == null)
            {
                return false;
            }

            var expressionBeforeShould = shouldBeMemberAccess.Expression is InvocationExpressionSyntax shouldInvocation
                                            ? shouldInvocation.Expression is MemberAccessExpressionSyntax memberAccess
                                                ? memberAccess.Expression
                                                : null
                                            : null;

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

        private static bool HasMemberAccess(SyntaxNode node, out MemberAccessExpressionSyntax memberAccess)
        {
            memberAccess = null;
            if (node is InvocationExpressionSyntax invocation
                    && invocation.Expression is MemberAccessExpressionSyntax tmpMemberAccess)
            {
                memberAccess = tmpMemberAccess;
                return true;
            }
            return false;
        }

        private static SimpleNameSyntax GetName(InvocationExpressionSyntax invocation)
        {
            if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)
            {
                return memberAccess.Name;
            }
            else if (invocation.Expression is MemberBindingExpressionSyntax memberBinding)
            {
                return memberBinding.Name;
            }
            return null;
        }
    }
}
