public class Foo
{
    int[] TestMethod() => new List<string>() { };
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