namespace Test.Integration.XUnit.TestCaseSourceClass
{
    public sealed class TestCaseSourceClassMultipleExpected : TheoryData<int, int, int, string>
    {
        public TestCaseSourceClassMultipleExpected()
        {
            foreach (var (a, b, expected, expectedHexString) in DataMethod())
            {
                Add(a, b, expected, expectedHexString);
            }
        }

        private static IEnumerable<(int A, int B, int Expected, string ExpectedHexString)> DataMethod() =>
        [
            (1, 2, 100, "0x100"),
            (-2, 2, 100, "0x100"),
            (int.MinValue, -1, 100, "0x100"),
            (-4, -6, 100, "0x100"),
        ];
    }
}
