using System.Globalization;
using FluentAssertions;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class AttributesTestStormPetrel
    {
        [Theory]
        [InlineData(0, 1, "one")]
        [InlineData(1, 2, "two")]
        [InlineData(2, 3, "three")]
        public void TestMethodStormPetrel(int stormPetrelUseCaseIndex, int intArg, string expected)
        {
            //Act
            var actual = "one_actual";
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethod",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "intArg",
                        Value = intArg,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethod",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethod"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 1
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0, 1, "one", 2)]
        [InlineData(1, 2, "two")]
        [InlineData(2, 3, "three")]
        public void TestMethodMultipleExpectedStormPetrel(int stormPetrelUseCaseIndex, int intArg, string expected, int expected2 = -1)
        {
            //Act
            var actual = "one_actual";
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethodMultipleExpected",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 2,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "intArg",
                        Value = intArg,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected2",
                        Value = expected2,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 1
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            var actual2 = "two_actual";
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual2,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected",
                    "actual2"
                },
                Expected = expected2,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodMultipleExpected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 2
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
            actual2.Should().BeEquivalentTo(expected2);
        }

        [Theory]
        [InlineData(0, 1, "one")]
        [InlineData(1, 2, "two")]
        [InlineData(2, 3, "three")]
        public void TestMethodWithParameterAttributesStormPetrel(int stormPetrelUseCaseIndex, [CallerFilePath] int intArg, [Obsolete("This method is obsolete. Use NewMethod instead."), SomeNameSpace.MyCustomAttribute("Example")] string expected[SomeNameSpace.CallerFilePath][CallerMemberName] string oneMoreArgWithTwoAttributes = "")
        {
            //Act
            var actual = "one_actual";
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethodWithParameterAttributes",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "intArg",
                        Value = intArg,
                        Attributes = new[]
                        {
                            new Scand.StormPetrel.Generator.Abstraction.AttributeInfo()
                            {
                                Name = "CallerFilePath"
                            }
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new[]
                        {
                            new Scand.StormPetrel.Generator.Abstraction.AttributeInfo()
                            {
                                Name = "Obsolete"
                            },
                            new Scand.StormPetrel.Generator.Abstraction.AttributeInfo()
                            {
                                Name = "SomeNameSpace.MyCustomAttribute"
                            }
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "oneMoreArgWithTwoAttributes",
                        Value = oneMoreArgWithTwoAttributes,
                        Attributes = new[]
                        {
                            new Scand.StormPetrel.Generator.Abstraction.AttributeInfo()
                            {
                                Name = "SomeNameSpace.CallerFilePath"
                            },
                            new Scand.StormPetrel.Generator.Abstraction.AttributeInfo()
                            {
                                Name = "CallerMemberName"
                            }
                        }
                    }
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodWithParameterAttributes",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodWithParameterAttributes"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 1
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0, 1, "1_incorrect")]
        [InlineData(1, 2, "incorrect_2")]
        public void TestMethodWithTwoExpectedVarStormPetrel(int stormPetrelUseCaseIndex, int i, string expected)
        {
            //Arrange
            int expectedLength = 11;
            //Act
            var actual = i.ToString(CultureInfo.InvariantCulture);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AttributesTest",
                MethodName = "TestMethodWithTwoExpectedVar",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 2,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "i",
                        Value = i,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodWithTwoExpectedVar",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodWithTwoExpectedVar"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
                {
                    Index = stormPetrelUseCaseIndex,
                    Name = "InlineData",
                    ParameterIndex = 1
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            var actualLength = actual.Length;
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actualLength,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodWithTwoExpectedVar",
                    "actualLength"
                },
                Expected = expectedLength,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Rewriter.Test.Resource",
                    "AttributesTest",
                    "TestMethodWithTwoExpectedVar",
                    "expectedLength"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
            actualLength.Should().Be(expectedLength);
        }
    }
}