using System;

namespace Scand.StormPetrel.FileSnapshotInfrastructure.Attributes
{
    /// <summary>
    /// Use this attribute to indicate a method parameter as a "use case ID".
    /// Alternatively, you can use the parameter name "useCaseId".
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class UseCaseIdAttribute : Attribute
    {
    }
}
