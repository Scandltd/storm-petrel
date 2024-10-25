using System.Globalization;

namespace Test.Integration.XUnit
{
    internal static class ExtensionExample
    {
        public static string ExtensionMethodExample(this int i) => i.ToString(CultureInfo.InvariantCulture);
    }
}
