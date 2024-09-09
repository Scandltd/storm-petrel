using FluentAssertions;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class AttributesTestStormPetrel
    {
        [Theory]
        [InlineData(0, 1, "one")]
        [InlineData(1, 2, "two")]
        [InlineData(2, 3, "three")]
        public void TestMethodStormPetrel(int stormPetrelUseCaseIndex, int intArg, string expected)
        {
            //Act
            var actual = "one_actual";
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethod",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethod",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethod"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Attribute,
                TestCaseAttributeInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseAttributeInfo()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 1
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}