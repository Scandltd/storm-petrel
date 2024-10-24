namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Input data for <see cref="IGeneratorRewriter"/>.
    /// </summary>
    public sealed class GenerationRewriteContext
    {
        public GenerationContext GenerationContext { get; set; }
        /// <summary>
        /// Actual variable value to use in the rewrite algorithm.
        /// </summary>
        public string Value { get; set; }
    }
}
