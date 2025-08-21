using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Shared;

namespace Scand.StormPetrel.Generator.Common
{
    internal static class SyntaxNodeHelperCommon
    {
        public static string[] GetValuePath(CSharpSyntaxNode node) => SyntaxNodeHelper.GetValuePath(node);
    }
}
