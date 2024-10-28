using Scand.StormPetrel.FileSnapshotInfrastructure;
using System.Runtime.CompilerServices;

namespace Test.Integration.DefaultConfigurationWithCustomOptions;
internal static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        SnapshotOptions.Current = new()
        {
            SnapshotInfoProvider = new SnapshotInfoProvider(fileExtension: "json"),
        };
    }
}
