using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resources
{
    public class VariableNamesStormPetrel
    {
        [Fact]
        public void TestAssignmentNotDeclarationStormPetrel()
        {
            int expected;
            expected = 1;
            int actual;
            actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "TestAssignmentNotDeclaration",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "TestAssignmentNotDeclaration",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "TestAssignmentNotDeclaration",
                    "expected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableAssignment
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Fact]
        public void TestBothAssignmentAndDeclarationStormPetrel()
        {
            int expected = -1;
            expected = 1;
            int actual = -1;
            actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "TestBothAssignmentAndDeclaration",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "TestBothAssignmentAndDeclaration",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "TestBothAssignmentAndDeclaration",
                    "expected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableAssignment
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Fact]
        public void TestWhitespacesInBothAssignmentAndDeclarationStormPetrelStormPetrel()
        {
            int expected = -1;
            expected = 1;
            int actual = -1;
            actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "TestWhitespacesInBothAssignmentAndDeclarationStormPetrel",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "TestWhitespacesInBothAssignmentAndDeclarationStormPetrel",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "TestWhitespacesInBothAssignmentAndDeclarationStormPetrel",
                    "expected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableAssignment
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Fact]
        public void DefaultRegexCaseSensitivityShouldDetectThisCaseStormPetrel()
        {
            int expectedVar = -1;
            int actualVar = -1;
            actualVar = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "DefaultRegexCaseSensitivityShouldDetectThisCase",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actualVar,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "DefaultRegexCaseSensitivityShouldDetectThisCase",
                    "actualVar"
                },
                Expected = expectedVar,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "DefaultRegexCaseSensitivityShouldDetectThisCase",
                    "expectedVar"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actualVar.Should().Be(expectedVar);
        }

        [Fact]
        public void DefaultRegexCaseSensitivityShouldDetectThisCase2StormPetrel()
        {
            int varExpected = -1;
            int varActual = -1;
            varActual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "DefaultRegexCaseSensitivityShouldDetectThisCase2",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = varActual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "DefaultRegexCaseSensitivityShouldDetectThisCase2",
                    "varActual"
                },
                Expected = varExpected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "DefaultRegexCaseSensitivityShouldDetectThisCase2",
                    "varExpected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            varActual.Should().Be(varExpected);
        }

        [Fact]
        public void DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShouldStormPetrel()
        {
            int varEXpected = -1;
            int varACtual = -1;
            varACtual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShould",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = varACtual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShould",
                    "varACtual"
                },
                Expected = varEXpected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShould",
                    "varEXpected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            varACtual.Should().Be(varEXpected);
        }

        [Fact]
        public void WhenNoExpectedVariableThenNoChanges()
        {
            int expctd = 1; //var name does not match regex
            int actual = 2;
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenNoActualVariableThenNoChanges()
        {
            int expected = 1;
            int actl = 2; //var name does not match regex
            actl.Should().Be(expected);
        }

        [Fact]
        public void MultipleVariablePairsShouldResultMultipleBaselineReplacementsStormPetrel()
        {
            int varExpected = -1;
            int varEXpected = -1;
            int varACtual = -1;
            int varActual = -1;
            varACtual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 2,
                Parameters = new Scand.StormPetrel.Generator.Abstraction.ParameterInfo[]
                {
                }
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = varACtual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varACtual"
                },
                Expected = varExpected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varExpected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            varActual = 2;
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = varActual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varActual"
                },
                Expected = varEXpected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varEXpected"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            varActual.Should().Be(varEXpected); //intentionally compare with varEXpected, not varExpected
            varACtual.Should().Be(varExpected);
        }
    }
}