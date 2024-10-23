using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resources
{
    public class AllPossibleTestAttributes
    {
        [NonTestAttribute]
        [Fact]
        public void TestXUnitFact()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [fAct]
        public void TestXUnitFactCaseSensitivity()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [Theory]
        public void TestXUnitTheory()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [Test]
        public void TestNUnitTest()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [TestCase]
        public void TestNUnitTestCase()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [TestCaseSource]
        public void TestNUnitTestCaseSource()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [Theory]
        public void TestNUnitTheory()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [TestMethod]
        public void TestMSTestTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [DataTestMethod]
        public void TestMSTestDataTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [CustomATestNotMatchingRegEx]
        public void TestCustomATestNotMatchingRegExMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [CustomATest]
        public void TestCustomATestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [CustomBTest]
        public void TestCustomBTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [cUstomBTest]
        public void TestCustomBCaseSensitivityTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
        [CustomATest]
        [CustomBTest]
        public void TestCustomAandBTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
    }
}
