using System;

namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext
{
    /// <summary>
    /// Data about the variable (declaration or assignment) initializer to be rewritten.
    /// </summary>
    public sealed class InitializerContext : AbstractExtraContext
    {
        public string[] InvocationPath { get; set; } = Array.Empty<string>();
        public InitializerContextKind Kind { get; set; }
    }
}
