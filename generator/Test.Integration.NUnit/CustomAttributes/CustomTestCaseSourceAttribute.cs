namespace NUnit;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class CustomTestCaseSourceAttribute : TestCaseSourceAttribute
{
    public CustomTestCaseSourceAttribute(string sourceName) : base(sourceName)
    {
    }

    public CustomTestCaseSourceAttribute(Type sourceType) : base(sourceType)
    {
    }

    public CustomTestCaseSourceAttribute(Type sourceType, string sourceName) : base(sourceType, sourceName)
    {
    }

    public CustomTestCaseSourceAttribute(string sourceName, object?[]? methodParams) : base(sourceName, methodParams)
    {
    }

    public CustomTestCaseSourceAttribute(Type sourceType, string sourceName, object?[]? methodParams) : base(sourceType, sourceName, methodParams)
    {
    }
}
