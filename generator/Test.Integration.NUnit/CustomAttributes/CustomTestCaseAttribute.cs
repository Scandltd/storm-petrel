namespace NUnit;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public sealed class CustomTestCaseAttribute(object? arg) : TestCaseAttribute(arg)
{
    public object? Arg { get; } = arg;
}
