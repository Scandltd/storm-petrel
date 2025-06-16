using System.Text.RegularExpressions;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class MainConfig
    {
        private Regex _ignoreFilePathRegex;
        public string TargetProjectGeneratorExpression { get; set; } = $"new {typeof(MainConfig).Namespace}.Generator()";
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

        internal readonly static object SerilogDefault = new object();
        internal const string StormPetrelRootPath = "{StormPetrelRootPath}";
        internal const string DefaultJson = @"
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
        internal bool IsMatchToIgnoreFilePathRegex(string path)
        {
            if (string.IsNullOrEmpty(IgnoreFilePathRegex) || string.IsNullOrEmpty(path))
            {
                return false;
            }
            if (_ignoreFilePathRegex == null)
            {
                _ignoreFilePathRegex = new Regex(IgnoreFilePathRegex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            return _ignoreFilePathRegex.IsMatch(path);
        }

        internal static bool IsFromTargetProjectNamespace(string str)
            => str != null && str.Contains(typeof(GeneratorConfig).Namespace);
    }
}
