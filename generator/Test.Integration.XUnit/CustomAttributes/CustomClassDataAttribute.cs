namespace Xunit;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class CustomClassDataAttribute : ClassDataAttribute
{
    public CustomClassDataAttribute(Type @class) : base(@class) { }
}
