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
    new int[]
    {
        4,
        5,
        6,
    };
    object[] TestMethodAfter() => new object[] { };
}