using FluentAssertions;

namespace Test.Integration.XUnit
{
    /// <summary>
    /// Demonstrates scenarios where test-case attribute arguments (input/expected values) cannot be expressed
    /// directly because of C# attribute argument restrictions:
    /// <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/attribute-usage-errors#attribute-arguments-and-parameters"/>.
    /// The applied workaround is to pass a `UseCase` enum value from the attribute and map it to the required
    /// complex objects inside helper methods (for example `GetExpectedResultForTestA` and `GetBWrapper`).
    /// </summary>
    public class AttributesAndComplexTypesTest
    {
        [Theory]
        [InlineData(UseCase.When2Plus2, 2, 2)]
        [InlineData(UseCase.WhenMinus2PlusMinus1, -2, 1)]
        public void TestMethodWithComplexTypesAsExpectedParameter(UseCase useCase, int a, int b)
        {
            // Arrange: no setup required for this simple example.
            // Act
            var actual = Calculator.Add(a, b);
            // Assert
            var expected = GetExpectedResultForTestA(useCase);
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(UseCase.When2Plus2, 1)]
        [InlineData(UseCase.WhenMinus2PlusMinus1, 2)]
        public void TestMethodWithComplexTypesAsUseCaseInputAndExpectedParameters(UseCase useCase, int a)
        {
            // Arrange
            // Resolve complex inputs from the `UseCase` enum via helper methods.
            var bWrapper = GetBWrapper(useCase);
            // Act
            var actual = Calculator.Add(a, bWrapper.Value);
            // Assert
            var expected = GetExpectedResultForTestB(useCase);
            actual.Should().BeEquivalentTo(expected);
        }

        public enum UseCase
        {
            When2Plus2,
            WhenMinus2PlusMinus1,
        }

        private static AddResult GetBWrapper(UseCase useCase) => useCase switch
        {
            UseCase.When2Plus2 => new()
            {
                Value = 2,
            },
            UseCase.WhenMinus2PlusMinus1 => new()
            {
                Value = -1,
            },
            _ => throw new NotImplementedException($"Use case {useCase} is not implemented."),
        };

        private static AddResult GetExpectedResultForTestA(UseCase useCase) => useCase switch
        {
            UseCase.When2Plus2 => new()
            {
                Value = 5,
                ValueAsHexString = "0x5 Incorrect",
            },
            UseCase.WhenMinus2PlusMinus1 => new()
            {
                Value = 5,
                ValueAsHexString = "0x5 Incorrect",
            },
            _ => throw new NotImplementedException($"Use case {useCase} is not implemented."),
        };

        private static AddResult GetExpectedResultForTestB(UseCase useCase) => useCase switch
        {
            UseCase.When2Plus2 => new()
            {
                Value = 5,
                ValueAsHexString = "0x5 Incorrect",
            },
            UseCase.WhenMinus2PlusMinus1 => new()
            {
                Value = 5,
                ValueAsHexString = "0x5 Incorrect",
            },
            _ => throw new NotImplementedException($"Use case {useCase} is not implemented."),
        };
    }
}
