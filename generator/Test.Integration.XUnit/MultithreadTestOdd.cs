using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class MultithreadTestOdd
    {
        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(9)]
        public void OverwriteExpectedFileTest(int expectedMethodArg)
        {
            //Arrange
            var expected = MultithreadTestHelper.GetExpected(expectedMethodArg);

            //Act
            var actual = expectedMethodArg * 100;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
