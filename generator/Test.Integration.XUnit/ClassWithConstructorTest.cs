using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class ClassWithConstructorTest
    {
        public ClassWithConstructorTest()
        {
        }

        [Fact]
        public void SomeTest()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = 456;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
