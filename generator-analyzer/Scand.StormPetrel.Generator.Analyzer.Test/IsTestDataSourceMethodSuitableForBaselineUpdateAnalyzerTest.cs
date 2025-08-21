using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

public partial class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest
{
    [Theory]
    [MemberData(nameof(MainTestData))]
    public async Task MainTest(string _,
        (string filename, string content)[] additionalFiles,
        (string filename, string content)[] sources,
        DiagnosticResult[] expectedDiagnostics)
    {
        //Arrange
        var analyzerTest = new CSharpAnalyzerTest<IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzer, DefaultVerifier>();
        var testState = analyzerTest.TestState;
        Array.ForEach(additionalFiles, testState.AdditionalFiles.Add);
        Array.ForEach(expectedDiagnostics, testState.ExpectedDiagnostics.Add);
        Array.ForEach(sources, testState.Sources.Add);
        testState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(FactAttribute).Assembly.Location));

        //Act, Assert
        await analyzerTest.RunAsync();
    }

    [Theory]
    [MemberData(nameof(MSTestFileData))]
    public async Task MSTestNoReportDiagnostic((string, string)[] files)
    {
        await CSharpAnalyzerVerifier<IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzer>.VerifyMSTestAsync(files, []);
    }

    [Theory]
    [MemberData(nameof(NUnitFileData))]
    public async Task NUnitNoReportDiagnostic((string, string)[] files) => await CSharpAnalyzerVerifier<IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzer>.VerifyNUnitTestAsync(files, []);
}
