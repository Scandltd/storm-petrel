using FluentAssertions;

namespace Test.Integration.XUnit.TestCaseSourceClass
{
    public class TestCaseSourceClassDataTest
    {
        [Theory]
        [ClassData(typeof(TestCaseSourceClassData))]
        public void WhenClassDataTypeThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(TestCaseSourceClassDataProperty))]
        public void WhenClassDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [Theory]
#pragma warning disable xUnit1045 //Justification: Intentionally test AddResult as non-serializable
        [ClassData(typeof(TestCaseSourceClassTheoryData))]
#pragma warning restore xUnit1045
        public void WhenClassTheoryDataThenItIsUpdated(int x, int y, AddResult expected)
        {
            var actual = Calculator.Add(x, y);
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
#pragma warning disable xUnit1045 //Justification: Intentionally test AddResult as non-serializable
        [ClassData(typeof(TestCaseSourceClassTheoryTupleData))]
#pragma warning restore xUnit1045
        public void WhenClassTheoryTupleDataThenItIsUpdated(int x, int y, AddResult expected)
        {
            var actual = Calculator.Add(x, y);
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [ClassData(typeof(TestCaseSourceClassTheoryNamedTupleData))]
        public void WhenClassTheoryNamedTupleDataThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(TestCaseSourceClassMultipleExpected))]
        public void WhenClassMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actualAddResult = Calculator.Add(x, y);
            var actual = actualAddResult.Value;
            var actualHexString = actualAddResult.ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
        }
    }
}
