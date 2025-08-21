namespace Scand.StormPetrel.Generator.Common.AssertExpressionDetector
{
    internal class AssertAreEqualDetector : AbstractAssertDetector
    {
        private static readonly string[] SupportedExpectedArgumentNamesStatic = new[]
        {
            "expected",
        };

        private static readonly string[] SupportedMethodNamesStatic = new[]
        {
            "AreEqual"
        };

        protected override string[] SupportedExpectedArgumentNames => SupportedExpectedArgumentNamesStatic;
        protected override string[] SupportedMethodNames => SupportedMethodNamesStatic;
        protected override int ActualArgumentIndex => 1;
        protected override int ExpectedArgumentIndex => 0;
    }
}
