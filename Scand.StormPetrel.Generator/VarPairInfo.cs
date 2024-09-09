using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.TargetProject;

namespace Scand.StormPetrel.Generator
{
    internal class VarPairInfo
    {
        public string ActualVarName { get; set; }
        public string[] ActualVarPath { get; set; }
        public string ExpectedVarName { get; set; }
        public string[] ExpectedVarPath { get; set; }
        public string[] ExpectedVarInvocationExpressionPath { get; set; }
        public string ExpectedVarInvocationExpressionStormPetrel { get; set; }
        public ArgumentListSyntax ExpectedVariableInvocationExpressionArgs { get; set; }
        public int StatementIndex { get; set; }
        public VarParameterInfo ExpectedVarParameterInfo { get; set; }
        public RewriterKind RewriterKind { get; set; }
    }
}
