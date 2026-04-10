using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;

namespace Scand.StormPetrel.Generator.Common.ExtraContextInternal
{
    internal class InvocationExpressionWithEmbeddedExpectedMemberAccessInternal : AbstractExtraContextInternal
    {
        public ExpressionSyntax ExpectedExpression { get; set; }
        public string ExpectedExpressionVariableName { get; set; }
        public InitializerContextKind? InitializerContextKind { get; set; }
        public string[] ExpectedVarInvocationPath { get; set; }
    }
}
