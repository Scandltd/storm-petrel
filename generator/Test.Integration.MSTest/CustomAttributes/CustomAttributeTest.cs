using FluentAssertions;
using MSTest;

namespace Test.Integration.MSTest.CustomAttributes;

/// <summary>
/// Prerequisites:
/// - Configure SCAND_STORM_PETREL_GENERATOR_CONFIG environment variable similar to build/build.ps1 file
/// so that its value is available in Incremental Generators process via `Environment.GetEnvironmentVariable("SCAND_STORM_PETREL_GENERATOR_CONFIG")` call of Scand.StormPetrel.Generator code.
/// </summary>
[TestClass]
public class CustomAttributeTest
{
    [CustomTestMethod]
    public void AddTestMethodAttributeTest()
    {
        //Arrange
        var expected = new AddResult
        {
            Value = 5,
            ValueAsHexString = "0x5"
        };

        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [CustomTestMethod]
    [CustomDataRow(2)]
    public void CustomDataRowAttributeTest(int a)
    {
        //Arrange
        var expected = new AddResult();

        //Act
        var actual = Calculator.Add(a, a);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
}