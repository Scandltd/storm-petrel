using NUnit.Framework;
using System.Runtime.CompilerServices;
using Test.Integration.CustomConfiguration.CustomSnapshotInfrastructure;

namespace Test.Integration.CustomConfiguration
{
    public sealed class ParallelismTest
    {
        internal static string ActualTextHandledInParallel = "some text handled in parallel";

        [Test]
        public static void WhenCreateWriteSnapshotFileInParallelWithAnotherTestThenNoExceptionsTest()
        {
            //Arrange
            var expected = CustomSnapshotProvider.Get().ReadAllText();

            //Act
            var actual = ActualTextHandledInParallel;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Returns current file path to use it in another test class to create/write the same snapshot file in parallel.
        /// </summary>
        /// <returns></returns>
        internal static string GetFilePath() => GetFilePathImplementation();
        private static string GetFilePathImplementation([CallerFilePath] string callerFilePath = "") => callerFilePath;
    }
}
