namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class GenerationDumpContext
    {
        public GenerationContext GenerationContext { get; set; }
        /// <summary>
        /// Value to dump. Typically, this is `expected` variable value.
        /// </summary>
        public object Value { get; set; }
    }
}
