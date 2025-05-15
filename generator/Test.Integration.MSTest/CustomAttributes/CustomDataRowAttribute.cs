namespace MSTest;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class CustomDataRowAttribute : DataRowAttribute
{
    public CustomDataRowAttribute()
    {
    }

    public CustomDataRowAttribute(object? customData) : base(customData)
    {
        CustomData = customData;
    }

    public CustomDataRowAttribute(string?[]? customStringArrayData) : base(customStringArrayData)
    {
        CustomStringArrayData = customStringArrayData;
    }

    public CustomDataRowAttribute(params object?[]? data) : base(data)
    {
    }
    public object? CustomData { get; }
    public string?[]? CustomStringArrayData { get; }
}
