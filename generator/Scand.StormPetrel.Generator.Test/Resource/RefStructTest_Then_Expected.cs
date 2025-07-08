using System.Text;

namespace Test.Integration.XUnit;
public partial class RefStructTestStormPetrel
{
    [Fact]
    public void Utf8StringLiteralTestStormPetrel()
    {
        //Act
        var actual = "cdef"u8;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "RefStructTest",
            MethodName = "Utf8StringLiteralTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = Encoding.UTF8.GetString(actual),
            ActualVariablePath = new[]
            {
                "RefStructTest",
                "Utf8StringLiteralTest"
            },
            Expected = "abc",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "RefStructTest",
                    "Utf8StringLiteralTest"
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
        Assert.Equal("abc", Encoding.UTF8.GetString(actual));
    }

    [Fact]
    public void StackAllocArrayCreationExpressionTestStormPetrel()
    {
        //Arrange
        Span<int> actual = stackalloc int[5];
        //Act
        FillActualSpan(actual);
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "RefStructTest",
            MethodName = "StackAllocArrayCreationExpressionTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.ToArray(),
            ActualVariablePath = new[]
            {
                "RefStructTest",
                "StackAllocArrayCreationExpressionTest"
            },
            Expected = (object[])[1, 2, 3],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "RefStructTest",
                    "StackAllocArrayCreationExpressionTest"
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
        Assert.Equal([1, 2, 3], actual.ToArray());
    }

    [Fact]
    public void ReadOnlySpanCreationExpressionTestStormPetrel()
    {
        //Arrange
        int[] expected = [1, 2, 3];
        //Act
        ReadOnlySpan<int> act = [4, 5, 6];
        //Assert
        var actual = act.ToArray();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "RefStructTest",
            MethodName = "ReadOnlySpanCreationExpressionTest",
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
                "RefStructTest",
                "ReadOnlySpanCreationExpressionTest",
                "actual"
            },
            Expected = expected,
            ExpectedVariablePath = new[]
            {
                "RefStructTest",
                "ReadOnlySpanCreationExpressionTest",
                "expected"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ReadOnlySpanFromMethodExpressionTestStormPetrel()
    {
        //Arrange
        int[] expected = [1, 2, 3];
        //Act
        var actual = GetActualAsReadOnlySpan().ToArray();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "RefStructTest",
            MethodName = "ReadOnlySpanFromMethodExpressionTest",
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
                "RefStructTest",
                "ReadOnlySpanFromMethodExpressionTest",
                "actual"
            },
            Expected = expected,
            ExpectedVariablePath = new[]
            {
                "RefStructTest",
                "ReadOnlySpanFromMethodExpressionTest",
                "expected"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        //Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ArrowMethodWithActualReadOnlySpanFromMethodExpressionTestStormPetrel()
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "RefStructTest",
            MethodName = "ArrowMethodWithActualReadOnlySpanFromMethodExpressionTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = GetActualAsReadOnlySpan().ToArray(),
            ActualVariablePath = new[]
            {
                "RefStructTest",
                "ArrowMethodWithActualReadOnlySpanFromMethodExpressionTest"
            },
            Expected = (object[])[1, 2, 3],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:0",
                    "RefStructTest",
                    "ArrowMethodWithActualReadOnlySpanFromMethodExpressionTest"
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
        Assert.Equal([1, 2, 3], GetActualAsReadOnlySpan().ToArray());
    }

    private static ReadOnlySpan<int> GetActualAsReadOnlySpan() => [4, 5, 6];
    private static (int NodeKind, int NodeIndex) GetActualAsReadOnlySpanStormPetrel() => (8917, 0);
    private static void FillActualSpan(Span<int> actual)
    {
        for (int i = 0; i < actual.Length; i++)
        {
            actual[i] = i;
        }
    }
}