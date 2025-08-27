using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Integration.Included
{
    internal static class IncludedDataSource
    {
        public static int ExpectedProperty => 123;

        public static IEnumerable<object[]> DataMethod()
        {
            return new object[][] {
                new object[] {1, 2, 0, "0x0" },
                new object[] {-2, 2, 0, "0x0" },
                new object[] {int.MinValue, -1, -100, "0x0" },
                new object[] {-4, -6, +50, "0x0" },
            };
        }
    }
}
