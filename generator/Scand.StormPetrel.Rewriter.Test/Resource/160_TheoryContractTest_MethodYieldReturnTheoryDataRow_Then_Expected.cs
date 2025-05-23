using FluentAssertions;

namespace Test.Integration.XUnit.v3;

public class TheoryContractTest
{
    [Theory]
    [MemberData(nameof(TheoryDataRowArray))]
    public void WhenTheoryDataRowIsArray(int a, int b, string expected)
    {
        //Act
        var actual = (a + b).ToString(); //emulate method being tested call

        //Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TheoryDataRowImplicitArray))]
    public void WhenTheoryDataRowImplicitArray(int a, int b, int expected)
    {
        //Act
        var actual = a + b;

        //Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TheoryDataRowMethodYieldReturn))]
    public void WhenMethodYieldReturnTheoryDataRow(int a, int b, int expected)
    {
        //Act
        var actual = a + b;

        //Assert
        actual.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(TheoryDataRowImplicitObjectArray))]
    public void WhenTheoryDataRowImplicitObjectArray(int input, List<int> expected)
    {
        //Act
        var actual = new List<int> { input };

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    public static IEnumerable<TheoryDataRow<int, int, string>> TheoryDataRowArray => new[]
    {
        new TheoryDataRow<int, int, string>(1, 2, "100"),
        new(0, 0, "100"),
        new(-1, -2, "100")
    };

    public static IEnumerable<TheoryDataRow> TheoryDataRowImplicitArray =>
    [
        new(1, 2, 100),
        new(0, 0, 100),
        new(-1, -2, 100)
    ];

    public static IEnumerable<TheoryDataRow> TheoryDataRowMethodYieldReturn()
    {
        yield return new(1, 123, 100);
        yield return new(0, 0, 100);
    }

    public static IEnumerable<TheoryDataRow> TheoryDataRowImplicitObjectArray =>
    [
        new(10,
            new List<int>
            {
                100
            }),
        new(20, new List<int>() { 100 }),
    ];
}
