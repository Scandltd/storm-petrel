using Test.Integration.XUnitV3AOT.LibraryBeingTested;

namespace Test.Integration.XUnitV3AOT;

/// <summary>
/// For manual execution in Debug configuration only, ensure the following:
/// - The class is wrapped with `#if DEBUG`.
/// - `appsettings.StormPetrel.Debug.json` sets `"IsDisabled": false` and `appsettings.StormPetrel.Release.json` sets `"IsDisabled": true`.
/// - The `.csproj` file contains the corresponding configuration of `appsettings.StormPetrel.Debug.json` and `appsettings.StormPetrel.Release.json`.
/// NOTE: The `StormPetrelExecutor` class name suffix is not required. We use it here solely to help locate these tests in IDE test runners by searching for the `StormPetrel` keyword alongside regular `StormPetrel` tests.
/// </summary>
public class CalculatorTestStormPetrelExecutor
{
    [Fact]
    public void AddTest() => new CalculatorTestStormPetrel().AddTestStormPetrel();
    [Theory]
    [MemberData(nameof(CalculatorTest.TheoryDataWithComplexExpectedObject), MemberType = typeof(CalculatorTest))]
    public static void AddTestWhenTheoryDataWithComplexExpectedObject(int a, int b, AddResultNoReflectionDump expected) =>
        CalculatorTestStormPetrel.AddTestWhenTheoryDataWithComplexExpectedObjectStormPetrel(a, b, expected);
    [Theory]
    [MemberData(nameof(CalculatorTest.TheoryDataRowArray), MemberType = typeof(CalculatorTest))]
    public static void AddTestWhenTheoryDataRowIsArray(int a, int b, AddResult expected) =>
        CalculatorTestStormPetrel.AddTestWhenTheoryDataRowIsArrayStormPetrel(a, b, expected);
    [Theory]
    [MemberData(nameof(AddTestWhenInlineDataAttributeDataSource))]
    public static void AddTestWhenInlineDataAttribute(int stormPetrelUseCaseIndex, int a, int b, int expectedValue, string expectedHexString) =>
        CalculatorTestStormPetrel.AddTestWhenInlineDataAttributeStormPetrel(stormPetrelUseCaseIndex, a, b, expectedValue, expectedHexString);
    public static IEnumerable<TheoryDataRow<int, int, int, int, string>> AddTestWhenInlineDataAttributeDataSource()
        => Scand.StormPetrel.Generator.Utils.CommonUtils.EnumerateAttributeData(
            typeof(CalculatorTest),
            nameof(CalculatorTest.AddTestWhenInlineDataAttribute),
            static (InlineDataAttribute x) => x.Data,
            static () => [0, -1, -2, ""] // default values from original test method
           )
            .Select(x => new TheoryDataRow<int, int, int, int, string>(x.Index, (int)x.Data[0]!, (int)x.Data[1]!, (int)x.Data[2]!, (string)x.Data[3]!));
    [Theory]
    [ClassData(typeof(CalculatorTestTheoryDataSource))]
    public static void AddTestWhenClassData(int a, int b, AddResult expected) =>
        CalculatorTestStormPetrel.AddTestWhenClassDataStormPetrel(a, b, expected);

}
