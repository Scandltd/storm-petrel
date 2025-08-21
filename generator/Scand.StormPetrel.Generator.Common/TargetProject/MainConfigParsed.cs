using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Scand.StormPetrel.Generator.Common.TargetProject
{
    public sealed class MainConfigParsed
    {
        private static readonly RegexOptions IgnoreFilePathRegexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;
        private static readonly RegexOptions IgnoreInvocationExpressionRegexOptions = RegexOptions.Compiled;
        private static readonly RegexOptions TestVarPairsRegexOptions = RegexOptions.Compiled | RegexOptions.CultureInvariant;

        public string TargetProjectGeneratorExpression { get; private set; }
        public GeneratorConfig GeneratorConfig { get; private set; }
        public bool IsDisabled { get; private set; }
        public Regex IgnoreFilePathRegex { get; private set; }
        public Regex IgnoreInvocationExpressionRegex { get; private set; }
        public TestVariablePairConfigParsed[] TestVariablePairConfigs { get; private set; }

        internal bool IsMatchToIgnoreFilePathRegex(string path)
        {
            if (IgnoreFilePathRegex == null || string.IsNullOrEmpty(path))
            {
                return false;
            }

            return IgnoreFilePathRegex.IsMatch(path);
        }

        public static MainConfigParsed ParseConfig(MainConfig mainConfig, out HashSet<string> invalidRegexes)
        {
            invalidRegexes = new HashSet<string>();
            var parsed = new MainConfigParsed
            {
                TargetProjectGeneratorExpression = mainConfig.TargetProjectGeneratorExpression,
                GeneratorConfig = mainConfig.GeneratorConfig,
                IsDisabled = mainConfig.IsDisabled,

                IgnoreFilePathRegex = ParseRegex(mainConfig.IgnoreFilePathRegex,
                    IgnoreFilePathRegexOptions,
                    out bool ignoreFilePathRegexError)
            };

            if (ignoreFilePathRegexError)
            {
                invalidRegexes.Add(mainConfig.IgnoreFilePathRegex);
            }

            parsed.IgnoreInvocationExpressionRegex = ParseRegex(mainConfig.IgnoreInvocationExpressionRegex,
                IgnoreInvocationExpressionRegexOptions,
                out bool ignoreInvocationExpressionRegexError);

            if (ignoreInvocationExpressionRegexError)
            {
                invalidRegexes.Add(mainConfig.IgnoreInvocationExpressionRegex);
            }

            if (mainConfig.TestVariablePairConfigs != null)
            {
                parsed.TestVariablePairConfigs = new TestVariablePairConfigParsed[mainConfig.TestVariablePairConfigs.Length];

                for (int i = 0; i < mainConfig.TestVariablePairConfigs.Length; i++)
                {
                    parsed.TestVariablePairConfigs[i] = new TestVariablePairConfigParsed
                    {
                        ActualVarNameTokenRegex = ParseRegex(mainConfig.TestVariablePairConfigs[i].ActualVarNameTokenRegex,
                            TestVarPairsRegexOptions,
                            out bool actualVarNameTokenRegexError),
                        ExpectedVarNameTokenRegex = ParseRegex(mainConfig.TestVariablePairConfigs[i].ExpectedVarNameTokenRegex,
                            TestVarPairsRegexOptions,
                            out bool expectedVarNameTokenRegexError)
                    };

                    if (actualVarNameTokenRegexError)
                    {
                        invalidRegexes.Add(mainConfig.TestVariablePairConfigs[i].ActualVarNameTokenRegex);
                    }

                    if (expectedVarNameTokenRegexError)
                    {
                        invalidRegexes.Add(mainConfig.TestVariablePairConfigs[i].ExpectedVarNameTokenRegex);
                    }
                }
            }

            return parsed;
        }

        public static Regex ParseRegex(string regex, RegexOptions regexOptions, out bool isError)
        {
            isError = false;
            if (string.IsNullOrEmpty(regex))
            {
                return null;
            }

            try
            {
                return new Regex(regex, regexOptions);
            }
            catch (ArgumentException)
            {
                isError = true;
                return null;
            }
        }
    }
}
