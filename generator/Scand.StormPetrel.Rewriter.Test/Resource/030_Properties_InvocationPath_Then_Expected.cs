public class Foo
{
    AddResult TestProperty { get; set; } = new()
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
    int[] TestPropertyBefore { get; set; }
    int[] TestProperty { get; set; } = new int[]
    {
        4,
        5,
        6,
    };
    object[] TestPropertyAfter { get; set; }
}