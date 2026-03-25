namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSource
{
    /// <summary>
    /// Data about the invocation source method nodes to be rewritten.
    /// </summary>
    public sealed class MethodNodeInfo : AbstractMethodInfo
    {
        public int MethodArgsCount { get; set; }
        public int NodeKind { get; set; }
        public int NodeIndex { get; set; }
    }
}
