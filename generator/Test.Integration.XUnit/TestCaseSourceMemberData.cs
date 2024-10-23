namespace Test.Integration.XUnit
{
    internal static class TestCaseSourceMemberData
    {
        public static IEnumerable<object[]> Data =>
        [
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];
    }
}
