using Scand.StormPetrel.Generator.Abstraction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class GeneratorBackuper : IGeneratorBackuper
    {
        private static ConcurrentDictionary<string, object> FilePathToLockObject = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, string> FilePathToBackupFilePath = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Creates backup file based on <paramref name="generationBackupContext"/>.
        /// </summary>
        /// <param name="generationBackupContext"></param>
        /// <returns></returns>
        public string Backup(GenerationBackupContext generationBackupContext)
        {
            var contextFilePath = generationBackupContext.FilePath;
            lock (FilePathToLockObject.GetOrAdd(contextFilePath, new object()))
            {
                if (!FilePathToBackupFilePath.TryGetValue(contextFilePath, out var backupFilePath))
                {
                    var currentDateTimeForFileExt = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                    backupFilePath = $"{contextFilePath}.backup{currentDateTimeForFileExt}";
                    File.Copy(contextFilePath, backupFilePath);
                    if (!FilePathToBackupFilePath.TryAdd(contextFilePath, backupFilePath))
                    {
                        throw new InvalidOperationException("This code line is unreachable due to locking mechanism above");
                    }
                }
                return backupFilePath;
            }
        }
    }
}
