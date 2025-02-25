using NUnit.Framework;
using Test.Integration.CustomConfiguration.CustomSnapshotInfrastructure;
using Test.Integration.Shared;

namespace Test.Integration.CustomConfiguration
{
    public sealed class MultipleBaselinesTest
    {
        [Test]
        public static void TwoExpectedBaselinesTest()
        {
            //Arrange
            //SnapshotProvider Convention in the test body: explicitly specify prefferedKind
            var expectedJson = CustomSnapshotProvider.Get(prefferedKind: CustomSnapshotKind.Json).ReadAllText();
            var expectedPng = CustomSnapshotProvider.Get(prefferedKind: CustomSnapshotKind.Png).ReadAllBytes();

            //Act
            var actualJson = Calculator.Add(2, 2).ToJsonText();
            var actualPng = Calculator.GetLogo();

            //Assert
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            Assert.That(actualPng, Is.EqualTo(expectedPng));
        }

        /// <summary>
        /// Demonstrates a test with two baselines in the body and useCaseId in the parameters.
        /// </summary>
        /// <param name="useCaseId">SnapshotProvider Convention in the test parameters:
        /// declare test parameter with `useCaseId` name or marked by <see cref="Scand.StormPetrel.FileSnapshotInfrastructure.Attributes.UseCaseIdAttribute"/>.</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        [TestCase("2+2", 2, 2)]
        [TestCase("5-1", 5, -1)]
        public void TwoExpectedBaselinesParamsTest(string useCaseId, int a, int b)
        {
            //Arrange
            //SnapshotProvider Convention in the test body: explicitly specify prefferedKind and useCaseId
            var expectedJson = CustomSnapshotProvider.Get(prefferedKind: CustomSnapshotKind.Json).ReadAllText(useCaseId);
            var expectedPng = CustomSnapshotProvider.Get(prefferedKind: CustomSnapshotKind.Png).ReadAllBytes(useCaseId);

            //Act
            var actualJson = Calculator.Add(a, b).ToJsonText();
            var actualPng = Calculator.GetLogo();

            //Assert
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            Assert.That(actualPng, Is.EqualTo(expectedPng));
        }
    }
}
