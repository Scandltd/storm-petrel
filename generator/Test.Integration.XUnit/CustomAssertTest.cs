using Test.Integration.XUnit.CustomAssert;

//Use `Assert` for custom assertion
using Assert = Test.Integration.XUnit.CustomAssert.XUnitStyleAssert;

namespace Test.Integration.XUnit;

public class CustomAssertTest
{
    [Fact]
    public void XUnitStyleTest() => Assert.Equal(123, TestedClass.TestedMethod());

    [Fact]
    public void XUnitStyleWithExtraParameterTest() => Assert.Equal(123, TestedClass.TestedMethod(), "custom assert equal failure example");

    [Fact]
    public async Task XUnitStyleTestAsync() => Assert.Equal(123, await TestedClass.ResultMethodAsync());

    [Fact]
    public void XUnitStyleEquivalentTest() => Assert.Equivalent(new AddResult
    {
        Value = 5,
    }, Calculator.Add(2, 2));

    [Fact]
    public void FluentAssertionStyleTest() => TestedClass.TestedMethod().Should().Be(123);

    [Fact]
    public void FluentAssertionStyleWithExtraParameterTest() => TestedClass.TestedMethod().Should().Be(123, true);

    [Fact]
    public async Task FluentAssertionStyleTestAsync() => (await TestedClass.ResultMethodAsync()).Should().Be(123);

    [Fact]
    public void FluentAssertionEquivalentTest() =>
        Calculator
            .Add(2, 2)
            .Should()
            .BeEquivalentTo(new AddResult
            {
                Value = 5,
            });

    [Fact]
    public void ShouldlyStyleTest() => TestedClass.TestedMethod().ShouldBe(123);

    [Fact]
    public void ShouldlyStyleWithExtraParameterTest() => TestedClass.TestedMethod().ShouldBe(123, true);

    [Fact]
    public async Task ShouldlyStyleTestAsync() => (await TestedClass.ResultMethodAsync()).ShouldBe(123);
}

