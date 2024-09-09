using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resources
{
    public class AllPossibleTestAttributesStormPetrel
    {
        [NonTestAttribute]
        [Fact]
        public void TestXUnitFactStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestXUnitFact",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestXUnitFact",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestXUnitFact",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [fAct]
        public void TestXUnitFactCaseSensitivity()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }

        [Theory]
        public void TestXUnitTheoryStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestXUnitTheory",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestXUnitTheory",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestXUnitTheory",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Test]
        public void TestNUnitTestStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTest",
                MethodTestAttributeNames = new[]
                {
                    "Test"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTest",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTest",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [TestCase]
        public void TestNUnitTestCaseStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTestCase",
                MethodTestAttributeNames = new[]
                {
                    "TestCase"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTestCase",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTestCase",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [TestCaseSource]
        public void TestNUnitTestCaseSourceStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTestCaseSource",
                MethodTestAttributeNames = new[]
                {
                    "TestCaseSource"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTestCaseSource",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTestCaseSource",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Theory]
        public void TestNUnitTheoryStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTheory",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTheory",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestNUnitTheory",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TestMSTestTestMethodStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestMSTestTestMethod",
                MethodTestAttributeNames = new[]
                {
                    "TestMethod"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestMSTestTestMethod",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestMSTestTestMethod",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [DataTestMethod]
        public void TestMSTestDataTestMethodStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestMSTestDataTestMethod",
                MethodTestAttributeNames = new[]
                {
                    "DataTestMethod"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestMSTestDataTestMethod",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resources",
                    "AllPossibleTestAttributes",
                    "TestMSTestDataTestMethod",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [CustomATestNotMatchingRegEx]
        public void TestCustomATestNotMatchingRegExMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }

        [CustomATest]
        public void TestCustomATestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }

        [CustomBTest]
        public void TestCustomBTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }

        [cUstomBTest]
        public void TestCustomBCaseSensitivityTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }

        [CustomATest]
        [CustomBTest]
        public void TestCustomAandBTestMethod()
        {
            int expected = 1;
            int actual = 2;
            actual.Should().Be(expected);
        }
    }
}