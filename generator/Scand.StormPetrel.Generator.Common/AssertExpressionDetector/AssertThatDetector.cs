using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Scand.StormPetrel.Generator.Common.AssertExpressionDetector
{
    internal class AssertThatDetector : AbstractAssertDetector
    {
        private static readonly string[] SupportedExpectedArgumentNamesStatic = new[]
        {
            "expression",
        };
        private static readonly string[] SupportedMethodNamesStatic = new[]
        {
            "That",
        };
        protected override string[] SupportedMethodNames => SupportedMethodNamesStatic;
        protected override int ActualArgumentIndex => 0;
        protected override int ExpectedArgumentIndex => 1;
        protected override string[] SupportedExpectedArgumentNames => SupportedExpectedArgumentNamesStatic;
        private static readonly string[] SupportedIsMethodNames = new[]
        {
            "EqualTo",
            "EquivalentTo"
        };
        private static readonly string[] SupportedTypeNames = new[]
        {
            "EqualConstraint",
            "CollectionEquivalentConstraint"
        };
        protected override bool IsAppropriateArgumentInTheList(ArgumentSyntax argument, int argumentIndex, string[] supportedArgumentNames, out ArgumentListSyntax argumentList)
        {
            argumentList = null;
            SyntaxNode parent = argument.Parent;
            while (parent != null)
            {
                if (parent is ArgumentSyntax parentArgument)
                {
                    bool isEqualToRegular = parentArgument.Expression is InvocationExpressionSyntax invocationExpression
                                                && invocationExpression.Expression is MemberAccessExpressionSyntax memberAccessExpression
                                                && memberAccessExpression.Expression is IdentifierNameSyntax name
                                                && name.Identifier.Text == "Is"
                                                    && SupportedIsMethodNames.Contains(memberAccessExpression.Name.Identifier.Text);
                    bool isEqualToViaObjectCreation = parentArgument.Expression is ObjectCreationExpressionSyntax expressionSyntax
                                                        && expressionSyntax.Type is IdentifierNameSyntax syntaxType
                                                        && SupportedTypeNames.Contains(syntaxType.Identifier.Text)
                                                        && expressionSyntax.ArgumentList.Arguments.Count == 1;
                    if (isEqualToRegular || isEqualToViaObjectCreation)
                    {
                        return base.IsAppropriateArgumentInTheList(parentArgument, argumentIndex, supportedArgumentNames, out argumentList);
                    }
                    break;
                }
                parent = parent.Parent;
            }
            return false;
        }
    }
}