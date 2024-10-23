using FluentAssertions;
using Xunit;

namespace Scand.StormPetrel.Rewriter.Test
{
    public static class DataSourceHelperTest
    {
        [Theory]
        [InlineData(nameof(DataSourceHelperTestData.PublicStaticMethod), null, new int[] { 1, 2, 3 })]
        [InlineData("NonExistingMember", null, null)]
        [InlineData(nameof(DataSourceHelperTestData.PublicNonStaticMethod), null, null)]
        [InlineData("PrivateStaticMethod", null, new int[] { 2 })]
        [InlineData("PrivateStaticProperty", null, new int[] { 3 })]
        [InlineData("PrivateStaticField", null, new int[] { 4 })]
        [InlineData(nameof(DataSourceHelperTestData.PublicStaticMethodWithArgs), new object[] { 1, "test" }, new int[] { 2, 4 })]
        public static void WhenEnumerateWithParametersThenExpectedArray(string memberName, object[]? memberParameters, int[]? expected)
        {
            //Arrange
            //Act
            var actual = DataSourceHelper.Enumerate(typeof(DataSourceHelperTestData), memberName, memberParameters);

            //Assert
            if (expected == null)
            {
                actual.Should().BeNull();
            }
            else
            {
                actual.Single().Should().BeEquivalentTo(expected);
            }
        }
    }
}
