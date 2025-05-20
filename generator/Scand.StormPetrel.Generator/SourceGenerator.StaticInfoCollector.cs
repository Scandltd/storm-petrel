using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Shared;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    partial class SourceGenerator
    {
        private class StaticInfoCollector : AbstractInfoCollector
        {
            public readonly List<string[]> CollectedPropertyPaths;
            public readonly List<(string[] Path, int ParametersCount)> CollectedMethodInfo;
            public StaticInfoCollector()
            {
                CollectedPropertyPaths = new List<string[]>();
                CollectedMethodInfo = new List<(string[], int)>();
            }
            public override bool NeedStormPetrelTestMethods => false;

            public override void CollectClassDeclaration(ClassDeclarationSyntax classDeclaration)
            {
                var properties = MethodHelper.GetExpectedVarPropertyInvocationExpressions(classDeclaration);
                CollectedPropertyPaths.AddRange(properties.Select(x => SyntaxNodeHelper.GetValuePath(x)));
            }

            public override void CollectExpectedVarInvocationExpressionCandidate(MethodDeclarationSyntax methodDeclaration)
            {
                CollectedMethodInfo.Add((SyntaxNodeHelper.GetValuePath(methodDeclaration), methodDeclaration.ParameterList.Parameters.Count));
            }
        }
    }
}
