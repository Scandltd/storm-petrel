using FluentAssertions;

namespace Test.Integration.MSTest
{
    /// <summary>
    /// Modified copy of TestCaseSourceMemberDataTest
    /// </summary>
    [TestClass]
    public sealed class TestCaseSourceTest
    {
        [TestMethod]
        [DynamicData(nameof(DataMethod))]
        public void WhenMemberDataMethodThenItIsUpdated(int x, int y, AddResult expected)
        {
            var actual = Calculator.Add(x, y);
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        [DynamicData(nameof(DataProperty))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [TestMethod]
        [DynamicData(nameof(TestCaseSource.Data), typeof(TestCaseSource))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [TestMethod]
        [DynamicData(dynamicDataDeclaringType: typeof(TestCaseSource), dynamicDataSourceName: nameof(TestCaseSource.DataForNamedParameters))]
        public void WhenMemberDataWithNamedParametersThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [TestMethod]
        [DynamicData(nameof(DataMultipleExpected))]
        public void WhenMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
        }

        [TestMethod]
        [DynamicData(nameof(DataYieldReturnAndMultipleExpected))]
        public void WhenYieldReturnAndMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
        }

        private static IEnumerable<object[]> DataMethod() =>
        [
            [1, 2, new AddResult()],
            [-2, 2, new AddResult()],
            [int.MinValue, -1, new AddResult()],
            [-4, -6, new AddResult()],
        ];

        private static IEnumerable<object[]> DataProperty =>
        [
            [1, 2, 0],
            [-2, 2, -100],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];

        private static IEnumerable<object[]> DataMultipleExpected =>
        [
            [1, 2, 0, "0x0"],
            [-2, 2, -100, "0x100"],
            [int.MinValue, -1, -100, "0x0"],
            [-4, -6, +50, "0x0"],
        ];

        private static IEnumerable<object[]> DataYieldReturnAndMultipleExpected()
        {
            yield return [1, 2, 0, "0x0"];
            yield return [-2, 2, -100, "0x100"];
            yield return [int.MinValue, -1, -100, "0x0"];
            yield return [-4, -6, +50, "0x0"];
        }
    }
}
