using FluentAssertions;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using Test.Integration.WpfApp;

namespace Test.Integration.WpfAppTest;

public class WpfTest
{
    [Fact]
    public async Task CompareWindowTest()
    {
        //Arrange
        var expectedFileBytesSnapshot = SnapshotProvider.ReadAllBytes();
        MemoryStream? memoryStream = null;

        //Act
        await WindowExtensions.StartSTATask(() =>
        {
            var window = new MainWindow();
            memoryStream = window.ToStream();
        });

        var actualFileBytesSnapshot = memoryStream?.ToArray();
        memoryStream?.Dispose();

        //Assert
        actualFileBytesSnapshot.Should().Equal(expectedFileBytesSnapshot);
    }

    [Fact]
    public async Task CompareWindowXamlTest()
    {
        //Arrange
        var expectedXaml = SnapshotProvider.ReadAllText();
        string? mainWindowXaml = null;

        //Act
        await WindowExtensions.StartSTATask(() =>
        {
            var window = new MainWindow();
            mainWindowXaml = window.ToXaml();
        });

        var actualXaml = mainWindowXaml;

        //Assert
        actualXaml.Should().Be(expectedXaml);
    }

    [Theory]
    [InlineData("300x400", 300, 400)]
    [InlineData("600x800", 600, 800)]
    public async Task CompareWindowSizeTest(string useCaseId, int height, int width)
    {
        //Arrange
        var expectedFileBytesSnapshot = SnapshotProvider.ReadAllBytes(useCaseId);
        MemoryStream? memoryStream = null;

        //Act
        await WindowExtensions.StartSTATask(() =>
        {
            var window = new MainWindow()
            {
                Height = height,
                Width = width
            };
            memoryStream = window.ToStream();
        });

        //Act
        var actualFileBytesSnapshot = memoryStream?.ToArray();
        memoryStream?.Dispose();

        //Assert
        actualFileBytesSnapshot.Should().BeEquivalentTo(expectedFileBytesSnapshot);
    }
}