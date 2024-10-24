namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Allows implementing a custom baseline generator to have a totally custom generation of expected baselines.
    /// </summary>
    public interface IGenerator
    {
        /// <summary>
        /// Generates a baseline based on input arguments.
        /// </summary>
        /// <param name="generationContext"></param>
        void GenerateBaseline(GenerationContext generationContext);
    }
}
