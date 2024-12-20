using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using NSubstitute;
using Serilog;
using Scand.StormPetrel.Generator.TargetProject;
using System.Diagnostics;
using System.Reflection;

namespace Scand.StormPetrel.Generator.Test
{
    public partial class XUnitGenerationTest
    {
        [Theory]
        [InlineData("010_OriginalCodeText")]
        [InlineData("010_OriginalCodeText", "CustomGeneratorInstance")]
        [InlineData("020_NoTestClasses")]
        [InlineData("030_OriginalCodeTextWithSeveralTestMethodsAndNonTestMethod")]
        [InlineData("040_OriginalCodeTextWithSeveralTestClasses")]
        [InlineData("050_PartialTestClass")]
        [InlineData("060_AllPossibleTestAttributes")]
        [InlineData("070_VariableNames")]
        [InlineData("070_VariableNames", "CustomTestVariablePairConfigs")]
        [InlineData("070_VariableNames", "MultipleTestVariablePairConfigs")]
        [InlineData("070_VariableNames", "MultipleTestVariablePairConfigsReverseOrder")]
        [InlineData("080_MultipleExpectedVarAssignments")]
        [InlineData("090_Method_Call_In_Declaration")]
        [InlineData("100_ClassWithoutTestMethods")]
        [InlineData("110_PatternMatch")]
        [InlineData("120_Property")]
        [InlineData("120_PropertyFromAnotherClass")]
        [InlineData("130_ClassWithConstructor")]
        [InlineData("140_Attributes")]
        [InlineData("150_MultipleExpectedLastAssignmentIsMethod")]
        [InlineData("160_MemberData")]
        [InlineData("170_ClassData")]
        [InlineData("180_IgnoreInvocationExpression", "IgnoreInvocationExpression")]
        [InlineData("ExpectedInMethodTest01Data")] //performance test
        [InlineData("ExpectedInMethodTest01Data", "IsReplaceOriginalInvocationMethod")]
        public async Task WhenInputCodeThenInjectStormPetrelStuffTest(string inputReplacementCodeResourceName, string? configKey = null)
        {
            //Arrange
            var assembly = Assembly.GetAssembly(typeof(XUnitGenerationTest));
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
            var inputCode = await ReadResourceAsync(assembly, $"{inputReplacementCodeResourceName}.cs");
            string? expectedCode = null;
            string? expectedResourceFileName = null;
            if (inputReplacementCodeResourceName != "020_NoTestClasses")
            {
                var configKeyPostfix = string.IsNullOrEmpty(configKey) ? "" : $"Config_{configKey}_";
                expectedResourceFileName = $"{inputReplacementCodeResourceName}_{configKeyPostfix}Then_Expected.cs";
                expectedCode = await ReadResourceAsync(assembly, expectedResourceFileName);
            }
            var tempFilePath = @"C:\temp\temp.cs";

            //Act
            var stopwatch = Stopwatch.StartNew();
            var actualSyntaxNode = SourceGenerator.CreateNewSource(tempFilePath, CSharpSyntaxTree.ParseText(inputCode), GetConfig(configKey), Substitute.For<ILogger>(), CancellationToken.None);
            stopwatch.Stop();
            string? actual = actualSyntaxNode?.ToFullString();

            //Assert
            stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5), "Typical execution time is less than ~3 seconds for worst case on `11th Gen Intel(R) Core(TM) i7-1165G7 @ 2.80GHz`");
            actual = NormalizeLineEndings(actual);
            if (expectedResourceFileName != null)
            {
                //Keep commented in git. Uncomment to overwrite baselines while development only.
                //await File.WriteAllTextAsync(@$"..\..\..\Resource\{expectedResourceFileName}", actual);
            }
            actual.Should().Be(expectedCode);

            static MainConfig GetConfig(string? key) => key switch
            {
                null => new MainConfig(),
                "IsReplaceOriginalInvocationMethod" => new MainConfig(),
                "CustomGeneratorInstance" => new MainConfig()
                {
                    TargetProjectGeneratorExpression = "new CustomGeneratorInstance()",
                },
                "CustomTestVariablePairConfigs" => new MainConfig()
                {
                    TestVariablePairConfigs =
                    [
                        new TestVariablePairConfig()
                        {
                            ActualVarNameTokenRegex = "[Aa]{1}[Cc]{1}tual",
                            ExpectedVarNameTokenRegex = "[Ee]{1}[Xx]{1}pected",
                        },
                    ],
                },
                "MultipleTestVariablePairConfigs" => new MainConfig()
                {
                    TestVariablePairConfigs =
                    [
                        new TestVariablePairConfig()
                        {
                            ActualVarNameTokenRegex = "[Aa]{1}ctual",
                            ExpectedVarNameTokenRegex = "[Ee]{1}xpected",
                        },
                        new TestVariablePairConfig()
                        {
                            ActualVarNameTokenRegex = "[Aa]{1}Ctual",
                            ExpectedVarNameTokenRegex = "[Ee]{1}Xpected",
                        },
                    ],
                },
                "MultipleTestVariablePairConfigsReverseOrder" => new MainConfig()
                {
                    TestVariablePairConfigs =
                    [
                        new TestVariablePairConfig()
                        {
                            ActualVarNameTokenRegex = "[Aa]{1}Ctual",
                            ExpectedVarNameTokenRegex = "[Ee]{1}Xpected",
                        },
                        new TestVariablePairConfig()
                        {
                            ActualVarNameTokenRegex = "[Aa]{1}ctual",
                            ExpectedVarNameTokenRegex = "[Ee]{1}xpected",
                        },
                    ],
                },
                "IgnoreInvocationExpression" => new MainConfig()
                {
                    IgnoreInvocationExpressionRegex = "InvocationExpressionToBeIgnored",
                },
                _ => throw new InvalidOperationException(),
            };
        }

        static async Task<string> ReadResourceAsync(Assembly assembly, string resourceFileName)
        {
            using var stream = assembly.GetManifestResourceStream(typeof(XUnitGenerationTest), $"Resource.{resourceFileName}");
            ArgumentNullException.ThrowIfNull(stream, nameof(stream));
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync();
        }

        private static string? NormalizeLineEndings(string? s) => s?.Replace("\r\n", "\n", StringComparison.OrdinalIgnoreCase);
    }
}