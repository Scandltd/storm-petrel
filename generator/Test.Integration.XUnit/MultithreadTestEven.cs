using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class MultithreadTestEven
    {
        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(10)]
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
