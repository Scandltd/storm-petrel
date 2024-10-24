using Scand.StormPetrel.Generator.Abstraction;
using System;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class GeneratorObjectDumper : IGeneratorDumper
    {
        private readonly DumpOptions _options;
        public GeneratorObjectDumper() : this(GetDumpOptionsDefault())
        {
        }

        public GeneratorObjectDumper(DumpOptions options)
        {
            if (options.DumpStyle != DumpStyle.CSharp)
            {
                throw new ArgumentException("The only supported value is DumpStyle CSharp", nameof(options.DumpStyle));
            }
            _options = options;
        }

        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = ObjectDumper.Dump(generationDumpContext.Value, _options);
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

        private static DumpOptions GetDumpOptionsDefault() => new DumpOptions()
        {
            DumpStyle = DumpStyle.CSharp,
        };
    }
}
