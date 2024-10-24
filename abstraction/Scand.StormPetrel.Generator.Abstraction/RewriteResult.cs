namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Data returned from <see cref="IGeneratorRewriter.Rewrite(GenerationRewriteContext)">.
    /// </summary>
    public sealed class RewriteResult
    {
        public bool IsRewritten { get; set; }
        public string BackupFilePath { get; set; }
    }
}
