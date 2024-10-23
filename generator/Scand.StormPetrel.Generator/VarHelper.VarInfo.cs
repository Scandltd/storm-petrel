using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.TargetProject;

namespace Scand.StormPetrel.Generator
{
    partial class VarHelper
    {
        private class VarInfo
        {
            public int StatementIndex { get; set; }
            public VarParameterInfo ExpectedVarParameterInfo { get; set; }
            public RewriterKind RewriterKind { get; set; }
            public string[] Path { get; set; }
            public string InvocationExpression { get; set; }
            public ArgumentListSyntax InvocationExpressionArgs { get; set; }
            public VarParameterTestCaseSourceInfo ExpectedVarParameterTestCaseSourceInfo { get; set; }
        }
    }
}
