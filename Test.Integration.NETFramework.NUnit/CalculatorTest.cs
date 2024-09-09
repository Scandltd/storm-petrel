using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace Test.Integration.NETFramework.NUnit
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void AddTest()
        {
            //Arrange
            // incorrect `expected` baseline value will be overwritten with correct `actual` value
            // after manual execution of auto-generated AddTestStormPetrel test.
            var expected = new AddResult
            {
                Value = 5, //incorrect value example
                ValueAsHexString = "0x5"
            };

            //Act
            var actual = Calculator.Add(2, 2);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }

    public static class Calculator
    {
        public static AddResult Add(int a, int b)
        {
            var result = a + b;
            return new AddResult
            {
                Value = result,
                ValueAsHexString = "0x" + result.ToString("x", CultureInfo.InvariantCulture),
            };
        }
    }

    public class AddResult
    {
        public int Value { get; set; }
        public string ValueAsHexString { get; set; } = string.Empty;
    }
}