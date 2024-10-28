using System.IO;
using System.Text.Json;

namespace Test.Integration.Shared
{
    internal static class Extensions
    {
        private readonly static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        public static string ToJsonText<T>(this T obj) => JsonSerializer.Serialize(obj, options);

        public static byte[] ToByteArray(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
