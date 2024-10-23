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
            int expected;
            expected = 2;
            expected = GetValue();
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
                    "expected[1]"
                },
                ExpectedVariableInvocationExpressionInfo = new Scand.StormPetrel.Generator.TargetProject.VariableInvocationExpressionInfo()
                {
                    Path = new[]
                    {
                        "Test.Integration.XUnit",
                        "UnitTest1",
                        "GetValue[*]"
                    },
                    NodeInfo = GetValueStormPetrel(),
                    ArgsCount = 0
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.MethodExpression
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        private static object GetValue() => new object ();
        private static (int NodeKind, int NodeIndex) GetValueStormPetrel() => (8917, 0);
        private static object StaticButNotExpectedMethod() => new object ();
        private static (int NodeKind, int NodeIndex) StaticButNotExpectedMethodStormPetrel() => (8917, 0);
        private object NotStaticMethod() => new object ();
    }
}