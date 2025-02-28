namespace Test.Integration.NUnit;

public class NoExpectedVarWithOperatorsTest
{
    [Test]
    public void WhenNullConditionalOperatorTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        Assert.That(actual?.ValueAsHexString, Is.EqualTo("Incorrect value"));
        Assert.That(actual?.ValueAsHexString, Is.EqualTo(null));
        Assert.That(actual?.ValueAsHexString?.ToString(), Is.EqualTo("Incorrect value"));
        Assert.That(actual?.ValueAsHexStringAsMethod(), Is.EqualTo("Incorrect value"));
        Assert.That(actual?.ValueAsHexStringAsMethod()?.ToString(), Is.EqualTo("Incorrect value"));
        Assert.That(actual.ValueAsHexStringAsMethod()?.ToString(), Is.EqualTo("Incorrect value"));
    }

    [Test]
    public void WhenNullConditionalOperatorAssertsNullTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);
        actual.ValueAsHexString = null!;

        //Assert
        Assert.That(/*Test comment*/ actual?.ValueAsHexString, Is.EqualTo("Incorrect not null"));
        Assert.That(actual?.ValueAsHexString, Is.EqualTo(null));
        Assert.That(actual?.ValueAsHexString?.ToString(), Is.EqualTo("Incorrect not null"));
        Assert.That(actual?
                        .ValueAsHexStringAsMethod()?
                        .ToString(), Is.EqualTo("Incorrect not null"));
        Assert.That(actual.ValueAsHexString?.ToString(), Is.EqualTo("Incorrect not null"));
    }

    [Test]
    public void WhenNullForgivenOperatorTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        Assert.That(actual!.ValueAsHexString, Is.EqualTo("Incorrect value"));
        Assert.That(actual!.ValueAsHexString, Is.EqualTo(null));
        Assert.That(actual!.ValueAsHexString!.ToString(), Is.EqualTo("Incorrect value"));
        Assert.That(actual!.ValueAsHexStringAsMethod(), Is.EqualTo("Incorrect value"));
        Assert.That(actual!.ValueAsHexStringAsMethod()!.ToString(), Is.EqualTo("Incorrect value"));
        Assert.That(actual.ValueAsHexString!.ToString(), Is.EqualTo("Incorrect value"));
    }

    [Test]
    public void WhenNullCoalescingOperatorTest()
    {
        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        Assert.That((actual ?? throw new InvalidOperationException()).ValueAsHexString, Is.EqualTo("Incorrect value"));
        Assert.That((actual ?? throw new InvalidOperationException()).ValueAsHexString, Is.EqualTo(null));
        Assert.That((actual ?? throw new InvalidOperationException()).ValueAsHexString.ToString(), Is.EqualTo("Incorrect value"));
        Assert.That((actual ?? throw new InvalidOperationException()).ValueAsHexStringAsMethod(), Is.EqualTo("Incorrect value"));
        Assert.That((actual ?? throw new InvalidOperationException()).ValueAsHexStringAsMethod().ToString(), Is.EqualTo("Incorrect value"));
        Assert.That((actual ?? throw new InvalidOperationException()).ValueAsHexString.ToString(), Is.EqualTo("Incorrect value"));
    }
}
