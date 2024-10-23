﻿using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit
{
    public class AttributesTest
    {
        [Theory]
        [InlineData(1, "1_incorrect")]
        [InlineData(2, "2")]
        [InlineData(3, "3_incorrect")]
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
    }
}
