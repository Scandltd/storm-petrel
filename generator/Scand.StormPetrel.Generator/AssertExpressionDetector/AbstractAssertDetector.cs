using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Scand.StormPetrel.Generator.AssertExpressionDetector
{
    internal abstract class AbstractAssertDetector : AbstractDetector
    {
        protected abstract string[] SupportedExpectedArgumentNames { get; }
        protected abstract string[] SupportedMethodNames { get; }
        protected abstract int ActualArgumentIndex { get; }
        protected abstract int ExpectedArgumentIndex { get; }
        public override bool IsExpectedArgument(ArgumentSyntax argument, out ExpressionSyntax actualExpression)
        {
            actualExpression = null;
            var parent = argument.Parent;
            while (parent != null && !(parent is MethodDeclarationSyntax))
            {
                if (parent is InvocationExpressionSyntax invocationExpression
                        && invocationExpression.Expression is MemberAccessExpressionSyntax memberAccess
                        && SupportedMethodNames.Contains(memberAccess.Name.Identifier.Text)
                        && IsAppropriateArgumentInTheList(argument, ExpectedArgumentIndex, SupportedExpectedArgumentNames, out ArgumentListSyntax argumentList))
                {
                    var arguments = argumentList.Arguments;
                    if (arguments.Count < 2)
                    {
                        parent = parent.Parent;
                        continue;
                    }
                    var actualCandidate = arguments
                                            .FirstOrDefault(a => a.NameColon?.Name?.Identifier.Text == "actual")
                                                ?? arguments[ActualArgumentIndex];

                    actualExpression = actualCandidate
                                        .Expression
                                        .WithoutLeadingTrivia()
                                        .WithoutTrailingTrivia();
                    return true;
                }

                parent = parent.Parent;
            }

            return false;
        }
    }
}
