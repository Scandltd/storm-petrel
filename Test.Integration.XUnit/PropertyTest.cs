using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class PropertyTest
    {
        [Fact]
        public void WhenExpectedFromStaticPropertyThenDeclarationIsReplacedTest()
        {
            //Arrange
            int expected = ExpectedProperty;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedIsFromStaticPropertyOfCurrentClassThenDeclarationIsReplacedTest()
        {
            //Arrange
            int expected = ExpectedPropertyArrow;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedFromPropertyGetArrowThenDeclarationIsReplacedTest()
        {
            //Arrange
            int expected = ExpectedPropertyGetArrow;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedFromPropertyGetSetThenDeclarationIsReplacedTest()
        {
            //Arrange
            int expected = ExpectedPropertyGetSet;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedFromPropertyReturnThenDeclarationIsReplacedTest()
        {
            //Arrange
            int expected = ExpectedPropertyReturn;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedIsFromStaticPropertyOfAnotherClassThenDeclarationIsReplacedTest()
        {
            //Arrange
            int expected = BaseTestHelper.GetExpectedPropertyArrow;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        private static int ExpectedProperty { get; } = 123;
        private static int ExpectedPropertyArrow => 123;
        private static int ExpectedPropertyGetArrow { get => 123; }
        private static int ExpectedPropertyGetSet { get; set; } = 123;
        private static int ExpectedPropertyReturn { get { return 123; } }
    }
}