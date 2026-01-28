using System.Globalization;

namespace Test.Integration.XUnit
{
    internal static class ExtensionExample
    {
        public static string ExtensionMethodExample(this int i) => i.ToString(CultureInfo.InvariantCulture);
#if true
        public static string ExtensionMethodExampleWithinPreprocessorDirective(this int i) => i.ToString(CultureInfo.InvariantCulture);
#endif
#if NET10_0_OR_GREATER
        extension<TSource>(IEnumerable<TSource> source)
        {
            public bool IsEmpty => !source.Any();
            public IEnumerable<TSource> WhereExtension(Func<TSource, bool> predicate) => source.Where(predicate);
        }
#endif
    }
}
