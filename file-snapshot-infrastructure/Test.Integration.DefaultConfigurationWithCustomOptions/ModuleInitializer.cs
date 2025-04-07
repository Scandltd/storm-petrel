using Scand.StormPetrel.FileSnapshotInfrastructure;
using System.Runtime.CompilerServices;

namespace Test.Integration.DefaultConfigurationWithCustomOptions;
internal static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // Use path per target framework for the case of multiple targets
        var extraPathPattern = $"{SnapshotInfoProvider.TokenForCallerFileNameWithoutExtension}.Expected.{GetCurrentTargetFramework()}";
        SnapshotOptions.Current = new()
        {
            SnapshotInfoProvider = new SnapshotInfoProvider(extraPathPattern: extraPathPattern, fileExtension: "json"),
        };
    }

    private static string GetCurrentTargetFramework()
    {
        string result =
#if NET8_0
        "NET8_0";
#elif NET9_0
        "NET9_0";
#else
        "NET_Unknown";
#endif
        return result;
    }
}
