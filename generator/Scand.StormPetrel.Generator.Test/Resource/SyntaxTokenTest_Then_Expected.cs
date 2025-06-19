using FluentAssertions;

namespace Test.Integration.XUnit;
/// <summary>
/// The tests are created based on <see cref = "Microsoft.CodeAnalysis.CSharp.SyntaxKind"/> reasonable values.
/// </summary>
public class SyntaxTokenTestStormPetrel
{
    [Fact]
    public void ShouldBeMinusTokenTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBeMinusTokenTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (-100),
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBeMinusTokenTest"
            },
            Expected = -123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBeMinusTokenTest"
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
        (-100).Should().Be(-123);
    }

    [Fact]
    public void ShouldBePlusTokenTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBePlusTokenTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 100,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBePlusTokenTest"
            },
            Expected = +123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBePlusTokenTest"
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
        100.Should().Be(+123);
    }

    [Fact]
    public void ShouldBeSingleQuouteTokenTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBeSingleQuouteTokenTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 'a',
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBeSingleQuouteTokenTest"
            },
            Expected = 'b',
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBeSingleQuouteTokenTest"
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
        'a'.Should().Be('b');
    }

    [Fact]
    public void ShouldBeExclamationTokenTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBeExclamationTokenTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = false,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBeExclamationTokenTest"
            },
            Expected = !false,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBeExclamationTokenTest"
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
        false.Should().Be(!false);
    }

    [Fact]
    public void AssertEqualMinusTokenTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualMinusTokenTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 100,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualMinusTokenTest"
            },
            Expected = -123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualMinusTokenTest"
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
        Assert.Equal(-123, 100);
    }

    [Fact]
    public void AssertEqualDefaultExpressionTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualDefaultExpressionTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 100,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualDefaultExpressionTest"
            },
            Expected = default,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualDefaultExpressionTest"
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
        Assert.Equal(default, 100);
    }

    [Fact]
    public void AssertEqualDefaultExpressionWithTypeTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualDefaultExpressionWithTypeTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 100,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualDefaultExpressionWithTypeTest"
            },
            Expected = default(int),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualDefaultExpressionWithTypeTest"
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
        Assert.Equal(default(int), 100);
    }

    [Fact]
    public void AssertEqualInterpolatedStringTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualInterpolatedStringTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = $"{100}",
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualInterpolatedStringTest"
            },
            Expected = $"{123}",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualInterpolatedStringTest"
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
        Assert.Equal($"{123}", $"{100}");
    }

    [Fact]
    public void AssertEqualInterpolatedVerbatimStringTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualInterpolatedVerbatimStringTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = @$"{100}",
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualInterpolatedVerbatimStringTest"
            },
            Expected = @$"{123}",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualInterpolatedVerbatimStringTest"
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
        Assert.Equal(@$"{123}", @$"{100}");
    }

    [Fact]
    public void AssertEqualInterpolatedRawStringTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualInterpolatedRawStringTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = $"""
        {100}
        """,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualInterpolatedRawStringTest"
            },
            Expected = $"""
        {123}
        """,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualInterpolatedRawStringTest"
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
        Assert.Equal($"""
        {123}
        """, $"""
        {100}
        """);
    }

    [Fact]
    public void ShouldBeInterpolatedStringExpressionTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBeInterpolatedStringExpressionTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = $"{100}",
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBeInterpolatedStringExpressionTest"
            },
            Expected = $"{123}",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBeInterpolatedStringExpressionTest"
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
        $"{100}".Should().Be($"{123}");
    }

    [Fact]
    public void ShouldBeOpenBracketTokenTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBeOpenBracketTokenTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = new int[]
            {
                100
            },
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBeOpenBracketTokenTest"
            },
            Expected = (object[])[123],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBeOpenBracketTokenTest"
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
        new int[]
        {
            100
        }.Should().BeEquivalentTo([123]);
    }

    [Fact]
    public void AssertEquivalentNullTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEquivalentNullTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = new object (),
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEquivalentNullTest"
            },
            Expected = null,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEquivalentNullTest"
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
        Assert.Equivalent(null, new object ());
    }

    [Fact]
    public void AssertEqualNumericTypesTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualNumericTypesTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 5,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 100.6f,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualNumericTypesTest"
            },
            Expected = 123f,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualNumericTypesTest"
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
            Actual = 100.7m,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualNumericTypesTest"
            },
            Expected = 123m,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "SyntaxTokenTest",
                    "AssertEqualNumericTypesTest"
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
            Actual = 100U,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualNumericTypesTest"
            },
            Expected = 123U,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "SyntaxTokenTest",
                    "AssertEqualNumericTypesTest"
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
            Actual = 100L,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualNumericTypesTest"
            },
            Expected = 123L,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "SyntaxTokenTest",
                    "AssertEqualNumericTypesTest"
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
            Actual = 100UL,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualNumericTypesTest"
            },
            Expected = 123UL,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "SyntaxTokenTest",
                    "AssertEqualNumericTypesTest"
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
        //Act, Assert
        Assert.Equal(123f, 100.6f);
        Assert.Equal(123m, 100.7m);
        Assert.Equal(123U, 100U);
        Assert.Equal(123L, 100L);
        Assert.Equal(123UL, 100UL);
    }

    [Fact]
    public void AssertEqualBitwiseNotTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualBitwiseNotTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = 100,
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualBitwiseNotTest"
            },
            Expected = ~123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualBitwiseNotTest"
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
        Assert.Equal(~123, 100);
    }

    [Fact]
    public void ShouldBeOmittedArraySizeExpressionTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "ShouldBeOmittedArraySizeExpressionTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = new int[, ]
            {
                {
                    100
                }
            },
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "ShouldBeOmittedArraySizeExpressionTest"
            },
            Expected = new int[, ]
            {
                {
                    123
                }
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "ShouldBeOmittedArraySizeExpressionTest"
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
        new int[, ]
        {
            {
                100
            }
        }.Should().BeEquivalentTo(new int[, ] { { 123 } });
    }

    [Fact]
    public void AssertEqualTupleTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "SyntaxTokenTest",
            MethodName = "AssertEqualTupleTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = (100, 100),
            ActualVariablePath = new[]
            {
                "SyntaxTokenTest",
                "AssertEqualTupleTest"
            },
            Expected = (123, 123),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "SyntaxTokenTest",
                    "AssertEqualTupleTest"
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
        Assert.Equal((123, 123), (100, 100));
    }
}