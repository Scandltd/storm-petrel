using FluentAssertions;

namespace Test.Integration.XUnit;

/// <summary>
/// (actualResult, expected) names used in this class tests do not match "TestVariablePairConfigs" regex values.
/// It allows Storm Petrel to analyze invocation paths of the `expected` variable/argument in the assertion expressions.
/// Other variable names can be used here instead of (actualResult, expected) pair or even an expression can be used instead of `actualResult` variable.
/// </summary>
public sealed class AssertInvocationPathTest
{
    [Fact]
    public void WhenAssertIndividualExpectedPropertiesOfLocalVarDeclarationThenShouldUpdateThemTest()
    {
        //Arrange
        var expected = new AddResult
        {
            Value = 5,
            ValueAsHexString = "0x5 - Incorrect",
        };
        //Act
        var actualResult = Calculator.Add(2, 2);
        //Assert
        actualResult.Value.Should().Be(expected.Value);
        actualResult.ValueAsHexString.Should().Be(expected.ValueAsHexString);
        // The following assertion will not update Length property or field in the baseline because it is not explicitely mentioned in expected assignment above.
        // So, we do not generate Length property because its actual semantic may be "getter only".
        actualResult.ValueAsHexString.Length.Should().Be(expected.ValueAsHexString.Length);
        // The following assertions will not update the type of actualResult in the baseline because expected expressions contain a method call.
        actualResult.GetType().Should().Be(expected.GetType());
        actualResult.GetType().FullName.Should().Be(expected.GetType().FullName);
    }
    [Fact]
    public void WhenAssertIndividualExpectedPropertiesOfLocalVarAssignmentThenShouldUpdateThemTest()
    {
        //Arrange
        AddResult expected;
        expected = new AddResult()
        {
            Value = 5,
            ValueAsHexString = "0x5 - Incorrect",
        };
        //Act
        var actualResult = Calculator.Add(2, 2);
        //Assert
        actualResult.Value.Should().Be(expected.Value);
        actualResult.ValueAsHexString.Should().Be(expected.ValueAsHexString);
    }
    [Theory]
    [MemberData(nameof(TheoryDataSource))]
    public static void WhenTheoryDataSourceThenShouldUpdateItTest(int a, int b, AddResult? expected)
    {
        ArgumentNullException.ThrowIfNull(expected);
        //Arrange
        //Act
        var actualResult = Calculator.Add(a, b);
        //Assert
        actualResult.Value.Should().Be(expected!.Value);
        actualResult.ValueAsHexString.Length.Should().Be(expected.ValueAsHexString.Length);
        actualResult.ValueAsHexString.Should().Be(expected?.ValueAsHexString!);
    }
    [Theory]
    [MemberData(nameof(TheoryDataSourceForNoActualVariable))]
    public static void WhenAnExpressionInsteadOfActualVariableThenShouldUpdateItTest(int a, int b, AddResult? myExp)
    {
        ArgumentNullException.ThrowIfNull(myExp);
        Calculator.Add(a, b).Value.Should().Be(myExp!.Value);
        Calculator.Add(a, b).ValueAsHexString.Should().Be(myExp?.ValueAsHexString!);
    }
    public static TheoryData<int, int, AddResult> TheoryDataSource =>
    new()
    {
        {
            2,
            2,
            new AddResult()
            {
                Value = 5,
                ValueAsHexString = "0x5 - Incorrect",
            }
        },
        {
            -2,
            10,
            new()
            {
                Value = 5,
                ValueAsHexString = "0x5 - Incorrect",
            }
        },
        {
            -2,
            -2,
            new AddResult
            {
                Value = 5,
                ValueAsHexString = "0x5 - Incorrect",
            }
        },
    };
    public static TheoryData<int, int, AddResult> TheoryDataSourceForNoActualVariable =>
    new()
    {
        {
            2,
            2,
            new AddResult()
            {
                Value = 5,
                ValueAsHexString = "0x5 - Incorrect",
            }
        },
    };
}
