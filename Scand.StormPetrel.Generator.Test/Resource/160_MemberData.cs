using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resource
{
    internal class MemberDataTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void MemberDataProperty(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeNameSpace.SomeType))]
        public void MemberDataWithMemberType(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType=typeof(SomeType))]
        public void MemberDataWithMemberTypeNoWhitespaces(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType 	= 	 typeof(SomeType))]
        public void MemberDataWithMemberTypeWithSpecialWhitespaces(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), 1, "string param", new object[] { 1, 2, 3 })]
        public void MemberDataWithParameters(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(parameters: 3, memberName: nameof(DataMethodWithArgsForNamedParameters))]
        public void MemberDataWithNamedParameters(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> Data =>
            new object[][]
            {
                new object[] { 1, 2, 3 },
                new object[] { -2, 2, 0 },
                new object[] { int.MinValue, -1, int.MaxValue },
                new object[] { -4, -6, -10 },
            };
    }
}
