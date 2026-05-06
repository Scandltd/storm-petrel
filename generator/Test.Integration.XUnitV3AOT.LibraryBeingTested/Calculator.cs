using System.Globalization;

namespace Test.Integration.XUnitV3AOT.LibraryBeingTested;

public static class Calculator
{
    public static AddResult Add(int a, int b)
    {
        var result = a + b;
        return new AddResult
        {
            Value = result,
            ValueAsHexString = "0x" + result.ToString("x", CultureInfo.InvariantCulture),
            NestedInfo = new()
            {
                IsEven = result % 2 == 0,
                IsPositive = result > 0,
                Description = $"The sum of {a} and {b} is {result}."
            }
        };
    }
    public static AddResultNoReflectionDump AddNoReflectionDump(int a, int b)
    {
        var result = a + b;
        return new AddResultNoReflectionDump
        {
            Value = result,
            ValueAsHexString = "0x" + result.ToString("x", CultureInfo.InvariantCulture),
        };
    }
}
