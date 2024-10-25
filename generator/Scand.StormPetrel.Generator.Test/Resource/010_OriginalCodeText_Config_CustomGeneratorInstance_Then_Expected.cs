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
            int expected = 1;
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "Test1",
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new CustomGeneratorInstance()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }
    }

    public static class TestedClassStormPetrel
    {
        public static int TestedMethod1()
        {
            return 100;
        }

        public static (int NodeKind, int NodeIndex) TestedMethod1StormPetrel()
        {
            return (8805, 0);
        }

        public static int TestedMethod2()
        {
            return 200;
        }

        public static (int NodeKind, int NodeIndex) TestedMethod2StormPetrel()
        {
            return (8805, 0);
        }
    }
}