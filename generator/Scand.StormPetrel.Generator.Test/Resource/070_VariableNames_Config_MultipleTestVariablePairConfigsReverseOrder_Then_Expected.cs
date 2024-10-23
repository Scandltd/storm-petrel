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
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "TestAssignmentNotDeclaration",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
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
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Assignment
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Fact]
        public void TestBothAssignmentAndDeclarationStormPetrel()
        {
            int expected = -1;
            expected = 1;
            int actual = -1;
            actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "TestBothAssignmentAndDeclaration",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
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
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Assignment
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Fact]
        public void TestWhitespacesInBothAssignmentAndDeclarationStormPetrelStormPetrel()
        {
            int expected = -1;
            expected = 1;
            int actual = -1;
            actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "TestWhitespacesInBothAssignmentAndDeclarationStormPetrel",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
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
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Assignment
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Fact]
        public void DefaultRegexCaseSensitivityShouldDetectThisCaseStormPetrel()
        {
            int expectedVar = -1;
            int actualVar = -1;
            actualVar = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "DefaultRegexCaseSensitivityShouldDetectThisCase",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
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
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actualVar.Should().Be(expectedVar);
        }

        [Fact]
        public void DefaultRegexCaseSensitivityShouldDetectThisCase2StormPetrel()
        {
            int varExpected = -1;
            int varActual = -1;
            varActual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "DefaultRegexCaseSensitivityShouldDetectThisCase2",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
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
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            varActual.Should().Be(varExpected);
        }

        [Fact]
        public void DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShouldStormPetrel()
        {
            int varEXpected = -1;
            int varACtual = -1;
            varACtual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShould",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
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
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
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
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = varACtual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varACtual"
                },
                Expected = varEXpected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varEXpected"
                },
                IsLastVariablePair = false,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            varActual = 2;
            var stormPetrelContext1 = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "VariableNames",
                MethodName = "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = varActual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varActual"
                },
                Expected = varExpected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "VariableNames",
                    "MultipleVariablePairsShouldResultMultipleBaselineReplacements",
                    "varExpected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            varActual.Should().Be(varEXpected); //intentionally compare with varEXpected, not varExpected
            varACtual.Should().Be(varExpected);
        }
    }
}