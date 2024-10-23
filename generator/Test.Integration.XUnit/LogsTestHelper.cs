using FluentAssertions;

namespace Test.Integration.XUnit
{
    /// <summary>
    /// Test class with the same name as <see cref="Shared.LogsTestHelper"/>.
    /// So, we implicitly check that both classes are handled properly.
    /// </summary>
    public class LogsTestHelper
    {
        [Fact]
        public void Test()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = 100;

            //Assert
            actual.Should().Be(expected);
        }
    }
}
