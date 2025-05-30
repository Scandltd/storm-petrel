using Scand.StormPetrel.FileSnapshotInfrastructure;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.TargetProject;

namespace Test.Integration.PlaywrightTest;

internal sealed class CustomRewriter : IGeneratorRewriter
{
    private readonly SnapshotRewriter _fsiRewriter = new();
    private readonly GeneratorRewriter _regularRewriter = new();

    public RewriteResult Rewrite(GenerationRewriteContext generationRewriteContext)
    {
        if (CustomDumper.IsFileSnapshotTest(generationRewriteContext.GenerationContext))
        {
            return _fsiRewriter.Rewrite(generationRewriteContext);
        }
        return _regularRewriter.Rewrite(generationRewriteContext);
    }
}
