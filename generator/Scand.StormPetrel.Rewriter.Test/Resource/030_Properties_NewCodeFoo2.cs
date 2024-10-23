using System.Collections.Generic;

public class Foo
{
    int[] TestProperty { get; set; } = new List<string>() { };
}

public class Foo2
{
    int[] TestPropertyBefore { get; set; }
    int[] TestProperty { get; set; } = new List<string>() { };
    object[] TestPropertyAfter { get; set; }
}