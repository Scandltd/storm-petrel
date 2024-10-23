namespace Test.Integration.NUnit
{
    internal static class TestCaseSource
    {
        public static IEnumerable<object[]> Data =>
        [
            [1, 2, 0],
            [-2, 2, -100],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];
    }
}
