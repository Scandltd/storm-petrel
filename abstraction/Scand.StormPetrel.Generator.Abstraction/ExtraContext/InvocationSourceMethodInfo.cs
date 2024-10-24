namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext
{
    /// <summary>
    /// Data about the invocation source method nodes (statements) to be rewritten.
    /// </summary>
    public sealed class InvocationSourceMethodInfo
    {
        public int NodeKind { get; set; }
        public int NodeIndex { get; set; }
        public int ArgsCount { get; set; }
    }
}
