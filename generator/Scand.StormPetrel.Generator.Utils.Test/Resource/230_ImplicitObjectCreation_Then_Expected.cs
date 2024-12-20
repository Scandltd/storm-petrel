using System.Collections.Generic;
using System;

public void CollectionExpressionTest()
{
    var expected = new()
    {
        Values = new(4)
        {
            1 , 2, 3, 4,
        }, // comment
        NewValues = new()
        {
            1
        },
        Object = new()
        {
            Value = 1
        }
    };
}
