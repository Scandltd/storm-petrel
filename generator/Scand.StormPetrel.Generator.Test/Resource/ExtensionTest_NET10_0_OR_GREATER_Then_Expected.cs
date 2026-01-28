using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit;
public static class ExtensionTestStormPetrel
{
    private static string ExtensionMethodExampleWithinTestClassAndPrivate(this int i) => i.ToString(CultureInfo.InvariantCulture);
    static string ExtensionMethodExampleWithinTestClassAndPrivateWithoutAccessModifier(this int i) => i.ToString(CultureInfo.InvariantCulture);
#if true
#endif
#if NET10_0_OR_GREATER
    //TODO: Remove CA1034 suppression after fix of https://github.com/dotnet/sdk/issues/51681
#pragma warning disable CA1034
    extension<TSource>(IEnumerable<TSource> source)
    {
        private bool IsEmptyWithinTestClassAndPrivate => !source.Any();

        bool IsEmptyWithinTestClassAndPrivateWithoutAccessModifier => !source.Any();
    }

#pragma warning restore CA1034
#endif
    [Fact]
    public static void WhenExtensionMethodWithinTestMethodStormPetrel()
    {
        //Arrange
        var expectedVar = "123";
        var expectedVarForPrivateExtension = "123";
        var expectedVarForPrivateExtensionWithoutAccessModifier = "123";
        var expectedForExtensionWithinPreprocessorDirective = "123";
        var expectedIsEmpty = false;
        var expectedIsEmptyForPrivateExtension = false;
        var expectedIsEmptyForPrivateExtensionWithoutAccessModifier = false;
        //Act
        var actualVar = TestedClass.TestedMethod().ExtensionMethodExampleWithinTestClass();
        var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
        {
            FilePath = "C:\\temp\\temp.cs",
            ClassName = "ExtensionTest",
            MethodName = "WhenExtensionMethodWithinTestMethod",
            VariablePairCurrentIndex = 0,
            VariablePairsCount = 7,
            Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
            {
            }
        };
        var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualVar,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualVar"
            },
            Expected = expectedVar,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedVar"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
        var actualVarForPrivateExtension = TestedClass.TestedMethod().ExtensionMethodExampleWithinTestClassAndPrivate();
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualVarForPrivateExtension,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualVarForPrivateExtension"
            },
            Expected = expectedVarForPrivateExtension,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedVarForPrivateExtension"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
        var actualVarForPrivateExtensionWithoutAccessModifier = TestedClass.TestedMethod().ExtensionMethodExampleWithinTestClassAndPrivateWithoutAccessModifier();
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext2 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualVarForPrivateExtensionWithoutAccessModifier,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualVarForPrivateExtensionWithoutAccessModifier"
            },
            Expected = expectedVarForPrivateExtensionWithoutAccessModifier,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedVarForPrivateExtensionWithoutAccessModifier"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext2);
        var actualForExtensionWithinPreprocessorDirective = TestedClass.TestedMethod().ExtensionMethodExampleWithinPreprocessorDirectiveAndTestClass();
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext3 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualForExtensionWithinPreprocessorDirective,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualForExtensionWithinPreprocessorDirective"
            },
            Expected = expectedForExtensionWithinPreprocessorDirective,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedForExtensionWithinPreprocessorDirective"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext3);
        bool actualIsEmpty = Array.Empty<int>()
#if NET10_0_OR_GREATER
        .WhereExtensionWithinTestClass(_ => true).IsEmptyWithinTestClass;
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext4 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualIsEmpty,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualIsEmpty"
            },
            Expected = expectedIsEmpty,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedIsEmpty"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext4);
#else
                .Length == 0;
#endif
        bool actualIsEmptyForPrivateExtension = Array.Empty<int>()
#if NET10_0_OR_GREATER
        .IsEmptyWithinTestClassAndPrivate;
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext5 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualIsEmptyForPrivateExtension,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualIsEmptyForPrivateExtension"
            },
            Expected = expectedIsEmptyForPrivateExtension,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedIsEmptyForPrivateExtension"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext5);
#else
                .Length == 0;
#endif
        bool actualIsEmptyForPrivateExtensionWithoutAccessModifier = Array.Empty<int>()
#if NET10_0_OR_GREATER
        .IsEmptyWithinTestClassAndPrivate;
        stormPetrelSharedContext.VariablePairCurrentIndex++;
        var stormPetrelContext6 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
        {
            Actual = actualIsEmptyForPrivateExtensionWithoutAccessModifier,
            ActualVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "actualIsEmptyForPrivateExtensionWithoutAccessModifier"
            },
            Expected = expectedIsEmptyForPrivateExtensionWithoutAccessModifier,
            ExpectedVariablePath = new[]
            {
                "ExtensionTest",
                "WhenExtensionMethodWithinTestMethod",
                "expectedIsEmptyForPrivateExtensionWithoutAccessModifier"
            },
            ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
            {
                Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
            },
            MethodSharedContext = stormPetrelSharedContext
        };
        ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext6);
#else
                .Length == 0;
#endif
        //Assert
        actualVar.Should().Be(expectedVar);
        actualVarForPrivateExtension.Should().Be(expectedVarForPrivateExtension);
        actualVarForPrivateExtensionWithoutAccessModifier.Should().Be(expectedVarForPrivateExtensionWithoutAccessModifier);
        actualForExtensionWithinPreprocessorDirective.Should().Be(expectedForExtensionWithinPreprocessorDirective);
        actualIsEmpty.Should().Be(expectedIsEmpty);
        actualIsEmptyForPrivateExtension.Should().Be(expectedIsEmptyForPrivateExtension);
        actualIsEmptyForPrivateExtensionWithoutAccessModifier.Should().Be(expectedIsEmptyForPrivateExtensionWithoutAccessModifier);
    }
}