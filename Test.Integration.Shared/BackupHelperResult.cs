using System;

namespace Test.Integration.Shared
{
    [Flags]
    internal enum BackupHelperResult
    {
        None = 0,
        VeryFirstCallForTheClass = 1,
        HadDeletedFile = 2,
        VeryFirstCallForTheClassAndHadDeletedFile = VeryFirstCallForTheClass | HadDeletedFile,
    }
}
