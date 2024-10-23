using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTest1StormPetrel
    {
        [Fact]
        public void Test1StormPetrel()
        {
            //Arrange
            int expected = SomeClass.Expected();
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "Test1",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "Test1",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "Test1",
                    "expected"
                },
                ExpectedVariableInvocationExpressionInfo = new Scand.StormPetrel.Generator.TargetProject.VariableInvocationExpressionInfo()
                {
                    Path = new[]
                    {
                        "SomeClass",
                        "Expected[*]"
                    },
                    NodeInfo = SomeClassStormPetrel.ExpectedStormPetrel(),
                    ArgsCount = 0
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.MethodExpression
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPathStormPetrel()
        {
            //Arrange
            object expected = Expected();
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPath",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPath",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPath",
                    "expected"
                },
                ExpectedVariableInvocationExpressionInfo = new Scand.StormPetrel.Generator.TargetProject.VariableInvocationExpressionInfo()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "UnitTest1",
                        "Expected[*]"
                    },
                    NodeInfo = ExpectedStormPetrel(),
                    ArgsCount = 0
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.MethodExpression
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContextStormPetrel()
        {
            //Arrange
            object expected = Expected(arg1, 1, "123", SomeType.SomeProperty, SomeType.SomeMethod());
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContext",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContext",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContext",
                    "expected"
                },
                ExpectedVariableInvocationExpressionInfo = new Scand.StormPetrel.Generator.TargetProject.VariableInvocationExpressionInfo()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "UnitTest1",
                        "Expected[*]"
                    },
                    NodeInfo = ExpectedStormPetrel(arg1, 1, "123", SomeType.SomeProperty, SomeType.SomeMethod()),
                    ArgsCount = 5
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.MethodExpression
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        private static object Expected() => new object ();
        private static (int NodeKind, int NodeIndex) ExpectedStormPetrel() => (8917, 0);
        private static object StaticButNotExpectedMethod() => new object ();
        private static (int NodeKind, int NodeIndex) StaticButNotExpectedMethodStormPetrel() => (8917, 0);
        private object NotStaticMethod() => new object ();
    }
}