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
            int expected = Expected;

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }

        private static int Expected => 100;
    }
}