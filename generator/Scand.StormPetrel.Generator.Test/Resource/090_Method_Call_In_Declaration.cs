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
            int expected = SomeClass.Expected();

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPath()
        {
            //Arrange
            object expected = Expected();

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContext()
        {
            //Arrange
            object expected = Expected(arg1, 1, "123", SomeType.SomeProperty, SomeType.SomeMethod());

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }

        #region A region with couple methods
        private static object ExpectedInRegion() => new object();
        private static object StaticButNotExpectedMethodInRegion() => new object();
        private object NotStaticMethodInRegion() => new object();
        #region

        private static object Expected() => new object();
        private static object StaticButNotExpectedMethod() => new object();
        private object NotStaticMethod() => new object();
        private static object ExpectedThrows() => throw new System.NotImplementedException();
    }
}
