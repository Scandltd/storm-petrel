using System;

namespace Scand.StormPetrel.Generator.Abstraction.Exceptions
{
    /// <summary>
    /// Can be thrown in <see cref="IGenerator"/> implementation to indicate that a StormPetrel test intentionally fails
    /// after a successful expected baseline change.
    /// </summary>
    public sealed class BaselineUpdatedException : Exception
    {
        public BaselineUpdatedException()
        {
        }

        public BaselineUpdatedException(string message) : base(message)
        {
        }

        public BaselineUpdatedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
