using FluentAssertions;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Test.Integration.XUnit
{
    public class AttributesTest
    {
        [Theory]
        [InlineData(1, "1_incorrect")]
        [InlineData(2, "2")]
        [InlineData(3, "3_incorrect", Skip = "For example of skipped use case")]
        [InlineData(4, "4_incorrect")]
        public void TestMethod(int i, string expected)
        {
            //Act
            var actual = i.ToString(CultureInfo.InvariantCulture);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1, 1, "1_incorrect")]
        [InlineData(2)]
        [InlineData(3, 3)]
        public void TestMethodWithDefaultArgs(int i, int multiplier = 1, string expected = "")
        {
            //Act
            var actual = (i * multiplier).ToString(CultureInfo.InvariantCulture);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1, 2, "1_incorrect", -2)]
        [InlineData(2)]
        [InlineData(3, 3)]
        public void TestMethodWithMultipleExpected(int i, int multiplier = 1, string expected = "", int expectedInt = -1)
        {
            //Act
            var actualInt = i * multiplier;
            var actual = actualInt.ToString(CultureInfo.InvariantCulture);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            actualInt.Should().Be(expectedInt);
        }

        [Theory]
        //Positive int means true, negative means false.
        //Use different int (not bool) values for each use case to distinguish them after StormPetrel rewrites.
        [InlineData(1, 1, 1, 1)]
        [InlineData(2, 2, 2, -2)]
        [InlineData(3, 3, -3, 3)]
        [InlineData(4, 4, -4, -4)]
        [InlineData(5, -5, 5, 5)]
        [InlineData(6, -6, 6, -6)]
        [InlineData(7, -7, -7, 7)]
        [InlineData(8, -8, -8, -8)]
        [InlineData(-9, 9, 9, 9)]
        [InlineData(-10, 10, 10, -10)]
        [InlineData(-11, 11, -11, 11)]
        [InlineData(-12, 12, -12, -12)]
        [InlineData(-13, -13, 13, 13)]
        [InlineData(-14, -14, 14, -14)]
        [InlineData(-15, -15, -15, 15)]
        [InlineData(-16, -16, -16, -16)]
        public void WhenTwoActualExpectedPairsThenGeneratorRewritesExpectedForEachFailedUseCaseTest(int actualProto1, int expected1, int actualProto2, int expected2)
        {
            //Act
            var actual1 = actualProto1;
            var actual2 = actualProto2;

            //Assert
            actual1.Should().Be(expected1);
            actual2.Should().Be(expected2);
        }

        [Theory]
        [InlineData(1, "1_incorrect")]
        public void TestMethodWithParameterAttribute(int i, string expected, [CallerMemberName] string callerMemberName = "")
        {
            //Act
            var actual = i.ToString(CultureInfo.InvariantCulture);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            callerMemberName.Should().BeEmpty();
        }
    }
}
