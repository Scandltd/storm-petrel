using System.Collections.Immutable;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    internal static class SupportedMethodInfo
    {
        /// <summary>
        /// Attribute names with namespaces. However, we don't support custom attribute namespaces because we have to use Compilation SemanticModel (see https://stackoverflow.com/questions/28947456/how-do-i-get-attributedata-from-attributesyntax-in-roslyn)
        /// what significantly degrades source generator performance.
        /// </summary>
        public readonly static ImmutableArray<string> AttributeFullNames = new[]
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
        }.ToImmutableArray();

        public readonly static ImmutableHashSet<string> AttributeNames =
            AttributeFullNames
                .Select(x => x.Split('.').Last().Replace("Attribute", ""))
                .ToImmutableHashSet();

        public readonly static ImmutableHashSet<string> AttributeNamesForTestCase =
        new[]
        {
            "InlineData", //xUnit
            "TestCase", //NUnit
            "DataRow", //MSTest
        }
        .ToImmutableHashSet();
    }
}
