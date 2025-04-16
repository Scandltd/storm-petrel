new FooClass()
{
    IntProperty = 0x7b,
    IgnoredDoubleProperty = 12.3,
    NestedObjectProperty = new NestedObject
    {
        IntProperty = -0xc8,
        IgnoredDoubleProperty = 12.3,
    },
    NestedObjectProperty2 = new NestedObject
    {
        IntProperty = 0x123,
        IgnoredDoubleProperty = 12.3,
    },
    NestedObjectFullNameProperty = new MyNamespace.MySubspace.NestedObject
    {
        IntProperty = 0x3039,
        IgnoredDoubleProperty = 1234567891101112131415,
    },
    TargetTypedNewObject = new() { IntProperty = 0x7b, IgnoredVerbatim = "test\none more line" },
}
