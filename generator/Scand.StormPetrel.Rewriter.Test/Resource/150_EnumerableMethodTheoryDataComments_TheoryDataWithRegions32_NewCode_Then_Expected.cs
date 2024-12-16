using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class EnumerableMethodClass
    {
        public static TheoryData<int, int, int> TheoryDataWithComments =>
        new TheoryData<int, int, int>
        {
            //Comment 1
            {
                /*Comment ignored*/-2,
                2,
                //Multiline comment

                //with empty extra line
                100
            },
            //Comment 2
            //Multiline
            {
                int.MinValue,
                    //Comment with +4 whitespaces
                  //Comment with +2 whitespaces
                      //Comment with +6 whitespaces
                        -1,
                int.MaxValue
            },
            { 1, 2, 100 },
            //Comment 3
            { -4, -6, -100 },
        };

        public static TheoryData<int, int, int> TheoryDataWithRegions =>
        new TheoryData<int, int, int>
        {
            #region Test Cases Group 1
            {
                -2,
                2,
                100
            },
            {
                int.MinValue,
                -1,
                int.MaxValue
            },
            #endregion
            #region Test Cases Group 2
            { 1, 2, 100 },
            //Comment 3
            { -4, -6, new List<string>()
            {
                "1",
                "2"
            } },
{
//No indent at all
-4,
-6,
-100
},
            #endregion
        };
    }
}
