using FluentAssertions;
using NUnit;
using Test.Integration.Shared;

namespace Test.Integration.NUnit.CustomAttributes;

/// <summary>
/// Prerequisites:
/// - Configure SCAND_STORM_PETREL_GENERATOR_CONFIG environment variable similar to build/build.ps1 file
/// so that its value is available in Incremental Generators process via `Environment.GetEnvironmentVariable("SCAND_STORM_PETREL_GENERATOR_CONFIG")` call of Scand.StormPetrel.Generator code.
/// </summary>
public class CustomAttributeTest
{
    [CustomTest]
    public void CustomTestAttributeTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.That(actual, Is.EqualTo(123));
        BackupHelper.DeleteBackupWithResultAssertion(GetType());
    }

    [CustomTestCase(2)]
    public void CustomTestCaseAttributeTest(int a)
    {
        //Arrange
        var expected = new AddResult();

        //Act
        var actual = Calculator.Add(a, a);

        //Assert
        actual.Should().BeEquivalentTo(expected);
        BackupHelper.DeleteBackupWithResultAssertion(GetType());
    }

    [CustomTestCaseSource(nameof(DataMethod))]
    public void CustomTestCaseSourceTest(int x, int y, AddResult expected)
    {
        //Act
        var actual = Calculator.Add(x, y);

        //Assert
        actual.Should().BeEquivalentTo(expected);
        BackupHelper.DeleteBackupWithResultAssertion(GetType());
    }

    private static IEnumerable<object[]> DataMethod() =>
    [
        [1, 2, new AddResult()],
        [-2, 2, new AddResult()]
    ];
}