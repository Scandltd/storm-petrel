using NUnit.Framework.Constraints;
using System.Globalization;

namespace Test.Integration.NUnit;
/// <summary>
/// Constraint Model (Assert.That) [1] is supported only.
/// Classic Model [2] is not supported.
/// See more details in [3].
/// [1] <see cref = "https :  / / docs . nunit . org / articles / nunit / writing - tests / assertions / assertion - models / constraint . html"/>
/// [2] <see cref = "https :  / / docs . nunit . org / articles / nunit / writing - tests / assertions / assertion - models / classic . html"/>
/// [3] <see cref = "https :  / / docs . nunit . org / articles / nunit / writing - tests / assertions / assertions . html"/>
/// </summary>
public class NoExpectedVarAssertThatTestStormPetrel
{
    [Test]
    public void AssertThatEqualToTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertThatTest",
            MethodName = "AssertThatEqualToTest",
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
                "NoExpectedVarAssertThatTest",
                "AssertThatEqualToTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatEqualToTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        //Assert
        Assert.That(actual, Is.EqualTo(123));
    }

    [Test]
    public void AssertThatActualWithMethodCallIsEqualToTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertThatTest",
            MethodName = "AssertThatActualWithMethodCallIsEqualToTest",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 1,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual.ToString(CultureInfo.InvariantCulture),
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertThatTest",
                "AssertThatActualWithMethodCallIsEqualToTest"
            },
            Expected = "123",
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatActualWithMethodCallIsEqualToTest"
                },
                MethodInfo = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceMethodInfo()
                {
                    NodeKind = 8638,
                    NodeIndex = 3,
                    ArgsCount = 0
                }
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        //Assert
        Assert.That(actual.ToString(CultureInfo.InvariantCulture), Is.EqualTo("123"));
    }

    /// <summary>
    /// According to an example from <see cref = "https :  / / docs . nunit . org / articles / nunit / writing - tests / assertions / assertion - models / constraint . html"/>
    /// </summary>
    [Test]
    public void AssertThatEqualConstraintTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertThatTest",
            MethodName = "AssertThatEqualConstraintTest",
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
                "NoExpectedVarAssertThatTest",
                "AssertThatEqualConstraintTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatEqualConstraintTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertThatTest",
                "AssertThatEqualConstraintTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatEqualConstraintTest"
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
        Assert.That(actual, new EqualConstraint(123));
        Assert.That(expression: new EqualConstraint(123), actual: actual);
    }

    [Test]
    public void AssertThatIsEqualToMultipleTestStormPetrel()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertThatTest",
            MethodName = "AssertThatIsEqualToMultipleTest",
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
                "NoExpectedVarAssertThatTest",
                "AssertThatIsEqualToMultipleTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatIsEqualToMultipleTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actual,
            ActualVariablePath = new[]
            {
                "NoExpectedVarAssertThatTest",
                "AssertThatIsEqualToMultipleTest"
            },
            Expected = 123,
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:2",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatIsEqualToMultipleTest"
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
        Assert.That(actual, Is.EqualTo(123));
        Assert.That(expression: Is.EqualTo(123), actual: actual);
    }

    [Test]
    public void AssertThatIsEquivalentToTestStormPetrel()
    {
        //Act
        int[] actual = [1, 0, 0];
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertThatTest",
            MethodName = "AssertThatIsEquivalentToTest",
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
                "NoExpectedVarAssertThatTest",
                "AssertThatIsEquivalentToTest"
            },
            Expected = new int[]
            {
                1,
                2,
                3
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatIsEquivalentToTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        //Assert
        Assert.That(actual, Is.EquivalentTo(new int[] { 1, 2, 3 }));
    }

    [Test]
    public void AssertThatCollectionEquivalentConstraintTestStormPetrel()
    {
        //Act
        int[] actual = [1, 0, 0];
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "NoExpectedVarAssertThatTest",
            MethodName = "AssertThatCollectionEquivalentConstraintTest",
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
                "NoExpectedVarAssertThatTest",
                "AssertThatCollectionEquivalentConstraintTest"
            },
            Expected = new int[]
            {
                1,
                2,
                3
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InvocationSourceContext()
            {
                Path = new[]
                {
                    "experimental-method-body-statement-index:1",
                    "NoExpectedVarAssertThatTest",
                    "AssertThatCollectionEquivalentConstraintTest"
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
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        //Assert
        Assert.That(actual, new CollectionEquivalentConstraint(new int[] { 1, 2, 3 }));
    }
}