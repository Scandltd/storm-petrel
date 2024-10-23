namespace Scand.StormPetrel.Rewriter.Test
{
    public sealed class DataSourceHelperTestData
    {
        public static IEnumerable<object[]> PublicStaticMethod() =>
        [
            [1, 2, 3]
        ];

        public static IEnumerable<object[]> PublicStaticMethodWithArgs(int argInt, string argString) =>
        [
            [2 * argInt, argString?.Length ?? throw new ArgumentNullException(nameof(argString))]
        ];

        public IEnumerable<object[]> PublicNonStaticMethod() =>
        [
            [1]
        ];

        private static IEnumerable<object[]> PrivateStaticMethod() =>
        [
            [2]
        ];

        private static IEnumerable<object[]> PrivateStaticProperty =>
        [
            [3]
        ];

        private static readonly IEnumerable<object[]> PrivateStaticField =
        [
            [4]
        ];
    }
}
