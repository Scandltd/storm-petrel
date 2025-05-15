using System.Reflection;
using Xunit.Sdk;

namespace Xunit;

/// <summary>
/// Initializes a new instance of the <see cref="CustomInlineDataAttribute"/> class.
/// </summary>
/// <param name="customData">The data values to pass to the theory.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class CustomInlineDataAttribute(params object[] customData) : DataAttribute
{
    public object[] CustomData { get; } = customData;

    /// <inheritdoc/>
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        // This is called by the WPA81 version as it does not have access to attribute ctor params
        return [CustomData];
    }
}
