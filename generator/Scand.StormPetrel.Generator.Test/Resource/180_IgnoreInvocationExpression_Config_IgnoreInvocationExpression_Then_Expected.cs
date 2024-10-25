using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class IgnoreInvocationExpressionTestStormPetrel
    {
        [Fact]
        public void ShouldIgnoreInvocationExpressionTestStormPetrel()
        {
            //Arrange
            int expected = InvocationExpressionToBeIgnored();
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "IgnoreInvocationExpressionTest",
                MethodName = "ShouldIgnoreInvocationExpressionTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "IgnoreInvocationExpressionTest",
                    "ShouldIgnoreInvocationExpressionTest",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "IgnoreInvocationExpressionTest",
                    "ShouldIgnoreInvocationExpressionTest",
                    "expected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "IgnoreInvocationExpressionTest",
                        "InvocationExpressionToBeIgnored[*]"
                    },
                    MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                    {
                        ArgsCount = 0
                    }
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void ShouldNotIgnoreInvocationExpressionDueToCaseSensitivityTestStormPetrel()
        {
            //Arrange
            int expected = InvocationExpressiontobeignored(); //should not be ignored
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "IgnoreInvocationExpressionTest",
                MethodName = "ShouldNotIgnoreInvocationExpressionDueToCaseSensitivityTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelMethodNode = InvocationExpressiontobeignoredStormPetrel();
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "IgnoreInvocationExpressionTest",
                    "ShouldNotIgnoreInvocationExpressionDueToCaseSensitivityTest",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "IgnoreInvocationExpressionTest",
                    "ShouldNotIgnoreInvocationExpressionDueToCaseSensitivityTest",
                    "expected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "IgnoreInvocationExpressionTest",
                        "InvocationExpressiontobeignored[*]"
                    },
                    MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                    {
                        NodeKind = stormPetrelMethodNode.NodeKind,
                        NodeIndex = stormPetrelMethodNode.NodeIndex,
                        ArgsCount = 0
                    }
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }
    }
}