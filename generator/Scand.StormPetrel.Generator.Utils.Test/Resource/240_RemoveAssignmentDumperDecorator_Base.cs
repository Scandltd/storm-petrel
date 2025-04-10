new FooClass()
{
    IntProperty = 123,
    DoubleProperty = 12.3,
    StringProperty = "test",
    StringPropertyMultiline = @"test
one more line",
    //Comment before the property
    //One more line of comment
    StringPropertyWithComments = "test" /*comment after*/, //one more comment
    RemovedFromAnyParent = "removed from any parent",
    NotRemovedProperty = 12345,
    NotRemovedNestedObjectProperty = new NestedObject
    {
        IntProperty = 123,
        StringProperty = "test",
        NotRemovedProperty = "test not removed",
        RemovedFromAnyParent = "removed from any parent",
        NestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IntProperty = 123,
            RemovedFromAnyParent = "removed from any parent",
            NotRemovedProperty = "test not removed",
            NotRemovedProperty2 = "test not removed2",
            StringProperty = "test"
        },
        NotRemovedNestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IntProperty = 123,
            NotRemovedProperty = "test not removed",
            StringProperty = "test",
            NotRemovedPropertyLast = "test not removed last",
        },
    },
    NestedObjectProperty = new NestedObject
    {
        IntProperty = 123,
        StringProperty = "test",
        NestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IntProperty = 123,
            StringProperty = "test"
        },
    },
    NestedObjectFullNameProperty = new MyNamespace.MySubspace.NestedObject
    {
        IntProperty = 123,
        RemovedFromAnyParent = "removed from any parent",
        NotRemovedStringProperty = "test",
        NestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IntProperty = 123,
            RemovedFromAnyParent = "removed from any parent",
            StringProperty = "test"
        },
        NotRemovedNestedAnonymousObjectPropertyOneMoreLevel = new
        {
            IntProperty = 123,
            NotRemovedProperty = "test not removed2",
            StringProperty = "test"
        },
    },
    TargetTypedNewObject = new(){ IntProperty = 123 },
    NotRemovedTargetTypedNewObject = new(){ NotRemovedProperty = 1, IntProperty = 123, RemovedFromAnyParent = "removed from any parent", NotRemovedProperty2 = 2 },
    NotRemovedNestedAnonymousObjectProperty = new
    {
        IntProperty = 123,
        NotRemovedProperty = "test not removed3",
        StringProperty = "test"
    },
    DateTimeProperty = DateTime.ParseExact("3000-05-20T10:05:15.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
    LatestPropertyWithoutComma = 1
}
