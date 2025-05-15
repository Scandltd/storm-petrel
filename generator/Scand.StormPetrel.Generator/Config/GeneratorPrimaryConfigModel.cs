using System;

namespace Scand.StormPetrel.Generator.Config
{
    public sealed class GeneratorPrimaryConfigModel
    {
        public CustomTestAttributeInfo[] CustomTestAttributes { get; set; } = Array.Empty<CustomTestAttributeInfo>();
    }
}
