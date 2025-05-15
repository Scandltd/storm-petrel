using FluentAssertions;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using System.Xml.Linq;
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
        //Do not call the property in real test projects. Use Scand.StormPetrel.Generator package version greater than "2.3.0" instead.
        if (IsStormPetrelDevelopmentRepositoryWithUnsupportedCustomAttributes)
        {
            return;
        }

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
        //Do not call the property in real test projects. Use Scand.StormPetrel.Generator package version greater than "2.3.0" instead.
        if (IsStormPetrelDevelopmentRepositoryWithUnsupportedCustomAttributes)
        {
            return;
        }

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

    /// <summary>
    /// Do not call the property in real test projects. Use Scand.StormPetrel.Generator package version greater than "2.3.0" instead.
    /// </summary>
    private static bool IsStormPetrelDevelopmentRepositoryWithUnsupportedCustomAttributes
        => GetStormPetrelNuGetPackageVersion() <= new Version("2.3.0");

    private static Version GetStormPetrelNuGetPackageVersion()
    {
        string? projectDir = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
        ArgumentNullException.ThrowIfNull(projectDir);
        string csprojPath = Path.Combine(projectDir, "Test.Integration.WpfAppTest.csproj");
        var xml = XDocument.Load(csprojPath);
        var packageRef = xml
                            .Descendants("PackageReference")
                            .Single(x => x.Attribute("Include")?.Value == "Scand.StormPetrel.Generator");
        var version = packageRef?.Attribute("Version")?.Value;
        ArgumentNullException.ThrowIfNull(version);
        return new Version(version);
    }
}
