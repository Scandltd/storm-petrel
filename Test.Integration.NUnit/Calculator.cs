using System.Globalization;

namespace Test.Integration.NUnit
{
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
