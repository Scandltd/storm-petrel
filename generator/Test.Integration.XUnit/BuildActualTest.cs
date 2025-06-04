namespace Test.Integration.XUnit;

/// <summary>
/// Storm Petrel injects baseline update logic immediately after the last `actual` or `expected` value assignment.
/// When actual values are generated via:
/// - Mocks (e.g., Moq/NSubstitute)
/// - Callbacks
/// - Complex algorithms
/// The baseline rewriting behavior may require test adjustments for reliable updates.
/// So, the class demonstrates related:
/// - ✅ Working patterns for baseline updates
/// - ⚠️ Edge cases requiring modifications
/// </summary>
public class BuildActualTest
{
    /// <summary>
    /// When
    /// - Build `actual` from elements
    /// - And `expected` var is assigned before `actual` value is ready for assertion
    /// - And `actual` var is assigned from temp variable
    /// Then baseline is properly updated.
    /// </summary>
    [Fact]
    public void WhenBuildActualFromElementsAndTempVarThenProperlyUpdated()
    {
        //Arrange
        //Use temp variable to build actual value from the elements
        var tempElements = new List<int>();
        var expected = new[] { 123, 123, 123 };

        //Act
        foreach (var item in EmulateActActualElements())
        {
            tempElements.Add(item);
        }
        var actual = tempElements; //Assign actual when temp variable is ready

        //Assert
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// When
    /// - Build `actual` from elements
    /// - And `expected` var is assigned after `actual` value is ready for assertion
    /// Then baseline is properly updated.
    /// </summary>
    [Fact]
    public void WhenBuildActualFromElementsAndExpectedVarIsAfterActualIsReadyThenProperlyUpdated()
    {
        //Arrange
        //Temporary var is not required to build actual value from the elements
        var actual = new List<int>();

        //Act
        foreach (var item in EmulateActActualElements())
        {
            actual.Add(item);
        }

        //Assert
        var expected = new[] { 123, 123, 123 };
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// When
    /// - Build `actual` from elements
    /// - And `expected` value is inlined in `actual` value assertion
    /// Then baseline is properly updated.
    /// </summary>
    [Fact]
    public void WhenBuildActualFromElementsAndExpectedValueIsInlinedThenProperlyUpdated()
    {
        //Arrange
        //Temporary var is not required to build actual value from the elements
        var actual = new List<int>();

        //Act
        foreach (var item in EmulateActActualElements())
        {
            actual.Add(item);
        }

        //Assert
        Assert.Equal([123, 123, 123], actual);
    }

    /// <summary>
    /// When
    /// - Build `actual` from elements
    /// - And `expected` var is assigned before `actual` value is ready for assertion
    /// Then baseline is NOT properly updated.
    /// See other test methods of the class demonstrating possible modification options to have proper updates.
    /// </summary>
    [Fact]
    public void WhenBuildActualFromElementsAndExpectedVarIsBeforeActualIsReadyThenNotProperlyUpdated()
    {
        //Arrange
        //No temp variable to build actual value from the elements.
        var actual = new List<int>();
        var expected = new[] { 123, 123, 123 };

        //Act
        foreach (var item in EmulateActActualElements())
        {
            actual.Add(item);
        }

        //Assert
        //Storm Petrel rewrites expected variable to empty list in this case what behavior is undesired because actual is not empty.
        //Assert this undesired state to demonstrate the behavior and avoid build failure.
        Assert.Equal(3, actual.Count);
        Assert.Empty(expected);
    }

    /// <summary>
    /// When
    /// - Build `actual` via mocks or callbacks
    /// - And `expected` var is assigned before `actual` value is ready for assertion
    /// - And `actual` var is assigned from temp variable
    /// Then baseline is properly updated.
    /// </summary>
    [Fact]
    public void WhenBuildActualViaMockOrCallbackAndTempVarThenProperlyUpdated()
    {
        //Act
        //Use temp variable to build actual value from the callback
        var tempElements = new List<string>();
        var expected = new[] { "incorrect value", "incorrect", "incorrect" };

        //Act
        EmulateActViaMockOrCallback((_, x) => tempElements.Add(x));
        var actual = tempElements; //Assign actual when temp variable is ready

        //Assert
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// When
    /// - Build `actual` via mocks or callbacks
    /// - And `expected` var is assigned after `actual` value is ready for assertion
    /// Then baseline is properly updated.
    /// </summary>
    [Fact]
    public void WhenBuildActualViaMockOrCallbackAndExpectedVarIsAfterActualIsReadyThenProperlyUpdated()
    {
        //Act
        //Temporary var is not required to build actual value from the elements
        var actual = new List<string>();

        //Act
        EmulateActViaMockOrCallback((_, x) => actual.Add(x));

        //Assert
        var expected = new[] { "incorrect value", "incorrect", "incorrect" };
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// When
    /// - Build `actual` via mocks or callbacks
    /// - And `expected` value is inlined in `actual` value assertion
    /// Then baseline is properly updated.
    /// </summary>
    [Fact]
    public void WhenBuildActualViaMockOrCallbackAndExpectedValueIsInlinedThenProperlyUpdated()
    {
        //Act
        //Temporary var is not required to build actual value from the elements
        var actual = new List<string>();

        //Act
        EmulateActViaMockOrCallback((_, x) => actual.Add(x));

        //Assert
        Assert.Equal(["incorrect value", "incorrect", "incorrect"], actual);
    }

    private static IEnumerable<int> EmulateActActualElements()
    {
        yield return 1;
        yield return 2;
        yield return 3;
    }

    private static void EmulateActViaMockOrCallback(Action<int, string> callback)
    {
        callback(1, "1");
        callback(2, "2");
        callback(3, "3");
    }
}
