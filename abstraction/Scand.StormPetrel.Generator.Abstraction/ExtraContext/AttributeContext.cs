namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext
{
    /// <summary>
    /// Data about the attribute to be rewritten.
    /// </summary>
    public sealed class AttributeContext : AbstractExtraContext
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int ParameterIndex { get; set; }
    }
}
