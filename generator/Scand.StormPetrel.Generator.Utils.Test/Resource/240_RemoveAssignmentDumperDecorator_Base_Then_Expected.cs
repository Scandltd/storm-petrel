new FooClass()
{
    NotRemovedProperty = 12345,
    NotRemovedNestedObjectProperty = new NestedObject
    {
        NotRemovedProperty = "test not removed",
        NestedAnonymousObjectPropertyOneMoreLevel = new
        {
            NotRemovedProperty = "test not removed",
            NotRemovedProperty2 = "test not removed2"
        },
        NotRemovedNestedAnonymousObjectPropertyOneMoreLevel = new
        {
            NotRemovedProperty = "test not removed",
            NotRemovedPropertyLast = "test not removed last",
        },
    },
    NestedObjectFullNameProperty = new MyNamespace.MySubspace.NestedObject
    {
        NotRemovedStringProperty = "test",
        NotRemovedNestedAnonymousObjectPropertyOneMoreLevel = new
        {
            NotRemovedProperty = "test not removed2"
        },
    },
    NotRemovedTargetTypedNewObject = new(){ NotRemovedProperty = 1, NotRemovedProperty2 = 2 },
    NotRemovedNestedAnonymousObjectProperty = new
    {
        NotRemovedProperty = "test not removed3"
    }
}
