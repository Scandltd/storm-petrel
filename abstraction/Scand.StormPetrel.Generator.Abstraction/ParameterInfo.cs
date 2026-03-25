using System;

namespace Scand.StormPetrel.Generator.Abstraction
{
    public sealed class ParameterInfo
    {
        public string Name { get; set; } = "";
        public object? Value { get; set; }
        public AttributeInfo[] Attributes { get; set; } = Array.Empty<AttributeInfo>();
        public string TypeToken { get; set; } = "";
        public string? DefaultExpression { get; set; }
    }
}
