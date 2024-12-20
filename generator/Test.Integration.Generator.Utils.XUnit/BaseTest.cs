using System.Globalization;
using FluentAssertions;

namespace Test.Integration.Generator.Utils.XUnit
{
    public class BaseTest
    {
        [Fact]
        public void CollectionExpressionTest()
        {
            AddResult expected = new()
            {
                Values = new List<int>() { 5 }, //incorrect value example
                ValueAsHexString = "0x5",
                Hashes = Array.Empty<string>(),
                SomeObject = new object(),
                Result = new CalculatorResult(5)
            };

            //Act
            var actual = Calculator.Add(2, 2);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }

    internal static class Calculator
    {
        public static AddResult Add(int a, int b)
        {
            var result = a + b;
            return new AddResult
            {
                Values = [result],
                ValueAsHexString = "0x" + result.ToString("x", CultureInfo.InvariantCulture),
                Result = new CalculatorResult(result)
            };
        }
    }

    internal sealed class AddResult
    {
        public List<int> Values { get; set; } = [];
        public string ValueAsHexString { get; set; } = string.Empty;
        public string[] Hashes { get; set; } = [];
        public object SomeObject { get; set; } = new();
        public CalculatorResult Result { get; set; }
    }

    internal struct CalculatorResult(int sum)
    {
        public int Sum { get; set; } = sum;
    }
}