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
        int[] localVar = new int[]
        {
            4,
            5,
            6,
        };
        localVar = new int[]
        {
            7,
            8,
        };
        var afterLocalVar = -1;
        localVar = new int[]
        {
            9,
            10,
        };
        //Some comment
    }

    private void Bla2(string x)
    {
        int[] localVar = new List<string>() { };
    }
}
