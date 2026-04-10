public class Foo
{
    AddResult TestMethod() => new AddResult()
    {
        Value = 4,
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