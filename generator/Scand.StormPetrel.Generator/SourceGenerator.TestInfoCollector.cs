using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator
{
    partial class SourceGenerator
    {
        private class TestInfoCollector : AbstractInfoCollector
        {
            public override bool NeedStormPetrelTestMethods => true;
            public override void CollectClassDeclaration(ClassDeclarationSyntax classDeclaration)
            {
                //Nothing to collect
            }
            public override void CollectExpectedVarInvocationExpressionCandidate(MethodDeclarationSyntax methodDeclaration)
            {
                //Nothing to collect
            }
        }
    }
}
