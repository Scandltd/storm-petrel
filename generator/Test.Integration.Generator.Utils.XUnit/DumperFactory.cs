using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Utils;
using Scand.StormPetrel.Generator.Utils.DumperDecorator;
using Test.Integration.Generator.Utils.XUnit;

internal static class DumperFactory
{
    public static IGeneratorDumper CreateDecorated(IGeneratorDumper dumper) =>
        new ImplicitObjectCreationDumperDecorator(
            new CollectionExpressionDumperDecorator(
                new LiteralExpressionDumperDecorator(
                    new RemoveAssignmentDumperDecorator(dumper, new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            "" /*means 'all types'*/, [nameof(AddResult.GuidPropertyIgnoredForAllTypes)]
                        },
                        {
                            nameof(AddResult), [nameof(AddResult.StringPropertyIgnored)]
                        },
                    }),
                    LiteralExpressionDumperDecorator.GetVerbatimStringDecoratingFunc(new Dictionary<string, IEnumerable<string>>
                    {
                        {
                            nameof(AddResult), [nameof(AddResult.ValueAsVerbatimString)]
                        },
                    }))));
}
