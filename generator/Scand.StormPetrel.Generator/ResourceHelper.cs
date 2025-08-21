using System.IO;
using System.Reflection;

namespace Scand.StormPetrel.Generator
{
    internal static class ResourceHelper
    {
        public const string StaticMethodInfoResourceFileName = "GeneratorRewriter.StaticMethodInfo";
        public const string StaticPropertyInfoResourceFileName = "GeneratorRewriter.StaticPropertyInfo";
        public static string ReadTargetProjectResource(string resourceClassName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(typeof(StormPetrelGenerator), $"TargetProject.{resourceClassName}.cs"))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
