using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils
{
    public static class CommonUtils
    {
        /// <summary>
        /// For each <see cref="TAttribute"/> attribute of <paramref name="methodName"/> of <paramref name="type"/> type:
        /// - Creates default data array via <paramref name="defaultAttributeDataFactory"/>.
        /// - Populates the data array with the arguments from the attribute, using <paramref name="attributeDataSelector"/> to extract the arguments from the attribute.
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="attributeDataSelector"></param>
        /// <param name="defaultAttributeDataFactory"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static IEnumerable<(int Index, object?[] Data)> EnumerateAttributeData<TAttribute>(
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type type,
            string methodName,
            Func<TAttribute, object?[]> attributeDataSelector,
            Func<object?[]> defaultAttributeDataFactory)
        {
            var methodInfo = type.GetMethod(methodName) ?? throw new ArgumentException($"Method '{methodName}' not found in type '{type.FullName}'.", nameof(methodName));
            var attributesData = methodInfo
                .GetCustomAttributes(typeof(TAttribute), inherit: true)
                .Cast<TAttribute>()
                .Select(attributeDataSelector)
                .ToArray();
            for (int i = 0; i < attributesData.Length; i++)
            {
                var data = defaultAttributeDataFactory();
                var arguments = attributesData[i];
                if (data.Length < arguments.Length)
                {
                    throw new InvalidOperationException($"The row data factory must create an array with a length of at least {arguments.Length} to accommodate the attribute data.");
                }
                for (int j = 0; j < arguments.Length; j++)
                {
                    data[j] = arguments[j];
                }
                yield return (i, data);
            }
        }
    }
}
