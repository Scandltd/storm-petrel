using System.Collections.Concurrent;

new FooClass()
{
    BlaCollection = new BlockingCollection<int>(new[] { 1, 2, 3, 4, 5 })
}