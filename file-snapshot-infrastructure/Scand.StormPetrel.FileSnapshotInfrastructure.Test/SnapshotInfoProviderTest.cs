using System.Runtime.InteropServices;

namespace Scand.StormPetrel.FileSnapshotInfrastructure.Test;
public class SnapshotInfoProviderTest
{
    [Theory]
    [InlineData(null, null, null, ".Expected")]
    [InlineData("", "", "", ".Expected")]
    [InlineData("UseCaseA", "", "", ".Expected\\.UseCaseA")]
    [InlineData("", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\MyTestClass.Expected\\MyTestMethod")]
    [InlineData("UseCaseA", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\MyTestClass.Expected\\MyTestMethod.UseCaseA")]
    [InlineData("UseCaseA", @"/c/test/MyTestClass.cs", "MyTestMethod", "/c/test/MyTestClass.Expected\\MyTestMethod.UseCaseA")]
    [InlineData("", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\MyTestClass.MyExpected\\MyExpected.MyTestMethod", "MyProvider")]
    [InlineData("UseCaseA", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\MyTestClass.MyExpected\\MyExpected.MyTestMethod.UseCaseA", "MyProvider")]
    [InlineData("", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\MyTestClass\\MyExpectedFolder\\MyTestMethod\\MyExpectedFile", "MyProviderWithExtraPath")]
    [InlineData("UseCaseA", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\MyTestClass\\MyExpectedFolder\\MyTestMethod\\UseCaseA\\MyExpectedFile", "MyProviderWithExtraPath")]
    [InlineData("", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\", "MyProviderUseCaseOnly")]
    [InlineData("UseCaseA", @"c:\test\MyTestClass.cs", "MyTestMethod", "c:\\test\\UseCaseA\\UseCaseA", "MyProviderUseCaseOnly")]
    [InlineData("", @"c:\test\obj\Debug\MyTestClass.cs.g.cs", "MyTestMethodStormPetrel", "c:\\test\\MyTestClass.Expected\\MyTestMethod")]
    [InlineData("UseCaseA", @"c:\test\obj\Debug\MyTestClass.cs.g.cs", "MyTestMethodStormPetrel", "c:\\test\\MyTestClass.Expected\\MyTestMethod.UseCaseA")]
    [InlineData("", @"c:\test\obj\Debug\MyTestClass.cs.g.cs", "MyTestMethodStormPetrel", "c:\\test\\MyTestClass.MyExpected\\MyExpected.MyTestMethod", "MyProvider")]
    [InlineData("UseCaseA", @"c:\test\obj\Debug\MyTestClass.cs.g.cs", "MyTestMethodStormPetrel", "c:\\test\\MyTestClass.MyExpected\\MyExpected.MyTestMethod.UseCaseA", "MyProvider")]
    [InlineData("", @"c:\test\obj\Debug\MyTestClass.cs.g.cs", "MyTestMethodStormPetrel", "c:\\test\\MyTestClass\\MyExpectedFolder\\MyTestMethod\\MyExpectedFile", "MyProviderWithExtraPath")]
    [InlineData("UseCaseA", @"c:\test\obj\Debug\MyTestClass.cs.g.cs", "MyTestMethodStormPetrel", "c:\\test\\MyTestClass\\MyExpectedFolder\\MyTestMethod\\UseCaseA\\MyExpectedFile", "MyProviderWithExtraPath")]
    public void WhenProviderThenExpectedBehaviorTest(string useCaseId, string callerFilePath, string callerMemberName, string expected, string? providerId = null)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            //Skip test for non-Windows platform
            return;
        }

        //Arrange
        SnapshotInfoProvider provider = GetProvider();

        //Act
        var actual = provider.GetFilePathWithoutExtension(useCaseId, callerFilePath, callerMemberName);

        //Assert
        Assert.Equal(expected, actual);

        SnapshotInfoProvider GetProvider() => providerId switch
        {
            null => new(),
            "MyProvider" => new("<CallerFileNameWithoutExtension>.MyExpected", "MyExpected.<CallerMemberName>.<UseCaseId>"),
            "MyProviderWithExtraPath" => new($"<CallerFileNameWithoutExtension>{Path.DirectorySeparatorChar}MyExpectedFolder{Path.DirectorySeparatorChar}<CallerMemberName>{Path.DirectorySeparatorChar}<UseCaseId>", "MyExpectedFile"),
            "MyProviderUseCaseOnly" => new(@"<UseCaseId>", "<UseCaseId>"),
            _ => throw new ArgumentOutOfRangeException(nameof(providerId)),
        };
    }
}