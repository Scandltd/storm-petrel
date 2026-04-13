#if IS_XUNIT_V3
using Xunit.Sdk;
#else
using Xunit.Abstractions;
#endif

namespace Test.Integration.XUnit;

/// <summary>
/// A helper class example to wrap a value of type <typeparamref name="T"/> with a label for better test case display in xUnit theories.
/// It implements <see cref="IXunitSerializable"/> to allow serialization and deserialization of the wrapped value
/// when used as a test method argument in xUnit theories. The <see cref="Label"/> property is used to provide a descriptive label for the test case,
/// which will be displayed in the test runner output, while the <see cref="Value"/> property holds the actual value of type <typeparamref name="T"/> that is being tested.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XunitSerializable<T> : IXunitSerializable
{
    public string Label { get; set; } = "";
    public T? Value { get; set; }
    public void Deserialize(IXunitSerializationInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);
        var json = info.GetValue<string>("data");
        var deserialized = json.Deserialize<XunitSerializable<T>>();
        Label = deserialized.Label;
        Value = deserialized.Value;
    }
    public void Serialize(IXunitSerializationInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);
        var json = this.Serialize();
        info.AddValue("data", json);
    }
    /// <summary>
    /// Shows <see cref="Label"/> as a test case.
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Label;
    #region Optionally override equality members to support this class as test method argument other than `expected`
    public override bool Equals(object? obj)
    {
        if (obj is not XunitSerializable<T> other)
        {
            return false;
        }
        // Note: We only compare the Label here what should be enough for test cases. At the same time, the Value may not be comparable or may not have a meaningful equality implementation.
        return Label.Equals(other.Label, StringComparison.Ordinal);
    }
    public override int GetHashCode() => Label.GetHashCode(StringComparison.Ordinal);
    #endregion
}
