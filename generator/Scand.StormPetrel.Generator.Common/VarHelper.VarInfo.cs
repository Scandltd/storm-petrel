using Scand.StormPetrel.Generator.Common.ExtraContextInternal;

namespace Scand.StormPetrel.Generator.Common
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
