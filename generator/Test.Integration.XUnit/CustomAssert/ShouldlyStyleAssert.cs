namespace Test.Integration.XUnit.CustomAssert;

internal static class ShouldlyStyleAssert
{
    public static void ShouldBe<T>(this T actual, T expected)
    {
        actual.ShouldBe(expected, false);
    }

    public static void ShouldBe<T>(this T actual, T expected, bool useShortMessage)
    {
        //Use Equals as an example of custom assertion
        if (!Equals(actual, expected))
        {
            throw new Xunit.Sdk.XunitException(useShortMessage ? $"{actual} not equal {expected} " : $"Expected: {expected}, Actual: {actual}");
        }
    }
}
