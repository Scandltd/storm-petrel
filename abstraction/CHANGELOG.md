# Change Log
## [2.0.0] - 2024-10-30

### Added
- Extracted abstractions from [Scand.StormPetrel.Generator](../generator/README.md) into a new individual NuGet package.
- `AbstractExtraContext` class and its inheritors: `AttributeContext`, `InitializerContext`, `InvocationSourceContext`, `TestCaseSourceContext`.
- `ExtraContext` and `MethodSharedContext` properties of the `GenerationContext` class and their related classes.

### Changed

- **Breaking:** The namespace for all public classes and interfaces. Manually update the namespace in custom implementations.

### Removed
- **Breaking:** `ExpectedVariableInvocationExpressionInfo`, `IsLastVariablePair`, `RewriterKind`, `TestCaseAttributeInfo`, and `TestCaseSourceInfo` properties of the `GenerationContext` class. Use the `GenerationContext.ExtraContext` added property instead in custom implementations.
- **Breaking:** `FilePath`, `ClassName`, and `MethodName` properties of the `GenerationContext` class. Use the `GenerationContext.MethodSharedContext` added property instead in custom implementations.
- **Breaking:** `MethodTestAttributeNames` property of the `GenerationContext` class. Use `System.Reflection` and `GenerationContext.MethodSharedContext` data instead in custom implementations.

## [1.0.0] - 2024-09-10
 
### Added

- Initial release in the context of [Scand.StormPetrel.Generator](../generator/README.md).
