namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Data about the current test method.
    /// </summary>
    public sealed class MethodContext
    {
        public string FilePath { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int VariablePairCurrentIndex { get; set; }
        public int VariablePairsCount { get; set; }
        public ParameterInfo[] Parameters { get; set; }
    }
}
