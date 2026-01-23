using Shouldly;
using System.Globalization;

namespace Test.Integration.XUnit;
public class NoExpectedVarShouldlyTestStormPetrel
{
    [Fact]
    public void ShouldBeTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeTest",
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
                "NoExpectedVarShouldlyTest",
                "ShouldBeTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeTest"
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
        actual.ShouldBe(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentWhenMultipleArgsTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldDetectExpectedArgumentWhenMultipleArgsTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 2,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldDetectExpectedArgumentWhenMultipleArgsTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldDetectExpectedArgumentWhenMultipleArgsTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarShouldlyTest",
                    "ShouldDetectExpectedArgumentWhenMultipleArgsTest"
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
        //Assert
        actual.ShouldBe(123, "some explanation");
        actual.ShouldBe(customMessage: "some explanation", expected: 123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualWithPropertyTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldDetectExpectedArgumentAndActualWithPropertyTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.IntProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldDetectExpectedArgumentAndActualWithPropertyTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
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
        actual.IntProperty.ShouldBe(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualWithMethodTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldDetectExpectedArgumentAndActualWithMethodTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.IntPropertyAsMethod(),
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldDetectExpectedArgumentAndActualWithMethodTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
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
        actual.IntPropertyAsMethod().ShouldBe(123);
    }

    [Fact]
    public void ShouldBeWhenMultipleActualsTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var actual2 = 100;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeWhenMultipleActualsTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 2,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenMultipleActualsTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenMultipleActualsTest"
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
            Actual = actual2,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenMultipleActualsTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenMultipleActualsTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        //Assert
        actual.ShouldBe(123);
        actual2.ShouldBe(123);
    }

    [Fact]
    public void ShouldBeWhithInvocationExpressionTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        //Assert
        //The assertion does not fail by default. Also, StormPetrel does not replace the invocation expression below.
        //But we keep this use case to indicate no unexpected failures in StormPetrel here.
        actual.ShouldBe(TestedClass.TestedMethod());
        actual.ShouldBe(TestedClass.ReturnInput(TestedClass.TestedMethod()));
    }

    [Fact]
    public void ShouldBeWhenNullConditionalOperatorTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeWhenNullConditionalOperatorTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 5,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual?.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorTest"
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
            Actual = actual?.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext2 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual?.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorTest"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext3 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual?.StringNullablePropertyAsMethod(),
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext3);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext4 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:6",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext4);
        //Assert
        actual?.StringNullableProperty.ShouldBe("Incorrect value");
        actual?.StringNullableProperty.ShouldBe(null);
        actual?.StringNullableProperty?.ShouldBe("Incorrect value");
        actual?.StringNullablePropertyAsMethod()?.ShouldBe("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty?.ShouldBe("Incorrect value");
    }

    [Fact]
    public void ShouldBeWhenNullConditionalOperatorAssertsNullTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        actual.StringNullableProperty = null;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeWhenNullConditionalOperatorAssertsNullTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 5,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual?.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
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
            Actual = actual?.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext2 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual?.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext3 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual?.StringNullablePropertyAsMethod(),
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:5",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext3);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext4 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:7",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullConditionalOperatorAssertsNullTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext4);
        //Assert
        /*Test comment*/
        actual?.StringNullableProperty.ShouldBe("Incorrect not null");
        //Test comment
        actual?.StringNullableProperty.ShouldBe(null);
        //Test multiline
        //comment
        actual?.StringNullableProperty?.ShouldBe("Incorrect not null");
        /*Test comment*/
        actual?.StringNullablePropertyAsMethod()?.ShouldBe("Incorrect not null");
        ArgumentNullException.ThrowIfNull(actual);
        /*Test multiline
         *comment*/
        actual.StringNullableProperty?.ShouldBe("Incorrect not null");
    }

    [Fact]
    public void ShouldBeWhenNullForgivenOperatorTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeWhenNullForgivenOperatorTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 5,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual!.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullForgivenOperatorTest"
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
            Actual = actual!.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullForgivenOperatorTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullForgivenOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext2 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual!.StringNullableProperty!,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullForgivenOperatorTest"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext3 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual!.StringNullablePropertyAsMethod()!,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullForgivenOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext3);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext4 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.StringNullableProperty!,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:6",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullForgivenOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext4);
        //Assert
        actual!.StringNullableProperty.ShouldBe("Incorrect value");
        actual!.StringNullableProperty.ShouldBe(null);
        actual!.StringNullableProperty!.ShouldBe("Incorrect value");
        actual!.StringNullablePropertyAsMethod()!.ShouldBe("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty!.ShouldBe("Incorrect value");
    }

    [Fact]
    public void ShouldBeWhenNullCoalescingOperatorTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        if (actual == null)
        {
            throw new InvalidOperationException();
        }

        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeWhenNullCoalescingOperatorTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 5,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullCoalescingOperatorTest"
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
            Actual = actual.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullCoalescingOperatorTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullCoalescingOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext2 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullCoalescingOperatorTest"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext3 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (actual.StringNullablePropertyAsMethod() ?? throw new InvalidOperationException()),
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:5",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullCoalescingOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext3);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext4 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (actual.StringNullableProperty ?? throw new InvalidOperationException()),
            ActualVariablePath = new[]
            {
                "NoExpectedVarShouldlyTest",
                "ShouldBeWhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:6",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeWhenNullCoalescingOperatorTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext4);
        //Assert
        actual.StringNullableProperty.ShouldBe("Incorrect value");
        actual.StringNullableProperty.ShouldBe(null);
        actual.StringNullableProperty?.ShouldBe("Incorrect value");
        (actual.StringNullablePropertyAsMethod() ?? throw new InvalidOperationException()).ShouldBe("Incorrect value");
        (actual.StringNullableProperty ?? throw new InvalidOperationException()).ShouldBe("Incorrect value");
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenAnonymousObjectCreationTestStormPetrel()
    {
        //Act
        var actual = new
        {
            Amount = 100,
            Message = "Hello"
        };
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeDetectExpectedArgumentWhenAnonymousObjectCreationTest",
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
                "NoExpectedVarShouldlyTest",
                "ShouldBeDetectExpectedArgumentWhenAnonymousObjectCreationTest"
            },
            Expected = new
            {
                Amount = 123,
                Message = "Hello incorrect"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeDetectExpectedArgumentWhenAnonymousObjectCreationTest"
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
        actual.ShouldBe(new { Amount = 123, Message = "Hello incorrect" });
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxTestStormPetrel()
    {
        //Act
        var actual = 100;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxTest",
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
                "NoExpectedVarShouldlyTest",
                "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxTest"
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
        actual.ShouldBe(123);
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTestStormPetrel()
    {
        //Act
        var actual = "Hello, World incorrect!";
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest",
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
                "NoExpectedVarShouldlyTest",
                "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest"
            },
            Expected = "Hello, World!",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest"
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
        actual.ShouldBe("Hello, World!");
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTestStormPetrel()
    {
        //Act
        var actual = 'B';
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest",
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
                "NoExpectedVarShouldlyTest",
                "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest"
            },
            Expected = 'A',
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest"
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
        actual.ShouldBe('A');
    }

    [Fact]
    public void ShouldBeDetectExpectedAndActualWhenComparerUsedTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "ShouldBeDetectExpectedAndActualWhenComparerUsedTest",
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
                "NoExpectedVarShouldlyTest",
                "ShouldBeDetectExpectedAndActualWhenComparerUsedTest"
            },
            Expected = new TestClassResult
            {
                IntProperty = 123,
                DateTimeProperty = DateTime.ParseExact("2025-03-17T18:11:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "ShouldBeDetectExpectedAndActualWhenComparerUsedTest"
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
        actual.ShouldBe(new TestClassResult { IntProperty = 123, DateTimeProperty = DateTime.ParseExact("2025-03-17T18:11:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind), }, new TestClassResultEqualityComparer());
    }

    [Fact]
    public void WhenShouldBeEquivalentToStormPetrel()
    {
        //Act
        var actual = Calculator.Add(2, 2);
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "WhenShouldBeEquivalentTo",
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
                "NoExpectedVarShouldlyTest",
                "WhenShouldBeEquivalentTo"
            },
            Expected = new(),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "WhenShouldBeEquivalentTo"
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
        actual.ShouldBeEquivalentTo(new());
    }

    [Fact]
    public void WhenShouldBeEquivalentToWithNamedArgsStormPetrel()
    {
        //Act
        int[] actual = [1, 2, 3];
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarShouldlyTest",
            MethodName = "WhenShouldBeEquivalentToWithNamedArgs",
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
                "NoExpectedVarShouldlyTest",
                "WhenShouldBeEquivalentToWithNamedArgs"
            },
            Expected = new int[]
            {
                3,
                4,
                5
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarShouldlyTest",
                    "WhenShouldBeEquivalentToWithNamedArgs"
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
        actual.ShouldBeEquivalentTo(customMessage: "Should detect `expected` as a named arg", expected: new int[] { 3, 4, 5 });
    }
}