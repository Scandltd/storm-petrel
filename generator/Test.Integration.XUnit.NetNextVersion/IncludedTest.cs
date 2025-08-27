namespace Test.Integration.XUnit.NetNextVersion;

/// <summary>
/// When local "IncludedTest.cs" and included "Included/IncludedTest.cs" have the same file name but different namespaces
/// Then baselines are successfully updated in both files.
/// </summary>
public class IncludedTest
{
    [Fact]
    public void Test() => Assert.Equal(123, 100);
}
