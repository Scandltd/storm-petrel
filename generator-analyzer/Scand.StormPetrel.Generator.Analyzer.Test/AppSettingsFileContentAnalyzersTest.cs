using Microsoft.CodeAnalysis.Testing;

namespace Scand.StormPetrel.Generator.Analyzer.Test
{
    public class AppSettingsFileContentAnalyzersTest
    {
        private const string _fileName = "appsettings.StormPetrel.json";

        [Theory]
        [InlineData(@$"{{
            ""$schema"": ""value""
        ")]
        public async Task MalformedJson_ShouldReportDiagnostic(string jsonContent)
        {
            var expected = new DiagnosticResult(AppSettingsFileContentFormedAnalyzer.Rule)
                .WithMessage($"Config file '{_fileName}' is not well-formed");

            await CSharpAnalyzerVerifier<AppSettingsFileContentFormedAnalyzer>.VerifyAdditionalFileAsync(_fileName, jsonContent, [expected, expected]);
        }

        [Theory]
        [InlineData("{ \"$schema\": \"\", \"key\": \"value\" }", "key", 1, 18, 1, 21)]
        [InlineData("{ \"IsDisabled\" : false, \"property\": true }", "property", 1, 25, 1, 33)]
        [InlineData("{ \"TestVariablePairConfigs\": [ { \"ActualVarNameTokenRegex1\": \"[Aa]{1}ctual\", \"ExpectedVarNameTokenRegex\": \"[Ee]{1}xpected\" }] }", "ActualVarNameTokenRegex1", 1, 34, 1, 58)]
        [InlineData("{ \"GeneratorConfig\": { \"BackuperExpression\": \"\", \"DumperExpression2\": \"\", \"RewriterExpression\": \"\" } }", "DumperExpression2", 1, 50, 1, 67)]
        public async Task ExtraField_ShouldReportDiagnostic(string jsonContent, string propertyName, int startLine, int startColumn, int endLine, int endColumn)
        {
            var expected = new DiagnosticResult(AppSettingsFileContentExtraFieldAnalyzer.Rule)
                .WithMessage($"Config file '{_fileName}' has redundant field: '{propertyName}'")
                .WithSpan(_fileName, startLine, startColumn, endLine, endColumn);

            await CSharpAnalyzerVerifier<AppSettingsFileContentExtraFieldAnalyzer>.VerifyAdditionalFileAsync(_fileName, jsonContent, [expected, expected]);
        }

        [Theory]
        [InlineData("{ \"IgnoreFilePathRegex\": \"Utils|RefStructTest\\\\.Unsupported\" }")]
        [InlineData("{ \"$schema\": \"\", \"TestVariablePairConfigs\": [ { \"ActualVarNameTokenRegex\": \"[Aa]{1}ctual\", \"ExpectedVarNameTokenRegex\": \"[Ee]{1}xpected\" }], \"GeneratorConfig\": { \"BackuperExpression\": \"\", \"DumperExpression\": \"\", \"RewriterExpression\": \"\" } }")]
        [InlineData("{ \"isDisabled\" : false }")]
        [InlineData("{ \"IsDisabled\" : false, }")]
        [InlineData("{ \"TestVariablePairConfigs\": [], \"$schema\": \"\", \"GeneratorConfig\": null, \"IsDisabled\" : false, \"Serilog\" : null }")]
        public async Task ValidJson_NoReportDiagnostic(string jsonContent)
        {
            await CSharpAnalyzerVerifier<AppSettingsFileContentFormedAnalyzer>.VerifyAdditionalFileAsync(_fileName, jsonContent, []);
            await CSharpAnalyzerVerifier<AppSettingsFileContentExtraFieldAnalyzer>.VerifyAdditionalFileAsync(_fileName, jsonContent, []);
            await CSharpAnalyzerVerifier<AppSettingsFileContentRegexAnalyzer>.VerifyAdditionalFileAsync(_fileName, jsonContent, []);
        }

        [Theory]
        [InlineData(@$"{{
            ""$schema"": ""https://raw.githubusercontent.com/Scandltd/storm-petrel/main/generator/assets/appsettings.StormPetrel.Schema.json"",
            ""IgnoreFilePathRegex"": ""(abc""
        }}", "(abc", 3, 36, 3, 40)]
        [InlineData(@$"{{
            ""IgnoreInvocationExpressionRegex"": ""(abc""
        }}", "(abc", 2, 48, 2, 52)]
        [InlineData(@$"{{
            ""TestVariablePairConfigs"": [{{
                   ""ActualVarNameTokenRegex"": ""(abc"",
                   ""ExpectedVarNameTokenRegex"": ""[Ee]{{1}}xpected""
            }}]
        }}", "(abc", 3, 47, 3, 51)]
        [InlineData(@$"{{
            ""TestVariablePairConfigs"": [{{
                   ""ActualVarNameTokenRegex"": ""[Aa]{{1}}ctual"",
                   ""ExpectedVarNameTokenRegex"": ""(abc""
            }}]
        }}", "(abc", 4, 49, 4, 53)]
        [InlineData(@$"{{
            ""IsDisabled"": false, // some comment
            ""IgnoreFilePathRegex"": ""(invalid regex""
        }}", "(invalid regex", 3, 36, 3, 50)]
        public async Task InvalidRegex_ShouldReportDiagnostic(string jsonContent, string invalidRegexValue, int startLine, int startColumn, int endLine, int endColumn)
        {
            var expected = new DiagnosticResult(AppSettingsFileContentRegexAnalyzer.Rule)
                .WithMessage($"Regex is invalid: {invalidRegexValue}")
                .WithSpan(_fileName, startLine, startColumn, endLine, endColumn);

            await CSharpAnalyzerVerifier<AppSettingsFileContentRegexAnalyzer>.VerifyAdditionalFileAsync(_fileName, jsonContent, [expected, expected]);
        }
    }
}
