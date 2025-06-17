using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using NSubstitute;
using Serilog;
using Scand.StormPetrel.Generator.TargetProject;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CodeAnalysis;

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
        [InlineData("100_ClassWithoutTestMethods", null, true)]
        [InlineData("110_PatternMatch")]
        [InlineData("110_PatternMatch", null, true)]
        [InlineData("120_Property")]
        [InlineData("120_PropertyFromAnotherClass")]
        [InlineData("130_ClassWithConstructor")]
        [InlineData("140_Attributes")]
        [InlineData("140_Attributes", "MultipleTestVariablePairConfigs")]
        [InlineData("140_Attributes", "MultipleTestVariablePairConfigsReverseOrder")]
        [InlineData("150_MultipleExpectedLastAssignmentIsMethod")]
        [InlineData("160_MemberData")]
        [InlineData("170_ClassData")]
        [InlineData("180_IgnoreInvocationExpression", "IgnoreInvocationExpression")]
        [InlineData("190_AssertionNoExpectedVar_ShouldBe")]
        [InlineData("200_AssertionNoExpectedVar_ExpectedExpressionKinds")]
        [InlineData("ExpectedInMethodTest01Data")] //performance test
        [InlineData("ExpectedInMethodTest01Data", null, true)]
        [InlineData("ExpectedInMethodTest01Data", "IsReplaceOriginalInvocationMethod")]
        [InlineData("ExpectedInMethodTest01Data", "IsReplaceOriginalInvocationMethod", true)]
        [InlineData("NoExpectedVarAssertTest")]
        [InlineData("NoExpectedVarAssertThatTest")]
        [InlineData("NoExpectedVarAssertMSTest")]
        [InlineData("NoExpectedVarExpressionKindsTest")]
        [InlineData("NoExpectedVarWithOperatorsTest")]
        [InlineData("NoExpectedVarShouldlyTest")]
        [InlineData("NoActualAndNoExpectedVarTest")]
        [InlineData("TestCaseSourceMemberDataTest.MemberDataInPartialFile")]
        [InlineData("TestCaseSourceMemberDataTest.MemberDataInPartialFile", null, true)]
        [InlineData("Utils")]
        [InlineData("Utils", null, true)]
        [InlineData("Utils.IgnoredMembersMiddleware")]
        [InlineData("Utils.OtherMethods")]
        [InlineData("Utils.OtherMethods", null, true)]
        [InlineData("210_SpecificActualVarName", "OnlyActualVarNameTokenRegex")]
        public async Task WhenInputCodeThenInjectStormPetrelStuffTest(string inputReplacementCodeResourceName, string? configKey = null, bool isStaticStuffUseCase = false)
        {
            //Arrange
            var assembly = Assembly.GetAssembly(typeof(XUnitGenerationTest));
            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
            var inputCode = await ReadResourceAsync(assembly, $"{inputReplacementCodeResourceName}.cs");
            ArgumentNullException.ThrowIfNull(inputCode);
            var postfixes = new List<string>();
            if (!string.IsNullOrEmpty(configKey))
            {
                postfixes.Add($"Config_{configKey}");
            }
            if (isStaticStuffUseCase)
            {
                postfixes.Add("StaticStuff");
            }
            var postfix = string.Join("_", postfixes);
            if (!string.IsNullOrEmpty(postfix))
            {
                postfix += "_";
            }
            var expectedResourceFileName = $"{inputReplacementCodeResourceName}_{postfix}Then_Expected.cs";
            var expectedCode = await ReadResourceAsync(assembly, expectedResourceFileName);
            var tempFilePath = @"C:\temp\temp.cs";

            //Act
            var stopwatch = Stopwatch.StartNew();
            SyntaxNode? actualSyntaxNode;
            if (isStaticStuffUseCase)
            {
                var (tempNode, _, _) = SourceGenerator.CreateNewSourceForStaticStuff(tempFilePath, CSharpSyntaxTree.ParseText(inputCode), GetConfig(configKey), Substitute.For<ILogger>(), CancellationToken.None);
                actualSyntaxNode = tempNode;
            }
            else
            {
                actualSyntaxNode = SourceGenerator.CreateNewSource(tempFilePath, CSharpSyntaxTree.ParseText(inputCode), GetConfig(configKey), Substitute.For<ILogger>(), CancellationToken.None);
            }
            stopwatch.Stop();
            string? actual = actualSyntaxNode?.ToFullString();

            //Assert
            stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(10), "Typical execution time is less than ~3 seconds for worst case on `11th Gen Intel(R) Core(TM) i7-1165G7 @ 2.80GHz`");
            actual = NormalizeLineEndings(actual);
            if (expectedCode != null)
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
                "OnlyActualVarNameTokenRegex" => new MainConfig()
                { 
                    TestVariablePairConfigs =
                    [
                        new TestVariablePairConfig()
                        { 
                            ActualVarNameTokenRegex = "[Ss]{1}pecificActualVarName",
                            ExpectedVarNameTokenRegex = null
                        }
                    ]
                },
                _ => throw new InvalidOperationException(),
            };
        }

        static async Task<string?> ReadResourceAsync(Assembly assembly, string resourceFileName)
        {
            using var stream = assembly.GetManifestResourceStream(typeof(XUnitGenerationTest), $"Resource.{resourceFileName}");
            if (stream == null)
            {
                return null;
            }
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync();
        }

        private static string? NormalizeLineEndings(string? s) => s?.Replace("\r\n", "\n", StringComparison.OrdinalIgnoreCase);
    }
}