using System.Collections.Concurrent;

new FooClass()
{
    BlaCollection = new ConcurrentDictionary<string, int> { ["key1"] = 1, ["key2"] = 2, ["key3"] = 3 }
}

