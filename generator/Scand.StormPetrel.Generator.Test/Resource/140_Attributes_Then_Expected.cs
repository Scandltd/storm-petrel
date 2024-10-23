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

        [Theory]
        [InlineData(0, 1, "one", 2)]
        [InlineData(1, 2, "two")]
        [InlineData(2, 3, "three")]
        public void TestMethodMultipleExpectedStormPetrel(int stormPetrelUseCaseIndex, int intArg, string expected, int expected2 = -1)
        {
            //Act
            var actual = "one_actual";
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethodMultipleExpected",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected"
                },
                IsLastVariablePair = false,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Attribute,
                TestCaseAttributeInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseAttributeInfo()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 1
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            var actual2 = "two_actual";
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethodMultipleExpected",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual2,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected",
                    "actual2"
                },
                Expected = expected2,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Attribute,
                TestCaseAttributeInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseAttributeInfo()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 2
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
            actual2.Should().BeEquivalentTo(expected2);
        }
    }
}