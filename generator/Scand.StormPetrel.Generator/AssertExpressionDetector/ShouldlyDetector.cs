using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator.AssertExpressionDetector
{
    /// <summary>
    /// For Shouldly, <see cref="https://docs.shouldly.org/"/>.
    /// </summary>
    internal class ShouldlyDetector : ShouldBeAbstractDetector
    {
        private static readonly string[] SupportedMethodNamesStatic = new[]
        {
            "ShouldBe"
        };
        private static readonly string[] SupportedArgumentNamesStatic = new[]
        {
            "expected"
        };
        protected override string[] SupportedMethodNames => SupportedMethodNamesStatic;
        protected override string[] SupportedArgumentNames => SupportedArgumentNamesStatic;
        protected override string GetBeIdentifier(SyntaxNode node, out ExpressionSyntax expressionBeforeShould)
        {
            expressionBeforeShould = null;
            if (node is InvocationExpressionSyntax invocation)
            {
                if (invocation.Expression is MemberAccessExpressionSyntax memberAccess)
                {
                    expressionBeforeShould = memberAccess.Expression;
                    return memberAccess.Name.Identifier.Text;
                }
                else if (invocation.Expression is ConditionalAccessExpressionSyntax conditionalAccess
                         && conditionalAccess.WhenNotNull is MemberAccessExpressionSyntax conditionalMemberAccess)
                {
                    expressionBeforeShould = conditionalMemberAccess.Expression;
                    return conditionalMemberAccess.Name.Identifier.Text;
                }
            }
            else if (node is ConditionalAccessExpressionSyntax conditionalNode
                     && conditionalNode.WhenNotNull is InvocationExpressionSyntax conditionalInvocation
                     && conditionalInvocation.Expression is MemberBindingExpressionSyntax memberBinding)
            {
                expressionBeforeShould = conditionalNode.Expression;
                return memberBinding.Name.Identifier.Text;
            }
            return null;
        }
    }
}
