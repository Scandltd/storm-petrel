namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class RewriteResult
    {
        public bool IsRewritten { get; set; }
        public string BackupFilePath { get; set; }
    }

    public interface IGeneratorRewriter
    {
        RewriteResult Rewrite(GenerationRewriteContext generationRewriteContext);
    }
}
