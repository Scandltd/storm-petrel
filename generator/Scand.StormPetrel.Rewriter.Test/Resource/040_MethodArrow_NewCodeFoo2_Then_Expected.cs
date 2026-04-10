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
    new List<string>() { };
    object[] TestMethodAfter() => new object[] { };
}