namespace Test.Integration.XUnit.CustomAssert;

internal static class ShouldStyleAssert
{
    public static AssertionWrapper<T> Should<T>(this T actual)
    {
        return new AssertionWrapper<T>(actual);
    }
}


public class AssertionWrapper<T>(T actual)
{
    private readonly T _actual = actual;

    public void Be(T expected)
    {
        Be(expected, false);
    }

    public void Be(T expected, bool useShortMessage)
    {
        //Use Equals as an example of custom assertion
        if (!Equals(_actual, expected))
        {
            throw new Xunit.Sdk.XunitException(useShortMessage ? $"{_actual} not equal {expected} " : $"Expected: {expected}, Actual: {_actual}");
        }
    }
    public void BeEquivalentTo(T expected)
    {
        //Assert necessary AddResult properties only
        if (typeof(T) != typeof(AddResult))
        {
            throw new Xunit.Sdk.XunitException($"Not supported type: {typeof(T).FullName}");
        }
        var actualCasted = _actual as AddResult;
        var expectedCasted = expected as AddResult;
        Assert.True(expectedCasted == null && actualCasted == null
                        || expectedCasted != null && actualCasted != null, "Both actual/expected must be null or not null");
        Assert.Equal(expectedCasted?.Value, actualCasted?.Value);
        Assert.Equal(expectedCasted?.ValueAsHexString, actualCasted?.ValueAsHexString);
    }
}
