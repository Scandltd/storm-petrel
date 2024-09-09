using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace Test.Integration.Shared
{
    internal static class BackupHelper
    {
        private static readonly ConcurrentDictionary<string, object> ClassFullNameToLock = new ConcurrentDictionary<string, object>();
        private static readonly ConcurrentDictionary<string, bool> ClassFullNameToWasHandled = new ConcurrentDictionary<string, bool>();
        public static bool IsStormPetrel(string classNameOrFullName)
            => !(classNameOrFullName == null || !classNameOrFullName.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase));
        /// <summary>
        /// Deletes backup file if <paramref name="classFullName"/> indicates StormPetrel class.
        /// </summary>
        /// <param name="classFullName"></param>
        /// <param name="onAfterGetAndDeleteFiles">Executes once per StormPetrel class. Input argument indicates did we have backup files or not</param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void DeleteBackup(string classFullName, Action<BackupHelperResult> onAfterGetAndDeleteFiles, string fileName = "")
        {
            // Duplicate logic of IsStormPetrel. Otherwise we have compilation error of BackupHelperStormPetrel class.
            if (classFullName == null || !classFullName.EndsWith("StormPetrel", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            lock (ClassFullNameToLock.GetOrAdd(classFullName, new object()))
            {
                if (!ClassFullNameToWasHandled.TryGetValue(classFullName, out var wasHandled))
                {
                    if (!ClassFullNameToWasHandled.TryAdd(classFullName, true))
                    {
                        throw new InvalidOperationException("This code line is unreachable due to locking mechanism above");
                    }
                }
                var rootDirectory = Directory
                                        .GetParent(Directory.GetCurrentDirectory())?
                                        .Parent?
                                        .Parent?
                                        .FullName;
                if (string.IsNullOrEmpty(rootDirectory))
                {
                    throw new InvalidOperationException("Unable to get root directory");
                }

                var className = classFullName.Split('.').Last();
                int lastIndex = className.LastIndexOf("StormPetrel", StringComparison.OrdinalIgnoreCase);
                var originalClassName = className.Substring(0, lastIndex);
                var backupFileMask = string.IsNullOrEmpty(fileName)
                                        ? $"{originalClassName}.cs.backup*"
                                        : $"{fileName}.backup*";
                var backupFiles = Directory.GetFiles(rootDirectory, backupFileMask, SearchOption.TopDirectoryOnly);
                foreach (var file in backupFiles)
                {
                    File.Delete(file);
                }
                var result = BackupHelperResult.None;
                if (!wasHandled)
                {
                    result |= BackupHelperResult.VeryFirstCallForTheClass;
                }
                if (backupFiles.Length == 1)
                {
                    result |= BackupHelperResult.HadDeletedFile;
                }
                onAfterGetAndDeleteFiles(result);
            }
        }

        public static bool IsProperlyDeleted(BackupHelperResult result)
            => result == BackupHelperResult.VeryFirstCallForTheClassAndHadDeletedFile
                || (result & BackupHelperResult.VeryFirstCallForTheClass) != BackupHelperResult.VeryFirstCallForTheClass
                    && (result & BackupHelperResult.HadDeletedFile) != BackupHelperResult.HadDeletedFile;
    }
}