using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resource
{
    internal class ClassDataTests
    {
        [Theory]
        [ClassData(typeof(SomeClass))]
        public void Test(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SomeClass))]
        public void MultipleVarsTest(int x, int y, int expected, string expectedString)
        {
            var actual = Calculator.Add(x, y);
            var actualString = actual.ToString();
            Assert.Equal(expected, actual);
            Assert.Equal(expectedString, actualString);
        }

        [Theory]
        [ClassData(typeof
                    (SomeClass))]
        public void TypeofWhitespacesTest(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            Assert.Equal(expected, actual);
        }
    }
}
