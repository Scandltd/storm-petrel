using FluentAssertions;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class AttributesTest
    {
        [Theory]
        [InlineData(123, "one", true)]
        [InlineData(2, "two", false)]
        [InlineData(3, "three")]
        public void TestMethod(int intArg, string stringArg, bool boolArg = false, int oneMoreDefaultValue = -1)
        {
            //Arrange
            var expected = 1;

            //Act
            var actual = 2;

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData()]
        public void TestMethodAllArgsWithDefaults(int intArg = 0, string stringArg = "", bool boolArg = false, int oneMoreDefaultValue = -1)
        {
            //Arrange
            var expected = 1;

            //Act
            var actual = 2;

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
