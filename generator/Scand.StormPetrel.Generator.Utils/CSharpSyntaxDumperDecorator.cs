using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Utils.DumperDecorator;

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
        private readonly LiteralExpressionDumperDecorator _literalExpressionDumperDecorator;

        /// <summary>
        /// Creates new instance of <see cref="CSharpSyntaxDumperDecorator"/>.
        /// </summary>
        /// <param name="dumper">Input instance to be decorated.</param>
        /// <param name="decorateByCollectionExpression">Indicates should <see cref="CollectionExpressionDumperDecorator.DecorateByCollectionExpression"/> be applied or not.</param>
        /// <param name="removeAssignmentDumperDecorator">An instance to apply decorations of <see cref="RemoveAssignmentDumperDecorator"/>.</param>
        /// <param name="literalExpressionDumperDecorator">An instance to apply decorations of <see cref="LiteralExpressionDumperDecorator"/>.</param>
        public CSharpSyntaxDumperDecorator(IGeneratorDumper dumper,
            bool decorateByCollectionExpression = true,
            RemoveAssignmentDumperDecorator removeAssignmentDumperDecorator = null,
            LiteralExpressionDumperDecorator literalExpressionDumperDecorator = null)
        {
            _dumper = dumper;
            _applyCollectionExpression = decorateByCollectionExpression;
            _removeAssignmentDumperDecorator = removeAssignmentDumperDecorator;
            _literalExpressionDumperDecorator = literalExpressionDumperDecorator;
        }

        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext);
            var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
            if (_removeAssignmentDumperDecorator != null)
            {
                rootNode = _removeAssignmentDumperDecorator.DecorateByRemoveAssignment(rootNode);
            }
            if (_literalExpressionDumperDecorator != null)
            {
                rootNode = _literalExpressionDumperDecorator.Decorate(rootNode);
            }
            if (_applyCollectionExpression)
            {
                rootNode = CollectionExpressionDumperDecorator.DecorateByCollectionExpression(rootNode);
            }
            return rootNode.ToFullString();
        }
    }
}
