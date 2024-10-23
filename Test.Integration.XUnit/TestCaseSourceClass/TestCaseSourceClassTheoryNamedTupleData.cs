namespace Test.Integration.XUnit.TestCaseSourceClass
{
    public sealed class TestCaseSourceClassTheoryNamedTupleData : TheoryData<int, int, int>
    {
        public TestCaseSourceClassTheoryNamedTupleData()
        {
            foreach (var (a, b, expected) in DataMethod())
            {
                Add(a, b, expected);
            }
        }

        private static IEnumerable<(int A, int B, int Expected)> DataMethod() =>
        [
            (1, 2, 100),
            (-2, 2, 100),
            (int.MinValue, -1, 100),
            (-4, -6, 100),
        ];
    }
}
