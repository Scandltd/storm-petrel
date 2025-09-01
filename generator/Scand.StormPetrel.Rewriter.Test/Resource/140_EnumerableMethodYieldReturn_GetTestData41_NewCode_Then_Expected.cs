using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class EnumerableMethodYieldReturnClass
    {
        public static IEnumerable<object[]> GetTestData()
        {
            yield return new object[]
            {
                1, //Test comment
                2,
                3,
            };
            yield return new object[]
            {
                4, 5, 6,
            };
            yield return new object[] { 7, 8, 9 };
            yield return [];
            yield return [1, new List<string>()
            {
                "1",
                "2"
            }];
        }

        public static IEnumerable<object[]> GetTestDataPropertyGetExplicit
        {
            get
            {
                yield return new object[]
                {
                    1,
                    2,
                    3,
                };
            }
        }

        private static IEnumerable<(int, int, AddResult)> GetTestDataViaEnumerableTuple()
        {
            yield return (1, 2, new AddResult());
            yield return (-2, 2, /*a comment*/ new AddResult());
            yield return (int.MinValue, -1, new AddResult());
            yield return (-4, -6, new AddResult());
        }
    }
}
