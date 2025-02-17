using NUnit.Framework.Constraints;
using System.Globalization;

namespace Test.Integration.NUnit;

/// <summary>
/// Constraint Model (Assert.That) [1] is supported only.
/// Classic Model [2] is not supported.
/// See more details in [3].
/// [1] <see cref="https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html"/>
/// [2] <see cref="https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/classic.html"/>
/// [3] <see cref="https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html"/>
/// </summary>
public class NoExpectedVarAssertThatTest
{
    [Test]
    public void AssertThatEqualToTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.That(actual, Is.EqualTo(123));
    }

    [Test]
    public void AssertThatActualWithMethodCallIsEqualToTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.That(actual.ToString(CultureInfo.InvariantCulture), Is.EqualTo("123"));
    }

    /// <summary>
    /// According to an example from <see cref="https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertion-models/constraint.html"/>
    /// </summary>
    [Test]
    public void AssertThatEqualConstraintTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.That(actual, new EqualConstraint(123));
        Assert.That(expression: new EqualConstraint(123), actual: actual);
    }

    [Test]
    public void AssertThatIsEqualToMultipleTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.That(actual, Is.EqualTo(123));
        Assert.That(expression: Is.EqualTo(123), actual: actual);
    }

    [Test]
    public void AssertThatIsEquivalentToTest()
    {
        //Act
        int[] actual = [1, 0, 0];

        //Assert
        Assert.That(actual, Is.EquivalentTo(new int[] { 1, 2, 3 }));
    }

    [Test]
    public void AssertThatCollectionEquivalentConstraintTest()
    {
        //Act
        int[] actual = [1, 0, 0];

        //Assert
        Assert.That(actual, new CollectionEquivalentConstraint(new int[] { 1, 2, 3 }));
    }
}
