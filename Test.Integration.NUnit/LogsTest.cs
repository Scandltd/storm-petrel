using FluentAssertions;
using Test.Integration.Shared;

namespace Test.Integration.NUnit
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LogsTest
    {
        [Test]
        public void LogTest()
        {
            LogsTestHelper.IsStormPetrelLogFileExists().Should().BeTrue();
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }
    }
}
