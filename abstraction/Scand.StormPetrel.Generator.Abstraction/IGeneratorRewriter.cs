namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Allows implementing a custom rewrite of dumped baselines in C# code, binary/text files, etc.
    /// </summary>
    public interface IGeneratorRewriter
    {
        /// <summary>
        /// Rewrites dumped baselines based on input arguments.
        /// </summary>
        /// <param name="generationRewriteContext"></param>
        /// <returns></returns>
        RewriteResult Rewrite(GenerationRewriteContext generationRewriteContext);
    }
}
