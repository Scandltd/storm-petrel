using System.Text.RegularExpressions;

namespace Scand.StormPetrel.Generator.Common.TargetProject
{
    public sealed class TestVariablePairConfigParsed
    {
        public Regex ActualVarNameTokenRegex { get; set; }
        public Regex ExpectedVarNameTokenRegex { get; set; }
    }
}
