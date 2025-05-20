using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator
{
    partial class SourceGenerator
    {
        private abstract class AbstractInfoCollector
        {
            public abstract bool NeedStormPetrelTestMethods { get; }
            public bool IsTestMethodCollected { get; set; }
            public abstract void CollectClassDeclaration(ClassDeclarationSyntax classDeclaration);
            public abstract void CollectExpectedVarInvocationExpressionCandidate(MethodDeclarationSyntax methodDeclaration);
        }
    }
}
