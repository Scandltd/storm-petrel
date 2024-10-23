using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Generator.Test.Resource
{
    internal class MemberDataTestsStormPetrel
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void MemberDataPropertyStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(MemberDataTests), nameof(Data)))
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
                ClassName = "MemberDataTests",
                MethodName = "MemberDataProperty",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataProperty",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataProperty"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(MemberDataTests), nameof(Data))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeNameSpace.SomeType))]
        public void MemberDataWithMemberTypeStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeNameSpace.SomeType), nameof(Data)))
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
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithMemberType",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithMemberType",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithMemberType"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeNameSpace.SomeType), nameof(Data))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeType))]
        public void MemberDataWithMemberTypeNoWhitespacesStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeType), nameof(Data)))
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
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithMemberTypeNoWhitespaces",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithMemberTypeNoWhitespaces",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithMemberTypeNoWhitespaces"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeType), nameof(Data))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeType))]
        public void MemberDataWithMemberTypeWithSpecialWhitespacesStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeType), nameof(Data)))
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
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithMemberTypeWithSpecialWhitespaces",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithMemberTypeWithSpecialWhitespaces",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithMemberTypeWithSpecialWhitespaces"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeType), nameof(Data))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), 1, "string param", new object[] { 1, 2, 3 })]
        public void MemberDataWithParametersStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(MemberDataTests), nameof(Data), 1, "string param", new object[] { 1, 2, 3 }))
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
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithParameters",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithParameters",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithParameters"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(MemberDataTests), nameof(Data))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(parameters: 3, memberName: nameof(DataMethodWithArgsForNamedParameters))]
        public void MemberDataWithNamedParametersStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelTestCaseSourceRowIndex = -1;
            foreach (var stormPetrelRow in Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(MemberDataTests), nameof(DataMethodWithArgsForNamedParameters), 3))
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
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithNamedParameters",
                MethodTestAttributeNames = new[]
                {
                    "Theory"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithNamedParameters",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithNamedParameters"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.EnumerableResultRewriter,
                TestCaseSourceInfo = new Scand.StormPetrel.Generator.TargetProject.TestCaseSourceInfo()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(MemberDataTests), nameof(DataMethodWithArgsForNamedParameters))
                }
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> Data => new object[][]
        {
            new object[]
            {
                1,
                2,
                3
            },
            new object[]
            {
                -2,
                2,
                0
            },
            new object[]
            {
                int.MinValue,
                -1,
                int.MaxValue
            },
            new object[]
            {
                -4,
                -6,
                -10
            },
        };
    }
}