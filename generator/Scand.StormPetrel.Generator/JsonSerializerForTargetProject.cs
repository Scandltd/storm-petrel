using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Scand.StormPetrel.Generator
{
    internal class JsonSerializerForTargetProject
    {
        /// <summary>
        /// See DeserializeStringArray from TargetProject folder to deserialize
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string SerializeStringArray(string[] array)
        {
            var serializer = new DataContractJsonSerializer(typeof(string[]));
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, array);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
