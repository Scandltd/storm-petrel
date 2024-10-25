public class Foo
{
    public void Bla()
    {
        var localVar = new List<string>()
        {
            "One",
            //Multiline json
            @"{
    ""propertyA"": ""valueA"",
    ""propertyB"": ""valueB
multiline"",
    ""propertyC"": 5,
}",
            "Three",
        };
    }
}