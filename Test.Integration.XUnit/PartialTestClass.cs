using FluentAssertions;

namespace Test.Integration.XUnit
{
    public partial class PartialTestClass
    {
        [Fact]
        public void TestInt()
        {
            int expected = 1000;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        //[Fact]
        public static async Task TestWhichIsNotATest()
        {
            //Some method body
            int expected = 1000;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
            await Task.CompletedTask; //to avoid code analysis warning for "async Task" method signature
        }

        [Fact]
        public void TestIntOneMoreExample()
        {
            int expected = 200;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedIsFromStaticMethodOfCurrentClassButInAnotherFileThenLastAssignmentIsReplacedTest()
        {
            //Arrange
            int expected = GetExpectedInAnotherFile();

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }
    }
}