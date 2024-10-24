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
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestXUnitFact",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
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
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestXUnitTheory",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Test]
        public void TestNUnitTestStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [TestCase]
        public void TestNUnitTestCaseStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTestCase",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [TestCaseSource]
        public void TestNUnitTestCaseSourceStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTestCaseSource",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [Theory]
        public void TestNUnitTheoryStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestNUnitTheory",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TestMSTestTestMethodStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestMSTestTestMethod",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            actual.Should().Be(expected);
        }

        [DataTestMethod]
        public void TestMSTestDataTestMethodStormPetrel()
        {
            int expected = 1;
            int actual = 2;
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "AllPossibleTestAttributes",
                MethodName = "TestMSTestDataTestMethod",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1
            };
            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContext()
                {
                    Kind = Scand.StormPetrel.Generator.Abstraction.ExtraContext.InitializerContextKind.VariableDeclaration
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
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