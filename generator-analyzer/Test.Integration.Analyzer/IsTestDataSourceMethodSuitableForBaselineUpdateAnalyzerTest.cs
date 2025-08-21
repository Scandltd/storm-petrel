namespace Test.Integration.Analyzer
{
    public class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest
    {
        [Theory(Skip = "To avoid test failure in the build")]
        [MemberData(nameof(DataMethod))]
        public void CurrentClassDataMethodTest(int _)
        {
            var actual = 1;
            var expected = 1;
            AssertEqual(expected, actual);
        }
        [Theory]
        [MemberData(nameof(DataMethodSuitableForUpdate))]
        public void CurrentClassDataMethodSuitableForUpdateTest(int input, int expected)
        {
            //Act
            var actual = input % 3; //emulate
            //Assert
            AssertEqual(expected, actual);
        }
        [Theory(Skip = "To avoid test failure in the build")]
        [MemberData(nameof(TestDataSource.TestDataSourceDataMethod), MemberType = typeof(TestDataSource))]
        public void OtherClassDataMethodTest(int _)
        {
            var actual = 1;
            var expected = 1;
            AssertEqual(expected, actual);
        }
        [Theory(Skip = "To avoid test failure in the build"), MemberData(nameof(AttributeListDataMethod))]
        public void AttributeListTest(int _)
        {
            var actual = 1;
            var expected = 1;
            AssertEqual(expected, actual);
        }
        public static IEnumerable<object[]> DataMethod()
        {
            throw new NotImplementedException("To have the diagnostic reported");
        }
        public static object[][] DataMethodSuitableForUpdate() =>
        [
            [1, 1],
            [5, 2],
        ];
        public static IEnumerable<object[]> AttributeListDataMethod() =>
            throw new NotImplementedException("To have the diagnostic reported");
        /// <summary>
        /// To avoid Xunit.Assert due to Unit tests limitations where this file is also referenced
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="actual"></param>
        /// <exception cref="InvalidOperationException"></exception>
        private static void AssertEqual(int expected, int actual)
        {
            if (actual != expected)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
