using System.Collections.Generic;
using System;

public void CollectionExpressionTest()
{
    var expected = new AddResult
    {
        Values = new List<int>(4)
        {
            1 , 2, 3, 4,
        }, // comment
        NewValues = new List<int>
        {
            1
        },
        Object = new SomeClass
        {
            Value = 1
        }
    };
}
