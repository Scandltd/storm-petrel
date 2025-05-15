using Scand.StormPetrel.Generator.Config;
using System.Collections.Immutable;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    internal static class SupportedMethodInfo
    {
        public static ImmutableHashSet<string> AttributeFullNames { get; } = GeneratorPrimaryConfig.Instance.AttributeFullNames;

        public static ImmutableHashSet<string> AttributeNames { get; } =
            AttributeFullNames
                .Select(x => GeneratorPrimaryConfig.GetAttributeName(x))
                .ToImmutableHashSet();

        public static ImmutableHashSet<string> AttributeNamesForTestCase { get; } = GeneratorPrimaryConfig.Instance.AttributeNamesForTestCase;

        public static ImmutableHashSet<string> AttributeNamesForTestCaseSource { get; } = GeneratorPrimaryConfig.Instance.XUnitNonClassDataAttributeNamesForTestCaseSource
                                                                                            .Union(GeneratorPrimaryConfig.Instance.XUnitClassDataAttributeNamesForTestCaseSource)
                                                                                            .Union(GeneratorPrimaryConfig.Instance.NUnitAttributeNamesForTestCaseSource)
                                                                                            .Union(GeneratorPrimaryConfig.Instance.MSTestAttributeNamesForTestCaseSource);

        public static bool IsXUnitNonClassDataAttributeForTestCaseSource(string attributeName)
            => GeneratorPrimaryConfig.Instance.XUnitNonClassDataAttributeNamesForTestCaseSource.Contains(attributeName);

        public static bool IsXUnitClassDataAttributeForTestCaseSource(string attributeName)
            => GeneratorPrimaryConfig.Instance.XUnitClassDataAttributeNamesForTestCaseSource.Contains(attributeName);

        public static bool IsNUnitAttributeForTestCaseSource(string attributeName)
            => GeneratorPrimaryConfig.Instance.NUnitAttributeNamesForTestCaseSource.Contains(attributeName);

        public static bool MSTestAttributeForTestCaseSource(string attributeName)
            => GeneratorPrimaryConfig.Instance.MSTestAttributeNamesForTestCaseSource.Contains(attributeName);
    }
}
