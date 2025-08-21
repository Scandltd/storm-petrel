using System.Text.Json.Serialization;

namespace Scand.StormPetrel.Generator.Common.Config
{
    public sealed class CustomTestAttributeInfo
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TestFrameworkKind TestFrameworkKindName { get; set; }

        public string FullName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomTestAttributeKind KindName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public XUnitTestCaseSourceKind? XUnitTestCaseSourceKindName { get; set; }
    }
}
