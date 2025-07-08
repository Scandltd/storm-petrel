namespace Test.Integration.XUnit;

partial class RefStructTest
{
    /// <summary>
    /// This file is ignored in `appsettings.StormPetrel.json` because
    /// it demonstrates ref struct cases unsupported by Storm Petrel.
    /// If we don't ignore the file then we have
    /// CS0029 (Cannot implicitly convert type '[concrete ref struct type]' to 'object')
    /// comilation error of auto-generated StormPetrel tests due to Storm Petrel and Dump Libraries API limitations against `ref struct` types.
    /// We also use correct expected values here to avoid the build failure.
    /// </summary>
    [Fact]
    public void Utf8StringLiteralNonSupportedTest()
    {
        //Act
        var actual = "cdef"u8;
        //Assert
        Assert.Equal("cdef"u8, actual);
    }

    [Fact]
    public void StackAllocArrayCreationExpressionNonSupportedTest()
    {
        //Arrange
        Span<int> actual = stackalloc int[5];
        //Act
        FillActualSpan(actual);
        //Assert
        Assert.Equal([0, 1, 2, 3, 4], actual);
    }

    [Fact]
    public void ReadOnlySpanCreationExpressionNonSupportedTest()
    {
        //Arrange
        ReadOnlySpan<int> expected = [1, 2, 3];
        //Act
        ReadOnlySpan<int> act = [1, 2, 3];
        //Assert
        var actual = act.ToArray();
        Assert.Equal(expected, actual);
    }
}
