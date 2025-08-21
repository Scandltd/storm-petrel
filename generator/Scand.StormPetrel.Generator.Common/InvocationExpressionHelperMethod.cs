using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator.Common
{
    internal class InvocationExpressionHelperMethod : InvocationExpressionHelper<MethodDeclarationSyntax>
    {
        protected override MethodDeclarationSyntax AdjustNewNode(MethodDeclarationSyntax newMethod, SyntaxToken newMethodName) =>
            newMethod.WithIdentifier(newMethodName).WithReturnType(CreateNamedTupleType());

        protected override ArrowExpressionClauseSyntax GetNodeExpressionBody(MethodDeclarationSyntax node) =>
            node.ExpressionBody;

        protected override string GetNodeIdentifierText(MethodDeclarationSyntax node) =>
            node.Identifier.Text;
    }
}