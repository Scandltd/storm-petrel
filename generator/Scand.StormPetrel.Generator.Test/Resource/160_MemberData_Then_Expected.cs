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
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataProperty",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(MemberDataTests), nameof(Data)));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int)) && (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int)\",\"default(int)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(MemberDataTests), nameof(Data))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeNameSpace.SomeType))]
        public void MemberDataWithMemberTypeStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithMemberType",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeNameSpace.SomeType), nameof(Data)));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int)) && (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int)\",\"default(int)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeNameSpace.SomeType), nameof(Data))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeType))]
        public void MemberDataWithMemberTypeNoWhitespacesStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithMemberTypeNoWhitespaces",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeType), nameof(Data)));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int)) && (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int)\",\"default(int)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeType), nameof(Data))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), MemberType = typeof(SomeType))]
        public void MemberDataWithMemberTypeWithSpecialWhitespacesStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithMemberTypeWithSpecialWhitespaces",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeType), nameof(Data)));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int)) && (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int)\",\"default(int)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeType), nameof(Data))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(Data), 1, "string param", new object[] { 1, 2, 3 })]
        public void MemberDataWithParametersStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithParameters",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(MemberDataTests), nameof(Data), 1, "string param", new object[] { 1, 2, 3 }));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int)) && (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int)\",\"default(int)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(MemberDataTests), nameof(Data))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(parameters: 3, memberName: nameof(DataMethodWithArgsForNamedParameters))]
        public void MemberDataWithNamedParametersStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithNamedParameters",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(MemberDataTests), nameof(DataMethodWithArgsForNamedParameters), 3));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int)) && (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int)stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (int)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(int))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int)\",\"default(int)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(MemberDataTests), nameof(DataMethodWithArgsForNamedParameters))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(DataWithEnumerable), MemberType = typeof(SomeType))]
        public void MemberDataWithArraysStormPetrel(int[] x, IEnumerable<int> y, int expected)
        {
            var actual = Calculator.Sum(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "MemberDataTests",
                MethodName = "MemberDataWithArrays",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 1,
                Parameters = new[]
                {
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "x",
                        Value = x,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "y",
                        Value = y,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expected",
                        Value = expected,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(typeof(SomeType), nameof(DataWithEnumerable)));
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if ((stormPetrelRow.Length > 0 && (x == (int[])stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int[])) && (stormPetrelRow.Length > 1 && (y == (IEnumerable<int>)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(IEnumerable<int>)))
                {
                    stormPetrelIsTestCaseSourceRowExist = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist)
            {
                var stormPetrelNoEqualArgNames = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows)
                {
                    if (stormPetrelRow.Length > 0 && (x == (int[])stormPetrelRow[0] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(x, stormPetrelRow[0]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(x, stormPetrelRow[0])) || stormPetrelRow.Length <= 0 && x == default(int[]))
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (stormPetrelRow.Length > 1 && (y == (IEnumerable<int>)stormPetrelRow[1] || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEqual(y, stormPetrelRow[1]) || Scand.StormPetrel.Rewriter.DataSourceHelper.AreEnumerablesOfEqualElements(y, stormPetrelRow[1])) || stormPetrelRow.Length <= 1 && y == default(IEnumerable<int>))
                    {
                        stormPetrelNoEqualArgNames.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames) + "' argument(s).");
            }

            var stormPetrelContext = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithArrays",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Scand.StormPetrel.Generator.Test.Resource",
                    "MemberDataTests",
                    "MemberDataWithArrays"
                },
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = new[]
                    {
                        "experimental-parameter-default-values:[\"default(int[])\",\"default(IEnumerable<int>)\",\"default(int)\"]"
                    }.Union(Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(typeof(SomeType), nameof(DataWithEnumerable))).ToArray()
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
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