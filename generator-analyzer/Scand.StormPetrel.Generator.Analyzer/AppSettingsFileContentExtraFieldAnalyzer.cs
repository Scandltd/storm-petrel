using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Scand.StormPetrel.Generator.Common.TargetProject;
using System;
using System.Text.Json;
using System.Threading;

namespace Scand.StormPetrel.Generator.Analyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class AppSettingsFileContentExtraFieldAnalyzer : AppSettingsFileContentAnalyzer
    {
        private const string DiagnosticId = "SCANDSP1002";
        public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            DiagnosticId,
            "Storm Petrel settings JSON file has redundant fields",
            "Config file '{0}' has redundant field: '{1}'",
            CategoryName.AppSettings,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: HelpLink(DiagnosticId));

        public AppSettingsFileContentExtraFieldAnalyzer()
            : base(Rule)
        {
        }

        protected override void AnalyzeFileContent(AdditionalText file, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            var jsonText = file.GetText(cancellationToken).ToString();

            if (!string.IsNullOrEmpty(jsonText))
            {
                JsonDocument doc = null;

                try
                {
                    doc = JsonDocument.Parse(jsonText, new JsonDocumentOptions()
                    {
                        CommentHandling = MainConfig.JsonOptions.ReadCommentHandling,
                        AllowTrailingCommas = MainConfig.JsonOptions.AllowTrailingCommas
                    });

                    var jsonProperties = doc.RootElement.EnumerateObject();
                    var mainJsonProps = GetPropertyNames(typeof(MainConfig));
                    var generatorJsonProps = GetPropertyNames(typeof(GeneratorConfig));
                    var testVarPairsJsonProps = GetPropertyNames(typeof(TestVariablePairConfig));

                    string schemaJsonProperty = "$schema";

                    // Find extra properties in a configuration file
                    foreach (var prop in jsonProperties)
                    {
                        if (!mainJsonProps.Contains(prop.Name)
                            && !prop.Name.Equals(schemaJsonProperty, StringComparison.OrdinalIgnoreCase))
                        {
                            var startIndexes = FindStartIndexes(jsonText, prop.Name, JsonTokenType.PropertyName);
                            AddDiagnostics(Rule, reportDiagnostic, jsonText, startIndexes, prop.Name.Length, file.Path, prop.Name);
                        }

                        if (prop.Name.Equals(nameof(MainConfig.GeneratorConfig), StringComparison.OrdinalIgnoreCase)
                            && prop.Value.ValueKind == JsonValueKind.Object)
                        {
                            foreach (var configProp in prop.Value.EnumerateObject())
                            {
                                if (!generatorJsonProps.Contains(configProp.Name))
                                {
                                    var startIndexes = FindStartIndexes(jsonText, configProp.Name, JsonTokenType.PropertyName);
                                    AddDiagnostics(Rule, reportDiagnostic, jsonText, startIndexes, configProp.Name.Length, file.Path, configProp.Name);
                                }
                            }
                        }

                        if (prop.Name.Equals(nameof(MainConfig.TestVariablePairConfigs), StringComparison.OrdinalIgnoreCase)
                            && prop.Value.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var varPairConfig in prop.Value.EnumerateArray())
                            {
                                foreach (var varPair in varPairConfig.EnumerateObject())
                                {
                                    if (!testVarPairsJsonProps.Contains(varPair.Name))
                                    {
                                        var startIndexes = FindStartIndexes(jsonText, varPair.Name, JsonTokenType.PropertyName);
                                        AddDiagnostics(Rule, reportDiagnostic, jsonText, startIndexes, varPair.Name.Length, file.Path, varPair.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    doc?.Dispose();
                }
            }
        }
    }
}
