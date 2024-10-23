using FluentAssertions;
using System.Globalization;

namespace Test.Integration.MSTest
{
    /// <summary>
    /// Lightweight copy of XUnit.AttributesTest adapted for MSTest.
    /// </summary>
    [TestClass]
    public class AttributesTest
    {
        [TestMethod]
        [DataRow(1, 2, "1_incorrect", -2)]
        [DataRow(2)]
        [DataRow(3, 3)]
        public void TestMethodWithMultipleExpected(int i, int multiplier = 1, string expected = "", int expectedInt = -1)
        {
            //Act
            var actualInt = i * multiplier;
            var actual = actualInt.ToString(CultureInfo.InvariantCulture);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            actualInt.Should().Be(expectedInt);
        }
    }
}
