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
            int expected;
            expected = 2;
            expected = GetValue();

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }

        private static object GetValue() => new object();
        private static object StaticButNotExpectedMethod() => new object();
        private object NotStaticMethod() => new object();
    }
}
