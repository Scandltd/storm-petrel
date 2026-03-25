using Scand.StormPetrel.Generator.Abstraction.ExtraContext;

namespace Scand.StormPetrel.Generator.Common.ExtraContextInternal
{
    internal class TestCaseSourceContextInternal : AbstractExtraContextInternal<TestCaseSourceContext>
    {
        public string[] NonExpectedParameterTypes { get; set; }
        public string[] NonExpectedParameterNames { get; set; }
        public string TestCaseSourceExpression { get; set; }
        public string TestCaseSourcePathExpression { get; set; }
    }
}
