using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit;
public class NoMatchToExpectedVarRegexTestStormPetrel
{
    [Theory]
    [InlineData(0, 100, 123)]
    [InlineData(1, 200, 123)]
    public void ArrowExpressionBodyWithActualAndExpectedStormPetrel(int stormPetrelUseCaseIndex, int arg, int exp)
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "ArrowExpressionBodyWithActualAndExpected",
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
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "exp",
                    Value = exp,
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
                "NoMatchToExpectedVarRegexTest",
                "ArrowExpressionBodyWithActualAndExpected"
            },
            Expected = exp,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "ArrowExpressionBodyWithActualAndExpected"
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
        TestedClass.ReturnInput(arg).Should().Be(exp);
    }

    [Theory]
    [InlineData(0, 100, 123, 200, 223)]
    [InlineData(1, 200, 123, 300, 333)]
    public void AttributeWithMultipleExpectedStormPetrel(int stormPetrelUseCaseIndex, int arg1, int exp1, int arg2, int exp2)
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "AttributeWithMultipleExpected",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 2,
            Parameters = new[]
            {
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "arg1",
                    Value = arg1,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "exp1",
                    Value = exp1,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "arg2",
                    Value = arg2,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "exp2",
                    Value = exp2,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                }
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.ReturnInput(arg1),
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "AttributeWithMultipleExpected"
            },
            Expected = exp1,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "AttributeWithMultipleExpected"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.ReturnInput(arg2),
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "AttributeWithMultipleExpected"
            },
            Expected = exp2,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "AttributeWithMultipleExpected"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
            {
                Index = stormPetrelUseCaseIndex,
                Name = "InlineData",
                ParameterIndex = 3
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        TestedClass.ReturnInput(arg1).Should().Be(exp1);
        TestedClass.ReturnInput(arg2).Should().Be(exp2);
    }

    [Theory]
    [InlineData(0, 100, 123)]
    [InlineData(1, 200, 123)]
    public void BodyWithMultipleExpectedStormPetrel(int stormPetrelUseCaseIndex, int arg1, int exp1)
    {
        var exp2 = 123;
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "BodyWithMultipleExpected",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 2,
            Parameters = new[]
            {
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "arg1",
                    Value = arg1,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "exp1",
                    Value = exp1,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                }
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.ReturnInput(arg1),
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "BodyWithMultipleExpected"
            },
            Expected = exp1,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "BodyWithMultipleExpected"
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
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = TestedClass.ReturnInput(100),
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "BodyWithMultipleExpected"
            },
            Expected = exp2,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "BodyWithMultipleExpected",
                "exp2"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        TestedClass.ReturnInput(arg1).Should().Be(exp1);
        TestedClass.ReturnInput(100).Should().Be(exp2);
    }

    [Theory]
    [InlineData(0, 2, 2, 5)]
    [InlineData(1, -2, -2, 123)]
    public void ComplexActualExpressionInAssertEqualAndExpectedArgMatchingRegexTestStormPetrel(int stormPetrelUseCaseIndex, int a, int b, int expected)
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "ComplexActualExpressionInAssertEqualAndExpectedArgMatchingRegexTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new[]
            {
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "a",
                    Value = a,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "b",
                    Value = b,
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
            Actual = Calculator.Add(a, b).Value,
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "ComplexActualExpressionInAssertEqualAndExpectedArgMatchingRegexTest"
            },
            Expected = expected,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "ComplexActualExpressionInAssertEqualAndExpectedArgMatchingRegexTest"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
            {
                Index = stormPetrelUseCaseIndex,
                Name = "InlineData",
                ParameterIndex = 2
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        Assert.Equal(expected, Calculator.Add(a, b).Value);
    }

    [Theory]
    [InlineData(0, 2, 2, 5)]
    [InlineData(1, -2, -2, 123)]
    public void ComplexActualExpressionInAssertEqualAndExpectedArgNotMatchingRegexTestStormPetrel(int stormPetrelUseCaseIndex, int a, int b, int exp /*does not match the regex*/)
    {
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "ComplexActualExpressionInAssertEqualAndExpectedArgNotMatchingRegexTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new[]
            {
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "a",
                    Value = a,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "b",
                    Value = b,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                },
                new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                {
                    Name = "exp",
                    Value = exp,
                    Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                    {
                    }
                }
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = Calculator.Add(a, b).Value,
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "ComplexActualExpressionInAssertEqualAndExpectedArgNotMatchingRegexTest"
            },
            Expected = exp,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "ComplexActualExpressionInAssertEqualAndExpectedArgNotMatchingRegexTest"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.AttributeContext()
            {
                Index = stormPetrelUseCaseIndex,
                Name = "InlineData",
                ParameterIndex = 2
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        Assert.Equal(exp, Calculator.Add(a, b).Value);
    }

    /// <summary>
    /// Use actual value to not fail build procedure.
    /// </summary>
    private const int IntConstant = 100;
    /// <summary>
    /// Use actual value to not fail build procedure.
    /// </summary>
    private readonly int IntProperty = 100;
    [Fact]
    public void WhenExpectedPropertyThenIgnoreIt() => Assert.Equal(IntProperty, TestedClass.TestedMethod());
    [Fact]
    public void WhenExpectedConstantThenIgnoreIt() => Assert.Equal(IntConstant, TestedClass.TestedMethod());
    [Fact]
    public void WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTestStormPetrel()
    {
        //Act
        var act = TestedClass.TestedMethod();
        var actAsString = act.ToString(CultureInfo.InvariantCulture);
        var exp = 123;
        var expAsString = "123";
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 2,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = act,
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTest"
            },
            Expected = exp,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTest",
                "exp"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actAsString,
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTest"
            },
            Expected = expAsString,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenTwoExpectedVarsNotMatchingConfigButAssertExpressionsThenVarsAreUpdatedInOneCallTest",
                "expAsString"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        //Assert
        Assert.Equal(exp, act);
        Assert.Equal(expAsString, actAsString);
    }

    [Fact]
    public void WhenExpectedVarMatchesBothVarPairAndAssertExpressionThenTheMatchDependsOnTheOrderTestStormPetrel()
    {
        //Arrange
        var expected = 123;
        var expectedString = "123";
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoMatchToExpectedVarRegexTest",
            MethodName = "WhenExpectedVarMatchesBothVarPairAndAssertExpressionThenTheMatchDependsOnTheOrderTest",
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
                "NoMatchToExpectedVarRegexTest",
                "WhenExpectedVarMatchesBothVarPairAndAssertExpressionThenTheMatchDependsOnTheOrderTest",
                "actual"
            },
            Expected = expected,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenExpectedVarMatchesBothVarPairAndAssertExpressionThenTheMatchDependsOnTheOrderTest",
                "expected"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        var actualString = actual.ToString(CultureInfo.InvariantCulture);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualString,
            ActualVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenExpectedVarMatchesBothVarPairAndAssertExpressionThenTheMatchDependsOnTheOrderTest",
                "actualString"
            },
            Expected = expectedString,
            ExpectedVariablePath = new[]
            {
                "NoMatchToExpectedVarRegexTest",
                "WhenExpectedVarMatchesBothVarPairAndAssertExpressionThenTheMatchDependsOnTheOrderTest",
                "expectedString"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        TestedClass.TestedClassResultMethod(); //One more code line to indicate more difference in unit tests for different configurations
        //Assert
        Assert.Equal(expected, actual);
        //Use ToUpperInvariant to distinguish (expectedString, actualString) and (expectedString, actualString.ToUpperInvariant()) pairs in unit tests for different configurations
        Assert.Equal(expectedString, actualString.ToUpperInvariant());
    }
}