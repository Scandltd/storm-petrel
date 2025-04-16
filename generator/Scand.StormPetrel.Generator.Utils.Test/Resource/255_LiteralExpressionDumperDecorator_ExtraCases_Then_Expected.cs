new FooClass()
{
    CharProperty = '\n',
    CharArrayProperty = new[] { '\"', '\r', '\n' },
    ArrayProperty = [@"test", @"a""", @"test"" with double quote and two double quotes:"""""],
    ArrayProperty2 = new[] { @"test", @"a""", @"test"" with double quote and two double quotes:""""" },
    ArrayProperty3 = new string[]
    {
        @"test",
        @"a""",
        @"test"" with double quote and two double quotes:""""",
        "",
        string.Empty,
        @"",
    },
    ArrayProperty4 = new Dictionary<string, string>
    {
        {
            @"test",
            @"a"""
        },
        { @"test"" with double quote and two double quotes:""""", "v2" }
    }
}
