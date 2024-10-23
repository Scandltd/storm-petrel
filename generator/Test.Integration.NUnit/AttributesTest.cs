using FluentAssertions;
using System.Globalization;
using Test.Integration.Shared;

namespace Test.Integration.NUnit
{
    /// <summary>
    /// Lightweight copy of XUnit.AttributesTest adapted for NUnit.
    /// </summary>
    public sealed class AttributesTest
    {
        [TestCase(1, 2, "1_incorrect", -2)]
        [TestCase(2)]
        [TestCase(3, 3)]
        public void TestMethodWithMultipleExpected(int i, int multiplier = 1, string expected = "", int expectedInt = -1)
        {
            //Act
            var actualInt = i * multiplier;
            var actual = actualInt.ToString(CultureInfo.InvariantCulture);

            //Assert
            actual.Should().BeEquivalentTo(expected);
            actualInt.Should().Be(expectedInt);
            BackupHelper.DeleteBackupWithResultAssertion(GetType());
        }
    }
}
