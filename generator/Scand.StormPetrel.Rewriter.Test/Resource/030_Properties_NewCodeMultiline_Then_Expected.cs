public class Foo
{
    int[] TestProperty { get; set; } = new List<string>()
    {
        "One",
        "Two",
        "Three",
    };
}

public class Foo2
{
    int[] TestPropertyBefore { get; set; }
    int[] TestProperty { get; set; } = new int[]
    {
        4,
        5,
        6,
    };
    object[] TestPropertyAfter { get; set; }
}