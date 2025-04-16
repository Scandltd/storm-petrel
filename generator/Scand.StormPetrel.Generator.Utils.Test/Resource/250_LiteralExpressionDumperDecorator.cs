new FooClass()
{
    IntProperty = 123,
    DoubleProperty = 12.3,
    NoCharsToEscape = "test'!@#$%^&*()_+",
    NoCharsToEscapeButVerbatimToken = @"test",
    LessThanThreeCharsButDoubleQuote = "a\"",
    MoreThanTwoCharsWithDoubleQuote = "test\" with double quote and two double quotes:\"\"",
    MultilineWithSlashNforAnyObject = "test\none more line",
    MultilineWithSlashR = "test\rone more line",
    //Comment before the property
    //One more line of comment
    WithCommentsAndDoubleQuote = "test\r\none more line" /*comment after*/, //one more comment
    IgnoredVerbatim = "test\none more line",
    NestedObjectProperty = new NestedObject
    {
        IntProperty = 123,
        MultilineWithSlashNforNestedObject = "test\none more line",
        MultilineWithSlashNforAnyObject = "test\none more line",
        NestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IgnoredVerbatim = "test\none more line",
            MultilineWithSlashNforAnyObject = "test\none more line",
        },
    },
    NestedObjectProperty2 = new NestedObject
    {
        MultilineWithSlashNforNestedObject = "test\none more line",
        MultilineWithSlashNforAnyObject = "test\none more line",
        IgnoredVerbatim = "test\none more line",
        IntProperty = 123,
    },
    NestedObjectFullNameProperty = new MyNamespace.MySubspace.NestedObject
    {
        IgnoredVerbatim = "test\none more line",
        MultilineWithSlashNforFullNameNestedObject = "test\none more line",
        MultilineWithSlashNforAnyObject = "test\none more line",
    },
    TargetTypedNewObject = new() { IntProperty = 123, MultilineWithSlashNforAnyObject = "test\none more line", IgnoredVerbatim = "test\none more line" },
}
