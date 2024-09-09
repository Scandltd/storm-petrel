using System;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class GeneratorDumper : IGeneratorDumper
    {
        private readonly VarDump.CSharpDumper _dumper;
        public GeneratorDumper(VarDump.CSharpDumper dumper)
        {
            _dumper = dumper;
        }
        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext.Value);
            var i = dump.IndexOf('=');
            if (i < 0)
            {
                throw new InvalidOperationException("Unexpected index of '='");
            }
            dump = dump
                    .Substring(i + 1)
                    .TrimStart(' ')
                    .TrimEnd('\n', '\r', ';');
            return dump;
        }
    }
}
