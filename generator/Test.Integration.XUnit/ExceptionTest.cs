using FluentAssertions;

namespace Test.Integration.XUnit;

public class ExceptionTest
{
    [Fact]
    public void VerifyExceptionTest()
    {
        //Act
        var actualException = Utils.SafeExecute(() => throw new InvalidOperationException("Message"));

        //Assert
        //Assert a property
        actualException?.Message.Should().Be("Incorrect message");
        //Assert via BeEquivalentTo
        actualException.ToExceptionInfo().Should().BeEquivalentTo(
        new ExceptionInfo
        {
            Type = "Incorrect Type",
            Message = "Incorrect Message",
            Source = "Incorrect Source"
        });
        //Assert via Snapshot Testing style
        actualException.ToExceptionInfoJson().Should().Be("{\"Type\":\"System.InvalidOperationException\",\"Message\":\"Incorrect message\",\"Source\":\"Test.Integration.XUnit\"}");

        //Storm Petrel cannot replace this code line argument. The developer do it manually if need.
        actualException.Should().BeOfType<InvalidOperationException>();
    }
}
