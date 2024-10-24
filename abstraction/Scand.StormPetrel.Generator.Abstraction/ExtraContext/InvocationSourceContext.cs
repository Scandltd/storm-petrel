namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext
{
    /// <summary>
    /// Data about the invocation source (method or property) to be rewritten.
    /// </summary>
    public sealed class InvocationSourceContext : AbstractExtraContext
    {
        public string[] Path { get; set; }
        public InvocationSourceMethodInfo MethodInfo { get; set; }
    }
}
