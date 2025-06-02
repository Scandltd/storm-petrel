//Not implemented via xRetry for xUnit v3. Wait for implementation of xUnit v3 support: https://github.com/JoshKeegan/xRetry/issues/265
#if !IS_XUNIT_V3
using FluentAssertions;
using System.Globalization;
using xRetry;

namespace Test.Integration.XUnit;

public class RetryTest
{
    private static volatile int TestRetryFactCallNumber;
    private static volatile int TestRetryTheoryCallNumber;

    [RetryFact(2, 1)]
    public void TestRetryFact()
    {
        TestRetryFactCallNumber++;
        if (TestRetryFactCallNumber == 1)
        {
            throw new InvalidOperationException("Throw to retry the test");
        }

        //Arrange
        int expected = 123;

        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        actual.Should().Be(expected);
    }

    [RetryTheory]
    [InlineData(1, "incorrect 1")]
    [InlineData(2, "incorrect 2")]
    public void TestRetryTheory(int actualInt, string expectedString)
    {
        TestRetryTheoryCallNumber++;
        if (TestRetryTheoryCallNumber == 1)
        {
            throw new InvalidOperationException("Throw to retry the test");
        }

        //Arrange
        //Act
        var actualString = actualInt.ToString(CultureInfo.InvariantCulture); //emulate method being tested call

        //Assert
        actualString.Should().Be(expectedString);
    }
}
#endif
