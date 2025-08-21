using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;

namespace Scand.StormPetrel.Generator.Common.TargetProject
{
    public sealed class MainConfig
    {
        internal const string GeneratorTargetProjectNamespace = "Scand.StormPetrel.Generator.TargetProject";
        public string TargetProjectGeneratorExpression { get; set; } = $"new {GeneratorTargetProjectNamespace}.Generator()";
        public GeneratorConfig GeneratorConfig { get; set; } = new GeneratorConfig();
        public bool IsDisabled { get; set; }
        public string IgnoreFilePathRegex { get; set; }
        public string IgnoreInvocationExpressionRegex { get; set; }
        public object Serilog { get; set; } = SerilogDefault;
        public TestVariablePairConfig[] TestVariablePairConfigs { get; set; } = new[]
        {
            new TestVariablePairConfig()
            {
                ActualVarNameTokenRegex = "[Aa]{1}ctual",
                ExpectedVarNameTokenRegex = "[Ee]{1}xpected",
            },
            new TestVariablePairConfig()
            {
                ActualVarNameTokenRegex = null,
                ExpectedVarNameTokenRegex = null,
            },
        };

        internal static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };
        private readonly static object SerilogDefault = new object();
        internal const string StormPetrelRootPath = "{StormPetrelRootPath}";
        private const string DefaultJson = @"
{
  ""Serilog"":
  {
    ""Using"":  [ ""Serilog.Sinks.File"" ],
    ""MinimumLevel"": ""Warning"",
    ""WriteTo"":
    [
      {
        ""Name"": ""File"",
        ""Args"":
        {
          ""path"": """ + StormPetrelRootPath + @"/Logs/StormPetrel-.log"",
          ""rollingInterval"": ""Day"",
          ""shared"": true,
          ""flushToDiskInterval"": ""00:00:30""
        }
      }
    ]
  }
}";
        internal static bool IsFromTargetProjectNamespace(string str)
            => str != null && str.Contains(GeneratorTargetProjectNamespace);

        internal static MainConfigParsed GetNew(AdditionalText configAdditionalText, out string jsonContent, out bool isJsonException, out HashSet<string> invalidRegexes)
        {
            MainConfig config;
            jsonContent = configAdditionalText == null
                                ? DefaultJson
                                : configAdditionalText.GetText(CancellationToken.None)?.ToString();
            isJsonException = false;
            try
            {
                config = JsonSerializer.Deserialize<MainConfig>(jsonContent, JsonOptions);
            }
            catch (JsonException)
            {
                // json can be not well formed, use default
                jsonContent = DefaultJson;
                config = JsonSerializer.Deserialize<MainConfig>(jsonContent);
                isJsonException = true;
            }

            var mainConfigParsed = MainConfigParsed.ParseConfig(config, out invalidRegexes);

            if (config.Serilog == SerilogDefault)
            {
                jsonContent = DefaultJson;
            }
            else if (config.Serilog == null)
            {
                jsonContent = null;
            }

            return mainConfigParsed;
        }
    }
}
