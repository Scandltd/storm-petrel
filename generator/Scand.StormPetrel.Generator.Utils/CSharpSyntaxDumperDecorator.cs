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
        private readonly RemoveAssignmentDumperDecorator _removeAssignmentDumperDecorator;

        /// <summary>
        /// Creates new instance of <see cref="CSharpSyntaxDumperDecorator"/>.
        /// </summary>
        /// <param name="dumper">Input instance to be decorated.</param>
        /// <param name="decorateByCollectionExpression">Indicates should <see cref="CollectionExpressionDumperDecorator.DecorateByCollectionExpression"/> be applied or not.</param>
        /// <param name="removeAssignmentDumperDecorator">An instance to apply decorations of <see cref="RemoveAssignmentDumperDecorator"/>.</param>
        public CSharpSyntaxDumperDecorator(IGeneratorDumper dumper,
            bool decorateByCollectionExpression = true,
            RemoveAssignmentDumperDecorator removeAssignmentDumperDecorator = null)
        {
            _dumper = dumper;
            _applyCollectionExpression = decorateByCollectionExpression;
            _removeAssignmentDumperDecorator = removeAssignmentDumperDecorator;
        }

        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext);
            var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
            if (_removeAssignmentDumperDecorator != null)
            {
                rootNode = _removeAssignmentDumperDecorator.DecorateByRemoveAssignment(rootNode);
            }
            if (_applyCollectionExpression)
            {
                rootNode = CollectionExpressionDumperDecorator.DecorateByCollectionExpression(rootNode);
            }
            return rootNode.ToFullString();
        }
    }
}
