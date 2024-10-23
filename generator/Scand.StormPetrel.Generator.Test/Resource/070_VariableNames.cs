using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resources
{
    public class VariableNames
    {
        [Fact]
        public void TestAssignmentNotDeclaration()
        {
            int expected;
            expected = 1;
            int actual;
            actual = 2;
            actual.Should().Be(expected);
        }
        [Fact]
        public void TestBothAssignmentAndDeclaration()
        {
            int expected = -1;
            expected = 1;
            int actual = -1;
            actual = 2;
            actual.Should().Be(expected);
        }
        [Fact]
        public void TestWhitespacesInBothAssignmentAndDeclarationStormPetrel()
        {
            int 	expected = -1;
            	expected  =	1;
            int actual = -1;
            actual=2;
            actual.Should().Be(expected);
        }
        [Fact]
        public void DefaultRegexCaseSensitivityShouldDetectThisCase()
        {
            int expectedVar = -1;
            int actualVar = -1;
            actualVar = 2;
            actualVar.Should().Be(expectedVar);
        }
        [Fact]
        public void DefaultRegexCaseSensitivityShouldDetectThisCase2()
        {
            int varExpected = -1;
            int varActual = -1;
            varActual = 2;
            varActual.Should().Be(varExpected);
        }
        [Fact]
        public void DefaultRegexCaseSensitivityShouldNotDetectThisCaseButCustomShould()
        {
            int varEXpected = -1;
            int varACtual = -1;
            varACtual = 2;
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
        public void MultipleVariablePairsShouldResultMultipleBaselineReplacements()
        {
            int varExpected = -1;
            int varEXpected = -1;
            int varACtual = -1;
            int varActual = -1;
            varACtual = 2;
            varActual = 2;
            varActual.Should().Be(varEXpected); //intentionally compare with varEXpected, not varExpected
            varACtual.Should().Be(varExpected);
        }
    }
}
