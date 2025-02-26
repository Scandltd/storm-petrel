using FluentAssertions;

namespace Test.Integration.XUnit;

public class ExceptionTest
{
    [Fact]
    public void VerifyExceptionTest()
    {
        //Act
        var actual = Utils.SafeExecute(() => throw new InvalidOperationException("Message"));

        //Assert
        ArgumentNullException.ThrowIfNull(actual, nameof(actual));
        //Assert a property
        actual.Message.Should().Be("Incorrect message");
        //Assert via BeEquivalentTo
        actual.ToExceptionInfo().Should().BeEquivalentTo(
        new ExceptionInfo
        {
            Type = "Incorrect Type",
            Message = "Incorrect Message",
            Source = "Incorrect Source"
        });
        //Assert via Snapshot Testing style
        actual.ToExceptionInfoJson().Should().Be("{\"Type\":\"System.InvalidOperationException\",\"Message\":\"Incorrect message\",\"Source\":\"Test.Integration.XUnit\"}");

        //Storm Petrel cannot replace this code line argument. The developer do it manually if need.
        actual.Should().BeOfType<InvalidOperationException>();
    }
}
