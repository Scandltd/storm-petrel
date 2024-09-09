public class Foo
{
    public void Bla()
    {
        int[] localVar = new List<string>()
        {
            "One",
            "Two",
            "Three",
        };
    }

    private void Bla2()
    {
        var beforeLocalVar = new object();
        int[] localVar = new int[]
        {
            4,
            5,
            6,
        };
        var afterLocalVar = -1;
    }

    private void Bla2()
    {
        int[] localVar, oneMoreVar = new int[]
        {
            4,
            5,
            6,
        };
    }
}