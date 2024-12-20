using Scand.StormPetrel.Generator.Abstraction;

internal static class DumperFactory
{
    public static IGeneratorDumper CreateDecorated(IGeneratorDumper dumper) =>
        new Scand.StormPetrel.Generator.Utils.ImplicitObjectCreationDumperDecorator(
            new Scand.StormPetrel.Generator.Utils.CollectionExpressionDumperDecorator(dumper));
}
