using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Generator.Abstraction;

namespace Scand.StormPetrel.Generator.Utils
{
    /// <summary>
    /// Applies decorations to input <see cref="IGeneratorDumper"/> instance
    /// according to <see cref="CSharpSyntaxDumperDecorator"/> arguments.
    /// </summary>
    public sealed class CSharpSyntaxDumperDecorator : IGeneratorDumper
    {
        private readonly IGeneratorDumper _dumper;
        private readonly bool _applyCollectionExpression;

        /// <summary>
        /// Creates new instance of <see cref="CSharpSyntaxDumperDecorator"/>.
        /// </summary>
        /// <param name="dumper">Input instance to be decorated.</param>
        /// <param name="decorateByCollectionExpression">Indicates should <see cref="CollectionExpressionDumperDecorator.DecorateByCollectionExpression"/> be applied or not.</param>
        public CSharpSyntaxDumperDecorator(IGeneratorDumper dumper, bool decorateByCollectionExpression = true)
        {
            _dumper = dumper;
            _applyCollectionExpression = decorateByCollectionExpression;
        }

        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext);
            var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
            if (_applyCollectionExpression)
            {
                rootNode = CollectionExpressionDumperDecorator.DecorateByCollectionExpression(rootNode);
            }
            return rootNode.ToFullString();
        }
    }
}
