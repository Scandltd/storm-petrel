using System.Text;

namespace Test.Integration.XUnit;

public partial class RefStructTest
{
    [Fact]
    public void Utf8StringLiteralTest()
    {
        //Act
        var actual = "cdef"u8;
        //Assert
        Assert.Equal("abc", Encoding.UTF8.GetString(actual));
    }

    [Fact]
    public void StackAllocArrayCreationExpressionTest()
    {
        //Arrange
        Span<int> actual = stackalloc int[5];
        //Act
        FillActualSpan(actual);
        //Assert
        Assert.Equal([1, 2, 3], actual.ToArray());
    }

    [Fact]
    public void ReadOnlySpanCreationExpressionTest()
    {
        //Arrange
        int[] expected = [1, 2, 3];
        //Act
        ReadOnlySpan<int> act = [4, 5, 6];
        //Assert
        var actual = act.ToArray();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ReadOnlySpanFromMethodExpressionTest()
    {
        //Arrange
        int[] expected = [1, 2, 3];
        //Act
        var actual = GetActualAsReadOnlySpan().ToArray();
        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ArrowMethodWithActualReadOnlySpanFromMethodExpressionTest()
        => Assert.Equal([1, 2, 3], GetActualAsReadOnlySpan().ToArray());

    private static ReadOnlySpan<int> GetActualAsReadOnlySpan() => [4, 5, 6];
    private static void FillActualSpan(Span<int> actual)
    {
        for (int i = 0; i < actual.Length; i++)
        {
            actual[i] = i;
        }
    }
}
