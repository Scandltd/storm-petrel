using Scand.StormPetrel.Generator.Common.Config;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace Scand.StormPetrel.Generator.Common
{
    internal class SupportedMethodInfo
    {
        private static readonly Lazy<SupportedMethodInfo> _lazyInstance = new Lazy<SupportedMethodInfo>(() => new SupportedMethodInfo(), isThreadSafe: true);
        public static SupportedMethodInfo Instance => _lazyInstance.Value;
        public SupportedMethodInfo()
        {
            var config = GeneratorPrimaryConfig.Instance;
            AttributeFullNames = config.AttributeFullNames;
            AttributeNames = config
                                .AttributeFullNames
                                .Select(x => GeneratorPrimaryConfig.GetAttributeName(x))
                                .ToImmutableHashSet();
            AttributeNamesForTestCase = config.AttributeNamesForTestCase;
        }
        public ImmutableHashSet<string> AttributeFullNames { get; }
        public ImmutableHashSet<string> AttributeNames { get; }
        public ImmutableHashSet<string> AttributeNamesForTestCase { get; }
        public ImmutableHashSet<string> AttributeNamesForTestCaseSource { get; }
        public static TestCaseSourceKind? GetTestCaseSourceKind(string attributeName)
        {
            if (ContainsAttribute(GeneratorPrimaryConfig.Instance.XUnitNonClassDataAttributeNamesForTestCaseSource, attributeName))
            {
                return TestCaseSourceKind.XUnitNonClassData;
            }
            else if (ContainsAttribute(GeneratorPrimaryConfig.Instance.XUnitClassDataAttributeNamesForTestCaseSource, attributeName))
            {
                return TestCaseSourceKind.XUnitClassData;
            }
            else if (ContainsAttribute(GeneratorPrimaryConfig.Instance.NUnitAttributeNamesForTestCaseSource, attributeName))
            {
                return TestCaseSourceKind.NUnit;
            }
            else if (ContainsAttribute(GeneratorPrimaryConfig.Instance.MSTestAttributeNamesForTestCaseSource, attributeName))
            {
                return TestCaseSourceKind.MSTest;
            }
            return null;
        }
        private static bool ContainsAttribute(ImmutableHashSet<string> set, string attributeName)
            => set.Contains(GeneratorPrimaryConfig.GetAttributeName(attributeName));
    }
}
