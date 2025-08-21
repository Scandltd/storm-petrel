using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Scand.StormPetrel.Rewriter
{
    public static class DataSourceHelper
    {
        public static IEnumerable<object> Enumerate(Type dataSourceType, string dataSourceMemberName, params object[] memberParameters)
        {
            if (dataSourceType == null)
            {
                throw new ArgumentNullException(nameof(dataSourceType));
            }
            var member = GetMember(dataSourceType, x => x.Name == dataSourceMemberName);
            if (member != null)
            {
                object result = null;
                if (member.MemberType == MemberTypes.Method)
                {
                    result = ((MethodInfo)member).Invoke(null, memberParameters);
                }
                else if (member.MemberType == MemberTypes.Property)
                {
                    result = ((PropertyInfo)member).GetValue(null);
                }
                else if (member.MemberType == MemberTypes.Field)
                {
                    result = ((FieldInfo)member).GetValue(null);
                }
                return result as IEnumerable<object>;
            }
            return null;
        }

        public static IEnumerable<object[]> ConvertToStormPetrelRows(IEnumerable<object> rows)
        {
            var stormPetrelRows = Enumerable.Select(rows, row =>
            {
                if (row is object[] tempRow)
                {
                    return tempRow;
                }
                //Now parse xUnit v3 format
                var propertyInfo = row.GetType().GetProperty("Data");
                var rowValue = (propertyInfo?.GetValue(row))
                                ?? throw new InvalidOperationException("Cannot get a value from test case source row.");
                if (rowValue is object[] rowValueAsArray)
                {
                    return rowValueAsArray;
                }
                //xUnit v3 tuple fields
                var fields = rowValue.GetType().GetFields().OrderBy(x => x.Name).ToArray();
                var convertedRow = new object[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    convertedRow[i] = fields[i].GetValue(rowValue);
                }
                return convertedRow;
            });
            return stormPetrelRows;
        }

        public static string[] GetPath(Type type, string memberName) =>
            GetPathImplementation(type, x => x.Name == memberName);

        public static string[] GetEnumerableStaticMemberPath(Type type) =>
            GetPathImplementation(type, x => IsEnumerableMember(x, MemberTypes.Property)) //check properties first to filter out "get_PropertyName" methods
                ?? GetPathImplementation(type, x => IsEnumerableMember(x, MemberTypes.Field))
                ?? GetPathImplementation(type, x => IsEnumerableMember(x, MemberTypes.Method));

        private static bool IsEnumerableMember(MemberInfo member, MemberTypes memberType) =>
            member.MemberType == memberType && typeof(System.Collections.IEnumerable).IsAssignableFrom(GetReturnType(member));

        private static Type GetReturnType(MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnParameter.ParameterType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                default:
                    return null;
            }
        }

        private static string[] GetPathImplementation(Type type, Func<MemberInfo, bool> memberPredicate)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            var member = GetMember(type, memberPredicate);
            if (member == null)
            {
                return null;
            }
            return new[]
            {
                type.Namespace,
                type.Name,
                member.Name + "[*]",
            };
        }

        private static MemberInfo GetMember(Type type, Func<MemberInfo, bool> memberPredicate)
        {
            var bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var member = type
                            .GetMembers(bindingFlags)
                            .Where(x => x.MemberType == MemberTypes.Method || x.MemberType == MemberTypes.Property || x.MemberType == MemberTypes.Field)
                            .Where(x => memberPredicate(x))
                            .SingleOrDefault();
            return member;
        }
    }
}
