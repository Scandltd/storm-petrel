[![Scand Storm Petrel Generator](https://raw.githubusercontent.com/Scandltd/storm-petrel/main/assets/logo-128x128-transparent.png)](https://scand.com/products/storm-petrel-expected-baselines-rewriter)
# Scand Storm Petrel Generator
* [Overview](#overview)
* [Primary Use Cases](#primary-use-cases)
    * [Enabler: Auto-Generate `CalculatorTestStormPetrel.AddTestStormPetrel`](#use-case-enabler)
    * [Overwrite `CalculatorTest.AddTest` expected baseline](#use-case-overwrite-baseline)
    * [Overwrite `CalculatorTestTheory.AddTestGetExpected` expected baseline method](#use-case-overwrite-baseline-get-expected-method)
    * [Use Case Variations](#use-case-variations)
* [Getting Started](#getting-started)
* [Configuration](#configuration)
* [Supported Software](#supported-software)
    * [Test Frameworks](#supported-test-frameworks)
    * [.NET Versions](#supported-net-versions)
* [References](#references)

## Overview<a name="overview"></a>

.NET Incremental Generator that creates modified copies of unit and/or integration tests to update expected baselines in original tests, automating baseline creation and accelerating test development.

## Primary Use Cases<a name="primary-use-cases"></a>

### Enabler: Auto-Generate `CalculatorTestStormPetrel.AddTestStormPetrel`<a name="use-case-enabler"></a>

###### Given
`Calculator` class with a bug introduced by the `buggyDelta` variable:
```csharp
public class Calculator
{
    public static AddResult Add(int a, int b)
    {
        var buggyDelta = 1;
        var result = a + b + buggyDelta;
        return new AddResult
        {
            Value = result,
            ValueAsHexString = "0x" + result.ToString("x"),
        };
    }
}

public class AddResult
{
    public int Value { get; set; }
    public string ValueAsHexString { get; set; } = string.Empty;
}
```

And its corresponding test with an expected baseline that matches the Calculator.Add buggy behavior:

```csharp
public class CalculatorTest
{
    [Xunit.Fact]
    public void AddTest()
    {
        //Arrange
        // incorrect `expected` baseline value will be overwritten with correct `actual` value
        // after manual execution of auto-generated AddTestStormPetrel test.
        var expected = new AddResult
        {
            Value = 5, //incorrect value example
            ValueAsHexString = "0x5"
        };

        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
```

##### When
The developer configures the test project with StormPetrel.Generator as per the [Getting Started](#getting-started).

##### Then
A new test method, `CalculatorTestStormPetrel.AddTestStormPetrel`, is generated. This method is a specially modified copy of the original CalculatorTest.AddTest to overwrite its expected baseline.

### Overwrite `CalculatorTest.AddTest` expected baseline <a name="use-case-overwrite-baseline"></a>
##### Given
`CalculatorTestStormPetrel.AddTestStormPetrel` is auto-generated after enabling Storm Petrel functionality.
##### When
The developer fixes `buggyDelta` to `0`
and executes `CalculatorTestStormPetrel.AddTestStormPetrel` test.
##### Then
`CalculatorTest.AddTest` code is populated with correct expected baseline value, i.e. its code becomes
```csharp
public class CalculatorTest
{
    [Xunit.Fact]
    public void AddTest()
    {
        //Arrange
        // incorrect `expected` baseline value will be overwritten with correct `actual` value
        // after manual execution of auto-generated AddTestStormPetrel test.
        var expected = new AddResult
        {
            Value = 4,
            ValueAsHexString = "0x4"
        };

        //Act
        var actual = Calculator.Add(2, 2);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
```
##### So that
The developer should only review expected baseline changes (no manual modification) what typically saves development time.

### Overwrite `CalculatorTestTheory.AddTestGetExpected` expected baseline method<a name="use-case-overwrite-baseline-get-expected-method"></a>
##### Given
`CalculatorTestTheory.AddTest` test below with

* multiple use cases;
* AddTestGetExpected static method call returning incorrect expected baselines based on arguments;
* AddTestGetExpected method possible variations per its comments.
```csharp
public class CalculatorTestTheory
{
    [Xunit.Theory]
    [Xunit.InlineData(1, 5)]
    [Xunit.InlineData(2, 2)]
    [Xunit.InlineData(2, 3)]
    public void AddTest(int a, int b)
    {
        //Arrange
        var expected = AddTestGetExpected(a, b);

        //Act
        var actual = Calculator.Add(a, b);

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Possible variations of AddTestGetExpected static method are:
    /// - Method body may have pattern matches within pattern matches.
    /// - Method body may have `switch` and/or `if` expressions with return statements returning expected baselines.
    /// - The method may be placed in another class and/or file.
    /// </summary>
    private static AddResult AddTestGetExpected(int a, int b) => (a, b) switch
    {
        (1, 5) => new AddResult(), // should be overwritten with correct expected baseline after
                                   // CalculatorTestTheoryStormPetrel.AddTestStormPetrel execution
        (2, 2) => new AddResult(),
        (2, 3) => new AddResult(),
        _ => throw new InvalidOperationException(),
    };
}
```
##### When
The developer executes `CalculatorTestTheoryStormPetrel.AddTestStormPetrel` test.
##### Then
`CalculatorTestTheory.AddTestGetExpected` code is populated with correct expected baseline values, i.e. its code becomes
```csharp
private static AddResult AddTestGetExpected(int a, int b) => (a, b) switch
{
    (1, 5) => new AddResult
    {
        Value = 6,
        ValueAsHexString = "0x6"
    }, //should be overwritten with correct expected baseline
    (2, 2) => new AddResult
    {
        Value = 4,
        ValueAsHexString = "0x4"
    },
    (2, 3) => new AddResult
    {
        Value = 5,
        ValueAsHexString = "0x5"
    },
    _ => throw new InvalidOperationException(),
};
```
##### So that
The developer should only review expected baseline changes (no manual modification) what typically saves development time.

### Use Case Variations<a name="use-case-variations"></a>
* Expected variable expression is static property assignment. See <a href="Test.Integration.XUnit/PropertyTest.cs">PropertyTest.cs</a> for more details.
* Expected expression is test method argument what comes from

    * Test case attribute (xUnit InlineData, NUnit TestCase, MSTest DataRow) argument value. See <a href="Test.Integration.XUnit/AttributesTest.cs">AttributesTest.cs</a> for more details.

    * Test data source attribute (xUnit MemberData or ClassData, NUnit TestCaseSource, MSTest DynamicData). See <a href="Test.Integration.XUnit/TestCaseSourceMemberDataTest.cs">TestCaseSourceMemberDataTest.cs</a>, <a href="Test.Integration.XUnit/TestCaseSourceClass/TestCaseSourceClassDataTest.cs">TestCaseSourceClassDataTest.cs</a>, <a href="Test.Integration.NUnit/TestCaseSourceTest.cs">NUnit TestCaseSourceTest.cs</a>, <a href="Test.Integration.MSTest/TestCaseSourceTest.cs">MSTest TestCaseSourceTest.cs</a> for more details.
    Known limitations:
        * The attributes should be configured against data source methods or properties, not fields.

## Getting Started<a name="getting-started"></a>
To utilize the StormPetrel tests, add the following NuGet Package references to your test project:
* Scand.StormPetrel.Generator.
* Object to C# code dumper. Represents `actual` test instance as C# code. See `DumperExpression` in [Configuration](#configuration) for more details.
    * **Option A**. [VarDump](https://www.nuget.org/packages/VarDump). Must be referenced in the case of StormPetrel.Generator default configuration (no `appsettings.StormPetrel.json` file in the test project). May be additionally configured.
    * **Option B**. [ObjectDumper.NET](https://github.com/thomasgalliker/ObjectDumper). May be referenced and configured.
    * **Option C**. Custom implementation of `IGeneratorDumper` interface. May be developed and configured.

## Configuration<a name="configuration"></a>
The StormPetrel Generator introduces several interfaces and classes to the Scand.StormPetrel.Generator.TargetProject namespace of the test project. These can be utilized alongside an optional JSON file to customize the rewriting of expected baselines. Key interfaces and classes include:
* `IGenerator`, `Generator`;
* `IGeneratorBackuper`, `GeneratorBackuper`;
* `IGeneratorDumper`, `GeneratorDumper`;
* `IGeneratorRewriter`, `GeneratorRewriter`.

Optionally `appsettings.StormPetrel.json` file (its `Build Action` should be `C# analyzer additional file`) can be added to a test project to configure Storm Petrel functionality.
The file changes are applied `on the fly` and can have the following settings:
```jsonc
{
  "TargetProjectGeneratorExpression": "...", // [optional] string, configures the default `Generator`. An expression for the `IGenerator` instance.
  "GeneratorConfig":             // [optional] object to configure `Generator` behavior.
  {
    "BackuperExpression": "...", // [optional] string, instantiates `GeneratorBackuper` by default. An expression for the `IGeneratorBackuper` instance. Set to 'null' to skip creating backup files.
    "DumperExpression": "...",   // [optional] string, instantiates `GeneratorDumper` by default. An expression for the `IGeneratorDumper` instance. `GeneratorDumper` references [VarDump](https://www.nuget.org/packages/VarDump) stuff. Use
                                 // - "new Scand.StormPetrel.Generator.TargetProject.GeneratorDumper(CustomCSharpDumperProvider.GetCSharpDumper())" to have `VarDump` with custom options. Need to implement `CustomCSharpDumperProvider.GetCSharpDumper()` method in this case.
                                 // - "new Scand.StormPetrel.Generator.TargetProject.GeneratorObjectDumper()" expression for `GeneratorObjectDumper` instance which references [ObjectDumper.NET](https://github.com/thomasgalliker/ObjectDumper) stuff.
                                 // - "new Scand.StormPetrel.Generator.TargetProject.GeneratorObjectDumper(CustomOptionsProvider.GetDumpOptions())" to have `ObjectDumper.NET` with custom options. Need to implement `CustomOptionsProvider.GetDumpOptions()` method in this case.
                                 // - "new CustomClassImplementingIGeneratorDumper()" or similar expression to have totally custom implementation of dumping of an instance to C# code.
    "RewriterExpression": "..."  // [optional] string, instantiates `GeneratorRewriter` by default. An expression for the `IGeneratorRewriter` instance.
  },
  "IsDisabled": false,           // [optional] boolean, false is by default. Indicates whether the generator should create 'StormPetrel' classes.
                                 // Even if set to 'false', the generator still adds classes like 'IGeneratorDumper', 'GeneratorDumper' to avoid test project compilation failures
                                 // in the case when custom classes uses them.
  "IgnoreFilePathRegex": "...",  // [optional] string, empty by default. Regular Expression to exclude certain paths from 'StormPetrel' class generation.
  "Serilog": "...",              // [optional] Logging configuration using Serilog (https://github.com/serilog/serilog-settings-configuration?tab=readme-ov-file#serilogsettingsconfiguration--).
                                 // Defaults to logging warnings to the test project's Logs folder. Set to 'null' to disable logging.
                                 // Use the '{StormPetrelRootPath}' token to indicate the target test project root path.
  "TestVariablePairConfigs": [   // [optional] array of objects. Configures naming pairs for actual/expected variables to generate correct expected baselines.
    {
      "ActualVarNameTokenRegex": "[Aa]{1}ctual",     // Default configuration object. Assumes variable pair names like (expected, actual), (myExpected, myActual), (expectedOne, actualOne), (ExpectedTwo, ActualTwo), etc.
      "ExpectedVarNameTokenRegex": "[Ee]{1}xpected", // Corresponds to the `ActualVarNameTokenRegex` for pairing.
    }
  ]
}
```

## Supported Software<a name="supported-software"></a>

### Test Frameworks<a name="supported-test-frameworks"></a>
* <a href="https://xunit.net/">xUnit</a>
* <a href="https://nunit.org/">NUnit</a>
* <a href="https://github.com/microsoft/testfx/">MSTest</a>

### .NET Versions<a name="supported-net-versions"></a>
* .NET Standard 2.0+
* .NET 8.0+
* .NET Framework 4.6.2+

## References<a name="references"></a>

At SCAND, we specialize in building advanced .NET solutions to help businesses develop new or modernize their legacy applications. If you need help getting started with Storm Petrel or support with implementation, we're ready to assist. Whether you're refactoring or rewriting, our team can help solve any challenges you might face. Visit our [page](https://scand.com/contact-us/) to learn more, or reach out for hands-on guidance.