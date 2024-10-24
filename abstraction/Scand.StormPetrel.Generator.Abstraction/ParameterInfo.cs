namespace Scand.StormPetrel.Generator.Abstraction
{
    public sealed class ParameterInfo
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public AttributeInfo[] Attributes { get; set; }
    }
}
