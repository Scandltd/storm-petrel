namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Allows implementing a custom dump (C# code, checksum, serialized value, etc.) of the actual baseline.
    /// </summary>
    public interface IGeneratorDumper
    {
        /// <summary>
        /// Creates a dump (C# code, checksum, serialized value, etc.) based on input arguments.
        /// </summary>
        /// <param name="generationDumpContext"></param>
        /// <returns>The dump.</returns>
        string Dump(GenerationDumpContext generationDumpContext);
    }
}
