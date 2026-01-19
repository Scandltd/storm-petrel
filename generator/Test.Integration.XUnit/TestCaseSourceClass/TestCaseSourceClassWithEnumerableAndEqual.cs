namespace Test.Integration.XUnit.TestCaseSourceClass;

public sealed class TestCaseSourceClassWithEnumerableAndEqual : TheoryData<CustomEqualValue, int[], IEnumerable<CustomEqualValue>, int>
{
    public TestCaseSourceClassWithEnumerableAndEqual()
    {
        foreach (var (x, y, z, expected) in DataMethod())
        {
            Add(x, y, z, expected);
        }
    }

    private static IEnumerable<(CustomEqualValue ArgCustomEqual, int[] ArgEnumerable, IEnumerable<CustomEqualValue> ArgEnumerableCustomEqual, int Expected)> DataMethod() =>
    [
        (null!, null!, null!, -100),
        (new(), [], [], -100),
        (new() { Value = 1 }, [ 1 ], [new() { Value = 1 }], -100),
        (new() { Value = 2 }, [ 1, 2 ], [new() { Value = 1 }, new() { Value = 2 }], -100),
    ];
}
