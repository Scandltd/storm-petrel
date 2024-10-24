namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Allows implementing a custom backup of the code before it is rewritten with updated baselines.
    /// </summary>
    public interface IGeneratorBackuper
    {
        /// <summary>
        /// Backs up the code based on input arguments.
        /// </summary>
        /// <param name="generationBackupContext"></param>
        /// <returns>Backup file path, URI, or another identifier where the backup is created, or null if not created.</returns>
        string Backup(GenerationBackupContext generationBackupContext);
    }
}
