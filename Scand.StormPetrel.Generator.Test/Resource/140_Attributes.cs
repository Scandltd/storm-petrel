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
    }
}
