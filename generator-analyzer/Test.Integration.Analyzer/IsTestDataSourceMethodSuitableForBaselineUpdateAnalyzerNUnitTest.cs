using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Test.Integration.Analyzer
{
    public class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerNUnitTest
    {
        [TestCaseSource(typeof(NUnitTestCaseSource), nameof(NUnitTestCaseSource.NUnitDataSourceDataMethod))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
            var actual = x + y;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(NUnitDataMethod))]
        public void WhenMemberDataMethodThenItIsUpdated(int x, int y, int expected)
        {
            var actual = x + y;
            Assert.That(actual, Is.EqualTo(expected));
        }

        private static IEnumerable<object[]> NUnitDataMethod() =>
            throw new NotImplementedException("To have the diagnostic reported");
    }

    public static class NUnitTestCaseSource
    {
        public static IEnumerable<object[]> NUnitDataSourceDataMethod() =>
            throw new NotImplementedException("To have the diagnostic reported");
    }
}
