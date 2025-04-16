new FooClass()
{
    IntProperty = 123,
    DoubleProperty = 12.3,
    NoCharsToEscape = "test'!@#$%^&*()_+",
    NoCharsToEscapeButVerbatimToken = @"test",
    LessThanThreeCharsButDoubleQuote = "a\"",
    MoreThanTwoCharsWithDoubleQuote = @"test"" with double quote and two double quotes:""""",
    MultilineWithSlashNforAnyObject = @"test
one more line",
    MultilineWithSlashR = @"testone more line",
    //Comment before the property
    //One more line of comment
    WithCommentsAndDoubleQuote = @"test
one more line" /*comment after*/, //one more comment
    IgnoredVerbatim = "test\none more line",
    NestedObjectProperty = new NestedObject
    {
        IntProperty = 123,
        MultilineWithSlashNforNestedObject = @"test
one more line",
        MultilineWithSlashNforAnyObject = @"test
one more line",
        NestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IgnoredVerbatim = "test\none more line",
            MultilineWithSlashNforAnyObject = @"test
one more line",
        },
    },
    NestedObjectProperty2 = new NestedObject
    {
        MultilineWithSlashNforNestedObject = @"test
one more line",
        MultilineWithSlashNforAnyObject = @"test
one more line",
        IgnoredVerbatim = "test\none more line",
        IntProperty = 123,
    },
    NestedObjectFullNameProperty = new MyNamespace.MySubspace.NestedObject
    {
        IgnoredVerbatim = "test\none more line",
        MultilineWithSlashNforFullNameNestedObject = @"test
one more line",
        MultilineWithSlashNforAnyObject = @"test
one more line",
    },
    TargetTypedNewObject = new() { IntProperty = 123, MultilineWithSlashNforAnyObject = @"test
one more line", IgnoredVerbatim = "test\none more line" },
}
