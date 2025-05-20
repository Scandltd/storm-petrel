using Xunit.v3;

namespace Xunit;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class CustomMemberDataAttribute(
    string memberName,
    params object?[] arguments) :
        MemberDataAttributeBase(memberName, arguments)
{ }
