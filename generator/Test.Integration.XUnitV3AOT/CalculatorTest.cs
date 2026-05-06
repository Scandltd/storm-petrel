using FluentAssertions;
using Test.Integration.XUnitV3AOT.LibraryBeingTested;

namespace Test.Integration.XUnitV3AOT;

public class CalculatorTest
{
    [Fact]
    public void AddTest()
    {
        //Arrange
        // incorrect `expected` baseline value will be overwritten with correct `actual` value
        // after manual execution of auto-generated AddTestStormPetrel test.
        var expected = new AddResult
        {
            Value = 5, //incorrect value example
            ValueAsHexString = "0x5"
        };

        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="expected"><see cref="AddResultNoReflectionDump"/> type is used as an example here.
    /// Optionally use <see cref="AddResult"/> and reflection based assertion. See more details in <see cref="Dumper"/> implementation and comments.</param>
    [Theory]
    [MemberData(nameof(TheoryDataWithComplexExpectedObject))]
    public static void AddTestWhenTheoryDataWithComplexExpectedObject(int a, int b, AddResultNoReflectionDump expected)
    {
        // Arrange
        // Act
        var actual = Calculator.AddNoReflectionDump(a, b);
        // Assert
        actual.Value.Should().Be(expected.Value);
        actual.ValueAsHexString.Should().Be(expected.ValueAsHexString);
    }
    [Theory]
    [MemberData(nameof(TheoryDataRowArray))]
    public static void AddTestWhenTheoryDataRowIsArray(int a, int b, AddResult expected)
    {
        //Act
        var actual = Calculator.Add(a, b);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
    [Theory]
    [InlineData(1, 2, -2, "1_incorrect")]
    [InlineData(2), InlineData(5)]
    [InlineData(3, 3)]
    public static void AddTestWhenInlineDataAttribute(int a, int b = -1, int expectedValue = -2, string expectedHexString = "")
    {
        //Act
        var actual = Calculator.Add(a, b);

        //Assert
        actual.Value.Should().Be(expectedValue);
        actual.ValueAsHexString.Should().Be(expectedHexString);
    }
    [Theory]
    [ClassData(typeof(CalculatorTestTheoryDataSource))]
    public static void AddTestWhenClassData(int x, int y, AddResult expected)
    {
        //Act
        var actual = Calculator.Add(x, y);
        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
    public static TheoryData<int, int, AddResultNoReflectionDump> TheoryDataWithComplexExpectedObject =>
    new()
    {
        {
            2,
            2,
            new()
            {
                Value = 5,
                ValueAsHexString = "0x5 Incorrect",
            }
        },
        {
            -5,
            -7,
            new()
            {
                Value = 5,
                ValueAsHexString = "0x5 Incorrect",
            }
        },
    };
    public static IEnumerable<TheoryDataRow<int, int, AddResult>> TheoryDataRowArray =>
    [
        new(1, 2, new()),
        new(
            0,
            0,
            new()
        ),
        new(-1, -2, new()),
    ];
}
