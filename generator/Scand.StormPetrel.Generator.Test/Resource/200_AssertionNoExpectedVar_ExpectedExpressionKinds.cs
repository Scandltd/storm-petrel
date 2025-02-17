using FluentAssertions;
using System.Collections.Generic;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class AssertionNoExpectedVarTest
    {
        [Fact]
        public void ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new
            {
                Amount = 108,
                Message = "Hello"
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new int[5]);
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new int[]
            {
                1,
                2,
            });
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new int[3, 3]);
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new int[3][]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenCollectionExpressionNoCastTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(
            [
                1,
                2,
            ]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenCollectionExpressionTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be((List<int>)
            [
                1,
                2,
            ]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new[] { 1, 2, 3, 4, 5 });
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxNoCastTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new());
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be((TestClassResultBase)new());
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(new()
            {
                IntProperty = 0
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(123);
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be("Hello, World!");
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be('A');
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().BeEquivalentTo(new FooExpected());
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().BeEquivalentTo(new FooExpected()
            {
                BlaProperty = "123"
            });
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().BeEquivalentTo(new FooExpected
            {
                BlaProperty = "123"
            });
        }
    }
}