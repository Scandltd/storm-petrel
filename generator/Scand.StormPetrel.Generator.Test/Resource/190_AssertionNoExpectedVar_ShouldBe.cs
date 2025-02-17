using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class AssertionNoExpectedVarTest
    {
        [Fact]
        public void ShouldDetectExpectedArgumentTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(1);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenMultipleArgsTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(123, "some explanation");
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenMultipleNamedArgsTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(because: "some explanation", expected: 123);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().BeEquivalentTo(new FooExpected
            {
                BlaProperty = "123"
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentAndActualWithPropertyTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.FooProperty.Should().Be(1);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentAndActualWithMethodTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.FooMethod().Should().Be(1);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentAndActualCouplePropertiesOrMethodsTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual
                .FooMethod()
                .BlaProperty
                .OneMoreFooMethod()
                .Should().Be(1);
        }
    }
}