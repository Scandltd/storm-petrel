using FluentAssertions;
using Test.Integration.Shared;

namespace Test.Integration.NUnit
{
    /// <summary>
    /// Modified copy of TestCaseSourceMemberDataTest
    /// </summary>
    public sealed class TestCaseSourceTest
    {
        [TestCaseSource(nameof(DataMethod))]
        public void WhenMemberDataMethodThenItIsUpdated(int x, int y, AddResult expected)
        {
            var actual = Calculator.Add(x, y);
            actual.Should().BeEquivalentTo(expected);
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }

        [TestCaseSource(sourceName: nameof(DataMethodWithArgs), new object[] { 3 })]
        public void WhenMemberDataMethodWithArgsThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
            var lastUseCase = DataMethodWithArgs(int.MaxValue).Last();
            lastUseCase[2].Should().Be(50, "Last use case should not be modified according to MemberData arguments");
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }

        [TestCaseSource(methodParams: [1, "22"], sourceName: nameof(DataMethodWithArgsForNamedParameters))]
        public void WhenMemberDataMethodWithArgNamesThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
            var lastUseCase = DataMethodWithArgs(int.MaxValue).Last();
            lastUseCase[2].Should().Be(50, "Last use case should not be modified according to MemberData arguments");
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }

        [TestCaseSource(nameof(DataProperty))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }

        [TestCaseSource(typeof(TestCaseSource), nameof(TestCaseSource.Data))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
            BackupHelper.DeleteBackupWithResultAssertion(typeof(TestCaseSource), doNotIgnoreOriginalClass: true);
        }

        [TestCaseSource(nameof(DataMultipleExpected))]
        public void WhenMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }

        [TestCaseSource(nameof(DataYieldReturnAndMultipleExpected))]
        public void WhenYieldReturnAndMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }

        private static IEnumerable<object[]> DataMethod() =>
        [
            [1, 2, new AddResult()],
            [-2, 2, new AddResult()],
            [int.MinValue, -1, new AddResult()],
            [-4, -6, new AddResult()],
        ];

        private static IEnumerable<object[]> DataMethodWithArgs(int count) =>
        new object[][]
        {
            [1, 2, 0],
            [-2, 2, -100],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        }.Take(count);

        private static IEnumerable<object[]> DataMethodWithArgsForNamedParameters(int arg1, string arg2) =>
        new object[][]
        {
            [1, 2, 0],
            [-2, 2, -100],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        }.Take(arg1 + (arg2?.Length ?? throw new ArgumentNullException(nameof(arg2))));

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
