using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit;

public class NoMatchToExpectedVarRegexTest
{
    [Theory]
    [InlineData(100, 123)]
    [InlineData(200, 123)]
    public void ArrowExpressionBodyWithActualAndExpected(int arg, int exp) => TestedClass.ReturnInput(arg).Should().Be(exp);

    [Theory]
    [InlineData(100, 123, 200, 223)]
    [InlineData(200, 123, 300, 333)]
    public void AttributeWithMultipleExpected(int arg1, int exp1, int arg2, int exp2)
    {
        TestedClass.ReturnInput(arg1).Should().Be(exp1);
        TestedClass.ReturnInput(arg2).Should().Be(exp2);
    }

    [Theory]
    [InlineData(100, 123)]
    [InlineData(200, 123)]
    public void BodyWithMultipleExpected(int arg1, int exp1)
    {
        var exp2 = 123;

        TestedClass.ReturnInput(arg1).Should().Be(exp1);
        TestedClass.ReturnInput(100).Should().Be(exp2);
    }

    /// <summary>
    /// Use actual value to not fail build procedure.
    /// </summary>
    private const int IntConstant = 100;
    /// <summary>
    /// Use actual value to not fail build procedure.
    /// </summary>
    private readonly int IntProperty = 100;
    [Fact]
    public void WhenExpectedPropertyThenIgnoreIt() => Assert.Equal(IntProperty, TestedClass.TestedMethod());

    [Fact]
    public void WhenExpectedConstantThenIgnoreIt() => Assert.Equal(IntConstant, TestedClass.TestedMethod());

    [Fact]
    public void WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTest()
    {
        //Act
        var act = TestedClass.TestedMethod();
        var actAsString = act.ToString(CultureInfo.InvariantCulture);
        var exp = 123;
        var expAsString = "123";

        //Assert
        Assert.Equal(exp, act);
        Assert.Equal(expAsString, actAsString);
    }
}
