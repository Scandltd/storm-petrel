using Codeuctivity.ImageSharpCompare;
using FluentAssertions;
using Microsoft.Playwright;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using Xunit.Sdk;

namespace Test.Integration.PlaywrightTest;

/// <summary>
/// Prerequisites [1]:
/// - Install required browsers. This example uses net8.0, if you are using a different version of .NET you will need to adjust the command and change net8.0 to your version:
///         pwsh bin/Debug/net8.0/playwright.ps1 install
/// [1] https://playwright.dev/dotnet/docs/intro
/// </summary>
public class PlaywrightTests : IAsyncLifetime
{
    private const string _webSite = "https://playwright.dev";
    private IPlaywright? _playwright;
    private IBrowser? _browser;

    public async Task DisposeAsync()
    {
        if (_browser != null)
        {
            await _browser.DisposeAsync();
        }
        _playwright?.Dispose();
    }

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = true });
    }

    private async Task<IPage> StartNewPageAsync()
    {
        if (_browser == null)
        {
            throw new InvalidOperationException();
        }
        return await _browser.NewPageAsync();
    }

    [Fact]
    public async Task CompareWebPageScreenSnapshotTest()
    {
        //Arrange
        var page = await StartNewPageAsync();
        await page.GotoAsync(_webSite);
        //Optionally wait page loaded
        await WaitForLoaded(page);

        //Option A. Read bytes and compare them exactly.
        //var expectedScreenshotBytes = SnapshotProvider.ReadAllBytes();

        //Option B. Open the stream, calculate and then assert the difference using a tolerance.
        using var expectedStream = SnapshotProvider.OpenReadWithShareReadWrite();

        //Act
        var actualScreenshotBytes = await page.ScreenshotAsync(new()
        {
            //Optionally provide screenshot stabilizing settings
            Animations = ScreenshotAnimations.Disabled, // Disable CSS/JS animations
            FullPage = true,                            // Consistent viewport
        });
        using MemoryStream actualStream = new(actualScreenshotBytes);

        //Assert
        //Option A. Read bytes and compare them exactly.
        //actualScreenshotBytes.Should().Equal(expectedScreenshotBytes);

        //Opiton B. Open the stream, calculate and then assert the difference using a tolerance.
        ICompareResult actualDiff = ImageSharpCompare.CalcDiff(actualStream, expectedStream);
        try
        {
            actualDiff.PixelErrorPercentage.Should().BeLessThan(0.02, "An example of pixel error percentage threshold");
            actualDiff.PixelErrorCount.Should().BeLessThan(1000, "An example of pixel error count threshold");
        }
        catch (XunitException)
        {
            //Optionally calculate diff mask and save
            await CalcDiffMaskImageAndSaveAsync("CompareWebPageScreenSnapshotTest-DifferenceMask.png", actualStream, expectedStream);
            throw;
        }

        static async Task CalcDiffMaskImageAndSaveAsync(string maskFilePath, Stream actual, Stream expected)
        {
            actual.Position = 0;
            expected.Position = 0;
            using var maskImage = ImageSharpCompare.CalcDiffMaskImage(actual, expected);
            using var fileStreamDifferenceMask = File.Create(maskFilePath);
            await SixLabors.ImageSharp.ImageExtensions.SaveAsPngAsync(maskImage, fileStreamDifferenceMask);
        }
    }

    [Fact]
    public async Task PageInformationTest()
    {
        //Arrange
        var expectedHeaderInnerHTML = "incorrect header html";
        var expectedHeaderInnerText = "incorrect header text";
        var expectedHeaderSnapshot = @"
                - banner:
                  - heading ""INCORRECT heading"" [level=1]
                  - link ""Get started""
                  - link ""Star microsoft/playwright on GitHub""
                  - link /[\\d]+k\\+ stargazers on GitHub/
            ";

        var page = await StartNewPageAsync();
        await page.GotoAsync(_webSite);
        //Optionally wait page loaded
        await WaitForLoaded(page);
        var headerLocator = page.Locator("header");

        //Act
        var actualTitle = await page.Locator("title").TextContentAsync();
        var actualHeaderSnapshot = await headerLocator.AriaSnapshotAsync();
        var actualHeaderInnerHTML = await headerLocator.InnerHTMLAsync();
        var actualHeaderInnerText = await headerLocator.InnerTextAsync();

        //Assert
        actualTitle.Should().Be("incorrect title");
        actualHeaderSnapshot.Should().Be(expectedHeaderSnapshot);
        actualHeaderInnerHTML.Should().Be(expectedHeaderInnerHTML);
        actualHeaderInnerText.Should().Be(expectedHeaderInnerText);
        await Assertions.Expect(headerLocator).ToMatchAriaSnapshotAsync(expectedHeaderSnapshot);
    }

    [Theory]
    [InlineData("https://playwright.dev", "incorrect title")]
    [InlineData("https://dotnet.microsoft.com", "incorrect title")]
    [InlineData("https://google.com", "incorrect title")]
    public async Task WebSiteTitleTest(string url, string expectedTitle)
    {
        //Arrange
        var page = await StartNewPageAsync();
        await page.GotoAsync(url);
        //Optionally wait page loaded
        await WaitForLoaded(page);

        //Act
        var actualTitle = await page.Locator("title").First.TextContentAsync();

        //Assert
        actualTitle.Should().Be(expectedTitle);
    }

    /// <summary>
    /// Some `wait page loaded` implementation what may differ in other test projects.
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    private static async Task WaitForLoaded(IPage page)
    {
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.Locator("header").WaitForAsync(new()
        {
            State = WaitForSelectorState.Visible,
        });
    }
}