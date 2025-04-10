using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils
{
    /// <summary>
    /// Decorates input <see cref="IGeneratorDumper"/> with <see cref="ImplicitObjectCreationExpressionSyntax"/> nodes
    /// where possible.
    /// </summary>
    public sealed class ImplicitObjectCreationDumperDecorator : AbstractDumperDecorator
    {
        public ImplicitObjectCreationDumperDecorator(IGeneratorDumper dumper) : base(dumper)
        {
        }
        private protected override SyntaxNode DumpImplementation(SyntaxNode node)
            => DecorateByImplicitObjectCreationExpression(node);

        public static SyntaxNode DecorateByImplicitObjectCreationExpression(SyntaxNode node)
        {
            var nodesToReplace = node.DescendantNodes().OfType<ObjectCreationExpressionSyntax>();

            node = node.ReplaceNodes(nodesToReplace, (_, nodeNew) =>
            {
                var implicitSyntax = SyntaxFactory.ImplicitObjectCreationExpression().WithTriviaFrom(nodeNew);

                if (nodeNew.ArgumentList != null)
                {
                    implicitSyntax = implicitSyntax.WithArgumentList(nodeNew.ArgumentList);
                }
                else
                {
                    var nameSyntax = nodeNew.ChildNodes().OfType<SimpleNameSyntax>().FirstOrDefault();

                    if (nameSyntax != null)
                    {
                        implicitSyntax = implicitSyntax.WithNewKeyword(implicitSyntax.NewKeyword).WithTrailingTrivia(nameSyntax.GetTrailingTrivia());
                    }
                }

                if (nodeNew.Initializer != null)
                {
                    implicitSyntax = implicitSyntax.WithInitializer(nodeNew.Initializer);
                }

                return implicitSyntax;
            });

            return node;
        }
    }
}
