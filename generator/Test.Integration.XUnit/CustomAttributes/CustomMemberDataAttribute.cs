using System.Globalization;
using System.Reflection;

namespace Xunit;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class CustomMemberDataAttribute : MemberDataAttributeBase
{
    public CustomMemberDataAttribute(string memberName, params object[] parameters)
        : base(memberName, parameters) { }

    protected override object[]? ConvertDataItem(MethodInfo testMethod, object item)
    {
        if (item == null)
        {
            return null;
        }

        var array = item as object[];
        if (array == null)
        {
            ArgumentNullException.ThrowIfNull(testMethod);
            throw new ArgumentException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "Property {0} on {1} yielded an item that is not an object[]",
                    MemberName,
                    MemberType ?? testMethod.DeclaringType 
                )
            );
        }
        return array;
    }
}
