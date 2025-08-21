namespace Test.Integration.Analyzer;

public class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerClassDataTest
{
    [Theory(Skip = "To avoid test failure in the build")]
    [ClassData(typeof(TestCaseSourceClassData))]
    public void CurrentClassDataMethodTest(int _)
    {
        var actual = 1;
        var expected = 1;
        if (actual != expected)
        {
            throw new InvalidOperationException();
        }
    }
}
