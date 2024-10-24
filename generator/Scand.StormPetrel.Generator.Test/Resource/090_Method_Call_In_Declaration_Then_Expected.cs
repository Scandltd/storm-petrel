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
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "Test1",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelMethodNode = SomeClassStormPetrel.ExpectedStormPetrel();
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "SomeClass",
                        "Expected[*]"
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

        [Fact]
        public void WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPathStormPetrel()
        {
            //Arrange
            object expected = Expected();
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "WhenMethodNameOnlyThenFullPathShouldBeGeneratedInExpectedVariableInvocationExpressionPath",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelMethodNode = ExpectedStormPetrel();
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "UnitTest1",
                        "Expected[*]"
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

        [Fact]
        public void WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContextStormPetrel()
        {
            //Arrange
            object expected = Expected(arg1, 1, "123", SomeType.SomeProperty, SomeType.SomeMethod());
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "WhenMethodWithArgsThenTheArgsShouldBeTransferredToGenerationContext",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelMethodNode = ExpectedStormPetrel(arg1, 1, "123", SomeType.SomeProperty, SomeType.SomeMethod());
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "UnitTest1",
                        "Expected[*]"
                    },
                    MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                    {
                        NodeKind = stormPetrelMethodNode.NodeKind,
                        NodeIndex = stormPetrelMethodNode.NodeIndex,
                        ArgsCount = 5
                    }
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
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