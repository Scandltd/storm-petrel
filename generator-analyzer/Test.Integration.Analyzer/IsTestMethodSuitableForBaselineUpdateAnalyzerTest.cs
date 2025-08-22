namespace Test.Integration.Analyzer;
using Assert = Xunit.Assert;

public class IsTestMethodSuitableForBaselineUpdateAnalyzerTest
{
    [Fact]
    public void WhenTestIsNotSuitableForUpdatesThenWarningTest()
    {
    }

    [Fact]
    public void WhenTestWithVariablesIsNotSuitableForUpdatesThenWarningTest()
    {
        //The test is not suitable for Storm Petrel updates because both:
        // - `actualNotMatchingToExpectedForPairing` is not a pair for `expected` variable according to current configuration.
        // - `expected.ToString()` is not variable or parameter identifier in `Assert.Equal` assertion.
        //Arrange
        var expected = 4;
        //Act
        var actualNotMatchingToExpectedForPairing = "5"; //emulate the Act
        //Assert
        Assert.Equal(expected.ToString(), actualNotMatchingToExpectedForPairing);
    }

    [Fact]
    public void WhenTestIsSuitableForUpdatesThenNoWarningTest()
    {
        //Arrange
        //Act
        var actual = 2 + 2; //emulate
        //Assert
        Assert.Equal(4, actual);
    }
}
