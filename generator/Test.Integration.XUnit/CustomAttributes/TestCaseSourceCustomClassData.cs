using System.Collections;

namespace Test.Integration.XUnit.CustomAttributes;

public sealed class TestCaseSourceCustomClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator() => Data().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Data().GetEnumerator();

    private static IEnumerable<object[]> Data() =>
    [
        [1, 2, 0]
    ];
}
