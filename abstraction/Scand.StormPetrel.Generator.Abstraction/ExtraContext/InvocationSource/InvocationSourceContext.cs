using System;

namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSource
{
    /// <summary>
    /// Data about the invocation source (method or property) to be rewritten.
    /// </summary>
    public sealed class InvocationSourceContext : AbstractExtraContext
    {
        public string[] Path { get; set; } = Array.Empty<string>();
        public AbstractMethodInfo? MethodInfo { get; set; }
    }
}
