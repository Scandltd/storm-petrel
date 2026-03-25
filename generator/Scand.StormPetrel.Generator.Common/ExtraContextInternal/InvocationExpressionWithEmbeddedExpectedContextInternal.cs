using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSource;
using System;

namespace Scand.StormPetrel.Generator.Common.ExtraContextInternal
{
    internal class InvocationExpressionWithEmbeddedExpectedContextInternal : AbstractExtraContextInternal
    {
        public ExpressionSyntax ExpectedExpression { get; set; }
        public MethodBodyStatementInfo MethodBodyStatementInfo { get; set; }
        public string[] Path { get; set; } = Array.Empty<string>();
    }
}
