using System.Globalization;
using FluentAssertions;

namespace Test.Integration.Generator.Utils.XUnit
{
    public class BaseTest
    {
        [Fact]
        public void CollectionExpressionTest()
        {
            var expected = new AddResult
            {
                Values = new List<int>() { 5 }, //incorrect value example
                ValueAsHexString = "0x5",
                Hashes = Array.Empty<string>(),
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
            };
        }
    }

    internal sealed class AddResult
    {
        public List<int> Values { get; set; } = [];
        public string ValueAsHexString { get; set; } = string.Empty;
        public string[] Hashes { get; set; } = [];
    }
}