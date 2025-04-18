﻿using FluentAssertions;
using System.Reflection;

namespace Test.Integration.XUnit
{
    public class TestCaseSourceMemberDataTest
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
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
            var lastUseCase = DataMethodWithArgs(int.MaxValue).Last();
            lastUseCase[2].Should().Be(50, "Last use case should not be modified according to MemberData arguments");
        }

        [Theory]
        [MemberData(parameters: [1, "22"], memberName: nameof(DataMethodWithArgsForNamedParameters))]
        public void WhenMemberDataMethodWithArgNamesThenItIsUpdated(int x, int y, int expected)
        {
            var actual = Calculator.Add(x, y).Value;
            actual.Should().Be(expected);
            var lastUseCase = DataMethodWithArgs(int.MaxValue).Last();
            lastUseCase[2].Should().Be(50, "Last use case should not be modified according to MemberData arguments");
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
        public void WhenMultipleExpectedThenItIsUpdated(int x, int y, int expected, string expectedHexString)
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
#pragma warning disable xUnit1045 //Justification: Intentionally test AddResult as non-serializable
        [MemberData(nameof(TheoryDataWithClassWithoutProperEqualityOperator))]
#pragma warning restore xUnit1045
        public static void WhenTheoryDataWithClassWithoutProperEqualityOperatorThrowingTheException(AddResult someArgWithoutProperEqualityOperator, AddResult expected)
        {
            //Arrange
            //Act
            var actual = someArgWithoutProperEqualityOperator; //emulate an action
            //Assert
            actual.Should().BeEquivalentTo(expected);
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

        public static IEnumerable<object[]> DataMethodWithArgsForNamedParameters(int arg1, string arg2) =>
        new object[][]
        {
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        }.Take(arg1 + (arg2?.Length ?? throw new ArgumentNullException(nameof(arg2))));

        public static IEnumerable<object[]> DataProperty =>
        [
            [1, 2, 0],
            [-2, 2, 0],
            [int.MinValue, -1, -100],
            [-4, -6, +50],
        ];

        public static IEnumerable<object[]> DataMultipleExpected =>
        [
            [1, 2, 0, "0x0"],
            [-2, 2, 0, "0x0"],
            [int.MinValue, -1, -100, "0x0"],
            [-4, -6, +50, "0x0"],
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
    }
}
