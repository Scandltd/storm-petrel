namespace Test.Integration.MSTest
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

        public static IEnumerable<object[]> DataForNamedParameters =>
        [
            [1, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];
    }
}
