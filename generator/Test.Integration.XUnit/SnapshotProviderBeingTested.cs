namespace Test.Integration.XUnit;

/// <summary>
/// Emulates a class that returns snapshots in a typical application.
/// </summary>
internal static class SnapshotProviderBeingTested
{
    public static byte[] Bytes(byte firstByte) => [firstByte, 2, 3, 4, 5];

    public static string JsonMoreThan256Chars =>
@"{
    ""propertyA"": ""valueA"",
    ""propertyB"": ""valueB"",
    ""childArray"":
    [
        1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79...
    ]
}";
    public static string Html(string siteName) =>
@$"<div class=""container"">
    <h1>Welcome to {siteName}</h1>
    <p>This is a simple div example.</p>
</div>";
}
