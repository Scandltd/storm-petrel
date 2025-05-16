using FluentAssertions;
using FluentAssertions.Equivalency;
using System.Globalization;

namespace Test.Integration.Generator.Utils.XUnit
{
    public class BaseTest
    {
        [Fact]
        public void DumperFactoryDecoratorsTest()
        {
            AddResult expected = new()
            {
                Values = new List<int>() { 5 }, //incorrect value example
                ValueAsHexString = "0x5",
                Hashes = Array.Empty<string>(),
                SomeObject = new object(),
                Result = new()
                {
                    Sum = 5,
                },
                ResultStruct = new CalculatorResultStruct(5)
            };

            //Act
            var actual = Calculator.Add(2, 2);

            //Assert
            actual.Should().BeEquivalentTo(expected, GetOptions);
        }

        private static EquivalencyOptions<AddResult> GetOptions(EquivalencyOptions<AddResult> options)
            => options
                    .Excluding(x => x.StringPropertyIgnored)
                    .Excluding(x => x.Path == nameof(AddResult.GuidPropertyIgnoredForAllTypes)
                                        || x.Path.EndsWith($".{nameof(AddResult.GuidPropertyIgnoredForAllTypes)}"));
    }

    internal static class Calculator
    {
        public static AddResult Add(int a, int b)
        {
            var result = a + b;
            var doubleResult = 2 * result;
            return new AddResult
            {
                Values = [result],
                ValueAsHexString = "0x" + result.ToString("x", CultureInfo.InvariantCulture),
                //2 lines x 100 times repeated result
                ValueAsVerbatimString = string.Join("\r\n", Enumerable.Repeat(string.Join(", ", Enumerable.Repeat(result, 100)), 2)),
                Result = new()
                {
                    Sum = result,
                },
                ResultStruct = new CalculatorResultStruct(result),
                ValueDate = new DateTime(2000 + result, ToMonth(result), ToDayInMonth(result)),
                DoubleValueDate = new DateTime(2000 + doubleResult, ToMonth(doubleResult), ToDayInMonth(doubleResult)),
            };
        }

        private static int ToMonth(int v) => (v % 12) == 0 ? 12 : (v % 12);
        private static int ToDayInMonth(int v) => (v % 28) == 0 ? 28 : (v % 28);
    }

    internal sealed class AddResult
    {
        public List<int> Values { get; set; } = [];
        public string ValueAsHexString { get; set; } = string.Empty;
        public string ValueAsVerbatimString { get; set; } = string.Empty;
        public string[] Hashes { get; set; } = [];
        public object SomeObject { get; set; } = new();
        public CalculatorResult? Result { get; set; }
        public CalculatorResultStruct ResultStruct { get; set; }
        /// <summary>
        /// Vary this property value for each instance.
        /// </summary>
        public string? StringPropertyIgnored { get; set; } = Guid.NewGuid().ToString();
        public Guid GuidPropertyIgnoredForAllTypes { get; set; } = Guid.NewGuid();
        public DateTime ValueDate { get; set; }
        public DateTime DoubleValueDate { get; set; }
    }

    internal sealed class CalculatorResult()
    {
        public int Sum { get; set; }
        public Guid GuidPropertyIgnoredForAllTypes { get; set; } = Guid.NewGuid();
    }

    internal struct CalculatorResultStruct(int sum)
    {
        public int Sum { get; set; } = sum;
    }
}