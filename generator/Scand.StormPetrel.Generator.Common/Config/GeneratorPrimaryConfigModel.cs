using System;

namespace Scand.StormPetrel.Generator.Common.Config
{
    public sealed class GeneratorPrimaryConfigModel
    {
        public CustomTestAttributeInfo[] CustomTestAttributes { get; set; } = Array.Empty<CustomTestAttributeInfo>();
    }
}
