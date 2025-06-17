using FluentAssertions;
using Shouldly;

namespace Test.Integration.XUnit;

public class NoActualAndNoExpectedVarTest
{
    [Fact]
    public async Task AssertEqualWithAwait()
    {
        //Act, Assert
        Assert.Equal(123, await TestedClass.ResultMethodAsync());
    }

    [Fact]
    public async Task AssertEqualWithAwaitAndColonName()
    {
        //Act, Assert
        Assert.Equal(actual: await TestedClass.ResultMethodAsync(), expected: 123);
    }

    [Fact]
    public async Task FluentAssertionsWithAwait()
    {
        //Act, Assert
        (await TestedClass.ResultMethodAsync()).Should().Be(123);
    }

    [Fact]
    public async Task ShouldBeWithAwait()
    {
        //Assert
        (await TestedClass.ResultMethodAsync()).ShouldBe(123);
    }

    [Fact]
    public void ArrowExpressionBody() => TestedClass.TestedMethod().Should().Be(123);

    [Fact]
    public void ArrowExpressionBodyWithAssertEqual() => Assert.Equal(123, TestedClass.TestedMethod());

    [Fact]
    public async Task ArrowExpressionBodyWithAssertEqualAndAwait() => Assert.Equal(actual: await TestedClass.ResultMethodAsync(), expected: 123);

    [Fact]
    public async Task ArrowExpressionBodyWithFluentAssertionsWithAwait() => (await TestedClass.ResultMethodAsync()).Should().Be(123);

    [Theory]
    [InlineData(100)]
    public void ArrowExpressionBodyWithDefaultArgs(int arg) => TestedClass.ReturnInput(arg).Should().Be(123);

    [Theory]
    [InlineData(1, 2)]
    public void ArrowExpressionBodyWithArgs(int x, int y) => (x + y).Should().Be(123);
}
