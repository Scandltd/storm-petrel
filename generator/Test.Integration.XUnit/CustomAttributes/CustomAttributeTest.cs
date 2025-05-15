using FluentAssertions;

namespace Test.Integration.XUnit.CustomAttributes;

/// <summary>
/// Prerequisites:
/// - Configure SCAND_STORM_PETREL_GENERATOR_CONFIG environment variable similar to build/build.ps1 file
/// so that its value is available in Incremental Generators process via `Environment.GetEnvironmentVariable("SCAND_STORM_PETREL_GENERATOR_CONFIG")` call of Scand.StormPetrel.Generator code.
/// </summary>
public class CustomAttributeTest
{
    [CustomFact]
    public void CustomFactAttributeTest()
    {
        //Arrange
        var expected = new AddResult
        {
            Value = 5,
            ValueAsHexString = "0x4"
        };

        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [CustomTheory]
    [InlineData(1, 5)]
    public void CustomTheoryAttributeTest(int a, int b)
    {
        //Arrange
        var expected = new AddResult();

        //Act
        var actual = Calculator.Add(a, b);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [CustomTheory]
    [CustomInlineData(1, 5)]
    public void CustomInlineDataAttribute(int a, int b)
    {
        //Arrange
        var expected = new AddResult();

        //Act
        var actual = Calculator.Add(a, b);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [CustomClassData(typeof(TestCaseSourceCustomClassData))]
    public void CustomClassDataAttributeTest(int x, int y, int expected)
    {
        //Act
        var actual = Calculator.Add(x, y).Value;

        //Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [CustomMemberData(nameof(DataMethod))]
    public void CustomMemberDataAttributeTest(int x, int y, AddResult expected)
    {
        //Act
        var actual = Calculator.Add(x, y);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<object[]> DataMethod() =>
    [
        [1, 2, new AddResult()]
    ];
}
