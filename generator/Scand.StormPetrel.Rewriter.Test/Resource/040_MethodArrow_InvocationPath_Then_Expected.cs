public class Foo
{
    AddResult TestMethod() => new AddResult()
    {
        Value = new List<string>()
        {
            "1",
            "2"
        },
    };
}

public class Foo2
{
    int[] TestMethodBefore() => null;
    int[] TestMethod()
        =>
    new int[]
    {
        4,
        5,
        6,
    };
    object[] TestMethodAfter() => new object[] { };
}