using FluentAssertions;
using System.Collections.Generic;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class AssertionNoExpectedVarTestStormPetrel
    {
        [Fact]
        public void ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTest",
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
                    "ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTest"
                },
                Expected = new
                {
                    Amount = 108,
                    Message = "Hello"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTest"
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
            actual.Should().Be(new { Amount = 108, Message = "Hello" });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenArrayCreationExpressionTest",
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
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionTest"
                },
                Expected = new int[5],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenArrayCreationExpressionTest"
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
            actual.Should().Be(new int[5]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTest",
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
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTest"
                },
                Expected = new int[]
                {
                    1,
                    2,
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTest"
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
            actual.Should().Be(new int[] { 1, 2, });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTest",
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
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTest"
                },
                Expected = new int[3, 3],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTest"
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
            actual.Should().Be(new int[3, 3]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTest",
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
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTest"
                },
                Expected = new int[3][],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTest"
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
            actual.Should().Be(new int[3][]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenCollectionExpressionNoCastTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenCollectionExpressionNoCastTest",
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
                    "ShouldDetectExpectedArgumentWhenCollectionExpressionNoCastTest"
                },
                Expected = [1, 2, ],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenCollectionExpressionNoCastTest"
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
            actual.Should().Be([1, 2, ]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenCollectionExpressionTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenCollectionExpressionTest",
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
                    "ShouldDetectExpectedArgumentWhenCollectionExpressionTest"
                },
                Expected = (List<int>)[1, 2, ],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenCollectionExpressionTest"
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
            actual.Should().Be((List<int>)[1, 2, ]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTest",
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
                    "ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTest"
                },
                Expected = new[]
                {
                    1,
                    2,
                    3,
                    4,
                    5
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTest"
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
            actual.Should().Be(new[] { 1, 2, 3, 4, 5 });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTest",
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
                    "ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTest"
                },
                Expected = new[, ]
                {
                    {
                        1,
                        2,
                        3
                    },
                    {
                        4,
                        5,
                        6
                    },
                    {
                        7,
                        8,
                        9
                    }
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTest"
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
            actual.Should().Be(new[, ] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxNoCastTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxNoCastTest",
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
                    "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxNoCastTest"
                },
                Expected = new(),
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxNoCastTest"
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
            actual.Should().Be(new());
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTest",
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
                    "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTest"
                },
                Expected = (TestClassResultBase)new(),
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTest"
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
            actual.Should().Be((TestClassResultBase)new());
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTest",
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
                    "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTest"
                },
                Expected = new()
                {
                    IntProperty = 0
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTest"
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
            actual.Should().Be(new() { IntProperty = 0 });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTest",
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
                    "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTest"
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
            actual.Should().Be(123);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest",
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
                    "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest"
                },
                Expected = "Hello, World!",
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest"
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
            actual.Should().Be("Hello, World!");
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest",
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
                    "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest"
                },
                Expected = 'A',
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest"
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
            actual.Should().Be('A');
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTest",
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
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTest"
                },
                Expected = new FooExpected(),
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "AssertionNoExpectedVarTest",
                        "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTest"
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
            actual.Should().BeEquivalentTo(new FooExpected());
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTest",
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
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTest"
                },
                Expected = new FooExpected()
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
                        "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTest"
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
            actual.Should().BeEquivalentTo(new FooExpected() { BlaProperty = "123" });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTestStormPetrel()
        {
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AssertionNoExpectedVarTest",
                MethodName = "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTest",
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
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTest"
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
                        "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTest"
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
    }
}