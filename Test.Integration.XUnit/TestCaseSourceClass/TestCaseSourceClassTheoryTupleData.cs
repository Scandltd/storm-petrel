namespace Test.Integration.XUnit.TestCaseSourceClass
{
    public class TestCaseSourceClassTheoryTupleData : TheoryData<int, int, AddResult>
    {
        public TestCaseSourceClassTheoryTupleData()
        {
            foreach (var useCase in DataMethod())
            {
                Add(useCase.Item1, useCase.Item2, useCase.Item3);
            }
        }

        private static IEnumerable<(int, int, AddResult)> DataMethod() =>
        [
            (1, 2, new AddResult()),
            (-2, 2, new AddResult()),
            (int.MinValue, -1, new AddResult()),
            (-4, -6, new AddResult()),
        ];
    }
}

