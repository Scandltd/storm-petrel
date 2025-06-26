using FluentAssertions;
using Shouldly;

namespace Test.Integration.XUnit;
public class NoActualAndNoExpectedVarTestStormPetrel
{
    [Fact]
    public async Task AssertEqualWithAwaitStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "AssertEqualWithAwait",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = await TestedClass.ResultMethodAsync(),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "AssertEqualWithAwait"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "AssertEqualWithAwait"
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
        //Act, Assert
        Assert.Equal(123, await TestedClass.ResultMethodAsync());
    }

    [Fact]
    public async Task AssertEqualWithAwaitAndColonNameStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "AssertEqualWithAwaitAndColonName",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = await TestedClass.ResultMethodAsync(),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "AssertEqualWithAwaitAndColonName"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "AssertEqualWithAwaitAndColonName"
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
        //Act, Assert
        Assert.Equal(actual: await TestedClass.ResultMethodAsync(), expected: 123);
    }

    [Fact]
    public async Task FluentAssertionsWithAwaitStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "FluentAssertionsWithAwait",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (await TestedClass.ResultMethodAsync()),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "FluentAssertionsWithAwait"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "FluentAssertionsWithAwait"
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
        //Act, Assert
        (await TestedClass.ResultMethodAsync()).Should().Be(123);
    }

    [Fact]
    public async Task ShouldBeWithAwaitStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ShouldBeWithAwait",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (await TestedClass.ResultMethodAsync()),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ShouldBeWithAwait"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ShouldBeWithAwait"
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
        (await TestedClass.ResultMethodAsync()).ShouldBe(123);
    }

    [Fact]
    public void ArrowExpressionBodyStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ArrowExpressionBody",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.TestedMethod(),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ArrowExpressionBody"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ArrowExpressionBody"
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
        TestedClass.TestedMethod().Should().Be(123);
    }

    [Fact]
    public void ArrowExpressionBodyWithAssertEqualStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ArrowExpressionBodyWithAssertEqual",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.TestedMethod(),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ArrowExpressionBodyWithAssertEqual"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ArrowExpressionBodyWithAssertEqual"
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
        Assert.Equal(123, TestedClass.TestedMethod());
    }

    [Fact]
    public async Task ArrowExpressionBodyWithAssertEqualAndAwaitStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ArrowExpressionBodyWithAssertEqualAndAwait",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = await TestedClass.ResultMethodAsync(),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ArrowExpressionBodyWithAssertEqualAndAwait"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ArrowExpressionBodyWithAssertEqualAndAwait"
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
        Assert.Equal(actual: await TestedClass.ResultMethodAsync(), expected: 123);
    }

    [Fact]
    public async Task ArrowExpressionBodyWithFluentAssertionsWithAwaitStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ArrowExpressionBodyWithFluentAssertionsWithAwait",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (await TestedClass.ResultMethodAsync()),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ArrowExpressionBodyWithFluentAssertionsWithAwait"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ArrowExpressionBodyWithFluentAssertionsWithAwait"
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
        (await TestedClass.ResultMethodAsync()).Should().Be(123);
    }

    [Theory]
    [InlineData(100)]
    public void ArrowExpressionBodyWithDefaultArgsStormPetrel(int arg)
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ArrowExpressionBodyWithDefaultArgs",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new[]
            {
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "arg",
                    Value = arg,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                }
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.ReturnInput(arg),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ArrowExpressionBodyWithDefaultArgs"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ArrowExpressionBodyWithDefaultArgs"
                },
                MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                {
                    NodeKind = 8638,
                    NodeIndex = 1,
                    ArgsCount = 1
                }
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        TestedClass.ReturnInput(arg).Should().Be(123);
    }

    [Theory]
    [InlineData(1, 2)]
    public void ArrowExpressionBodyWithArgsStormPetrel(int x, int y)
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoActualAndNoExpectedVarTest",
            MethodName = "ArrowExpressionBodyWithArgs",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new[]
            {
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "x",
                    Value = x,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "y",
                    Value = y,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                }
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (x + y),
            ActualVariablePath = new[]
            {
                "NoActualAndNoExpectedVarTest",
                "ArrowExpressionBodyWithArgs"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "NoActualAndNoExpectedVarTest",
                    "ArrowExpressionBodyWithArgs"
                },
                MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                {
                    NodeKind = 8638,
                    NodeIndex = 0,
                    ArgsCount = 2
                }
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        (x + y).Should().Be(123);
    }
}