using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit;

public class NoExpectedVarTest
{
    [Fact]
    public void ShouldBeTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        actual.Should().Be(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentWhenMultipleArgsTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        actual.Should().Be(123, "some explanation");
    }

    [Fact]
    public void ShouldDetectExpectedArgumentWhenMultipleNamedArgsTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        actual.Should().Be(because: "some explanation", expected: 123);
    }

    [Fact]
    public void ShouldBeWhenObjectCreationExpressionAndMultipleNamedArgsTest()
    {
        //Act
        var actualVar = TestedClass.TestedClassResultMethodWithIgnorable();

        //Assert
        actualVar.Should().BeEquivalentTo(because: "some explanation", expectation: new TestClassResultWithIgnorable()
        {
            StringNullableProperty = "Incorrect Test StringNullableProperty",
            DateTimeProperty = DateTime.ParseExact("2024-05-20T10:05:15.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
        }, config: config => config.Excluding(x => x.StringPropertyIgnored));
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualWithPropertyTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual.IntProperty.Should().Be(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualWithMethodTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual.IntPropertyAsMethod().Should().Be(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualCouplePropertiesOrMethodsTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual
            .DateTimePropertyAsMethod()
            .Day
            .ToString(CultureInfo.InvariantCulture)
            .Should().BeEquivalentTo("123");
    }

    [Fact]
    public void ShouldBeWhenMultipleActualsTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var actual2 = 100;

        //Assert
        actual.Should().Be(123);
        actual2.Should().Be(123);
    }

    [Fact]
    public void ShouldBeWhithInvocationExpressionTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        //The assertion does not fail by default. Also, StormPetrel does not replace the invocation expression below.
        //But we keep this use case to indicate no unexpected failures in StormPetrel here.
        actual.Should().Be(TestedClass.TestedMethod());
        actual.Should().Be(TestedClass.ReturnInput(TestedClass.TestedMethod()));
    }
}
