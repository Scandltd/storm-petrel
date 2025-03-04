using System.Globalization;

namespace Test.Integration.XUnit;
/// <summary>
/// According to <see cref = "https :  / / github . com / xunit / assert . xunit / blob / main /"/>
/// </summary>
public class NoExpectedVarAssertTestStormPetrel
{
#region Assert.cs
    [Fact]
    public void AssertEqualTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 3,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
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
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
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
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarAssertTest",
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
        Assert.Equal(123, actual);
        Assert.Equal(actual: actual, expected: 123);
        Assert.Equal(expected: 123, actual: actual);
    }

    [Fact(Skip = "This test is skipped because two runs are required and two separate generation calls are generated.")]
    public static void AssertEqualWithTwoExpectedVarTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var expected = 101;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualWithTwoExpectedVarTest",
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
                "NoExpectedVarAssertTest",
                "AssertEqualWithTwoExpectedVarTest",
                "actual"
            },
            Expected = expected,
            ExpectedVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualWithTwoExpectedVarTest",
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
        Assert.Equal(actual: actual, expected: expected);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualWithTwoExpectedVarTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarAssertTest",
                    "AssertEqualWithTwoExpectedVarTest"
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
        Assert.Equal(123, actual);
    }

    [Fact]
    public void AssertEqualWhenActualWithMethodCallTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualWhenActualWithMethodCallTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 2,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.ToString(CultureInfo.InvariantCulture),
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualWhenActualWithMethodCallTest"
            },
            Expected = "123",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
                    "AssertEqualWhenActualWithMethodCallTest"
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
            Actual = actual.ToString(CultureInfo.InvariantCulture),
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualWhenActualWithMethodCallTest"
            },
            Expected = "123",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
                    "AssertEqualWhenActualWithMethodCallTest"
                },
                MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                {
                    NodeKind = 8638,
                    NodeIndex = 2,
                    ArgsCount = 0
                }
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        //Assert
        Assert.Equal("123", actual.ToString(CultureInfo.InvariantCulture));
        Assert.Equal(actual: actual.ToString(CultureInfo.InvariantCulture), expected: "123");
    }

    [Fact]
    public void AssertEqualTwoStructuresTestStormPetrel()
    {
        //Act
        var actual = new TestedStructure(100, 100);
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualTwoStructuresTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 3,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualTwoStructuresTest"
            },
            Expected = new TestedStructure(123, 123),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
                    "AssertEqualTwoStructuresTest"
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
                "NoExpectedVarAssertTest",
                "AssertEqualTwoStructuresTest"
            },
            Expected = new TestedStructure()
            {
                X = 123,
                Y = 123
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
                    "AssertEqualTwoStructuresTest"
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
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualTwoStructuresTest"
            },
            Expected = new TestedStructure(123, 123),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarAssertTest",
                    "AssertEqualTwoStructuresTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext2);
        //Assert
        Assert.Equal(new TestedStructure(123, 123), actual);
        Assert.Equal(new TestedStructure() { X = 123, Y = 123 }, actual);
        Assert.Equal(actual: actual, expected: new TestedStructure(123, 123));
    }

    [Fact]
    public void AssertEqualTwoTuplesTestStormPetrel()
    {
        //Act
        var actual = (x: 100, y: 100);
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualTwoTuplesTest",
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
                "NoExpectedVarAssertTest",
                "AssertEqualTwoTuplesTest"
            },
            Expected = (123, 123),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
                    "AssertEqualTwoTuplesTest"
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
                "NoExpectedVarAssertTest",
                "AssertEqualTwoTuplesTest"
            },
            Expected = (x: 123, y: 123),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
                    "AssertEqualTwoTuplesTest"
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
        Assert.Equal((123, 123), actual);
        Assert.Equal(actual: actual, expected: (x: 123, y: 123));
    }

    [Fact]
    public void AssertEqualTwoSystemTuplesTestStormPetrel()
    {
        //Act
        var actual = new Tuple<int, int>(100, 100);
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualTwoSystemTuplesTest",
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
                "NoExpectedVarAssertTest",
                "AssertEqualTwoSystemTuplesTest"
            },
            Expected = new Tuple<int, int>(123, 123),
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
                    "AssertEqualTwoSystemTuplesTest"
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
        Assert.Equal(actual: actual, expected: new Tuple<int, int>(123, 123));
    }

    [Fact]
    public async Task AssertEqualWithAwaitOfActualTestStormPetrel()
    {
        //Arrange
        var actual = new ActualTestClass();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEqualWithAwaitOfActualTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = await actual.TestMethodAsync(),
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEqualWithAwaitOfActualTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
                    "AssertEqualWithAwaitOfActualTest"
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
        Assert.Equal(actual: await actual.TestMethodAsync(), expected: 123);
    }

    [Fact]
    public void AssertJaggedArrayTestStormPetrel()
    {
        //Arrange
        int[][] actual = [[1, 2, 3], [1, 2, 3]];
        int[] actualOneDimensional = [1, 2];
        int[][, ] actualMultiArray = [new int[, ]
        {
            {
                1,
                2,
                3
            },
            {
                1,
                2,
                3
            }
        }, new int[, ]
        {
            {
                1,
                2,
                3
            },
            {
                1,
                2,
                3
            }
        }

        ];
        int[][][] actual3d = [[[1], [2], [3]], [[1], [2], [3]]];
        int[][][][] actual4d = [(int[][][])[[(int[])[1, 2]], [(int[])[1, 2]]], (int[][][])[[(int[])[1, 2]]], (int[][][])[[(int[])[1, 2]]]];
        object[] actualObjects = [1, new DateTime(2025, 2, 4)];
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertJaggedArrayTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 8,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[][])[[123, 123, 123], [123, 123, 123]],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:6",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualOneDimensional,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[])[],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:7",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[][])[[], []],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:8",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
            Actual = actual3d,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[])[(int[][])[[]], (int[][])[[]]],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:9",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
            Actual = actualMultiArray,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[])[new int[, ]
            {
                {
                    123,
                    123,
                    123
                },
                {
                    123,
                    123,
                    123
                }
            }, new int[, ]
            {
                {
                    123,
                    123,
                    123
                },
                {
                    123,
                    123,
                    123
                }
            }

            ],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:10",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext5 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual3d,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[])[(int[][])[[123, 123, 123], [123, 123, 123]], (int[][])[[123, 123, 123], [123, 123, 123]]],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:11",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext5);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext6 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual4d,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[])[(int[][][])[[(int[])[3, 4]], [(int[])[3, 4]]], (int[][][])[[(int[])[3, 4]]], (int[][][])[[(int[])[3, 4]]]],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:12",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext6);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext7 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualObjects,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertJaggedArrayTest"
            },
            Expected = (object[])[123, new DateTime(2025, 12, 14)],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:13",
                    "NoExpectedVarAssertTest",
                    "AssertJaggedArrayTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext7);
        //Act, Assert
        Assert.Equal(actual: actual, expected: [[123, 123, 123], [123, 123, 123]]);
        Assert.Equal([], actualOneDimensional);
        Assert.Equal([[], []], actual);
        Assert.Equal([(int[][])[[]], (int[][])[[]]], actual3d);
        Assert.Equal([new int[, ] { { 123, 123, 123 }, { 123, 123, 123 } }, new int[, ] { { 123, 123, 123 }, { 123, 123, 123 } }], actualMultiArray);
        Assert.Equal([(int[][])[[123, 123, 123], [123, 123, 123]], (int[][])[[123, 123, 123], [123, 123, 123]]], actual3d);
        Assert.Equal([(int[][][])[[(int[])[3, 4]], [(int[])[3, 4]]], (int[][][])[[(int[])[3, 4]]], (int[][][])[[(int[])[3, 4]]]], actual4d);
        Assert.Equal([123, new DateTime(2025, 12, 14)], actualObjects);
        //Assert ignored use cases
        //All asserts below have "expected" to not fail because Storm Petrel ignores these "expected" values
        Assert.Equal([(int[])[1, 2, 3], [1, 2, 3]], actual);
        Assert.Equal([[1, 2, 3], (int[])[1, 2, 3]], actual);
        Assert.Equal([[[1], [2], (int[])[3]], [[1], [2], [3]]], actual3d);
        Assert.Equal([[[1], [2], (int[])[3]], (int[][])[[1], [2], [3]]], actual3d);
        Assert.Equal([(int[][])[[1], [2], [3]], (int[][])[[1], (int[])[2], [3]]], actual3d);
    }

#endregion
#region AsyncCollectionAsserts.cs
    private static async IAsyncEnumerable<int> GetActualAsIAsyncEnumerableAsync()
    {
        yield return 1;
        yield return 2;
        yield return 3;
        await Task.CompletedTask; // Simulate asynchronous work
    }

    [Fact]
    public async Task AsyncCollectionAssertsEqualTestStormPetrel()
    {
        //Act
        var list = new List<int>();
        await foreach (var item in GetActualAsIAsyncEnumerableAsync())
        {
            list.Add(item);
        }

        var actual = list;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AsyncCollectionAssertsEqualTest",
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
                "NoExpectedVarAssertTest",
                "AsyncCollectionAssertsEqualTest"
            },
            Expected = (object[])[123, 123, 123],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarAssertTest",
                    "AsyncCollectionAssertsEqualTest"
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
                "NoExpectedVarAssertTest",
                "AsyncCollectionAssertsEqualTest"
            },
            Expected = (object[])[123, 123, 123],
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:4",
                    "NoExpectedVarAssertTest",
                    "AsyncCollectionAssertsEqualTest"
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
        Assert.Equal([123, 123, 123], actual);
        Assert.Equal(actual: actual, expected: [123, 123, 123]);
    }

#endregion
#region EqualityAsserts.cs
    [Fact]
    public void EqualityAssertsStrictEqualTestStormPetrel()
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
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "EqualityAssertsStrictEqualTest",
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
                "NoExpectedVarAssertTest",
                "EqualityAssertsStrictEqualTest"
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
                    "NoExpectedVarAssertTest",
                    "EqualityAssertsStrictEqualTest"
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
                "NoExpectedVarAssertTest",
                "EqualityAssertsStrictEqualTest"
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
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
                    "EqualityAssertsStrictEqualTest"
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
        Assert.StrictEqual(new { Amount = 123, Message = "Hello incorrect" }, actual);
        Assert.StrictEqual(actual: actual, expected: new { Amount = 123, Message = "Hello incorrect" });
    }

#endregion
#region EquivalenceAsserts.cs
    [Fact]
    public void EquivalenceAssertsEquivalentTestStormPetrel()
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
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "EquivalenceAssertsEquivalentTest",
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
                "NoExpectedVarAssertTest",
                "EquivalenceAssertsEquivalentTest"
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
                    "NoExpectedVarAssertTest",
                    "EquivalenceAssertsEquivalentTest"
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
                "NoExpectedVarAssertTest",
                "EquivalenceAssertsEquivalentTest"
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
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
                    "EquivalenceAssertsEquivalentTest"
                },
                MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                {
                    NodeKind = 8638,
                    NodeIndex = 2,
                    ArgsCount = 0
                }
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        //Assert
        Assert.Equivalent(new { Amount = 123, Message = "Hello incorrect" }, actual);
        Assert.Equivalent(strict: false, actual: actual, expected: new { Amount = 123, Message = "Hello incorrect" });
    }

    [Fact]
    public void AssertEquivalentWithVariedNumberOfExpressionArgumentsTestStormPetrel()
    {
        //Act
        var actual = new
        {
            Date = DateTime.ParseExact("2025-02-11T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
        };
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertTest",
            MethodName = "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 3,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest"
            },
            Expected = new
            {
                Date = new DateTime(123, 1, 1)
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertTest",
                    "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest"
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
            Actual = actual.Date.Year,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertTest",
                    "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest"
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
            Actual = actual.Date.Year,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertTest",
                "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:3",
                    "NoExpectedVarAssertTest",
                    "AssertEquivalentWithVariedNumberOfExpressionArgumentsTest"
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
        Assert.Equivalent(new { Date = new DateTime(123, 1, 1) }, actual);
        Assert.Equal(actual: actual.Date.Year, expected: 123);
        Assert.Equal(expected: 123, actual: actual.Date.Year);
    }

#endregion
#region MultipleAsserts.cs
    //Do not support this case till an explicit request
#endregion
    [Fact]
    public void AssertEqualWhithInvocationExpressionTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        //Assert
        //The assertion does not fail by default. Also, StormPetrel does not replace the invocation expression below.
        //But we keep this use case to indicate no unexpected failures in StormPetrel here.
        Assert.Equal(TestedClass.TestedMethod(), actual);
        Assert.Equal(TestedClass.ReturnInput(TestedClass.TestedMethod()), actual);
    }
}