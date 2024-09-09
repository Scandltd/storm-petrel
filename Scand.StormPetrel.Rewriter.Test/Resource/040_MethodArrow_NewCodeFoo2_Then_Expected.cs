public class Foo
{
    int[] TestMethod() => new int[]
    {
        1,
        2,
        3,
    };
}

public class Foo2
{
    int[] TestMethodBefore() => null;
    int[] TestMethod()
        =>
    new List<string>() { };
    object[] TestMethodAfter() => new object[] { };
}