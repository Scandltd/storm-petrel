using FluentAssertions;
using Test.Integration.Shared;

namespace Test.Integration.NUnit
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class BackupMultithreadTestOdd
    {
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        public void OverwriteExpectedFileTest(int expectedMethodArg)
        {
            //Arrange
            var expected = BackupMultithreadTestHelper.GetExpected(expectedMethodArg);

            //Act
            var actual = expectedMethodArg * 100;

            //Assert
            actual.Should().Be(expected);
            if (BackupHelper.IsStormPetrel(GetType().FullName!))
            {
                BackupHelper.DeleteBackup(typeof(BackupMultithreadTestHelper).FullName! + "StormPetrel", backupResult => BackupHelper.IsProperlyDeleted(backupResult).Should().BeTrue());
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        public void SimpleTest(int expectedMethodArg)
        {
            //Arrange
            var expected = GetExpectedValue(expectedMethodArg);

            //Act
            var actual = expectedMethodArg * 100;

            //Assert
            actual.Should().Be(expected);
            BackupHelper.DeleteBackup(GetType().FullName!, backupResult => BackupHelper.IsProperlyDeleted(backupResult).Should().BeTrue());
        }

        private static int GetExpectedValue(int arg)
            =>
        arg switch
        {
            1 => 1,
            2 => 1,
            3 => 1,
            4 => 1,
            5 => 1,
            6 => 1,
            7 => 1,
            8 => 1,
            9 => 1,
            10 => 1,
            _ => throw new InvalidOperationException(),
        };
    }
}
