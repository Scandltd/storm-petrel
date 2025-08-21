using System.Collections.Concurrent;

namespace Scand.StormPetrel.Generator.Analyzer
{
    partial class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzer
    {
        private class AnalysisState
        {
            public ConcurrentDictionary<string, ConcurrentBag<string>> SourceTypeFullNameToDataSourceMethodNames { get; } = new ConcurrentDictionary<string, ConcurrentBag<string>>();
            public ConcurrentBag<string> ClassDataSourceTypeFullName { get; } = new ConcurrentBag<string>();
        }
    }
}
