using FluentAssertions;
using System.Globalization;

namespace Test.Integration.XUnit
{
    public class BaseTest
    {
        [Fact]
        public void TestInt()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void TestClassResultMethod()
        {
            //Arrange
            var expected = new TestClassResultWithIgnorable
            {
                StringProperty = "Test StringProperty Incorrect",
                StringPropertyIgnored = "MUST be removed while StormPetrel test execution because the property is configured to be ignored",
                StringNullableProperty = "Test StringNullableProperty Incorrect",
                IntProperty = 100,
                IntNullableProperty = 100,
                EnumProperty = TestProperty.Two,
                EnumNullableProperty = TestProperty.Two,
                DateTimeProperty = DateTime.ParseExact("3000-05-20T10:05:15.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                DateTimeNullableProperty = DateTime.ParseExact("3000-05-20T10:05:26.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
                BooleanProperty = false,
                BooleanNullableProperty = false,
                TestClassResultObject = new TestClassResult
                {
                    StringProperty = "Test Object Property Incorrect",
                },
                TestClassResultEnumerable = new TestClassResult[]
                {
                    new TestClassResult
                    {
                        StringProperty = "Test Enumerable Object0 Property Incorrect",
                    },
                    new TestClassResult
                    {
                        StringProperty = "Test Enumerable Object1 Property Incorrect",
                    }
                },
                TestClassResultList = new List<TestClassResult>
                {
                    new TestClassResult
                    {
                        StringProperty = "Test List Object0 Property Incorrect",
                    },
                    new TestClassResult
                    {
                        StringProperty = "Test List Object1 Property Incorrect",
                    }
                },
                TestClassResultDict = new Dictionary<string, TestClassResult>
                {
                    {
                        "key0",
                        new TestClassResult
                        {
                            StringProperty = "Test Dict Object0 Property Incorrect",
                        }
                    },
                    {
                        "key1",
                        new TestClassResult
                        {
                            StringProperty = "Test Dict Object1 Property Incorrect",
                        }
                    }
                }
            };

            //Act
            var actual = TestedClass.TestedClassResultMethodWithIgnorable();

            //Assert
            actual.Should().BeEquivalentTo(expected, config => config.Excluding(x => x.StringPropertyIgnored));
        }

        [Fact]
        public void WhenVariableNamesDifferFromActualExpectedThenOkTest()
        {
            //Arrange
            int expectedVar = 123;

            //Act
            var actualVar = TestedClass.TestedMethod();

            //Assert
            actualVar.Should().Be(expectedVar);
        }

        [Fact]
        public void WhenMultipleVariableNamesThenOkTest()
        {
            //Arrange
            int expectedVAR = 123;
            int expectedVar = 123;

            //Act
            var actualVar = TestedClass.TestedMethod();
            var actualVAR = TestedClass.TestedMethod() + 1;

            //Assert
            actualVar.Should().Be(expectedVar);
            actualVAR.Should().Be(expectedVAR);
        }

        [Fact]
        public void WhenMultipleAssignmentThenLastAssignmentIsReplacedTest()
        {
            //Arrange
            int expectedVar;

            //Act
            var actualVar = TestedClass.TestedMethod();
            expectedVar = 123;

            //Assert
            expectedVar.Should().Be(123); //ensure first assignment is not replaced
            expectedVar = 456;
            actualVar.Should().Be(expectedVar);
        }

        [Fact]
        public void WhenExpectedIsFromStaticMethodOfCurrentClassThenLastAssignmentIsReplacedTest()
        {
            //Arrange
            int expected = GetExpected();

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExpectedIsFromStaticMethodOfAnotherClassThenLastAssignmentIsReplacedTest()
        {
            //Arrange
            int expected = BaseTestHelper.GetExpected();

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenMultipleExpectedThenLastAssignmentIsReplacedTest()
        {
            //Arrange
            int expected;
            expected = 400;
            expected = GetExpectedForMultipleAssignment();

            //Act
            var actual = TestedClass.TestedMethod();

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void WhenExtensionMethodThenProjectIsCompiledSuccessfullyTest()
        {
            //Arrange
            var expectedVar = "123";

            //Act
            var actualVar = TestedClass.TestedMethod().ExtensionMethodExample();

            //Assert
            actualVar.Should().Be(expectedVar);
        }

        [Theory]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        public void WhenExpectedIsFromStaticMethodWithArgsThenAllReturnsAreReplacedInTheMethodTest(string testCase, int subCase)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResult(testCase, subCase);

            //Act
            var actual = new TestClassResult
            {
                IntProperty = subCase,
                StringProperty = testCase,
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        public void WhenExpectedIsFromStaticMethodViaIfWithArgsThenAllReturnsAreReplacedInTheMethodTest(string testCase, int subCase)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResultViaIf(testCase, subCase);

            //Act
            var actual = new TestClassResult
            {
                IntProperty = subCase,
                StringProperty = testCase,
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        public void WhenExpectedIsFromStaticMethodViaReturnArrowWithArgsThenOkTest(string testCase, int subCase)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResultReturnArrow(testCase, subCase);

            //Act
            var actual = new TestClassResult
            {
                IntProperty = subCase,
                StringProperty = testCase,
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        public void WhenExpectedIsFromStaticMethodViaReturnArrowWithArgsAndReverseExecutionOrderThenOkTest(string testCase, int subCase)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResultReturnArrowReverse(testCase, subCase);

            //Act
            var actual = new TestClassResult
            {
                IntProperty = subCase,
                StringProperty = testCase,
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        public void WhenExpectedIsFromStaticMethodViaArrowPatternMatchThenOkTest(string testCase, int subCase)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResultArrowPatternMatch(testCase, subCase);

            //Act
            var actual = new TestClassResult
            {
                IntProperty = subCase,
                StringProperty = testCase,
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void WhenExpectedIsFromStaticMethodViaArrowPatternMatchWithSingleArg1ValueThenOkTest(int subCase)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResultArrowPatternMatchWithSingleArg1Value("A", subCase);

            //Act
            var actual = new TestClassResult
            {
                IntProperty = subCase,
                StringProperty = "A",
            };

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        public void WhenExpectedIsFromStaticMethodViaArrowPatternMatchWithWhenCondition(int arg1, int arg2)
        {
            //Arrange
            var expected = BaseTestHelper.GetExpectedClassResultArrowPatternMatchWithWhenCondition(arg1, arg2);

            //Act
            int actual = 5;

            //Assert
            actual.Should().Be(expected);
        }

        private static int GetExpected() => 123;

        private static int GetExpectedForMultipleAssignment() => 123;
    }

    public static class TestedClass
    {
        public static int TestedMethod()
        {
            return 100;
        }

        public static Task<int> ResultMethodAsync()
        {
            return Task.FromResult(100);
        }

        public static TestClassResult TestedClassResultMethod() => TestedClassResultMethod<TestClassResult>();

        public static TestClassResultWithIgnorable TestedClassResultMethodWithIgnorable() => TestedClassResultMethod<TestClassResultWithIgnorable>();
        private static T TestedClassResultMethod<T>() where T : TestClassResult, new()
        {
            return new T()
            {
                StringNullableProperty = "Test StringNullableProperty",
                StringProperty = "Test StringProperty",
                IntProperty = 1,
                IntNullableProperty = 3,
                EnumProperty = TestProperty.One,
                EnumNullableProperty = TestProperty.Three,
                DateTimeProperty = new DateTime(2024, 5, 20, 10, 5, 15),
                DateTimeNullableProperty = new DateTime(2024, 5, 20, 10, 5, 26),
                BooleanProperty = true,
                BooleanNullableProperty = true,
                TestClassResultObject = new TestClassResult
                {
                    StringProperty = "Test Object Property",
                },
                TestClassResultEnumerable =
                [
                    new TestClassResult
                    {
                        StringProperty = "Test Enumerable Object0 Property",
                    },
                    new TestClassResult
                    {
                        StringProperty = "Test Enumerable Object1 Property",
                    },
                ],
                TestClassResultList =
                [
                    new TestClassResult
                    {
                        StringProperty = "Test List Object0 Property",
                    },
                    new TestClassResult
                    {
                        StringProperty = "Test List Object1 Property",
                    },
                ],
                TestClassResultDict = new Dictionary<string, TestClassResult>
                {
                    {
                        "key0",
                        new TestClassResult
                        {
                            StringProperty = "Test Dict Object0 Property",
                        }
                    },
                    {
                        "key1",
                        new TestClassResult
                        {
                            StringProperty = "Test Dict Object1 Property",
                        }
                    },
                },
            };
        }

        public static int ReturnInput(int input) => input;
    }

    public class TestClassResult
    {
        public string StringProperty { get; set; } = string.Empty;
        public string? StringNullableProperty { get; set; }
        public string? StringNullablePropertyAsMethod() => StringNullableProperty;
        public int IntProperty { get; set; }
        public int IntPropertyAsMethod() => IntProperty;
        public int? IntNullableProperty { get; set; }
        public TestProperty EnumProperty { get; set; }
        public TestProperty? EnumNullableProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public DateTime DateTimePropertyAsMethod() => DateTimeProperty;
        public DateTime? DateTimeNullableProperty { get; set; }
        public bool BooleanProperty { get; set; }
        public bool? BooleanNullableProperty { get; set; }
        public TestClassResult? TestClassResultObject { get; set; }
        public IEnumerable<TestClassResult>? TestClassResultEnumerable { get; set; }
        public List<TestClassResult>? TestClassResultList { get; set; }
        public Dictionary<string, TestClassResult>? TestClassResultDict { get; set; }
    }

    public class TestClassResultWithIgnorable : TestClassResult
    {
        /// <summary>
        /// Always vary this property value to easily detect "ignore mechanism" failure.
        /// </summary>
        public string? StringPropertyIgnored { get; set; } = Guid.NewGuid().ToString();
    }

    [Flags]
    public enum TestProperty
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = One | Two,
    }
}