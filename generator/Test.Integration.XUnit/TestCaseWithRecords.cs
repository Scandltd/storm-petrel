namespace Test.Integration.XUnit;

public class TestCaseWithRecords
{
    [Fact]
    public void AssertEqualWithRecordTest()
    {
        //Act
        var actual = new TestedRecord(100);

        //Assert
        Assert.Equal(new TestedRecord(123), actual);
    }

    [Fact]
    public void AssertEqualWithRecordAndRecordPropertyTest()
    {
        //Act
        var actual = new TestedRecordWithProperty { Value = 100 };

        //Assert
        Assert.Equal(new TestedRecordWithProperty { Value = 123 }, actual);
    }
}

public record TestedRecord(int Value);

public record TestedRecordWithProperty
{
    public int Value { get; init; }
}