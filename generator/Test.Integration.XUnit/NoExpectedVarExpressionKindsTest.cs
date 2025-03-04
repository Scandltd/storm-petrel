using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class NoExpectedVarExpressionKindsTest
    {
        [Fact]
        public void ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTest()
        {
            //Act
            var actual = new
            {
                Amount = 100,
                Message = "Hello"
            };

            //Assert
            actual.Should().Be(new
            {
                Amount = 123,
                Message = "Hello incorrect"
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionTest()
        {
            //Act
            var actual = new int[6];

            //Assert
            actual.Should().BeEquivalentTo(new int[5]);
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTest()
        {
            //Act
            var actual = new int[]
            {
                2,
                3,
            };

            //Assert
            actual.Should().BeEquivalentTo(new int[]
            {
                1,
                2,
            });
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTest()
        {
            //Act
            var actual = new int[1, 1];

            //Assert
            actual.Should().BeEquivalentTo(new int[3, 3]);
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTest()
        {
            //Act
            var actual = new int[1][]
            {
                [1]
            };

            //Assert
            actual.Should().BeEquivalentTo(new int[3][]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenCollectionExpressionTest()
        {
            //Act
            List<int> actual = [3, 4];

            //Assert
            actual.Should().BeEquivalentTo(
            [
                1,
                2,
            ]);
            //Assert when explicit cast
            actual.Should().BeEquivalentTo(
            (List<int>)[
                1,
                2,
            ]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTest()
        {
            //Act
            var actual = new[] { 1, };

            //Assert
            actual.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTest()
        {
            //Act
            var actual = new[,]
            {
                { 1, 1, 1 },
                { 2, 2, 2 },
            };

            //Assert
            actual.Should().BeEquivalentTo(new[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTest()
        {
            //Act
            TestClassResultBase actual = new()
            {
                StringNullableProperty = "Incorrect Test StringNullableProperty",
            };

            //Assert
            actual.Should().BeEquivalentTo((TestClassResultBase)new());
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTest()
        {
            //Act
            TestClassResultBase actual = new()
            {
                StringNullableProperty = "Incorrect Test StringNullableProperty",
            };

            //Assert
            actual.Should().BeEquivalentTo((TestClassResultBase)new()
            {
                IntProperty = 0
            });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTest()
        {
            //Act
            var actual = 100;

            //Assert
            actual.Should().Be(123);
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest()
        {
            //Act
            var actual = "Hello, World incorrect!";

            //Assert
            actual.Should().Be("Hello, World!");
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest()
        {
            //Act
            var actual = 'B';

            //Assert
            actual.Should().Be('A');
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTest()
        {
            //Act
            var actual = new TestClassResultBase
            {
                StringProperty = "Test String property",
            };

            //Assert
            actual
                .Should()
                .BeEquivalentTo(
                    new TestClassResultBase());
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTest()
        {
            //Act
            var actual = new TestClassResultBase
            {
                StringProperty = "Test String property",
            };

            //Assert
            actual.Should().BeEquivalentTo(new TestClassResultBase()
            {
                StringProperty = "Incorrect Test StringNullableProperty",
            });
        }
        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTest()
        {
            //Act
            var actual = new TestClassResultBase
            {
                StringProperty = "Test String property",
            };

            //Assert
            actual.Should().BeEquivalentTo(new TestClassResult
            {
                StringProperty = "Incorrect Test StringNullableProperty",
            });
        }
    }
}
