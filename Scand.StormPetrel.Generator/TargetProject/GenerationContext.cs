namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class GenerationContext
    {
        public string FilePath { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string[] MethodTestAttributeNames { get; set; }
        public object Actual { get; set; }
        public string[] ActualVariablePath { get; set; }
        public object Expected { get; set; }
        public string[] ExpectedVariablePath { get; set; }
        public VariableInvocationExpressionInfo ExpectedVariableInvocationExpressionInfo { get; set; }
        public bool IsLastVariablePair { get; set; }
        public RewriterKind RewriterKind { get; set; }
        public TestCaseAttributeInfo TestCaseAttributeInfo { get; set; }
    }
}
