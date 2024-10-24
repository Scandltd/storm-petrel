using Scand.StormPetrel.Generator.ExtraContextInternal;

namespace Scand.StormPetrel.Generator
{
    partial class VarHelper
    {
        private class VarInfo
        {
            public int StatementIndex { get; set; }
            public string[] Path { get; set; }
            public AbstractExtraContextInternal ExtraContextInternal { get; set; }
        }
    }
}
