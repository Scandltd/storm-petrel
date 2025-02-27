using System.Collections.Generic;

public class Foo
{
    public void ShouldBeCorrectTrivia()
    {
        //Assert
        actual.Should().Be(
            new List<string>()
            {
                "1",
                "2"
            });
    }
}
