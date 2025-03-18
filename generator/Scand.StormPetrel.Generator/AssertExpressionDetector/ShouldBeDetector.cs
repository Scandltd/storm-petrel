using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator.AssertExpressionDetector
{
    /// <summary>
    /// For FluentAssertions, <see cref="https://fluentassertions.com/"/>.
    /// </summary>
    internal class ShouldBeDetector : ShouldBeAbstractDetector
    {
        private static readonly string[] SupportedMethodNamesStatic = new[]
        {
            "Be",
            "BeEquivalentTo",
        };
        private static readonly string[] SupportedArgumentNamesStatic = new[]
        {
            "expected", //for Be
            "expectation", //for BeEquivalentTo
        };
        protected override string[] SupportedMethodNames => SupportedMethodNamesStatic;
        protected override string[] SupportedArgumentNames => SupportedArgumentNamesStatic;
        protected override string GetBeIdentifier(SyntaxNode node, out ExpressionSyntax expressionBeforeShould)
        {
            expressionBeforeShould = null;
            if (node is InvocationExpressionSyntax invocation
                    && invocation.Expression is MemberAccessExpressionSyntax memberAccess
                    && memberAccess.Expression is InvocationExpressionSyntax shouldInvocation
                    && GetName(shouldInvocation)?.Identifier.Text == "Should")
            {
                expressionBeforeShould = shouldInvocation.Expression is MemberAccessExpressionSyntax tempMemberAccess
                                            ? tempMemberAccess.Expression
                                            : null;
                return memberAccess.Name.Identifier.Text;
            }
            return null;
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
