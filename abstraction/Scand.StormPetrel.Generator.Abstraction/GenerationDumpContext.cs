namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Input data for <see cref="IGeneratorDumper"/>.
    /// </summary>
    public sealed class GenerationDumpContext
    {
        public GenerationContext GenerationContext { get; set; }
        /// <summary>
        /// Expected or actual variable value to dump.
        /// </summary>
        public object Value { get; set; }
    }
}
