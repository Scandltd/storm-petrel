using FluentAssertions;

namespace Test.Integration.XUnit.AFolder
{
    public partial class UnitTestInFolder
    {
        [Fact]
        public void Test2Addendum()
        {
            int expected = 200;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }
    }
}
