using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class AssertionNoExpectedVarTest
    {
        [Fact]
        public void AssertEqualTest()
        {
            //Act
            var specificActualVarName = TestedClass.TestedMethod();
            var specificActualVarname = 1;

            //Assert
            Assert.Equal(123, specificActualVarName);
            Assert.Equal(123, specificActualVarname); //Ignored by Storm Petrel because `n` in `name` does not match configuration regex
            Assert.Equal(actual: specificActualVarName, expected: 123);
            Assert.Equal(expected: 123, actual: specificActualVarName);
        }

        [Fact]
        public void IgnoredByStormPetrelTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(123, "some explanation");
        }
    }
}