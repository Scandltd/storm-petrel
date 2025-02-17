using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class AssertionNoExpectedVarTestStormPetrel
    {
        [Fact]
        public void ShouldDetectExpectedArgumentTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentTest",
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
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentTest"
                },
                Expected = 1,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentTest"
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
            //Assert
            actual.Should().Be(1);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenMultipleArgsTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenMultipleArgsTest",
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
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentWhenMultipleArgsTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenMultipleArgsTest"
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
            //Assert
            actual.Should().Be(123, "some explanation");
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenMultipleNamedArgsTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenMultipleNamedArgsTest",
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
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentWhenMultipleNamedArgsTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenMultipleNamedArgsTest"
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
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(because: "some explanation", expected: 123);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenObjectCreationExpressionTest",
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
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionTest"
                },
                Expected = new FooExpected
                {
                    BlaProperty = "123"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenObjectCreationExpressionTest"
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
            //Assert
            actual.Should().BeEquivalentTo(new FooExpected { BlaProperty = "123" });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentAndActualWithPropertyTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentAndActualWithPropertyTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual.FooProperty,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentAndActualWithPropertyTest"
                },
                Expected = 1,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentAndActualWithPropertyTest"
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
            //Assert
            actual.FooProperty.Should().Be(1);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentAndActualWithMethodTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentAndActualWithMethodTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual.FooMethod(),
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentAndActualWithMethodTest"
                },
                Expected = 1,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentAndActualWithMethodTest"
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
            //Assert
            actual.FooMethod().Should().Be(1);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentAndActualCouplePropertiesOrMethodsTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentAndActualCouplePropertiesOrMethodsTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual.FooMethod().BlaProperty.OneMoreFooMethod(),
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "AssertionNoExpectedVarTest",
                    "ShouldDetectExpectedArgumentAndActualCouplePropertiesOrMethodsTest"
                },
                Expected = 1,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentAndActualCouplePropertiesOrMethodsTest"
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
            //Assert
            actual.FooMethod().BlaProperty.OneMoreFooMethod().Should().Be(1);
        }
    }
}