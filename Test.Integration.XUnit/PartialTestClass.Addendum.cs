using FluentAssertions;

namespace Test.Integration.XUnit
{
    public partial class PartialTestClass
    {
        [Fact]
        public void TestIntInAddendum()
        {
            int expected = 200;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        private static int GetExpectedInAnotherFile() => 123;
    }
}
