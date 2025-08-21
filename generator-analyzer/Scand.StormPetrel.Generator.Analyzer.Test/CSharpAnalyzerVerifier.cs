using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Xunit.Sdk;

namespace Scand.StormPetrel.Generator.Analyzer.Test
{
    public static class CSharpAnalyzerVerifier<TAnalyzer> where TAnalyzer : DiagnosticAnalyzer, new()
    {
        public static async Task VerifyMSTestAsync((string filename, string content)[] files, params DiagnosticResult[] expected)
        {
            var test = new AnalyzerTest<TAnalyzer>();
            Array.ForEach(files, test.TestState.Sources.Add);
            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(DynamicDataAttribute).Assembly.Location));
            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(TestClass).Assembly.Location));
            test.ExpectedDiagnostics.AddRange(expected);
            await test.RunAsync();
        }

        public static async Task VerifyNUnitTestAsync((string filename, string content)[] files, params DiagnosticResult[] expected)
        {
            var test = new AnalyzerTest<TAnalyzer>();
            Array.ForEach(files, test.TestState.Sources.Add);
            test.TestState.AdditionalReferences.Add(MetadataReference.CreateFromFile(typeof(TestCaseSourceAttribute).Assembly.Location));
            test.ExpectedDiagnostics.AddRange(expected);
            await test.RunAsync();
        }

        public static async Task VerifyAdditionalFileAsync(string fileName, string jsonContent, params DiagnosticResult[] expected)
        {
            var test = new AnalyzerTest<TAnalyzer>
            {
                TestCode = string.Empty // No C# code needed
            };

            test.ExpectedDiagnostics.AddRange(expected);
            test.TestState.AdditionalFiles.Add((fileName, jsonContent));

            await test.RunAsync();
        }

        private class AnalyzerTest<T> : CSharpAnalyzerTest<T, DefaultVerifier>
            where T : DiagnosticAnalyzer, new()
        {
        }
    }
}
