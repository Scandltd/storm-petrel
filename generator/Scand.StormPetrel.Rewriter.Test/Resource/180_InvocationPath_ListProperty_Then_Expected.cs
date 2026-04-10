public class FooClass
{
    public static void WhenVariableAssignment()
    {
        Bla localVar;
        localVar = new Bla()
        {
            FooProperty = new List<string>()
            {
                "1",
                "2"
            },
        };
    }
}
