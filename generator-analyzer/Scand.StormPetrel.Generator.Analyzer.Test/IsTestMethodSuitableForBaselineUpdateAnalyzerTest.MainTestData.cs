using Microsoft.CodeAnalysis.Testing;
using System.Globalization;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

partial class IsTestMethodSuitableForBaselineUpdateAnalyzerTest
{
    public static TheoryData<string,
                                (string filename, string content)[],
                                (string filename, string content)[],
                                DiagnosticResult[]> MainTestData =>
    new()
    {
        {
            "When default config and no test methods Then no diagnostics",
            [],
            [("Foo.cs", @"internal class Foo {}")],
            []
        },
        {
            "When default config and empty test method Then the diagnostic",
            [],
            [("Foo.cs", @"
using Xunit;
internal class FooTest
{
    [Fact]
    public void TestInt()
    {
    }
}")],
            [new DiagnosticResult(IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "TestInt"))
                    .WithLocation("Foo.cs", 5, 5)]
        },
        {
            "When default config and a test method with actual/expected pair Then no diagnostics",
            [],
            [("Foo.cs", @"
using Xunit;
internal class FooTest
{
    [Fact]
    public void TestInt()
    {
        //Arrange
        int expected = 123;
        //Act
        var actual = 100;
        //Assert
        //actual.Should().Be(expected);
    }
}")],
            []
        },
        {
            "When default config and a test method with actual/expected pair not matching the config Then the diagnostic",
            [],
            [("Foo.cs", @"
using Xunit;
internal class FooTest
{
    [Fact]
    public void TestInt()
    {
        //Arrange
        int expected = 123;
        //Act
        var actualNotMatchingToExpected = 100;
        //Assert
        //actualNotMatchingToExpected.Should().Be(expected);
    }
}")],
            [new DiagnosticResult(IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "TestInt"))
                    .WithLocation("Foo.cs", 5, 5)]
        },
        {
            "When default config and a test method with actual/expected pair not matching the config Then the diagnostic",
            [],
            [("Foo.cs", @"
internal class FooTest
{
    [Xunit.FactAttribute] //Use full name with `Attribute` suffix
    public void TestInt()
    {
        //Arrange
        //Act
        var blaVar = ""some text"";
        //Assert
        Assert.Equal(1, blaVar.Length);
    }
}"),
                ("Assert.cs", @"
internal static class Assert
{
    public static void Equal(int expected, int actual) => throw new System.InvalidOperationException(""No need implementation here"");
}")

            ],
            []
        },
        {
            "When custom config and a test method with actual/expected pair not matching the config Then the diagnostic",
            [("appsettings.StormPetrel.json", @"{
    ""TestVariablePairConfigs"": [
        {
            ""ActualVarNameTokenRegex"": ""actual"",
            ""ExpectedVarNameTokenRegex"": null
        }
    ]
}")],
            [("Foo.cs", @"
using Xunit;
internal class FooTest
{
    [Trait(""Category"",""SomeCategory""), Fact] //Multiple attributes in the list
    public void TestInt()
    {
        //Arrange
        //Act
        var blaVar = ""some text"";
        //Assert
        Assert.Equal(1, blaVar.Length);
    }
}"),
                ("Assert.cs", @"
internal static class Assert
{
    public static void Equal(int expected, int actual) => throw new System.InvalidOperationException(""No need implementation here"");
}")

            ],
            [new DiagnosticResult(IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "TestInt"))
                    .WithLocation("Foo.cs", 5, 5)]
        },
        {
            "Performance",
            [],
            GetPerformanceFiles().ToArray(),
            [new DiagnosticResult(IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "TestInt"))
                    .WithLocation("Foo999.cs", 5, 5)]
        },
    };

    private static IEnumerable<(string filename, string content)> GetPerformanceFiles(int count = 1000)
    {
        yield return ("Assert.cs", @"
internal static class Assert
{
    public static void Equal(int expected, int actual) => throw new System.InvalidOperationException(""No need implementation here"");
}");
        for (int i = 0; i < count; i++)
        {
            string assert = i == count - 1
                                ? "" //to have some diagnostics
                                : "Assert.Equal(1, actual.Length);";
            yield return ($"Foo{i}.cs", @"
using Xunit;
internal class Foo" + i + @"Test
{
    [Fact]
    public void TestInt()
    {
        //Arrange
        //Act
        var actual = ""some text"";
        //Assert
        " + assert + @"
    }
}");
        }
    }
}
