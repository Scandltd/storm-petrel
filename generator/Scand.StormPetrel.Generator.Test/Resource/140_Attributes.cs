using System.Globalization;
using FluentAssertions;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class AttributesTest
    {
        [Theory]
        [InlineData(1, "one")]
        [InlineData(2, "two")]
        [InlineData(3, "three")]
        public void TestMethod(int intArg, string expected)
        {
            //Act
            var actual = "one_actual";

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1, "one", 2)]
        [InlineData(2, "two")]
        [InlineData(3, "three")]
        public void TestMethodMultipleExpected(int intArg, string expected, int expected2 = -1)
        {
            //Act
            var actual = "one_actual";
            var actual2 = "two_actual";

            //Assert
            actual.Should().BeEquivalentTo(expected);
            actual2.Should().BeEquivalentTo(expected2);
        }

        [Theory]
        [InlineData(1, "one")]
        [InlineData(2, "two")]
        [InlineData(3, "three")]
        public void TestMethodWithParameterAttributes([CallerFilePath] int intArg,
                                                      [Obsolete("This method is obsolete. Use NewMethod instead."), SomeNameSpace.MyCustomAttribute("Example")] string expected
                                                      [SomeNameSpace.CallerFilePath] [CallerMemberName] string oneMoreArgWithTwoAttributes = "")
        {
            //Act
            var actual = "one_actual";

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1, "1_incorrect")]
        [InlineData(2, "incorrect_2")]
        public void TestMethodWithTwoExpectedVar(int i, string expected)
        {
            //Arrange
            int expectedLength = 11;

            //Act
            var actual = i.ToString(CultureInfo.InvariantCulture);
            var actualLength = actual.Length;

            //Assert
            actual.Should().BeEquivalentTo(expected);
            actualLength.Should().Be(expectedLength);
        }
    }
}
