using VarDump.Visitor.Descriptors;

namespace Test.Integration.XUnit
{
    internal partial class Utils
    {
        private sealed class IgnoredMembersMiddleware : IObjectDescriptorMiddleware
        {
            private readonly Dictionary<string, HashSet<string>> _typeToExcludeMemberNames = new()
            {
                {
                    "Test.Integration.XUnit.TestClassResultWithIgnorable",
                    new HashSet<string>
                    {
                        "StringPropertyIgnored",
                    }
                },
            };
            public IObjectDescription GetObjectDescription(object @object, Type objectType, Func<IObjectDescription> prev)
            {
                var objectDescription = prev();
                var typeFullName = objectDescription.Type.BaseType;

                return new ObjectDescription
                {
                    Type = objectDescription.Type,
                    ConstructorArguments = objectDescription.ConstructorArguments,
                    Properties = objectDescription.Properties.Where(x => IsNotIgnored(x, typeFullName)),
                    Fields = objectDescription.Fields.Where(x => IsNotIgnored(x, typeFullName))
                };
            }

            private bool IsNotIgnored(ReflectionDescription description, string typeFullName)
            {
                return !_typeToExcludeMemberNames.TryGetValue(typeFullName, out var memberNames)
                                                || !memberNames.Contains(description.Name);
            }
        }
    }
}
