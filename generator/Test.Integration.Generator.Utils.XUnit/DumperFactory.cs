using Scand.StormPetrel.Generator.Abstraction;
using Test.Integration.Generator.Utils.XUnit;

internal static class DumperFactory
{
    public static IGeneratorDumper CreateDecorated(IGeneratorDumper dumper) =>
        new Scand.StormPetrel.Generator.Utils.ImplicitObjectCreationDumperDecorator(
            new Scand.StormPetrel.Generator.Utils.CollectionExpressionDumperDecorator(
                new Scand.StormPetrel.Generator.Utils.RemoveAssignmentDumperDecorator(dumper, new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "" /*means 'all types'*/, [nameof(AddResult.GuidPropertyIgnoredForAllTypes)]
                    },
                    {
                        nameof(AddResult), [nameof(AddResult.StringPropertyIgnored)]
                    },
                })));
}
