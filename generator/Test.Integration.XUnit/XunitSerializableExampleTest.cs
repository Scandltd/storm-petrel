using FluentAssertions;

namespace Test.Integration.XUnit;

public sealed class XunitSerializableExampleTest
{
    [Theory]
    [MemberData(nameof(TheoryDataWithXunitSerializable))]
    public static void WhenTheoryDataWithClassWithoutProperEqualityOperatorThrowingTheException(int a, XunitSerializable<AddResult> bWrapped, XunitSerializable<AddResult> expected)
    {
        ArgumentNullException.ThrowIfNull(bWrapped);
        ArgumentNullException.ThrowIfNull(expected);
        //Arrange
        //Act
        var actualValue = Calculator.Add(a, bWrapped.Value!.Value);
        //Assert
        actualValue.Should().BeEquivalentTo(expected.Value);
    }
    public static TheoryData<int, XunitSerializable<AddResult>, XunitSerializable<AddResult>> TheoryDataWithXunitSerializable =>
    new()
    {
        {
            2,
            new()
            {
                Label = "XunitSerializable Input Value Example: 2",
                Value = new()
                {
                    Value = 2,
                }
            },
            new()
            {
                Label = "XunitSerializable Expected Example",
                Value = new()
                {
                    Value = 5,
                    ValueAsHexString = "0x5 - Incorrect",
                }
            }
        },
        {
            3,
            new()
            {
                Value = new()
                {
                    Value = -5,
                }
            },
            new()
            {
                Value = new()
                {
                    Value = 5,
                    ValueAsHexString = "0x5 - Incorrect",
                }
            }
        },
    };
}
