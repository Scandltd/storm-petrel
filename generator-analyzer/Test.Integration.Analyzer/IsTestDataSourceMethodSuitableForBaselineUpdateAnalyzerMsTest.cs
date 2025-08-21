using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Test.Integration.Analyzer
{
    [TestClass]
    public class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerMsTest
    {
        [TestMethod]
        [Ignore("To avoid test failure in the build")]
        [DynamicData(nameof(MsTestDataMethod))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
            var actual = x + y;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [Ignore("To avoid test failure in the build")]
        [DynamicData(nameof(MsTestDataSource.MsTestDataSourceDataMethod), typeof(MsTestDataSource))]
        public void WhenMemberDataMethodThenItIsUpdated(int x, int y, int expected)
        {
            var actual = x + y;
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<object[]> MsTestDataMethod() =>
            throw new NotImplementedException("To have the diagnostic reported");
    }

    public static class MsTestDataSource
    {
        public static object[][] MsTestDataSourceDataMethod() =>
            throw new NotImplementedException("To have the diagnostic reported");
    }
}
