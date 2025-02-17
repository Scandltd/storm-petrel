using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            var parent = argument.Parent;
            while (parent != null && !(parent is MethodDeclarationSyntax))
            {
                if (parent is InvocationExpressionSyntax invocationExpression
                        && invocationExpression.Expression is MemberAccessExpressionSyntax memberAccess
                        && SupportedMethodNames.Contains(memberAccess.Name.Identifier.Text)
                        && IsAppropriateArgumentInTheList(argument, 0, SupportedArgumentNames, out var _))
                {
                    if (invocationExpression
                                    .DescendantNodes()
                                    .Where(x => (x is MemberAccessExpressionSyntax tmpMemberAccess) && tmpMemberAccess.Name.Identifier.Text == "Should")
                                    .FirstOrDefault() is MemberAccessExpressionSyntax should)
                    {
                        actualExpression = should
                                            .Expression
                                            .WithoutLeadingTrivia()
                                            .WithoutTrailingTrivia();
                        return true;
                    }
                }
                parent = parent.Parent;
            }
            return false;
        }
    }
}
