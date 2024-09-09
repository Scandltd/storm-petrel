namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class VariableInvocationExpressionInfo
    {
        public string[] Path { get; set; }
        public (int NodeKind, int NodeIndex) NodeInfo { get; set; }
        public int ArgsCount { get; set; }
    }
}
