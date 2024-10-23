public class Foo
{
    public void Bla()
    {
        var localVar = new int[]
        {
            1,
            2,
            3,
        };

        localVar = new int[]
        {
            4,
            5,
        };

        void BlaInBla()
        {
            var localVar = new List<string>() { };

            localVar = new int[]
            {
                7,
            };
        }

        void BlaInBla(string someArg)
        {
            var localVar = new int[]
            {
                8,
            };

            localVar = new int[]
            {
                9,
            };
        }
    }
}