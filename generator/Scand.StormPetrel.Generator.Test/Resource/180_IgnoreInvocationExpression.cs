using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class IgnoreInvocationExpressionTest
    {
        [Fact]
        public void ShouldIgnoreInvocationExpressionTest()
        {
            //Arrange
            int expected = InvocationExpressionToBeIgnored();

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ShouldNotIgnoreInvocationExpressionDueToCaseSensitivityTest()
        {
            //Arrange
            int expected = InvocationExpressiontobeignored(); //should not be ignored

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }
    }
}