using System.Globalization;

namespace Test.Integration.XUnit;

/// <summary>
/// According to <see cref="https://github.com/xunit/assert.xunit/blob/main/"/>
/// </summary>
public class NoExpectedVarAssertTest
{
    #region Assert.cs
    [Fact]
    public void AssertEqualTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.Equal(123, actual);
        Assert.Equal(actual: actual, expected: 123);
        Assert.Equal(expected: 123, actual: actual);
    }

    [Fact(Skip = "This test is skipped because two runs are required and two separate generation calls are generated.")]
    public static void AssertEqualWithTwoExpectedVarTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var expected = 101;

        //Assert
        Assert.Equal(actual: actual, expected: expected);
        Assert.Equal(123, actual);
    }

    [Fact]
    public void AssertEqualWhenActualWithMethodCallTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        Assert.Equal("123", actual.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(actual: actual.ToString(CultureInfo.InvariantCulture), expected: "123");
    }

    [Fact]
    public void AssertEqualTwoStructuresTest()
    {
        //Act
        var actual = new TestedStructure(100, 100);

        //Assert
        Assert.Equal(new TestedStructure(123, 123), actual);
        Assert.Equal(new TestedStructure() { X = 123, Y = 123 }, actual);
        Assert.Equal(actual: actual, expected: new TestedStructure(123, 123));
    }

    [Fact]
    public void AssertEqualTwoTuplesTest()
    {
        //Act
        var actual = (x: 100, y: 100);

        //Assert
        Assert.Equal((123, 123), actual);
        Assert.Equal(actual: actual, expected: (x: 123, y: 123));
    }

    [Fact]
    public void AssertEqualTwoSystemTuplesTest()
    {
        //Act
        var actual = new Tuple<int, int>(100, 100);

        //Assert
        Assert.Equal(actual: actual, expected: new Tuple<int, int>(123, 123));
    }

    [Fact]
    public async Task AssertEqualWithAwaitOfActualTest()
    {
        //Arrange
        var actual = new ActualTestClass();

        //Act, Assert
        Assert.Equal(actual: await actual.TestMethodAsync(), expected: 123);
    }

    [Fact]
    public void AssertJaggedArrayTest()
    {
        //Arrange
        int[][] actual = [[1, 2, 3], [1, 2, 3]];
        int[] actualOneDimensional = [1, 2];
        int[][,] actualMultiArray = [new int[,] { { 1, 2, 3 }, { 1, 2, 3 } }, new int[,] { { 1, 2, 3 }, { 1, 2, 3 } }];
        int[][][] actual3d = [[[1], [2], [3]], [[1], [2], [3]]];
        int[][][][] actual4d = [(int[][][])[[(int[])[1, 2]], [(int[])[1, 2]]], (int[][][])[[(int[])[1, 2]]], (int[][][])[[(int[])[1, 2]]]];
        object[] actualObjects =
        [
            1,
            new DateTime(2025, 2, 4)
        ];

        //Act, Assert
        Assert.Equal(actual: actual, expected: [[123, 123, 123], [123, 123, 123]]);
        Assert.Equal([], actualOneDimensional);
        Assert.Equal([[], []], actual);
        Assert.Equal([(int[][])[[]], (int[][])[[]]], actual3d);
        Assert.Equal([new int[,] { { 123, 123, 123 }, { 123, 123, 123 } }, new int[,] { { 123, 123, 123 }, { 123, 123, 123 } }], actualMultiArray);
        Assert.Equal([(int[][]) [ [ 123, 123, 123 ], [ 123, 123, 123 ] ], (int[][]) [ [ 123, 123, 123 ], [ 123, 123, 123 ] ]], actual3d);
        Assert.Equal([(int[][][])[[(int[])[3, 4]],[(int[])[3, 4]]],(int[][][])[[(int[])[3, 4]]],(int[][][])[[(int[])[3, 4]]]], actual4d);
        Assert.Equal(
        [
            123,
            new DateTime(2025, 12, 14)
        ], actualObjects);

        //Assert ignored use cases
        //All asserts below have "expected" to not fail because Storm Petrel ignores these "expected" values
        Assert.Equal([(int[])[1, 2, 3], [1, 2, 3]], actual);
        Assert.Equal([[1, 2, 3], (int[])[1, 2, 3]], actual);
        Assert.Equal([[[1], [2], (int[])[3]], [[1], [2], [3]]], actual3d);
        Assert.Equal([[[1], [2], (int[])[3]], (int[][])[[1], [2], [3]]], actual3d);
        Assert.Equal([(int[][])[[1], [2], [3]], (int[][])[[1], (int[])[2], [3]]], actual3d);
    }
    #endregion

    #region AsyncCollectionAsserts.cs
    private static async IAsyncEnumerable<int> GetActualAsIAsyncEnumerableAsync()
    {
        yield return 1;
        yield return 2;
        yield return 3;
        await Task.CompletedTask; // Simulate asynchronous work
    }

    [Fact]
    public async Task AsyncCollectionAssertsEqualTest()
    {
        //Act
        var list = new List<int>();
        await foreach (var item in GetActualAsIAsyncEnumerableAsync())
        {
            list.Add(item);
        }
        var actual = list;

        //Assert
        Assert.Equal([123, 123, 123], actual);
        Assert.Equal(actual: actual, expected: [123, 123, 123]);
    }
    #endregion

    #region EqualityAsserts.cs
    [Fact]
    public void EqualityAssertsStrictEqualTest()
    {
        //Act
        var actual = new
        {
            Amount = 100,
            Message = "Hello"
        };

        //Assert
        Assert.StrictEqual(new
        {
            Amount = 123,
            Message = "Hello incorrect"
        }, actual);
        Assert.StrictEqual(actual: actual, expected: new
        {
            Amount = 123,
            Message = "Hello incorrect"
        });
    }
    #endregion

    #region EquivalenceAsserts.cs
    [Fact]
    public void EquivalenceAssertsEquivalentTest()
    {
        //Act
        var actual = new
        {
            Amount = 100,
            Message = "Hello"
        };

        //Assert
        Assert.Equivalent(new
        {
            Amount = 123,
            Message = "Hello incorrect"
        }, actual);
        Assert.Equivalent(strict: false, actual: actual, expected: new
        {
            Amount = 123,
            Message = "Hello incorrect"
        });
    }

    [Fact]
    public void AssertEquivalentWithVariedNumberOfExpressionArgumentsTest()
    {
        //Act
        var actual = new
        {
            Date = DateTime.ParseExact("2025-02-11T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
        };

        //Assert
        Assert.Equivalent(new
        {
            Date = new DateTime(123, 1, 1)
        }, actual);
        Assert.Equal(actual: actual.Date.Year, expected: 123);
        Assert.Equal(expected: 123, actual: actual.Date.Year);
    }
    #endregion

    #region MultipleAsserts.cs
    //Do not support this case till an explicit request
    #endregion

    [Fact]
    public void AssertEqualWhithInvocationExpressionTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        //The assertion does not fail by default. Also, StormPetrel does not replace the invocation expression below.
        //But we keep this use case to indicate no unexpected failures in StormPetrel here.
        Assert.Equal(TestedClass.TestedMethod(), actual);
        Assert.Equal(TestedClass.ReturnInput(TestedClass.TestedMethod()), actual);
    }
}
