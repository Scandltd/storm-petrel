public class Foo
{
    public void ShouldBe123()
    {
        //Act
        var actual = TestedClass.TestedMethod(1, 2, "3");

        //Assert
        actual.Should().Be(100);
    }

    public void ShouldBeArgumentWithNameColon()
    {
        //Assert
        actual.Should().Be(because: "some explanation", expected: 123);
    }
}
