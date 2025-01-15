using System.IO;
using Scand.StormPetrel.FileSnapshotInfrastructure;

namespace Test.Integration.CustomConfiguration.CustomSnapshotInfrastructure
{
    internal static class CustomSnapshotOptions
    {
        private readonly static SnapshotOptions JsonWithDefaultSnapshotFolderStructure = new SnapshotOptions()
        {
            SnapshotInfoProvider = new SnapshotInfoProvider(fileExtension: "json"),
        };

        private readonly static SnapshotOptions PngWithCustomSnapshotFolderStructure = new SnapshotOptions()
        {
            SnapshotInfoProvider = new SnapshotInfoProvider($"<CallerFileNameWithoutExtension>.Expected{Path.DirectorySeparatorChar}<CallerMemberName>", fileExtension: "png"),
        };

        /// <summary>
        /// Implements custom logic to select appropriate <see cref="SnapshotOptions"/> options.
        /// </summary>
        /// <param name="callerName"></param>
        /// <param name="callerMemberName"></param>
        /// <returns></returns>
        public static SnapshotOptions Get(string callerName, string callerMemberName)
        {
            if (callerName.StartsWith(nameof(CalculatorSnapshotTest))
                && callerMemberName.StartsWith("GetLogo"))
            {
                return PngWithCustomSnapshotFolderStructure;
            }
            return JsonWithDefaultSnapshotFolderStructure;
        }
    }
}
