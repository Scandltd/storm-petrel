using FluentAssertions;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

public partial class AppSettingsFileReferenceAnalyzerTest
{
    [Theory]
    [MemberData(nameof(MainTestData))]
    public async Task MainTest(string _,
        (string filename, SourceText content)[] additionalFiles,
        (Dictionary<string, string>? Cache, Func<string, IEnumerable<string>> EnumerateFilesFunc, Func<long> TicksProvider) analyzerInfo,
        DiagnosticResult? expectedDiagnostic,
        Dictionary<string, string>? expectedCache)
    {
        //Arrange
        analyzerInfo.Cache ??= [];
        analyzerInfo.EnumerateFilesFunc ??= _ => [];
        analyzerInfo.TicksProvider ??= () => new DateTime(2025, 6, 11).Ticks;
        expectedCache ??= [];
        AppSettingsFileReferenceAnalyzer analyzer = new(analyzerInfo.Cache, analyzerInfo.TicksProvider, analyzerInfo.EnumerateFilesFunc);
        var analyzerTest = new CSharpAnalyzerTestEx<AppSettingsFileReferenceAnalyzer, DefaultVerifier>(analyzer);
        var testState = analyzerTest.TestState;
        testState.AdditionalFiles.AddRange(additionalFiles);
        if (expectedDiagnostic != null)
        {
            testState.ExpectedDiagnostics.Add(expectedDiagnostic.Value);
        }
        testState.Sources.Add((Path.Combine("c", "my", "project", "Foo.cs"), @"internal class Foo {}"));

        //Act, Assert
        await analyzerTest.RunAsync();
        analyzerInfo.Cache.Should().BeEquivalentTo(expectedCache);
    }
}