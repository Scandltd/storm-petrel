using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class AssertionNoExpectedVarTestStormPetrel
    {
        [Fact]
        public void AssertEqualTestStormPetrel()
        {
            //Act
            var specificActualVarName = TestedClass.TestedMethod();
            var specificActualVarname = 1;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "AssertEqualTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 3,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = specificActualVarName,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "AssertionNoExpectedVarTest",
                    "AssertEqualTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:2",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "AssertEqualTest"
                    },
                    MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                    {
                        NodeKind = 8638,
                        NodeIndex = 0,
                        ArgsCount = 0
                    }
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = specificActualVarName,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "AssertionNoExpectedVarTest",
                    "AssertEqualTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:4",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "AssertEqualTest"
                    },
                    MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                    {
                        NodeKind = 8638,
                        NodeIndex = 1,
                        ArgsCount = 0
                    }
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelContext2 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = specificActualVarName,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "AssertionNoExpectedVarTest",
                    "AssertEqualTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:5",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "AssertEqualTest"
                    },
                    MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                    {
                        NodeKind = 8638,
                        NodeIndex = 0,
                        ArgsCount = 0
                    }
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext2);
            //Assert
            Assert.Equal(123, specificActualVarName);
            Assert.Equal(123, specificActualVarname); //Ignored by Storm Petrel because `n` in `name` does not match configuration regex
            Assert.Equal(actual: specificActualVarName, expected: 123);
            Assert.Equal(expected: 123, actual: specificActualVarName);
        }

        [Fact]
        public void IgnoredByStormPetrelTest()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            //Assert
            actual.Should().Be(123, "some explanation");
        }
    }
}