new FooClass()
{
    IntProperty = 123,
    IgnoredDoubleProperty = 12.3,
    NestedObjectProperty = new NestedObject
    {
        IntProperty = -200,
        IgnoredDoubleProperty = 12.3,
    },
    NestedObjectProperty2 = new NestedObject
    {
        IntProperty = 0x123,
        IgnoredDoubleProperty = 12.3,
    },
    NestedObjectFullNameProperty = new MyNamespace.MySubspace.NestedObject
    {
        IntProperty = 12345,
        IgnoredDoubleProperty = 1234567891101112131415,
    },
    TargetTypedNewObject = new() { IntProperty = 123, IgnoredVerbatim = "test\none more line" },
}
