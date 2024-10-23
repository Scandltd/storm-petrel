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
    }
}
