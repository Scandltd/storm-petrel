using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Scand.StormPetrel.Generator
{
    internal class InvocationExpressionHelperProperty : InvocationExpressionHelper<PropertyDeclarationSyntax>
    {
        protected override PropertyDeclarationSyntax AdjustNewNode(PropertyDeclarationSyntax newProperty, SyntaxToken newPropertyName) =>
            newProperty.WithIdentifier(newPropertyName).WithType(CreateNamedTupleType());

        protected override ArrowExpressionClauseSyntax GetNodeExpressionBody(PropertyDeclarationSyntax node) =>
            node.ExpressionBody;

        protected override string GetNodeIdentifierText(PropertyDeclarationSyntax node) =>
            node.Identifier.Text;
    }
}