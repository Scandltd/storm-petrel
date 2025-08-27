using System.Globalization;
using FluentAssertions;
using Test.Integration.Included;

namespace Test.Integration.XUnit.NetNextVersion;

public class IncludedDataSourceTest
{
    [Theory]
    [MemberData(nameof(IncludedDataSource.DataMethod), MemberType = typeof(IncludedDataSource))]
    public void DataMethodInIncludedFile(int x, int y, int expected, string expectedHexString)
    {
        var actual = x + y;
        var actualHexString = "0x" + actual.ToString("x", CultureInfo.InvariantCulture);
        actual.Should().Be(expected);
        actualHexString.Should().Be(expectedHexString);
    }

    [Fact]
    public void ExpectedPropertyInIncludedFile()
    {
        //Arrange
        int expected = IncludedDataSource.ExpectedProperty;

        //Act
        var actual = 100;

        //Assert
        actual.Should().Be(expected);
    }
}
