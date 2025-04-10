using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Generator.Abstraction;

namespace Scand.StormPetrel.Generator.Utils
{
    public abstract class AbstractDumperDecorator : IGeneratorDumper
    {
        private readonly IGeneratorDumper _dumper;
        private protected AbstractDumperDecorator(IGeneratorDumper dumper)
        {
            _dumper = dumper;
        }
        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext);
            var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
            rootNode = DumpImplementation(rootNode);
            return rootNode.ToFullString();
        }
        private protected abstract SyntaxNode DumpImplementation(SyntaxNode node);
    }
}
