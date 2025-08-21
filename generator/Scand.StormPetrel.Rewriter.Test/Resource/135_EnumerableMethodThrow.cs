using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class EnumerableMethodClass
    {
        public static IEnumerable<object[]> GetTestData()
        {
            throw new NotImplementedException("Foo");
        }
        public static IEnumerable<object[]> GetTestDataArrow() => throw new NotImplementedException("Foo");
        public static IEnumerable<object[]> GetTestDataProperty { get; } = throw new NotImplementedException("Foo");
        public static IEnumerable<object[]> GetTestDataPropertyArrow => throw new NotImplementedException("Foo");
        public static IEnumerable<object[]> GetTestDataPropertyGetExplicit
        {
            get
            {
                throw new NotImplementedException("Foo");
            }
        }
        public static TheoryData<int, int, int> TheoryData => throw new NotImplementedException("Foo");
    }
}
