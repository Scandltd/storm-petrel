namespace Test.Integration.MSTest;

[TestClass]
public class NoExpectedVarAssertTest
{
    [TestMethod]
    public void AssertAreEqualTest()
    {
        //Act
        var actual = 100;

        //Assert
        Assert.AreEqual(123, actual);
        Assert.AreEqual(actual: actual, expected: 123);
    }
}
