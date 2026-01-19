using FluentAssertions;
using System.Reflection;

namespace Test.Integration.XUnit
{
    public partial class TestCaseSourceMemberDataTest
    {
#pragma warning disable xUnit1042 //Justification: Intentionally test MemberData
        [Theory]
        [MemberData(nameof(DataMethod))]
        public void WhenMemberDataMethodThenItIsUpdated(int x, int y, AddResult expected)
        {
            var actual = Calculator.Add(x, y);
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(DataMethodWithArgs), 3)]
        public void WhenMemberDataMethodWithArgsThenItIsUpdated(int x, int y, int expected)
        {
            //Assert Storm Petrel
            var lastUseCase = DataMethodWithArgs(int.MaxValue).Last();
            lastUseCase[2].Should().Be(50, "Last use case should not be modified by Strom Petrel according to MemberData arguments");

            //Arrange, Act
            var actual = Calculator.Add(x, y).Value;

            //Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(parameters: [1, "22"], memberName: nameof(DataMethodWithArgsForNamedParameters))]
        public void WhenMemberDataMethodWithArgNamesThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(DataProperty))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TestCaseSourceMemberData.Data), MemberType = typeof(TestCaseSourceMemberData))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(DataMultipleExpected))]
        public void WhenMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString = "\"[,'json-special-symbol-examples")
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
        }

        [Theory]
        [MemberData(nameof(DataYieldReturnAndMultipleExpected))]
        public void WhenYieldReturnAndMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
        }
#pragma warning restore xUnit1042

        [Theory]
        [MemberData(nameof(TheoryDataMultipleExpected))]
        public void WhenTheoryDataMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
        {
            var actual = Calculator.Add(x, y).Value;
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            actual.Should().Be(expected);
            actualHexString.Should().Be(expectedHexString);
        }

        [Fact]
        public void WhenTheoryDataWithClassWithoutProperEqualityOperator()
        {
            //Arrange
            var isStormPetrelTest = GetType().Name.EndsWith("StormPetrel", StringComparison.Ordinal);
            if (!isStormPetrelTest)
            {
                //Nothing to assert if not StormPetrel test class
                return;
            }
            var stormPetrelTestMethod = GetType()
                                            .GetMethod("WhenTheoryDataWithClassWithoutProperEqualityOperatorThrowingTheExceptionStormPetrel");
            ArgumentNullException.ThrowIfNull(stormPetrelTestMethod);
            //Act
            //Intentionally avoid "actual" name to not rewrite the baseline because we explicitely assert the message
            var resultEx = Assert.Throws<TargetInvocationException>(() => stormPetrelTestMethod.Invoke(null, [new AddResult(), new AddResult()]));
            //Assert
            resultEx.InnerException.Should().BeOfType<InvalidOperationException>();
            resultEx.InnerException.Message.Should().Be("Cannot detect appropriate test case source row to rewrite because the equality operator (==) does not return 'true' against all values of 'someArgWithoutProperEqualityOperator' argument(s).");
        }

        [Theory(Skip = "The test does not correspond normal build flow. However, we have to mark the method by Theory attribute to have StormPetrel method generated")]
        [MemberData(nameof(TheoryDataWithClassWithoutProperEqualityOperator))]
        public static void WhenTheoryDataWithClassWithoutProperEqualityOperatorThrowingTheException(AddResult someArgWithoutProperEqualityOperator, AddResult expected)
        {
            //Arrange
            //Act
            var actual = someArgWithoutProperEqualityOperator; //emulate an action
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// An example when use case input data (<see cref="AddResult"/> in our case) do not have equality (==) operator overload.
        /// Use <see cref="TestCaseSourceUseCase{T}"/> where <see cref="TestCaseSourceUseCase{T}.Name"/> is only used in the equality operator.
        /// </summary>
        /// <param name="useCase"></param>
        /// <param name="expected"></param>
        [Theory]
        [MemberData(nameof(TheoryDataWithTestCaseEqualityOperator))]
        public static void WhenTheoryDataWithTestCaseEqualityOperator(TestCaseSourceUseCase<AddResult> useCase, AddResult expected)
        {
            //Arrange
            //Act
            var actual = useCase?.Input1; //emulate an action
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(MemberDataInPartialFile))]
        public void WhenMemberDataInPartialFileThenExpectedIsUpdated(int x, int y, string expectedHexString)
        {
            //Arrange
            //Act
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            //Assert
            actualHexString.Should().Be(expectedHexString);
        }

        [Theory]
        [MemberData(nameof(MemberDataPropertyInPartialFile))]
        public void WhenMemberDataPropertyInPartialFileThenExpectedIsUpdated(int x, int y, string expectedHexString)
        {
            //Arrange
            //Act
            var actualHexString = Calculator.Add(x, y).ValueAsHexString;
            //Assert
            actualHexString.Should().Be(expectedHexString);
        }

        [Theory]
        [MemberData(nameof(TheoryDataCustomEqualArg))]
        public void WhenCustomEqualArg(CustomEqualValue x, int expected)
        {
            //Arrange
            //Act
            var actual = Calculator.Add(x?.Value ?? 0, x?.Value ?? 0).Value;
            //Assert
            actual.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(TheoryDataEnumerableArgs))]
        public void WhenEnumerableArgs(int[] x, IEnumerable<CustomEqualValue> y, string expectedHexString)
        {
            //Arrange
            //Act
            var actualHexString = Calculator.Add((x ?? []).Sum(), (y ?? []).Sum(a => a.Value)).ValueAsHexString;
            //Assert
            actualHexString.Should().Be(expectedHexString);
        }

        [Theory]
        [MemberData(nameof(TheoryDataMultiDimensionalEnumerableArgs))]
        public void WhenMultiDimensionalEnumerableArgs(int[][] x, IEnumerable<IEnumerable<CustomEqualValue>> y, int expected)
        {
            //Arrange
            //Act
            var actual = Calculator.Add((x ?? []).SelectMany(a => a).Sum(), (y ?? []).SelectMany(a => a).Sum(a => a.Value)).Value;
            //Assert
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> DataMethod() =>
        [
            [1, 2, new AddResult()],
            [-2, 2, new AddResult()],
            [int.MinValue, -1, new AddResult()],
            [-4, -6, new AddResult()],
        ];

        public static IEnumerable<object[]> DataMethodWithArgs(int count) =>
        new object[][]
        {
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        }.Take(count);

        #region Test Region
        /// <summary>
        /// Test comment
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<object[]> DataMethodWithArgsForNamedParameters(int arg1, string arg2) =>
        new object[][]
        {
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        }.Take(arg1 + (arg2?.Length ?? throw new ArgumentNullException(nameof(arg2))));

        /// <summary>
        /// Test comment
        /// Multiline
        /// </summary>
        public static IEnumerable<object[]> DataProperty =>
        [
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];
        #endregion

        public static IEnumerable<object[]> DataMultipleExpected =>
        [
            [1, 2, 0, "0x0"],
            [-2, 2, 0, "0x0"],
            [int.MinValue, -1, -100, "0x0"],
            [-4, -6, +50, "0x0"],
#region Test data for method parameter default values
            //[100, 200], //Ignore due to Xunit runtime error: System.InvalidOperationException : The test method expected 4 parameter values, but 2 parameter value was provided.
            [100, 200, -5],
#endregion
        ];

        public static IEnumerable<object[]> DataYieldReturnAndMultipleExpected()
        {
            yield return [1, 2, 0, "0x0"];
            yield return [-2, 2, 0, "0x0"];
            yield return [int.MinValue, -1, -100, "0x0"];
            yield return [-4, -6, +50, "0x0"];
        }

        public static TheoryData<int, int, int, string> TheoryDataMultipleExpected =>
        new()
        {
            { 1, 2, 0, "0x0" },
            { -2, 2, 0, "0x0" },
            { int.MinValue, -1, -100, "0x0" },
            { -4, -6, +50, "0x0" },
        };

        public static TheoryData<AddResult, AddResult> TheoryDataWithClassWithoutProperEqualityOperator =>
        new()
        {
            { new AddResult(), new AddResult() },
        };

        public static TheoryData<TestCaseSourceUseCase<AddResult>, AddResult> TheoryDataWithTestCaseEqualityOperator =>
        new()
        {
            {
                new TestCaseSourceUseCase<AddResult>("2+2", new AddResult
                {
                    Value = 4,
                    ValueAsHexString = "0x4",
                }),
                new AddResult
                {
                    Value = 5, //keep incorrect to have original test failed
                    ValueAsHexString = "0x5",
                }
            },
            {
                new TestCaseSourceUseCase<AddResult>("5-1", new AddResult
                {
                    Value = 4,
                    ValueAsHexString = "0x4",
                }),
                new AddResult() //keep incorrect (default properties) to have original test failed
            },
        };
        public static TheoryData<CustomEqualValue, int> TheoryDataCustomEqualArg =>
        new()
        {
            { null!, -123 },
            { new() { Value = 1 }, -123 },
            { new() { Value = 2 }, -123 },
        };
        public static TheoryData<int[], IEnumerable<CustomEqualValue>, string> TheoryDataEnumerableArgs =>
        new()
        {
            { null!, null!, "0x123" },
            { [], [], "0x123" },
            { [1], [new() { Value = 1 } ], "0x123" },
            { [1, 2], [new() { Value = 1 }, new() { Value = 2 } ], "0x123" },
        };
        public static TheoryData<int[][], IEnumerable<IEnumerable<CustomEqualValue>>, int> TheoryDataMultiDimensionalEnumerableArgs =>
        new()
        {
            { null!, null!, -100 },
            { [], [], -100 },
            { [[1]], [[new() { Value = 1 } ]], -100 },
            { [[1, 2], [1, 2]], [[new() { Value = 1 }, new() { Value = 2 }] ], -100 },
        };
    }
}
