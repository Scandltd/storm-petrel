using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class NoExpectedVarExpressionKindsTestStormPetrel
    {
        [Fact]
        public void ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTestStormPetrel()
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
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenAnonymousObjectCreationTest"
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
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().Be(new { Amount = 123, Message = "Hello incorrect" });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionTestStormPetrel()
        {
            //Act
            var actual = new int[6];
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionTest"
                },
                Expected = new int[5],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new int[5]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionWithInitializerTestStormPetrel()
        {
            //Act
            var actual = new int[]
            {
                2,
                3,
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
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
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new int[] { 1, 2, });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTestStormPetrel()
        {
            //Act
            var actual = new int[1, 1];
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionMultidimensionalTest"
                },
                Expected = new int[3, 3],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new int[3, 3]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTestStormPetrel()
        {
            //Act
            var actual = new int[1][]
            {
                [1]
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenArrayCreationExpressionJaggedTest"
                },
                Expected = new int[3][],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new int[3][]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenCollectionExpressionTestStormPetrel()
        {
            //Act
            List<int> actual = [3, 4];
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
                MethodName = "ShouldDetectExpectedArgumentWhenCollectionExpressionTest",
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
                    "Test.Integration.XUnit",
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenCollectionExpressionTest"
                },
                Expected = (object[])[1, 2, ],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenCollectionExpressionTest"
                },
                Expected = (List<int>)[1, 2, ],
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:2",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            //Assert
            actual.Should().BeEquivalentTo([1, 2, ]);
            //Assert when explicit cast
            actual.Should().BeEquivalentTo((List<int>)[1, 2, ]);
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionTestStormPetrel()
        {
            //Act
            var actual = new[]
            {
                1,
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
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
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitArrayCreationExpressionMultiDimensionalTestStormPetrel()
        {
            //Act
            var actual = new[, ]
            {
                {
                    1,
                    1,
                    1
                },
                {
                    2,
                    2,
                    2
                },
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
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
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new[, ] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTestStormPetrel()
        {
            //Act
            TestClassResultBase actual = new()
            {
                StringNullableProperty = "Incorrect Test StringNullableProperty",
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxTest"
                },
                Expected = (TestClassResultBase)new(),
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo((TestClassResultBase)new());
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTestStormPetrel()
        {
            //Act
            TestClassResultBase actual = new()
            {
                StringNullableProperty = "Incorrect Test StringNullableProperty",
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenImplicitObjectCreationExpressionSyntaxWithInitializerTest"
                },
                Expected = (TestClassResultBase)new()
                {
                    IntProperty = 0
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo((TestClassResultBase)new() { IntProperty = 0 });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTestStormPetrel()
        {
            //Act
            var actual = 100;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxTest"
                },
                Expected = 123,
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            var actual = "Hello, World incorrect!";
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest"
                },
                Expected = "Hello, World!",
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            var actual = 'B';
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest"
                },
                Expected = 'A',
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            var actual = new TestClassResultBase
            {
                StringProperty = "Test String property",
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxTest"
                },
                Expected = new TestClassResultBase(),
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new TestClassResultBase());
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTestStormPetrel()
        {
            //Act
            var actual = new TestClassResultBase
            {
                StringProperty = "Test String property",
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerTest"
                },
                Expected = new TestClassResultBase()
                {
                    StringProperty = "Incorrect Test StringNullableProperty",
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new TestClassResultBase() { StringProperty = "Incorrect Test StringNullableProperty", });
        }

        [Fact]
        public void ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTestStormPetrel()
        {
            //Act
            var actual = new TestClassResultBase
            {
                StringProperty = "Test String property",
            };
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "NoExpectedVarExpressionKindsTest",
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
                    "NoExpectedVarExpressionKindsTest",
                    "ShouldDetectExpectedArgumentWhenObjectCreationExpressionSyntaxInitializerNoConstructorParametersTest"
                },
                Expected = new TestClassResult
                {
                    StringProperty = "Incorrect Test StringNullableProperty",
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
                {
                    Path = new[]
                    {
                        "experimental-method-body-statement-index:1",
                        "Test.Integration.XUnit",
                        "NoExpectedVarExpressionKindsTest",
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
            actual.Should().BeEquivalentTo(new TestClassResult { StringProperty = "Incorrect Test StringNullableProperty", });
        }
    }
}