using FluentAssertions;

namespace Test.Integration.XUnit;
public class NoExpectedVarWithOperatorsTestStormPetrel
{
    [Fact]
    public void WhenNullConditionalOperatorTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarWithOperatorsTest",
            MethodName = "WhenNullConditionalOperatorTest",
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:6",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorTest"
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
        actual?.StringNullableProperty.Should().Be("Incorrect value");
        actual?.StringNullableProperty.Should().Be(null);
        actual?.StringNullableProperty?.Should().Be("Incorrect value");
        actual?.StringNullablePropertyAsMethod()?.Should().BeEquivalentTo("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty?.Should().Be("Incorrect value");
    }

    [Fact]
    public void WhenNullConditionalOperatorAssertsNullTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        actual.StringNullableProperty = null;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarWithOperatorsTest",
            MethodName = "WhenNullConditionalOperatorAssertsNullTest",
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorAssertsNullTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorAssertsNullTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorAssertsNullTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:5",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorAssertsNullTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullConditionalOperatorAssertsNullTest"
            },
            Expected = "Incorrect not null",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:7",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullConditionalOperatorAssertsNullTest"
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
        actual?.StringNullableProperty.Should().Be("Incorrect not null");
        //Test comment
        actual?.StringNullableProperty.Should().Be(null);
        //Test multiline
        //comment
        actual?.StringNullableProperty?.Should().Be("Incorrect not null");
        /*Test comment*/
        actual?.StringNullablePropertyAsMethod()?.Should().BeEquivalentTo("Incorrect not null");
        ArgumentNullException.ThrowIfNull(actual);
        /*Test multiline
         *comment*/
        actual.StringNullableProperty?.Should().Be("Incorrect not null");
    }

    [Fact]
    public void WhenNullForgivenOperatorTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarWithOperatorsTest",
            MethodName = "WhenNullForgivenOperatorTest",
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullForgivenOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullForgivenOperatorTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullForgivenOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullForgivenOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullForgivenOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullForgivenOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:6",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullForgivenOperatorTest"
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
        actual!.StringNullableProperty.Should().Be("Incorrect value");
        actual!.StringNullableProperty.Should().Be(null);
        actual!.StringNullableProperty!.Should().Be("Incorrect value");
        actual!.StringNullablePropertyAsMethod()!.Should().BeEquivalentTo("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty!.Should().Be("Incorrect value");
    }

    [Fact]
    public void WhenNullCoalescingOperatorTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarWithOperatorsTest",
            MethodName = "WhenNullCoalescingOperatorTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 5,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (actual ?? throw new InvalidOperationException()).StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarWithOperatorsTest",
                "WhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullCoalescingOperatorTest"
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
            Actual = (actual ?? throw new InvalidOperationException()).StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarWithOperatorsTest",
                "WhenNullCoalescingOperatorTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullCoalescingOperatorTest"
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
            Actual = (actual ?? throw new InvalidOperationException()).StringNullableProperty,
            ActualVariablePath = new[]
            {
                "NoExpectedVarWithOperatorsTest",
                "WhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullCoalescingOperatorTest"
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
            Actual = ((actual ?? throw new InvalidOperationException()).StringNullablePropertyAsMethod() ?? throw new InvalidOperationException()),
            ActualVariablePath = new[]
            {
                "NoExpectedVarWithOperatorsTest",
                "WhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullCoalescingOperatorTest"
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
                "NoExpectedVarWithOperatorsTest",
                "WhenNullCoalescingOperatorTest"
            },
            Expected = "Incorrect value",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:5",
                    "NoExpectedVarWithOperatorsTest",
                    "WhenNullCoalescingOperatorTest"
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
        (actual ?? throw new InvalidOperationException()).StringNullableProperty.Should().Be("Incorrect value");
        (actual ?? throw new InvalidOperationException()).StringNullableProperty.Should().Be(null);
        (actual ?? throw new InvalidOperationException()).StringNullableProperty?.Should().Be("Incorrect value");
        ((actual ?? throw new InvalidOperationException()).StringNullablePropertyAsMethod() ?? throw new InvalidOperationException()).Should().BeEquivalentTo("Incorrect value");
        (actual.StringNullableProperty ?? throw new InvalidOperationException()).Should().Be("Incorrect value");
    }
}