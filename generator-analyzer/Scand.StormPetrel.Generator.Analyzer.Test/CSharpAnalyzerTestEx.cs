using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

internal class CSharpAnalyzerTestEx<TAnalyzer, TVerifier>(TAnalyzer analyzer) : CSharpAnalyzerTest<TAnalyzer, TVerifier>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TVerifier : IVerifier, new()
{
    private readonly TAnalyzer _analyzer = analyzer;
    protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers() => [_analyzer];
}
