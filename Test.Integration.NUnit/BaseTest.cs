using FluentAssertions;
using Test.Integration.Shared;

namespace Test.Integration.NUnit
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public partial class Tests
    {
        [Test]
        public void TestInt()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
            var fullName = GetType().FullName;
            ArgumentNullException.ThrowIfNull(fullName);
            BackupHelper.DeleteBackup(fullName, backupResult => BackupHelper.IsProperlyDeleted(backupResult).Should().BeTrue(), "BaseTest.cs");
        }
    }

    public static class TestedClass
    {
        public static int TestedMethod()
        {
            return 100;
        }
    }
}
