using Scand.StormPetrel.Generator.Abstraction.ExtraContext;

namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Input data for <see cref="IGenerator"/>.
    /// </summary>
    public sealed class GenerationContext
    {
        /// <summary>
        /// Actual variable value.
        /// </summary>
        public object Actual { get; set; }
        public string[] ActualVariablePath { get; set; }
        /// <summary>
        /// Expected variable value.
        /// </summary>
        public object Expected { get; set; }
        public string[] ExpectedVariablePath { get; set; }
        /// <summary>
        /// Extra context data to be used to rewrite expected variable initializers, attribute arguments, methods/properties, etc.
        /// </summary>
        public AbstractExtraContext ExtraContext { get; set; }
        /// <summary>
        /// Method context instance shared within one StormPetrel test method call.
        /// Can be used in functionality like "detect if the last variable pair is being rewritten"
        /// to throw <see cref="Exceptions.BaselineUpdatedException"/> if at least one expected/actual variable pair differs.
        /// </summary>
        public MethodContext MethodSharedContext { get; set; }
    }
}
