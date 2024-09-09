using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resources
{
    public partial class PartialTestClass
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            int expected = 1;

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }
    }

    public partial class PartialTestClass
    {
        [Fact]
        public void Test2()
        {
            //Arrange
            int expected = 1;

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }
    }
}
