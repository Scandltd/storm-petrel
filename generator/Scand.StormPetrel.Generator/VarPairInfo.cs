using Scand.StormPetrel.Generator.ExtraContextInternal;

namespace Scand.StormPetrel.Generator
{
    internal class VarPairInfo
    {
        public string ActualVarName { get; set; }
        public string[] ActualVarPath { get; set; }
        public string ExpectedVarName { get; set; }
        public string[] ExpectedVarPath { get; set; }
        public int StatementIndex { get; set; }
        public AbstractExtraContextInternal ExpectedVarExtraContextInternal { get; set; }
    }
}
