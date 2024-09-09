using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            int expected = 1;

            //Act
            expected = 2;
            var actual = TestedClass.TestedMethod1();
            expected = 3;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
