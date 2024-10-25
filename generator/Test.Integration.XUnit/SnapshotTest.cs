using FluentAssertions;

namespace Test.Integration.XUnit;

public class SnapshotTest
{
    [Fact]
    public void WhenSnapshotIsJsonTest()
    {
        //Arrange
        var expectedJson =
@"{
    ""propertyA"": ""valueA_Incorrect"",
    ""propertyB"": ""valueB"",
    ""childArray_incorrect"":
    [
        'incorrect', 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79...
    ]
}";

        //Act
        //Use more than 256 characters string in the test because VarDump represents 256+ and shorter strings differently.
        //See less than 256 character string examples in other tests.
        var actualJson = SnapshotProviderBeingTested.JsonMoreThan256Chars;

        //Assert
        actualJson.Should().Be(expectedJson);
    }

    [Theory]
    [MemberData(nameof(HtmlSnapshotTheoryDataProperty))]
    public void WhenSnapshotIsHtmlViaMemberDataTest(string siteName, string expectedHtml)
    {
        //Act
        var actualHtml = SnapshotProviderBeingTested.Html(siteName);

        //Assert
        actualHtml.Should().Be(expectedHtml);
    }

    [Theory]
    [MemberData(nameof(ByteSnapshots))]
    public void WhenSnapshotIsByteArray(byte byteUseCase, byte[] expectedBytes)
    {
        //Act
        var actualBytes = SnapshotProviderBeingTested.Bytes(byteUseCase);

        //Assert
        actualBytes.Should().Equal(expectedBytes);
    }

    public static TheoryData<string, string> HtmlSnapshotTheoryDataProperty =>
    new()
    {
        {
            "Short Site Name",
            "Incorrect Expected Html"
        },
        {
            "Long Site Name. Use more than 256 characters string in the test because VarDump represents 256+ and shorter strings differently. 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20",
            @"<div class=""container"">
    <h1>Welcome to Incorrect Site Name</h1>
    <p>This is a simple div example.</p>
</div>"
        },
    };

    public static TheoryData<byte, byte[]> ByteSnapshots() =>
    new()
    {
        {
            1,
            [0, 1, 2]
        },
        {
            2,
            [0, 1, 2]
        },
    };
}
