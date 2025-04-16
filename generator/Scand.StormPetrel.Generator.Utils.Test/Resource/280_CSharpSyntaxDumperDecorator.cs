new FooClass()
{
    #region Collection Expressions
    ArrayProperty = new[] { 1, 2, 3 },
    #endregion
    #region Remove Assignment
    ToRemove = new(),
    ToNotRemove = new(),
    #endregion
    #region Literal Expressions
    IntProperty = 123,
    IgnoredDoubleProperty = 12.3,
    #endregion
}
