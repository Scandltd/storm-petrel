using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit;

public static class ExtensionTest
{
    public static string ExtensionMethodExampleWithinTestClass(this int i) => i.ToString(CultureInfo.InvariantCulture);
    private static string ExtensionMethodExampleWithinTestClassAndPrivate(this int i) => i.ToString(CultureInfo.InvariantCulture);
    static string ExtensionMethodExampleWithinTestClassAndPrivateWithoutAccessModifier(this int i) => i.ToString(CultureInfo.InvariantCulture);
#if true
    internal static string ExtensionMethodExampleWithinPreprocessorDirectiveAndTestClass(this int i) => i.ToString(CultureInfo.InvariantCulture);
#endif
#if NET10_0_OR_GREATER
    //TODO: Remove CA1034 suppression after fix of https://github.com/dotnet/sdk/issues/51681
#pragma warning disable CA1034
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool IsEmptyWithinTestClass => !source.Any();
        public IEnumerable<TSource> WhereExtensionWithinTestClass(Func<TSource, bool> predicate) => source.Where(predicate);
        private bool IsEmptyWithinTestClassAndPrivate => !source.Any();
        bool IsEmptyWithinTestClassAndPrivateWithoutAccessModifier => !source.Any();
    }
#pragma warning restore CA1034
#endif

    [Fact]
    public static void WhenExtensionMethodWithinTestMethod()
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
        var actualVarForPrivateExtension = TestedClass.TestedMethod().ExtensionMethodExampleWithinTestClassAndPrivate();
        var actualVarForPrivateExtensionWithoutAccessModifier = TestedClass.TestedMethod().ExtensionMethodExampleWithinTestClassAndPrivateWithoutAccessModifier();
        var actualForExtensionWithinPreprocessorDirective = TestedClass.TestedMethod().ExtensionMethodExampleWithinPreprocessorDirectiveAndTestClass();
        bool actualIsEmpty = Array.Empty<int>()
#if NET10_0_OR_GREATER
            .WhereExtensionWithinTestClass(_ => true).IsEmptyWithinTestClass;
#else
                .Length == 0;
#endif
        bool actualIsEmptyForPrivateExtension = Array.Empty<int>()
#if NET10_0_OR_GREATER
            .IsEmptyWithinTestClassAndPrivate;
#else
                .Length == 0;
#endif
        bool actualIsEmptyForPrivateExtensionWithoutAccessModifier = Array.Empty<int>()
#if NET10_0_OR_GREATER
            .IsEmptyWithinTestClassAndPrivate;
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
