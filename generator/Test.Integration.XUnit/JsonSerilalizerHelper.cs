using System.Text.Json;
using System.Text.Json.Serialization;

namespace Test.Integration.XUnit;

internal static class JsonSerilalizerHelper
{
    public static T Deserialize<T>(this string json) =>
        JsonSerializer.Deserialize<T>(json, GetSerializerOptions())!;
    public static string Serialize<T>(this T obj) =>
        JsonSerializer.Serialize(obj, GetSerializerOptions());
    private static JsonSerializerOptions GetSerializerOptions() =>
    new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        Converters = { new JsonStringEnumConverter() }
    };
}
