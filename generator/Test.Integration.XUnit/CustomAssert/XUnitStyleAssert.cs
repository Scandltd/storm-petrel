using System.Diagnostics.CodeAnalysis;

namespace Test.Integration.XUnit.CustomAssert;

internal static class XUnitStyleAssert
{
    public static void Equal<T>([AllowNull] T expected, [AllowNull] T actual) =>
        //Use Assert.Equal as an example of custom assertion
        Assert.Equal(expected, actual);

    public static void Equal<T>([AllowNull] T expected, [AllowNull] T actual, [AllowNull] string errorBannerAsCustomParameterExample)
    {
        try
        {
            //Use Assert.Equal as an example of custom assertion
            Assert.Equal(expected, actual);
        }
        catch (Xunit.Sdk.EqualException)
        {
            //Throw an exception to demonstrate custom result
            throw Xunit.Sdk.EqualException.ForMismatchedValuesWithError(expected, actual, banner: errorBannerAsCustomParameterExample);
        }
    }
    public static void Equivalent(AddResult expected, AddResult actual)
    {
        //Assert necessary AddResult properties only
        Assert.True(expected == null && actual == null
                        || expected != null && actual != null, "Both actual/expected must be null or not null");
        Assert.Equal(expected?.Value, actual?.Value);
        Assert.Equal(expected?.ValueAsHexString, actual?.ValueAsHexString);
    }
}
