using FluentAssertions;

namespace Test.Integration.XUnit;

public class NoExpectedVarWithOperatorsTest
{
    [Fact]
    public void WhenNullConditionalOperatorTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual?.StringNullableProperty.Should().Be("Incorrect value");
        actual?.StringNullableProperty.Should().Be(null);
        actual?.StringNullableProperty?.Should().Be("Incorrect value");
        actual?
            .StringNullablePropertyAsMethod()?
            .Should()
            .BeEquivalentTo("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty?.Should().Be("Incorrect value");
    }

    [Fact]
    public void WhenNullConditionalOperatorAssertsNullTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        actual.StringNullableProperty = null;

        //Assert
        /*Test comment*/actual?.StringNullableProperty.Should().Be("Incorrect not null");
        //Test comment
        actual?.StringNullableProperty.Should().Be(null);
        //Test multiline
        //comment
        actual?.StringNullableProperty?.Should().Be("Incorrect not null");
        /*Test comment*/
        actual?
            .StringNullablePropertyAsMethod()?
            .Should()
            .BeEquivalentTo("Incorrect not null");
        ArgumentNullException.ThrowIfNull(actual);
        /*Test multiline
         *comment*/
        actual.StringNullableProperty?.Should().Be("Incorrect not null");
    }

    [Fact]
    public void WhenNullForgivenOperatorTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual!.StringNullableProperty.Should().Be("Incorrect value");
        actual!.StringNullableProperty.Should().Be(null);
        actual!.StringNullableProperty!.Should().Be("Incorrect value");
        actual!
            .StringNullablePropertyAsMethod()!
            .Should()
            .BeEquivalentTo("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty!.Should().Be("Incorrect value");
    }

    [Fact]
    public void WhenNullCoalescingOperatorTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        (actual ?? throw new InvalidOperationException()).StringNullableProperty.Should().Be("Incorrect value");
        (actual ?? throw new InvalidOperationException()).StringNullableProperty.Should().Be(null);
        (actual ?? throw new InvalidOperationException()).StringNullableProperty?.Should().Be("Incorrect value");
        ((actual ?? throw new InvalidOperationException())
            .StringNullablePropertyAsMethod() ?? throw new InvalidOperationException())
            .Should()
            .BeEquivalentTo("Incorrect value");
        (actual.StringNullableProperty ?? throw new InvalidOperationException()).Should().Be("Incorrect value");
    }
}
