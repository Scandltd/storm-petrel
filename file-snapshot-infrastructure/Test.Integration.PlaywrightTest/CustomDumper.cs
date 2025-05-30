using Scand.StormPetrel.FileSnapshotInfrastructure;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Utils.DumperDecorator;

namespace Test.Integration.PlaywrightTest;

internal sealed class CustomDumper : IGeneratorDumper
{
    private readonly SnapshotDumper _fsiDumper;
    private readonly LiteralExpressionDumperDecorator _regularDumper;
    public CustomDumper()
    {
        _fsiDumper = new();

        //Use null for those versions of Scand.StormPetrel.Generator where it is applicable
        Dictionary<string, IEnumerable<string>> typeNameToVerbatimStringPropertyNames = new()
        {
            {
                "" /*any type*/, ["" /*any property*/]
            }
        };
        var nativeDumper = new Scand.StormPetrel.Generator.TargetProject.GeneratorObjectDumper();
        _regularDumper = new LiteralExpressionDumperDecorator(nativeDumper, typeNameToVerbatimStringPropertyNames);
    }

    public string Dump(GenerationDumpContext generationDumpContext)
    {
        if (IsFileSnapshotTest(generationDumpContext.GenerationContext))
        {
            return _fsiDumper.Dump(generationDumpContext);
        }
        return _regularDumper.Dump(generationDumpContext);
    }

    public static bool IsFileSnapshotTest(GenerationContext context)
        => context.MethodSharedContext.MethodName.Contains("Snapshot", StringComparison.OrdinalIgnoreCase);
}
