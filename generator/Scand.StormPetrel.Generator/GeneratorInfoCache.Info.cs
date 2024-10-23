using Serilog;
using Scand.StormPetrel.Generator.TargetProject;

namespace Scand.StormPetrel.Generator
{
    internal partial class GeneratorInfoCache
    {
        private class Info
        {
            public MainConfig MainConfig { get; set; }
            public ILogger Logger { get; set; }
        }
    }
}
