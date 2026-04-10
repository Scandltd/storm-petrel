public class Foo
{
    public int[] ArrowMethodWithPatternMatch(string x) => x switch
    {
        "1" => new int[]
        {
            7,
            8,
        },
        "2" => new int[]
        {
            2,
            2,
        },
        "3" => new AddResult()
        {
            Value = "5",
        },
    };
}
