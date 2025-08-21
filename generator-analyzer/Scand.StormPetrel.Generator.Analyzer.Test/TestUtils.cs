using System.Reflection;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

internal static class TestUtils
{
    public static string ReadResource(string resourceFileName)
    {
        var assembly = Assembly.GetAssembly(typeof(TestUtils)) ?? throw new InvalidOperationException();
        using var stream = assembly
                            .GetManifestResourceStream(typeof(TestUtils), $"Resource.{resourceFileName}.cs")
                            ?? throw new InvalidOperationException();
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }
}
