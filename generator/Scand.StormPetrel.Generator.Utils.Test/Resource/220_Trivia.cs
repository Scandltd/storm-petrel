using System.Collections.Generic;
using System;

public void CollectionExpressionTest()
{
    var expected = new AddResult
    {
        Values = new List<int>()
        {
            4
        }, // comment
        ValueAsHexString = "0x4",
        Hashes = new string[0],
        NewValues = new List<int>
        {
            5
        },
    };

    //Act
    var actual = Calculator.Add(2, 2);

    //Assert
    actual.Should().BeEquivalentTo(expected);
}
