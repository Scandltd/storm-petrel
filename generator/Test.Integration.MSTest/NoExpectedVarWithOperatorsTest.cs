namespace Test.Integration.MSTest;

[TestClass]
public class NoExpectedVarWithOperatorsTest
{
    [TestMethod]
    public void WhenNullConditionalOperatorTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        Assert.AreEqual("Incorrect value", actual?.ValueAsHexString);
        Assert.AreEqual(null, actual?.ValueAsHexString);
        Assert.AreEqual("Incorrect value", actual?.ValueAsHexString?.ToString());
        Assert.AreEqual("Incorrect value", actual?.ValueAsHexStringAsMethod());
        Assert.AreEqual("Incorrect value", actual?.ValueAsHexStringAsMethod()?.ToString());
        ArgumentNullException.ThrowIfNull(actual);
        Assert.AreEqual("Incorrect value", actual.ValueAsHexStringAsMethod()?.ToString());
    }

    [TestMethod]
    public void WhenNullConditionalOperatorAssertsNullTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);
        actual.ValueAsHexString = null!;

        //Assert
        Assert.AreEqual("Incorrect not null", actual?.ValueAsHexString);
        Assert.AreEqual(null, actual?.ValueAsHexString);
        Assert.AreEqual("Incorrect not null", actual?.ValueAsHexString?.ToString());
        Assert.AreEqual("Incorrect not null", actual?
                                                .ValueAsHexStringAsMethod()?
                                                .ToString());
        ArgumentNullException.ThrowIfNull(actual);
        Assert.AreEqual("Incorrect not null", actual.ValueAsHexString?.ToString());
    }

    [TestMethod]
    public void WhenNullForgivenOperatorTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        Assert.AreEqual("Incorrect value", actual!.ValueAsHexString);
        Assert.AreEqual(null, actual!.ValueAsHexString);
        Assert.AreEqual("Incorrect value", actual!.ValueAsHexString!.ToString());
        Assert.AreEqual("Incorrect value", actual!.ValueAsHexStringAsMethod());
        Assert.AreEqual("Incorrect value", actual!.ValueAsHexStringAsMethod()!.ToString());
        Assert.AreEqual("Incorrect value", actual.ValueAsHexString!.ToString());
    }

    [TestMethod]
    public void WhenNullCoalescingOperatorTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        Assert.AreEqual("Incorrect value", (actual ?? throw new InvalidOperationException()).ValueAsHexString);
        Assert.AreEqual(null, (actual ?? throw new InvalidOperationException()).ValueAsHexString);
        Assert.AreEqual("Incorrect value", (actual ?? throw new InvalidOperationException()).ValueAsHexString.ToString());
        Assert.AreEqual("Incorrect value", (actual ?? throw new InvalidOperationException()).ValueAsHexStringAsMethod());
        Assert.AreEqual("Incorrect value", (actual ?? throw new InvalidOperationException()).ValueAsHexStringAsMethod().ToString());
        Assert.AreEqual("Incorrect value", (actual ?? throw new InvalidOperationException()).ValueAsHexString.ToString());
    }
}
