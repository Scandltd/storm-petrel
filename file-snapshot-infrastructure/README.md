[![Scand Storm Petrel File Snapshot Infrastructure](assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel File Snapshot Infrastructure
* [Overview](#overview)
* [Use Cases](#use-cases)
    * [By Configuration](#by-configuration)
        * [Default Configuration](#default-configuration)
        * [Default Configuration With Custom Options](#default-configuration-with-custom-options)
        * [Custom Configuration](#custom-configuration)
    * [By Snapshot Read Kind](#by-snapshot-read-kind)
        * [Text](#text)
        * [Binary](#binary)
        * [Stream](#stream)
    * [By StormPetrel Use Cases](#by-stormpetrel-use-cases)
    * [By Test Method Use Case Id](#by-test-method-use-case-id)
        * [Use Case Id in Test Method Parameters](#use-case-id-in-test-method-parameters)
        * [No Use Case Id in Test Method Parameters](#no-use-case-id-in-test-method-parameters)
* [Getting Started](#getting-started)
* [Supported Software](#supported-software)
* [CHANGELOG](CHANGELOG.md)
* [References](#references)

## Overview
[![NuGet Version](http://img.shields.io/nuget/v/Scand.StormPetrel.FileSnapshotInfrastructure.svg?style=flat)](https://www.nuget.org/packages/Scand.StormPetrel.FileSnapshotInfrastructure)

.NET library that implements [Scand.StormPetrel.Generator.Abstraction](../abstraction/README.md) to rewrite expected baseline files with actual snapshots (HTML, JSON, XML, images, or other bytes). This can be utilized in Snapshot Unit Testing when snapshots are stored as individual files in the file system.

[![Primary Use Case](assets/primary-use-case.gif)](assets/primary-use-case.gif)

## Use Cases

### By Configuration

#### Default Configuration
Rewrites expected baseline files as demonstrated by [CalculatorSnapshotTest](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.cs) against the default baseline file structure.
Initial baseline files like [AddTest.json](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.Expected/AddTest.json) must exist before StormPetrel auto-generated tests and their origin tests execution because the default [appsettings.StormPetrel.json](Test.Integration.DefaultConfiguration/appsettings.StormPetrel.json) does not specify snapshot file extensions.

#### Default Configuration With Custom Options
Similar to the Default Configuration, but explicitly specifies the `json` extension and custom baseline file structure on test assembly initialization in [ModuleInitializer](Test.Integration.DefaultConfigurationWithCustomOptions/ModuleInitializer.cs). Initial baseline files may not exist before StormPetrel auto-generated tests and their origin tests execution in this case.

#### Custom Configuration
Rewrites expected baseline files as demonstrated by [CalculatorSnapshotTest](Test.Integration.CustomConfiguration/CalculatorSnapshotTest.cs) against a custom baseline file structure.
Initial baseline files may not exist before StormPetrel auto-generated tests and their origin tests execution because the custom configuration may specify snapshot file extensions as indicated in [CustomSnapshotOptions](Test.Integration.CustomConfiguration/CustomSnapshotInfrastructure/CustomSnapshotOptions.cs).
See other files in [CustomSnapshotInfrastructure](Test.Integration.CustomConfiguration/CustomSnapshotInfrastructure) for detailed examples of custom configurations.

### By Snapshot Read Kind

#### Text
See `ReadAllText` method call examples in [CalculatorSnapshotTest](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.cs) and other tests.

#### Binary
See `ReadAllBytes` method call examples in [CalculatorSnapshotTest](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.cs) and other tests.

#### Stream
See `OpenReadWithShareReadWrite` method call examples in [CalculatorSnapshotTest](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.cs) and other tests.

### By StormPetrel Use Cases
StormPetrel supports many [use cases](../generator/README.md#primary-use-cases) based on test framework attributes.
All these use cases are supported by the File Snapshot Infrastructure package. Some of them are demonstrated in [DefaultConfiguration/CalculatorSnapshotTest](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.cs) and [CustomConfiguration/CalculatorSnapshotTest](Test.Integration.CustomConfiguration/CalculatorSnapshotTest.cs).

### By Test Method Use Case Id

#### Use Case Id in Test Method Parameters

If a test method has `useCaseId` parameter name or a method parameter marked by [UseCaseIdAttribute](Scand.StormPetrel.FileSnapshotInfrastructure/Attributes/UseCaseIdAttribute.cs), then the parameter value is transferred to/from File Snapshot Infrastructure code to get the appropriate snapshot baseline file or folder name. Find `useCaseId` in [DefaultConfiguration/CalculatorSnapshotTest](Test.Integration.DefaultConfiguration/CalculatorSnapshotTest.cs) or [CustomConfiguration/CalculatorSnapshotTest](Test.Integration.CustomConfiguration/CalculatorSnapshotTest.cs) for more details.

#### No Use Case Id in Test Method Parameters

If a test method does not have `useCaseId` parameter name and no method parameter marked by [UseCaseIdAttribute](Scand.StormPetrel.FileSnapshotInfrastructure/Attributes/UseCaseIdAttribute.cs), then File Snapshot Infrastructure code treats `useCaseId` as empty in snapshot baseline file or folder names.

## Getting Started
To utilize File Snapshot Infrastructure in a test .NET project:
* Add the [Scand.StormPetrel.Generator](https://nuget.org/packages/Scand.StormPetrel.Generator) NuGet package to the project.
* Add the [Scand.StormPetrel.FileSnapshotInfrastructure](https://nuget.org/packages/Scand.StormPetrel.FileSnapshotInfrastructure) NuGet package to the project.
* Add an `appsettings.StormPetrel.json` file with `Build Action` set to `C# analyzer additional file` according to Scand.StormPetrel.Generator [Configuration](../generator/README.md#configuration). The file should reference File Snapshot Infrastructure classes like below:
```jsonc
{
  "$schema": "https://raw.githubusercontent.com/Scandltd/storm-petrel/main/generator/assets/appsettings.StormPetrel.Schema.json", // [optional] string, path to json schema. 
  "GeneratorConfig":
  {
    "DumperExpression": "new Scand.StormPetrel.FileSnapshotInfrastructure.SnapshotDumper()", //Use SnapshotDumper to dump string/binary/stream snapshots stored in actual/expected test variables.
    "RewriterExpression": "new Scand.StormPetrel.FileSnapshotInfrastructure.SnapshotRewriter()" //Use SnapshotRewriter to rewrite snapshot files with actual values.
  },
  "IgnoreInvocationExpressionRegex": "SnapshotProvider", //Ignore SnapshotProvider expressions in Storm Petrel flow.
  "Serilog": null //Optionally disable the logging.
}
```
See this and other configuration examples in [DefaultConfiguration](Test.Integration.DefaultConfiguration/Test.Integration.DefaultConfiguration.csproj), [DefaultConfigurationWithCustomOptions](Test.Integration.DefaultConfigurationWithCustomOptions/Test.Integration.DefaultConfigurationWithCustomOptions.csproj), and [CustomConfiguration](Test.Integration.CustomConfiguration/Test.Integration.CustomConfiguration.csproj) test projects.

## Supported Software
The same as in [Scand.StormPetrel.Generator](../generator/README.md#supported-software).

## [CHANGELOG](CHANGELOG.md)

## References

At SCAND, we specialize in [building advanced .NET solutions](https://scand.com/technologies/net/) to help businesses develop new or modernize their legacy applications. If you need help getting started with Storm Petrel or support with implementation, we're ready to assist. Whether you're refactoring or rewriting, our team can help solve any challenges you might face. Visit our [page](https://scand.com/contact-us/) to learn more, or reach out for hands-on guidance.