namespace Scand.StormPetrel.Generator.AssertExpressionDetector
{
    internal class AssertEqualDetector : AbstractAssertDetector
    {
        private static readonly string[] SupportedExpectedArgumentNamesStatic = new[]
        {
            "expected",
        };
        private static readonly string[] SupportedMethodNamesStatic = new[]
        {
            "Equal",
            "StrictEqual",
            "Equivalent"
        };
        protected override string[] SupportedMethodNames => SupportedMethodNamesStatic;
        protected override int ActualArgumentIndex => 1;
        protected override int ExpectedArgumentIndex => 0;
        protected override string[] SupportedExpectedArgumentNames => SupportedExpectedArgumentNamesStatic;
    }
}
