public class Foo
{
    public void Bla()
    {
        int[] localVar = new int[]
        {
            1,
            2,
            3,
        };
    }

    private void Bla2()
    {
        var beforeLocalVar = new object();
        int[] localVar = new List<string>() { };
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