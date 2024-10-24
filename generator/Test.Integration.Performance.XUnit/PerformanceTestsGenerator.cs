using System.Globalization;

namespace Test.Integration.Performance.XUnit;

public sealed class PerformanceTestsGenerator
{
    // Uncomment and run to generate test files
    //[Fact]
    public static void GeneratePerformanceTests()
    {
        GenerateExpectedInlineInstance();
        GenerateExpectedInMethod();
    }

    private static string GetAutoGeneratedPreamble()
        => $"//Auto-generated by {nameof(PerformanceTestsGenerator)}.{nameof(GeneratePerformanceTests)}. Do not change it manually.";
    private static void GenerateExpectedInlineInstance(int classesCount = 10, int methodsInClassCount = 10)
    {
        string classTemplate =
@$"{GetAutoGeneratedPreamble()}
using FluentAssertions;
using System.Globalization;
namespace Test.Integration.Performance.XUnit.ExpectedInlineInstance
{{
    [Trait(""TestCategory"", ""Performance"")]
    public sealed class ExpectedInlineInstanceTestClassIndex
    {{
MethodsPlaceholder
    }}
}}
";
        string methodTemplate =
@$"
        [Fact]
        public static void TestMethodIndex()
        {{
            //Arrange
            var expected = ExpectedPlaceholder;
            //Act
            var actual = ActualPlaceholder;
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }}";

        for (int i = 1; i <= classesCount; i++)
        {
            var methods = Enumerable
                            .Range(1, methodsInClassCount)
                            .Select(j => (Index: j, Level: (j % ((i % 2) + 1)) + 1))
                            .Select(x => methodTemplate
                                            .Replace("MethodIndex", x.Index.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                            .Replace("ExpectedPlaceholder", GetTestDtoAsString(false, x.Level), StringComparison.OrdinalIgnoreCase)
                                            .Replace("ActualPlaceholder", $"PerformanceTestsGenerator.GetTestDto(true, {x.Level})", StringComparison.OrdinalIgnoreCase));
            var methodsString = string.Join("", methods);

            var classText = classTemplate
                                .Replace("ClassIndex", i.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                .Replace("MethodsPlaceholder", methodsString, StringComparison.OrdinalIgnoreCase);
            var filePath = $"../../../ExpectedInlineInstance/ExpectedInlineInstanceTest{i:D2}.cs";
            File.WriteAllText(filePath, classText);
        }
    }

    private static void GenerateExpectedInMethod(int classesCount = 10, int methodsInClassCount = 10)
    {
        string classTemplate =
@$"{GetAutoGeneratedPreamble()}
using FluentAssertions;
namespace Test.Integration.Performance.XUnit.ExpectedInMethod
{{
    [Trait(""TestCategory"", ""Performance"")]
    public sealed class ExpectedInMethodTestClassIndex
    {{
MethodsPlaceholder
    }}
}}
";

        string classExpectedTemplate =
@$"{GetAutoGeneratedPreamble()}
using System.Globalization;
namespace Test.Integration.Performance.XUnit.ExpectedInMethod
{{
    internal static class ExpectedInMethodTestClassIndexData
    {{
MethodsPlaceholder
    }}
}}
";
        string methodTemplate =
@$"
        [Fact]
        public static void TestMethodIndex()
        {{
            //Arrange
            var expected = ExpectedInMethodTestClassIndexData.GetExpectedMethodIndex();
            //Act
            var actual = ActualPlaceholder;
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }}";

        string methodExpectedArrowTemplate =
@$"
        public static TestDto GetExpectedMethodIndex() => ExpectedPlaceholder;";

        string methodExpectedReturnTemplate =
@$"
        public static TestDto GetExpectedMethodIndex()
        {{
            return ExpectedPlaceholder;
        }}";

        for (int i = 1; i <= classesCount; i++)
        {
            var methods = Enumerable
                            .Range(1, methodsInClassCount)
                            .Select(j => (Index: j, Level: (j % ((i % 2) + 1)) + 1))
                            .Select(x => methodTemplate
                                            .Replace("ClassIndex", i.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                            .Replace("MethodIndex", x.Index.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                            .Replace("ActualPlaceholder", $"PerformanceTestsGenerator.GetTestDto(true, {x.Level})", StringComparison.OrdinalIgnoreCase));
            var methodsString = string.Join("", methods);

            var classText = classTemplate
                                .Replace("ClassIndex", i.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                .Replace("MethodsPlaceholder", methodsString, StringComparison.OrdinalIgnoreCase);
            var filePath = $"../../../ExpectedInMethod/ExpectedInMethodTest{i:D2}.cs";
            File.WriteAllText(filePath, classText);

            var methodsExpected = Enumerable
                                    .Range(1, methodsInClassCount)
                                    .Select(j => (Index: j, Level: (j % ((i % 2) + 1)) + 1))
                                    .Select(x => (x.Index % 2 == 1 ? methodExpectedArrowTemplate : methodExpectedReturnTemplate)
                                                    .Replace("MethodIndex", x.Index.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                                    .Replace("ExpectedPlaceholder", GetTestDtoAsString(false, x.Level), StringComparison.OrdinalIgnoreCase));
            var methodsExpectedString = string.Join("", methodsExpected);

            var classExpectedText = classExpectedTemplate
                                        .Replace("ClassIndex", i.ToString("D2", CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase)
                                        .Replace("MethodsPlaceholder", methodsExpectedString, StringComparison.OrdinalIgnoreCase);
            var fileExpectedPath = $"../../../ExpectedInMethod/ExpectedInMethodTest{i:D2}Data.cs";
            File.WriteAllText(fileExpectedPath, classExpectedText);
        }
    }

    private static string GetTestDtoAsString(bool isActual, int level)
    {
        const string indent = "            ";
        var dto = GetTestDto(isActual, level);
        var dtoDump = Dump(dto);
        dtoDump = dtoDump.Replace("\r\n", "\n", StringComparison.OrdinalIgnoreCase);
        var dumpIndented = dtoDump
                            .Split('\n')
                            .Select((x, i) => i == 0 ? x : $"{indent}{x}");
        return string.Join("\n", dumpIndented);
    }

    internal static TestDto GetTestDto(bool isActual, int level)
    {
        const int arrayPropertyLength = 2;
        bool stop = level <= 0;
        int nextLevel = level - 1;
        var dto = new TestDto
        {
            Bool00 = isActual,
            Bool10 = isActual,
            Bool20 = isActual,
            Bool30 = isActual,
            Bool40 = isActual,
            Bool50 = isActual,
            Bool60 = isActual,
            Bool70 = isActual,
            Bool80 = isActual,
            Bool90 = isActual,
            DateTime00 = new DateTime(2010, isActual ? 1 : 2, 1),
            DateTime10 = new DateTime(2011, isActual ? 1 : 2, 1),
            DateTime20 = new DateTime(2012, isActual ? 1 : 2, 1),
            DateTime30 = new DateTime(2013, isActual ? 1 : 2, 1),
            DateTime40 = new DateTime(2014, isActual ? 1 : 2, 1),
            Int00 = (isActual ? 1000 : 2000),
            Int10 = (isActual ? 1000 : 2000),
            Int20 = (isActual ? 1000 : 2000),
            Int30 = (isActual ? 1000 : 2000),
            Int40 = (isActual ? 1000 : 2000),
            Int50 = (isActual ? 1000 : 2000),
            Int60 = (isActual ? 1000 : 2000),
            Int70 = (isActual ? 1000 : 2000),
            Int80 = (isActual ? 1000 : 2000),
            Int90 = (isActual ? 1000 : 2000),
            String00 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String10 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String20 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String30 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String40 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String50 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String60 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String70 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String80 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            String90 = "My Pretty Long Test String " + (isActual ? 1000 : 2000),
            TestDto00 = stop ? null : GetTestDto(isActual, nextLevel),
            TestDto10 = stop ? null : GetTestDto(isActual, nextLevel),
            TestDto20 = stop ? null : GetTestDto(isActual, nextLevel),
            TestDto30 = stop ? null : GetTestDto(isActual, nextLevel),
            TestDto40 = stop ? null : GetTestDto(isActual, nextLevel),
        };
        if (stop)
        {
            return dto;
        }
        dto.TestDtoArray00 = new TestDto[arrayPropertyLength];
        dto.TestDtoArray10 = new TestDto[arrayPropertyLength];
        dto.TestDtoArray20 = new TestDto[arrayPropertyLength];
        dto.TestDtoArray30 = new TestDto[arrayPropertyLength];
        dto.TestDtoArray40 = new TestDto[arrayPropertyLength];
        for (int i = 0; i < arrayPropertyLength; i++)
        {
            dto.TestDtoArray00[i] = GetTestDto(isActual, nextLevel);
            dto.TestDtoArray10[i] = GetTestDto(isActual, nextLevel);
            dto.TestDtoArray20[i] = GetTestDto(isActual, nextLevel);
            dto.TestDtoArray30[i] = GetTestDto(isActual, nextLevel);
            dto.TestDtoArray40[i] = GetTestDto(isActual, nextLevel);
        }
        return dto;
    }

    private static string Dump(object obj)
    {
        var dumper = new VarDump.CSharpDumper();
        string dump = dumper.Dump(obj);
        var i = dump.IndexOf('=', StringComparison.OrdinalIgnoreCase);
        if (i < 0)
        {
            throw new InvalidOperationException("Unexpected index of '='");
        }
        dump = dump[(i + 1)..]
                .TrimStart(' ')
                .TrimEnd('\n', '\r', ';');
        return dump;
    }
}