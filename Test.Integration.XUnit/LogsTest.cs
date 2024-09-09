using FluentAssertions;
using Test.Integration.Shared;

namespace Test.Integration.XUnit
{
    public class LogsTest
    {
        [Fact]
        public void LogTest()
        {
            Shared.LogsTestHelper.IsStormPetrelLogFileExists().Should().BeTrue();
            BackupHelper.DeleteBackup(GetType().FullName!, backupResult => BackupHelper.IsProperlyDeleted(backupResult).Should().BeTrue());
        }
    }
}
