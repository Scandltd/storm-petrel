# Change Log
## [2.1.0] - 2024-12-24

### Added
- More references to the documentation.
- Collection Initializer decorator for dumped expected baseline.
- Implicit Object Creation decorator for dumped expected baseline.
- Build scripts.

### Fixed
- Incorrect indentation if data source method has comments.

## [2.0.0] - 2024-10-30

### Added
- Support for `expected` test method parameters from test case attributes (xUnit InlineData, NUnit TestCase, MSTest DataRow).
- Support for `expected` test method parameters from test data source attributes (xUnit MemberData or ClassData, NUnit TestCaseSource, MSTest DynamicData).
- Instantiation of new classes and population of their properties, added in the [2.0.0] release of [Scand.StormPetrel.Generator.Abstraction](../abstraction/README.md).
- Snapshot test examples for cases where snapshots are hardcoded in C# code.
- Support for C# file scoped namespace declarations in test methods and expected baseline source methods/properties.
- `IgnoreInvocationExpressionRegex` configuration property.

### Fixed
- Baseline indentation algorithm to properly indent multiline strings.
- StormPetrel method compilation failure when the original test uses a C# extension method.

### Removed
- **Breaking:** Abstractions extracted into the [Scand.StormPetrel.Generator.Abstraction](../abstraction/README.md) NuGet package. See its [CHANGELOG](../abstraction/CHANGELOG.md) for breaking details.

## [1.0.0] - 2024-09-10
 
### Added

- Initial release.
