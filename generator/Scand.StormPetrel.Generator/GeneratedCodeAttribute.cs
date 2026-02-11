using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Reflection;

namespace Scand.StormPetrel.Generator
{
    internal static class GeneratedCodeAttribute
    {
        private static readonly AssemblyName AssemblyName = typeof(StormPetrelGenerator).Assembly.GetName();
        private static AttributeListSyntax _attributeList;
        public static ClassDeclarationSyntax WithAttribute(ClassDeclarationSyntax @class)
        {
            if (_attributeList == null)
            {
                _attributeList = CreateAttributeList();
            }
            var attribute = _attributeList;
            if (@class.Modifiers.Any(x => x.IsKind(SyntaxKind.PartialKeyword)))
            {
                SyntaxFactory.Comment(attribute.ToFullString());
                return @class
                            .WithLeadingTrivia(
                                SyntaxFactory.Comment("// Commented out for partial classes to avoid CS0579 error"),
                                SyntaxFactory.ElasticCarriageReturnLineFeed,
                                SyntaxFactory.Comment("// " + attribute.ToFullString()),
                                SyntaxFactory.ElasticCarriageReturnLineFeed);
            }
            attribute = _attributeList.WithLeadingTrivia(Shared.Utils.GetLeadingWhitespace(@class));
            var newList = @class.AttributeLists.Insert(0, attribute);
            return @class.WithAttributeLists(newList);
        }
        public static string GetAttributeFullString() =>
            $"[global::System.CodeDom.Compiler.GeneratedCode(\"{AssemblyName.Name}\", \"{AssemblyName.Version.ToString(3)}\")]";
        private static AttributeListSyntax CreateAttributeList()
        {
            var incomplete = (IncompleteMemberSyntax)SyntaxFactory
                                                        .ParseCompilationUnit(GetAttributeFullString())
                                                        .Members[0];
            return incomplete.AttributeLists[0];
        }
    }
}
