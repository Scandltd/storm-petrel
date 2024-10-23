using System.Collections;

namespace Test.Integration.XUnit.TestCaseSourceClass
{
    public sealed class TestCaseSourceClassDataProperty : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();

        private static IEnumerable<object[]> Data =>
        [
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];
    }
}
