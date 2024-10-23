using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resource
{
    internal class ClassDataTestsStormPetrel
    {
        [Theory]
        [ClassData(typeof(SomeClass))]
        public void TestStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in new SomeClass())
            {
                stormPetrelTestCaseSourceRowIndex++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
                {
                    break;
                }
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "Test",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "Test",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "Test"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SomeClass))]
        public void MultipleVarsTestStormPetrel(int x, int y, int expected, string expectedString)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in new SomeClass())
            {
                stormPetrelTestCaseSourceRowIndex++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
                {
                    break;
                }
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "MultipleVarsTest",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "MultipleVarsTest",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "MultipleVarsTest"
                },
                IsLastVariablePair = false,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            var actualString = actual.ToString();
            var stormPetrelTestCaseSourceRowIndex1 = -1;
            foreach (var stormPetrelRow in new SomeClass())
            {
                stormPetrelTestCaseSourceRowIndex1++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
                {
                    break;
                }
            }

            var stormPetrelContext1 = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "MultipleVarsTest",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actualString,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "MultipleVarsTest",
                    "actualString"
                },
                Expected = expectedString,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "MultipleVarsTest"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 3,
                    RowIndex = stormPetrelTestCaseSourceRowIndex1,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            Assert.Equal(expected, actual);
            Assert.Equal(expectedString, actualString);
        }

        [Theory]
        [ClassData(typeof(SomeClass))]
        public void TypeofWhitespacesTestStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in new SomeClass())
            {
                stormPetrelTestCaseSourceRowIndex++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
                {
                    break;
                }
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "TypeofWhitespacesTest",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "TypeofWhitespacesTest",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "ClassDataTests",
                    "TypeofWhitespacesTest"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }
    }
}