using System.Collections;

namespace Test.Integration.XUnit.TestCaseSourceClass
{
    public sealed class TestCaseSourceClassDataPropertyForInvocationPath : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();

        private static IEnumerable<object[]> Data =>
        [
            [1, 2, new AddResult
            {
                Value = -1000,
                ValueAsHexString = "Incorrect value",
            }],
            [-2, 2, new AddResult
            {
                Value = -1000,
                ValueAsHexString = "Incorrect value",
            }],
            [int.MinValue, -1, new AddResult()
            {
                Value = -1000,
                ValueAsHexString = "Incorrect value",
            }],
            [-4, -6, new AddResult()
            {
                Value = -1000,
                ValueAsHexString = "Incorrect value",
            }],
        ];
    }
}
