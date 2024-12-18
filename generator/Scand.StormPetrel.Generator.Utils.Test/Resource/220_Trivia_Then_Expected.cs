using System.Collections.Generic;
using System;

public void CollectionExpressionTest()
{
    var expected = new AddResult
    {
        Values =
        [
            4
        ], // comment
        ValueAsHexString = "0x4",
        Hashes = [],
        NewValues =
        [
            5
        ],
    };

    //Act
    var actual = Calculator.Add(2, 2);

    //Assert
    actual.Should().BeEquivalentTo(expected);
}
