namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Input data for <see cref="IGeneratorBackuper"/>.
    /// </summary>
    public sealed class GenerationBackupContext
    {
        public GenerationContext GenerationContext { get; set; }
        /// <summary>
        /// File path needed to create a backup.
        /// </summary>
        public string FilePath { get; set; }
    }
}
