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
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "Test",
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
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(new SomeClass());
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
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
                    if (x == (int)stormPetrelRow[0])
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (y == (int)stormPetrelRow[1])
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(SomeClass))]
        public void MultipleVarsTestStormPetrel(int x, int y, int expected, string expectedString)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "MultipleVarsTest",
                VariablePairCurrentIndex = 0,
                VariablePairsCount = 2,
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
                    },
                    new Scand.StormPetrel.Generator.Abstraction.ParameterInfo()
                    {
                        Name = "expectedString",
                        Value = expectedString,
                        Attributes = new Scand.StormPetrel.Generator.Abstraction.AttributeInfo[]
                        {
                        }
                    }
                }
            };
            var stormPetrelTestCaseSourceRowIndex = -1;
            var stormPetrelIsTestCaseSourceRowExist = false;
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(new SomeClass());
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
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
                    if (x == (int)stormPetrelRow[0])
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (y == (int)stormPetrelRow[1])
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            var actualString = actual.ToString();
            stormPetrelSharedContext.VariablePairCurrentIndex++;
            var stormPetrelTestCaseSourceRowIndex1 = -1;
            var stormPetrelIsTestCaseSourceRowExist1 = false;
            var stormPetrelRows1 = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(new SomeClass());
            foreach (var stormPetrelRow in stormPetrelRows1)
            {
                stormPetrelTestCaseSourceRowIndex1++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
                {
                    stormPetrelIsTestCaseSourceRowExist1 = true;
                    break;
                }
            }

            if (!stormPetrelIsTestCaseSourceRowExist1)
            {
                var stormPetrelNoEqualArgNames1 = new System.Collections.Generic.List<string>()
                {
                    "x",
                    "y"
                };
                foreach (var stormPetrelRow in stormPetrelRows1)
                {
                    if (x == (int)stormPetrelRow[0])
                    {
                        stormPetrelNoEqualArgNames1.Remove("x");
                    }

                    if (y == (int)stormPetrelRow[1])
                    {
                        stormPetrelNoEqualArgNames1.Remove("y");
                    }
                }

                throw new System.InvalidOperationException("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of '" + string.Join("', '", stormPetrelNoEqualArgNames1) + "' argument(s).");
            }

            var stormPetrelContext1 = new Scand.StormPetrel.Generator.Abstraction.GenerationContext()
            {
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 3,
                    RowIndex = stormPetrelTestCaseSourceRowIndex1,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext1);
            Assert.Equal(expected, actual);
            Assert.Equal(expectedString, actualString);
        }

        [Theory]
        [ClassData(typeof(SomeClass))]
        public void TypeofWhitespacesTestStormPetrel(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y);
            var stormPetrelSharedContext = new Scand.StormPetrel.Generator.Abstraction.MethodContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassDataTests",
                MethodName = "TypeofWhitespacesTest",
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
            var stormPetrelRows = Scand.StormPetrel.Rewriter.DataSourceHelper.ConvertToStormPetrelRows(new SomeClass());
            foreach (var stormPetrelRow in stormPetrelRows)
            {
                stormPetrelTestCaseSourceRowIndex++;
                if (x == (int)stormPetrelRow[0] && y == (int)stormPetrelRow[1])
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
                    if (x == (int)stormPetrelRow[0])
                    {
                        stormPetrelNoEqualArgNames.Remove("x");
                    }

                    if (y == (int)stormPetrelRow[1])
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
                ExtraContext = new Scand.StormPetrel.Generator.Abstraction.ExtraContext.TestCaseSourceContext()
                {
                    ColumnIndex = 2,
                    RowIndex = stormPetrelTestCaseSourceRowIndex,
                    Path = Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(typeof(SomeClass))
                },
                MethodSharedContext = stormPetrelSharedContext
            };
            ((Scand.StormPetrel.Generator.Abstraction.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            Assert.Equal(expected, actual);
        }
    }
}