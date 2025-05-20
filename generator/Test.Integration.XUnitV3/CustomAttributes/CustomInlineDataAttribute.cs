using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Xunit;

/// <summary>
/// Initializes a new instance of the <see cref="CustomInlineDataAttribute"/> class.
/// </summary>
/// <param name="customData">The data values to pass to the theory.</param>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class CustomInlineDataAttribute(params object?[]? data) : DataAttribute
{
    public object?[] Data { get; } = data ?? [null];

    /// <inheritdoc/>
    public override ValueTask<IReadOnlyCollection<ITheoryDataRow>> GetData(
        MethodInfo testMethod,
        DisposalTracker disposalTracker)
    {
        var traits = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        TestIntrospectionHelper.MergeTraitsInto(traits, Traits);

        return new([
            new TheoryDataRow(Data)
            {
                Explicit = ExplicitAsNullable,
                Skip = Skip,
                TestDisplayName = TestDisplayName,
                Timeout = TimeoutAsNullable,
                Traits = traits,
            }
        ]);
    }

    /// <inheritdoc/>
    public override bool SupportsDiscoveryEnumeration() =>
        true;
}
