# SCANDSP2000

*"Scand Storm Petrel cannot detect baselines to update in test method"*

⚠️ Warning by default

## Cause

Storm Petrel Generator cannot detect baselines to update in test method for current configuration.

## Reason for rule

If Storm Petrel Generator cannot detect baselines to update in test method then developer cannot update the baselines via auto-generated `StormPetrel` test.

## How to fix violations

Adjust your test method according to [Storm Petrel configuration](../../generator/README.md#optional-json-file):
- Add/update actual-expected variable/parameter pair(s) to match the configuration.
- Or properly add/update assertion expression(s).

## Examples

The following examples are based on [IsTestMethodSuitableForBaselineUpdateAnalyzerTest](../Test.Integration.Analyzer/IsTestMethodSuitableForBaselineUpdateAnalyzerTest.cs) integration tests.
See more examples in [IsTestMethodSuitableForBaselineUpdateAnalyzerTest.MainTestData](../Scand.StormPetrel.Generator.Analyzer.Test/IsTestMethodSuitableForBaselineUpdateAnalyzerTest.MainTestData.cs) unit test data.

### Violates

The test method below is not suitable for Storm Petrel updates because both:
- `actualNotMatchingToExpectedForPairing` is not a pair for `expected` variable according to default configuration.
- `expected.ToString()` expression is not a variable/parameter identifier or other supported expression to update in `Assert.Equal(...)` assertion.

```csharp
using Xunit;
public class TestClass
{
    [Fact]
    public void TestMethod()
    {
        //Arrange
        var expected = 4;
        //Act
        var actualNotMatchingToExpectedForPairing = "5"; //emulate Act step
        //Assert
        Assert.Equal(expected.ToString(), actualNotMatchingToExpectedForPairing);
    }
}
```

### Does not violate

```csharp
using Xunit;
public class TestClass
{
    [Fact]
    public void TestMethod()
    {
        //Arrange
        var expected = 4;
        //Act
        var actual = "5"; //emulate Act step
        //Assert
        Assert.Equal(expected.ToString(), actual);
    }
}
```

```csharp
using Xunit;
public class TestClass
{
    [Fact]
    public void TestMethod()
    {
        //Arrange
        var expected = 4;
        //Act
        var actualNotMatchingToExpectedForPairing = "5"; //emulate Act step
        //Assert
        Assert.Equal(expected, int.Parse(actualNotMatchingToExpectedForPairing));
    }
}
```

```csharp
using Xunit;
public class TestClass
{
    [Fact]
    public void TestMethod()
    {
        //Act
        var actualNotMatchingToExpectedForPairing = "5"; //emulate Act step
        //Assert
        Assert.Equal(4, int.Parse(actualNotMatchingToExpectedForPairing));
    }
}
```
