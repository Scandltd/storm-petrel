using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;

namespace Scand.StormPetrel.Generator.Config
{
    internal class GeneratorPrimaryConfig
    {
        private GeneratorPrimaryConfig()
        {
            var generatorPrimaryConfig = Environment.GetEnvironmentVariable("SCAND_STORM_PETREL_GENERATOR_CONFIG");
            GeneratorPrimaryConfigModel config = null;

            if (!string.IsNullOrEmpty(generatorPrimaryConfig))
            {
                try
                {
                    config = JsonSerializer.Deserialize<GeneratorPrimaryConfigModel>(generatorPrimaryConfig);
                }
                catch (JsonException)
                {
                    //Hide exception for invalid json because no way to print a message
                }
            }

            AttributeFullNames = DefaultAttributeFullNames
                                    .Union(GetConfigValueByKind(config, x => x.KindName == CustomTestAttributeKind.Test
                                                                                || x.TestFrameworkKindName == TestFrameworkKind.NUnit, false));
            AttributeNamesForTestCase = DefaultAttributeNamesForTestCase
                                            .Union(GetConfigValueByKind(config, x => x.KindName == CustomTestAttributeKind.TestCase, true));
            XUnitNonClassDataAttributeNamesForTestCaseSource = DefaultXUnitNonClassDataAttributeNamesForTestCaseSource
                                                                .Union(GetConfigValueByKind(config, x => x.TestFrameworkKindName == TestFrameworkKind.XUnit
                                                                                                            && x.KindName == CustomTestAttributeKind.TestCaseSource
                                                                                                            && x.XUnitTestCaseSourceKindName == XUnitTestCaseSourceKind.MemberData, true));
            XUnitClassDataAttributeNamesForTestCaseSource = DefaultXUnitClassDataAttributeNamesForTestCaseSource
                                                                .Union(GetConfigValueByKind(config, x => x.TestFrameworkKindName == TestFrameworkKind.XUnit
                                                                                                                && x.KindName == CustomTestAttributeKind.TestCaseSource
                                                                                                                && x.XUnitTestCaseSourceKindName == XUnitTestCaseSourceKind.ClassData, true));
            NUnitAttributeNamesForTestCaseSource = DefaultNUnitAttributeNamesForTestCaseSource
                                                    .Union(GetConfigValueByKind(config, x => x.TestFrameworkKindName == TestFrameworkKind.NUnit
                                                                                                && x.KindName == CustomTestAttributeKind.TestCaseSource, true));
            MSTestAttributeNamesForTestCaseSource = DefaultMSTestAttributeNamesForTestCaseSource
                                                        .Union(GetConfigValueByKind(config, x => x.TestFrameworkKindName == TestFrameworkKind.MSTest
                                                                                                    && x.KindName == CustomTestAttributeKind.TestCaseSource, true));
        }

        public static GeneratorPrimaryConfig Instance => _lazyInstance.Value;
        public static string GetAttributeName(string attributeNameOrFullName)
        {
            const string attributeSuffix = "Attribute";
            string name = attributeNameOrFullName.Split('.').Last();
            if (name.EndsWith(attributeSuffix, StringComparison.Ordinal))
            {
                name = name.Substring(0, name.Length - attributeSuffix.Length);
            }
            return name;
        }
        private static readonly Lazy<GeneratorPrimaryConfig> _lazyInstance = new Lazy<GeneratorPrimaryConfig>(() => new GeneratorPrimaryConfig(), isThreadSafe: true);

        private readonly static ImmutableHashSet<string> DefaultAttributeFullNames = new[]
        {
            //Only popular frameworks according to https://learn.microsoft.com/en-us/dotnet/core/testing/
            //xUnit, https://xunit.net/
            "Xunit.FactAttribute",
            "Xunit.TheoryAttribute",
            //NUnit, https://nunit.org/
            "NUnit.Framework.TestAttribute",
            "NUnit.Framework.TestCaseAttribute",
            "NUnit.Framework.TestCaseSourceAttribute",
            "NUnit.Framework.TheoryAttribute",
            //MSTest, https://github.com/microsoft/testfx
            "Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute",
            "Microsoft.VisualStudio.TestTools.UnitTesting.DataTestMethodAttribute",
        }.ToImmutableHashSet();

        private readonly static ImmutableHashSet<string> DefaultAttributeNamesForTestCase =
        new[]
        {
            "InlineData", //xUnit
            "TestCase", //NUnit
            "DataRow", //MSTest
        }
        .ToImmutableHashSet();

        private static readonly ImmutableHashSet<string> DefaultXUnitNonClassDataAttributeNamesForTestCaseSource = new[]
        {
            "MemberData", //xUnit
        }
        .ToImmutableHashSet();

        private static readonly ImmutableHashSet<string> DefaultXUnitClassDataAttributeNamesForTestCaseSource =
        new[]
        {
            "ClassData", //xUnit
        }
        .ToImmutableHashSet();

        private static readonly ImmutableHashSet<string> DefaultNUnitAttributeNamesForTestCaseSource =
        new[]
        {
            "TestCaseSource", //NUnit
        }
        .ToImmutableHashSet();

        private static readonly ImmutableHashSet<string> DefaultMSTestAttributeNamesForTestCaseSource =
        new[]
        {
             "DynamicData", //MSTest
        }
        .ToImmutableHashSet();

        public ImmutableHashSet<string> AttributeFullNames { get; }
        public ImmutableHashSet<string> AttributeNamesForTestCase { get; }

        public ImmutableHashSet<string> NUnitAttributeNamesForTestCaseSource { get; }
        public ImmutableHashSet<string> MSTestAttributeNamesForTestCaseSource { get; }

        public ImmutableHashSet<string> XUnitNonClassDataAttributeNamesForTestCaseSource { get; }
        public ImmutableHashSet<string> XUnitClassDataAttributeNamesForTestCaseSource { get; }

        private static IEnumerable<string> GetConfigValueByKind(GeneratorPrimaryConfigModel config, Func<CustomTestAttributeInfo, bool> predicate, bool selectAttributeName)
            => config == null
                ? Array.Empty<string>()
                : config
                    .CustomTestAttributes
                    .Where(predicate)
                    .Select(x => selectAttributeName ? GetAttributeName(x.FullName) : x.FullName);
    }
}
