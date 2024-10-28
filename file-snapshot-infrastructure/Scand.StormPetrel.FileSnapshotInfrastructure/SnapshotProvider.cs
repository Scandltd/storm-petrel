using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Provides file snapshot content.
    /// </summary>
    public sealed class SnapshotProvider
    {
        private static readonly ConcurrentDictionary<string, object> FilePathToLockObject = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        private readonly SnapshotOptions _options;
        public SnapshotProvider(SnapshotOptions options = null)
        {
            _options = options ?? SnapshotOptions.Current;
        }

        #region Instance, public
        public byte[] ReadAllBytes(string useCaseId = "", [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "") =>
            ReadAllBytes(useCaseId, callerFilePath, callerMemberName, _options);
        public string ReadAllText(string useCaseId = "", Encoding encoding = null, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "") =>
            ReadAllText(useCaseId, encoding, callerFilePath, callerMemberName, _options);
        public Stream OpenReadWithShareReadWrite(string useCaseId = "", [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "") =>
            OpenReadWithShareReadWrite(useCaseId, callerFilePath, callerMemberName, _options);
        #endregion

        #region Static, public
        public static byte[] ReadAllBytes(string useCaseId = "", [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", SnapshotOptions options = null) =>
            ExecuteFuncWithMissedDirectoryOrFileCreation(options ?? SnapshotOptions.Current, useCaseId, callerFilePath, callerMemberName, x => File.ReadAllBytes(x));
        public static string ReadAllText(string useCaseId = "", Encoding encoding = null, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", SnapshotOptions options = null) =>
            ExecuteFuncWithMissedDirectoryOrFileCreation(options ?? SnapshotOptions.Current, useCaseId, callerFilePath, callerMemberName, x => FileReadAllText(x, encoding));
        public static Stream OpenReadWithShareReadWrite(string useCaseId = "", [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "", SnapshotOptions options = null) =>
            ExecuteFuncWithMissedDirectoryOrFileCreation(options ?? SnapshotOptions.Current, useCaseId, callerFilePath, callerMemberName, x => File.Open(x, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        #endregion

        internal static T ExecuteFuncWithMissedDirectoryOrFileCreation<T>(SnapshotOptions options, string useCaseId, string callerFilePath, string callerMemberName, Func<string, T> func)
        {
            var provider = options.SnapshotInfoProvider;
            var snapshotFilePathWithoutExtension = provider.GetFilePathWithoutExtension(useCaseId, callerFilePath, callerMemberName);
            var fileExtension = provider.GetFileExtension(useCaseId, callerFilePath, callerMemberName);
            var snapshotDir = Path.GetDirectoryName(snapshotFilePathWithoutExtension);
            if (string.IsNullOrEmpty(snapshotDir))
            {
                throw new InvalidOperationException($"Cannot find directory for file path: {snapshotFilePathWithoutExtension}");
            }
            string snapshotFileFullPath;
            bool tryCreateFile = false;
            if (string.IsNullOrEmpty(fileExtension))
            {
                var snapshotFileNameWithoutExtension = Path.GetFileName(snapshotFilePathWithoutExtension);
                var searchPattern = $"{snapshotFileNameWithoutExtension}.*";
                var snapshotFileFullPaths = Directory.GetFiles(snapshotDir, searchPattern);
                if (snapshotFileFullPaths.Length != 1)
                {
                    var message = snapshotFileFullPaths.Length == 0
                                    ? $"No snapshot files found in `{snapshotDir}` by `{searchPattern}` pattern. You must create snapshot files first."
                                    : $"Multiple snapshot files found in `{snapshotDir}` by `{searchPattern}` pattern. You must have single snapshot file found here.";
                    throw new InvalidOperationException(message);
                }
                snapshotFileFullPath = snapshotFileFullPaths[0];
            }
            else
            {
                snapshotFileFullPath = $"{snapshotFilePathWithoutExtension}.{fileExtension}";
                tryCreateFile = true;
            }

            //Use lock to avoid `System.IO.IOException : The process cannot access the file ...` exception
            //in scenarios like when two tests (e.g. AddTest and AddTestStormPetrel) are executed in parallel
            //and try to create the same file and/or directory or write to the same file.
            lock (FilePathToLockObject.GetOrAdd(snapshotFileFullPath, new object()))
            {
                if (tryCreateFile)
                {
                    tryCreateFile = false;
                    try
                    {
                        return func(snapshotFileFullPath);
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Directory.CreateDirectory(snapshotDir);
                        tryCreateFile = true;
                    }
                    catch (FileNotFoundException)
                    {
                        tryCreateFile = true;
                    }
                    if (tryCreateFile)
                    {
                        //If no snapshots yet then we create empty file
                        using (var _ = File.Create(snapshotFileFullPath))
                        {
                            //"using" is just to close the file
                        }
                    }
                }
                return func(snapshotFileFullPath);
            }
        }

        private static string FileReadAllText(string filePath, Encoding encoding) => encoding == null
                                                                                        ? File.ReadAllText(filePath)
                                                                                        : File.ReadAllText(filePath, encoding);
    }
}
