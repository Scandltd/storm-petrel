using Scand.StormPetrel.Generator.Abstraction.ExtraContext;

namespace Scand.StormPetrel.Generator.Common.ExtraContextInternal
{
    internal abstract class AbstractExtraContextInternal
    {
    }
    internal abstract class AbstractExtraContextInternal<T> : AbstractExtraContextInternal where T : AbstractExtraContext
    {
        /// <summary>
        /// Partially populated <see cref="T"/> extra context.
        /// </summary>
        public T PartialExtraContext { get; set; }
    }
}
