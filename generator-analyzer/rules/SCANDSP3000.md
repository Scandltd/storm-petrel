# SCANDSP3000

*"Test data source method design unsuitable for baseline updates"*

⚠️ Warning by default

## Cause

Storm Petrel Generator cannot detect an appropriate structure in the test data source method to update baselines.

## Reason for rule

If Storm Petrel Generator cannot detect an appropriate structure in the test data source method to update baselines, the developer cannot update baselines via the auto-generated `StormPetrel` test.

## How to fix violations

Adjust your test method according to the [Test data source attribute](../../generator/README.md#test-data-source-attribute) examples.

## Examples

The following examples are based on [IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest](../Test.Integration.Analyzer/IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest.cs) and similar tests for NUnit and MSTest frameworks.
See more examples in the [IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest.MainTestData](../Scand.StormPetrel.Generator.Analyzer.Test/IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest.MainTestData.cs) unit test data.

### Violates

```csharp
using Xunit;
public class TestClass
{
    public static object[][] DataMethod() =>
        throw new NotImplementedException("Throwing an exception is an example of a data source method structure that Storm Petrel cannot detect for baseline updates");

    [Theory]
    [MemberData(nameof(DataMethod))]
    public void Test(int input, int expected)
    {
        //Act
        var actual = input % 3; // Emulate action
        //Assert
        Assert.Equal(expected, actual);
    }
}
```

### Does not violate

```csharp
using Xunit;
public class TestClass
{
    public static object[][] DataMethod() =>
    [
        [1, 1],
        [5, 555],
    ];

    [Theory]
    [MemberData(nameof(DataMethod))]
    public void Test(int input, int expected)
    {
        //Act
        var actual = input % 3; // Emulate action
        //Assert
        Assert.Equal(expected, actual);
    }
}
```
