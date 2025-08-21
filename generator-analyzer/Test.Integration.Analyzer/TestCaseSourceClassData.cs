using System.Collections;

namespace Test.Integration.Analyzer;

public sealed class TestCaseSourceClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator() => TestCaseSourceClassDataMethod().GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => TestCaseSourceClassDataMethod().GetEnumerator();
    private static IEnumerable<object[]> TestCaseSourceClassDataMethod() => throw new NotImplementedException("To have the diagnostic reported");
}
