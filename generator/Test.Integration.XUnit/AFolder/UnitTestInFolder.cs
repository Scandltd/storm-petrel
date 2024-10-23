using FluentAssertions;

namespace Test.Integration.XUnit.AFolder
{
    public partial class UnitTestInFolder
    {
        [Fact]
        public void Test12()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }
    }
}
