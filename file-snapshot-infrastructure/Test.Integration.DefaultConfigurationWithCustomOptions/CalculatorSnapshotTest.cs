using FluentAssertions;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using Test.Integration.Shared;

namespace Test.Integration.DefaultConfigurationWithCustomOptions;

public class CalculatorSnapshotTest
{
    [Fact]
    public static void AddTest()
    {
        var expectedFileJsonSnapshot = SnapshotProvider.ReadAllText();

        //Act
        var actualFileJsonSnapshot = Calculator
                                        .Add(2, 2)
                                        .ToJsonText();

        //Assert
        actualFileJsonSnapshot.Should().Be(expectedFileJsonSnapshot);
    }
}