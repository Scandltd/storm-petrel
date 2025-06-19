using FluentAssertions;

namespace Test.Integration.XUnit;

/// <summary>
/// The tests are created based on <see cref="Microsoft.CodeAnalysis.CSharp.SyntaxKind"/> reasonable values.
/// </summary>
public class SyntaxTokenTest
{
    [Fact]
    public void ShouldBeMinusTokenTest() => (-100).Should().Be(-123);

    [Fact]
    public void ShouldBePlusTokenTest() => 100.Should().Be(+123);

    [Fact]
    public void ShouldBeSingleQuouteTokenTest() => 'a'.Should().Be('b');

    [Fact]
    public void ShouldBeExclamationTokenTest() => false.Should().Be(!false);

    [Fact]
    public void AssertEqualMinusTokenTest() => Assert.Equal(-123, 100);

    [Fact]
    public void AssertEqualDefaultExpressionTest() => Assert.Equal(default, 100);

    [Fact]
    public void AssertEqualDefaultExpressionWithTypeTest() => Assert.Equal(default(int), 100);

    [Fact]
    public void AssertEqualInterpolatedStringTest() => Assert.Equal($"{123}", $"{100}");

    [Fact]
    public void AssertEqualInterpolatedVerbatimStringTest() => Assert.Equal(@$"{123}", @$"{100}");

    [Fact]
    public void AssertEqualInterpolatedRawStringTest() => Assert.Equal($"""
        {123}
        """, $"""
        {100}
        """);

    [Fact]
    public void ShouldBeInterpolatedStringExpressionTest() => $"{100}".Should().Be($"{123}");

    [Fact]
    public void ShouldBeOpenBracketTokenTest() => new int[] { 100 }.Should().BeEquivalentTo([123]);

    [Fact]
    public void AssertEquivalentNullTest() => Assert.Equivalent(null, new object());

    [Fact]
    public void AssertEqualNumericTypesTest()
    {
        //Act, Assert
        Assert.Equal(123f, 100.6f);
        Assert.Equal(123m, 100.7m);
        Assert.Equal(123U, 100U);
        Assert.Equal(123L, 100L);
        Assert.Equal(123UL, 100UL);
    }

    [Fact]
    public void AssertEqualBitwiseNotTest() => Assert.Equal(~123, 100);

    [Fact]
    public void ShouldBeOmittedArraySizeExpressionTest() =>
        new int[,] { { 100 } }.Should().BeEquivalentTo(new int[,] { { 123 } });

    [Fact]
    public void AssertEqualTupleTest() => Assert.Equal((123, 123), (100, 100));
}
