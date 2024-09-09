using FluentAssertions;
using NUnit.Framework;

namespace Test.Integration.NETFramework.NUnit
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class LogsTest
    {
        [Test]
        public void LogTest() => Shared.LogsTestHelper.IsStormPetrelLogFileExists().Should().BeFalse();
    }
}
