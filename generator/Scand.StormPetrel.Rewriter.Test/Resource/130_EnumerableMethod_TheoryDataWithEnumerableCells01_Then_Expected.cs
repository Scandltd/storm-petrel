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
            //Test comment
            return new object[]
            {
                //Test comment
                new object[]
                {
                    1, //Test comment
                    2,
                    3,
                },
                new object[]
                {
                    4, 5, 6,
                },
                new object[] { 7, 8, 9 },

                new object[] {  }, //Missed cells use cases
                new object[] { 1 },
                new object[] { 1, }
                //Test comment
            }
        }

        public static IEnumerable<object[]> GetTestDataWithVariedInitializers() =>
        [
            [
                //Test comment
                1,
                //Test
                //multiline comment
                2,
                3,
            ],
            new []
            {
                4, /*test comment*/ 5, 6, //test comment again
            },
            new [3] { 7, 8, 9 }
        ];

        public static IEnumerable<object[]> GetTestDataProperty { get; } =
            new object[]
            {
                new object[]
                {
                    1,
                    2,
                    3,
                },
            };

        public static IEnumerable<object[]> GetTestDataPropertyArrow =>
            new object[]
            {
                new object[]
                {
                    1,
                    2,
                    3,
                },
            };

        public static IEnumerable<object[]> GetTestDataPropertyGetExplicit
        {
            get
            {
                return new object[]
                {
                    new object[]
                    {
                        1,
                        2,
                        3,
                    },
                };
            }
        }

        private static IEnumerable<(int, int, AddResult)> GetTestDataViaEnumerableTuple() =>
        [
            (1, 2, new AddResult()),
            (-2, 2, /*a comment*/ new AddResult()),
            (int.MinValue, -1, new AddResult()),
            (-4, -6, new AddResult()),
        ];

        public static TheoryData<int, int, int> TheoryData =>
        new TheoryData<int, int, int>
        {
            { -2, 2, 100 },
            { int.MinValue, -1, int.MaxValue },
            { 1, 2, 100 },
            { -4, -6, -100 },
        };

        public static TheoryData<int, int, int> TheoryDataImplicitObjectCreation =>
        new()
        {
            { -2, 2, 100 },
            { int.MinValue, -1, int.MaxValue },
            { 1, 2, 100 },
            { -4, -6, -100 },
        };

        public static TheoryData<IEnumerable<int>, string[]> TheoryDataWithEnumerableCells =>
        new()
        {
            { new int[]{ }, new List<string>()
            {
                "1",
                "2"
            } },
            { Enumerable.Empty<int>(), new[2] },
        };

        public static IEnumerable<object[]> GetTestData3D()
        {
            return new object[]
            {
                new object[]
                {
                    new object[] { 1, 2, 3 },
                    new [] { "a", "b", "c" },
                    new int[3] { 1, 2, 3 },
                },
            }
        }

        public static IEnumerable<DataSourceRow> ClassDataMethod() =>
        [
            (1, 2, new AddResult()),
            new(),
            new(-4),
            new DataSourceRow(),
            new DataSourceRow(-4),
        ];
    }
}
