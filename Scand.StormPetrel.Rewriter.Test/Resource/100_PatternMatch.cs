public class Foo
{
    public int[] ArrowMethodWithPatternMatch(string x) => x switch
    {
        "1" => new int[]
        {
            1,
        },
        "2" => new int[]
        {
            2,
            2,
        },
    };
}
