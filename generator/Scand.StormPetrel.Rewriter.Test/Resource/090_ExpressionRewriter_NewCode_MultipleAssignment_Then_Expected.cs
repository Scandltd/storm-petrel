public class Foo
{
    public AddResult Bla()
    {
        AddResult localVar = null;

        localVar = new()
        {
            Value = "5",
        };

        return localVar;
    }

    public int ArrowMethod() => 123;

    public int[] ArrowMethodArray() => new int[]
    {
        1,
        2,
    };

    public AddResult ArrowMethodAddResult() => new AddResult()
    {
        Value = "5",
    };

    public int ReturnMethod()
    {
        return 123;
    }

    public int ReturnMethodArray()
    {
        return new int[]
        {
            1,
            2,
        };
    }

    public AddResult ReturnMethodAddResult()
    {
        return new AddResult
        {
            Value = "5",
        };
    }

    public int[] Bla(string arg1)
    {
        int[] localVar = null;

        localVar = new int[]
        {
            1,
            2,
        };

        return localVar;
    }

    public int[] Bla(string arg1, int arg2)
    {
        int[] localVar = null;

        localVar = new int[]
        {
            1,
            2,
        };

        return localVar;
    }

    public int[] Bla(string arg1, int arg2, int arg3)
    {
        int[] localVar;

        localVar = [];

        localVar = new List<string>() { };

        return localVar;
    }
}
