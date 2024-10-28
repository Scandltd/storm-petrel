using Scand.StormPetrel.FileSnapshotInfrastructure;
using System.IO;
using System.Runtime.CompilerServices;

namespace Test.Integration.CustomConfiguration.CustomSnapshotInfrastructure
{
    internal static class CustomSnapshotProvider
    {
        /// <summary>
        /// Creates <see cref="SnapshotProvider"/> based on custom options from <see cref="CustomSnapshotOptions.Get(string, string)"/>.
        /// </summary>
        /// <param name="callerFilePath"></param>
        /// <param name="callerMemberName"></param>
        /// <returns></returns>
        public static SnapshotProvider Get([CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
        {
            var callerFileNameWithoutExtension = Path.GetFileNameWithoutExtension(callerFilePath);
            //Assume callerFileNameWithoutExtension matches with caller name in the project
            var options = CustomSnapshotOptions.Get(callerFileNameWithoutExtension, callerMemberName);
            return new SnapshotProvider(options);
        }
    }
}
