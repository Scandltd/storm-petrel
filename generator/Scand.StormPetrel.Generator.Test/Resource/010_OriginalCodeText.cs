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
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }
    }

    public static class TestedClass
    {
        public static int TestedMethod1()
        {
            return 100;
        }
        public static int TestedMethod2()
        {
            return 200;
        }
    }
}