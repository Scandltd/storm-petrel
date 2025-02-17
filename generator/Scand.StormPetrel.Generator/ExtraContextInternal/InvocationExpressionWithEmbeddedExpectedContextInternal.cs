using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;

namespace Scand.StormPetrel.Generator.ExtraContextInternal
{
    internal class InvocationExpressionWithEmbeddedExpectedContextInternal : AbstractExtraContextInternal<InvocationSourceContext>
    {
        public ExpressionSyntax ExpectedExpression { get; set; }
        public ExpressionSyntax ActualExpression { get; set; }
    }
}
