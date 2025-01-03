﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;

namespace Scand.StormPetrel.Generator.Utils
{
    /// <summary>
    /// Decorates input <see cref="IGeneratorDumper"/> with <see cref="ImplicitObjectCreationExpressionSyntax"/> nodes
    /// where possible.
    /// </summary>
    public sealed class ImplicitObjectCreationDumperDecorator : IGeneratorDumper
    {
        private readonly IGeneratorDumper _dumper;

        public ImplicitObjectCreationDumperDecorator(IGeneratorDumper dumper)
        {
            _dumper = dumper;
        }

        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext);
            var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
            rootNode = DecorateByImplicitObjectCreationExpression(rootNode);
            return rootNode.ToFullString();
        }

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
