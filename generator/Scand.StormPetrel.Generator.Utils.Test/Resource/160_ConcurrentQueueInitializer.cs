using System.Collections.Concurrent;

new FooClass()
{
    BlaCollection = new ConcurrentQueue<int>(new[] { 1, 2, 3, 4, 5 } )
}

