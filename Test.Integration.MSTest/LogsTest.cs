using FluentAssertions;

namespace Test.Integration.MSTest
{
    [TestClass]
    public class LogsTest
    {
        [TestMethod]
        public void LogTest() => Shared.LogsTestHelper.IsStormPetrelLogFileExists().Should().BeFalse();
    }
}
