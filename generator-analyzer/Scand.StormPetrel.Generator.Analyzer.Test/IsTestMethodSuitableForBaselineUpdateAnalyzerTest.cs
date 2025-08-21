using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using System.Diagnostics;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

public partial class IsTestMethodSuitableForBaselineUpdateAnalyzerTest
{
    [Theory]
    [MemberData(nameof(MainTestData))]
    public async Task MainTest(string useCaseDesription,
        (string filename, string content)[] additionalFiles,
        (string filename, string content)[] sources,
        DiagnosticResult[] expectedDiagnostics)
    {
        //Arrange
        var analyzerTest = new CSharpAnalyzerTest<IsTestMethodSuitableForBaselineUpdateAnalyzer, DefaultVerifier>();
        var testState = analyzerTest.TestState;
        Array.ForEach(additionalFiles, testState.AdditionalFiles.Add);
        Array.ForEach(expectedDiagnostics, testState.ExpectedDiagnostics.Add);
        Array.ForEach(sources, testState.Sources.Add);
        testState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(FactAttribute).Assembly.Location));
        var stopWatch = Stopwatch.StartNew();

        //Act, Assert
        await analyzerTest.RunAsync();
        stopWatch.Stop();
        if (useCaseDesription.IndexOf("Performance", StringComparison.OrdinalIgnoreCase) > -1)
        {
            (stopWatch.Elapsed < TimeSpan.FromSeconds(10)).Should().BeTrue($"Typically it takes ~2 seconds on `11th Gen Intel(R) Core(TM) i7-1165G7 @ 2.80GHz   2.80 GHz`, but now: {stopWatch.Elapsed}");
        }
    }
}
