using FluentAssertions;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using Test.Integration.Shared;
using Test.Integration.WinFormsApp;

namespace Test.Integration.WinFormsAppTest;

public sealed class WinFormsTest
{
    [Fact]
    public static void CompareWinFormTest()
    {
        //Arrange
        var expectedFileBytesSnapshot = SnapshotProvider.ReadAllBytes();
        using var form = new TestForm();
        using var memoryStream = form.ToStream();

        //Act
        var actualFileBytesSnapshot = memoryStream.ToArray();

        //Assert
        actualFileBytesSnapshot.Should().Equal(expectedFileBytesSnapshot);
    }

    [Fact]
    public static void CompareWinFormJsonTest()
    {
        //Arrange
        var expectedFileJsonSnapshot = SnapshotProvider.ReadAllText();
        using var form = new TestForm();

        //Act
        var actualFileJsonSnapshot = form.ToJsonText();

        //Assert
        actualFileJsonSnapshot.Should().Be(expectedFileJsonSnapshot);
    }

    [Theory]
    [InlineData("300x400", 300, 400)]
    [InlineData("600x800", 600, 800)]
    public static void CompareWinFormSizeTest(string useCaseId, int height, int width)
    {
        //Arrange
        var expectedFileBytesSnapshot = SnapshotProvider.ReadAllBytes(useCaseId);
        using var form = new TestForm()
        {
            Height = height,
            Width = width
        };
        using var memoryStream = form.ToStream();

        //Act
        var actualFileBytesSnapshot = memoryStream.ToArray();

        //Assert
        actualFileBytesSnapshot.Should().BeEquivalentTo(expectedFileBytesSnapshot);
    }
}