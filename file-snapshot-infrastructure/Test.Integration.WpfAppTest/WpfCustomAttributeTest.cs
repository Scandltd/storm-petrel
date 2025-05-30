using FluentAssertions;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using Test.Integration.WpfApp;

namespace Test.Integration.WpfAppTest;

/// <summary>
/// Prerequisites:
/// 1) Scand.StormPetrel.Generator NuGet package version should be > 2.3.0
/// 2) Add or Update SCAND_STORM_PETREL_GENERATOR_CONFIG environment variable with value:
/// {"CustomTestAttributes":[{"TestFrameworkKindName":"XUnit","FullName":"Xunit.UIFactAttribute","KindName":"Test"},{"TestFrameworkKindName":"XUnit","FullName":"Xunit.UITheoryAttribute","KindName":"Test"}]}
/// </summary>
public class WpfCustomAttributeTest
{
    [UIFact]
    public static void StaFactCompareWindowTest()
    {
        //Arrange
        var expectedFileBytesSnapshot = SnapshotProvider.ReadAllBytes();

        //Act
        var window = new MainWindow(); //emulate method call creating MainWindow
        var actualFileBytesSnapshot = window.ToStream().ToArray();

        //Assert
        actualFileBytesSnapshot.Should().Equal(expectedFileBytesSnapshot);
    }

    [UITheory]
    [InlineData("300x400", 300, 400)]
    [InlineData("600x800", 600, 800)]
    public void CompareWindowSizeTest(string useCaseId, int height, int width)
    {
        //Arrange
        var expectedFileBytesSnapshot = SnapshotProvider.ReadAllBytes(useCaseId);

        //Act
        var window = new MainWindow() //emulate method call creating MainWindow
        {
            Height = height,
            Width = width
        };
        var actualFileBytesSnapshot = window.ToStream().ToArray();

        //Assert
        actualFileBytesSnapshot.Should().BeEquivalentTo(expectedFileBytesSnapshot);
    }
}
