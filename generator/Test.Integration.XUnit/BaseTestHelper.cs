namespace Test.Integration.XUnit
{
    internal static class BaseTestHelper
    {
        public static int GetExpected() => 123;

        public static int GetExpectedPropertyArrow => 123;

        public static TestClassResult GetExpectedClassResultArrow() => new()
        {
            StringProperty = "AAA Incorrect",
            IntProperty = 111,
        };

        public static TestClassResult GetExpectedClassResult()
        {
            return new TestClassResult()
            {
                StringProperty = "AAA Incorrect",
                IntProperty = 111,
            };
        }

        public static TestClassResult GetExpectedClassResult(string arg1, int arg2)
        {
            switch (arg1)
            {
                case "A":
                    switch (arg2)
                    {
                        case 1:
                            return new TestClassResult
                            {
                                StringProperty = "AAA Incorrect",
                                IntProperty = 111,
                            };
                        default:
                            throw new InvalidOperationException();
                    }
                case "B":
                    switch (arg2)
                    {
                        case 2:
                            return new TestClassResult
                            {
                                StringProperty = "BBB Incorrect",
                                IntProperty = 222,
                            };
                        default:
                            throw new InvalidOperationException();
                    }
                default:
                    throw new InvalidOperationException();
            }
        }

        public static TestClassResult GetExpectedClassResultViaIf(string arg1, int arg2)
        {
            if (arg1 == "A" && arg2 == 1)
            {
                return new TestClassResult
                {
                    StringProperty = "AAA Incorrect",
                    IntProperty = 111,
                };
            }
            else if (arg1 == "B" && arg2 == 2)
            {
                return new TestClassResult
                {
                    StringProperty = "BBB Incorrect",
                    IntProperty = 222,
                };
            }
            throw new InvalidOperationException();
        }

        public static TestClassResult GetExpectedClassResultReturnArrow(string arg1, int arg2)
        {
            return (arg1, arg2) switch
            {
                ("A", 1) => new TestClassResult()
                {
                    StringProperty = "AAA Incorrect",
                    IntProperty = 111,
                },
                ("B", 2) => new TestClassResult()
                {
                    StringProperty = "BBB Incorrect",
                    IntProperty = 222,
                },
                _ => throw new InvalidOperationException(),
            };
        }

        public static TestClassResult GetExpectedClassResultReturnArrowReverse(string arg1, int arg2)
        {
            return (arg1, arg2) switch
            {
                ("B", 2) => new TestClassResult()
                {
                    StringProperty = "BBB Incorrect",
                    //IntProperty = 222, //intentionally comment out to vary use cases
                },
                ("A", 1) => new TestClassResult()
                {
                    //StringProperty = "AAA Incorrect", //intentionally comment out to vary use cases
                    IntProperty = 111,
                },
                _ => throw new InvalidOperationException(),
            };
        }

        public static TestClassResult GetExpectedClassResultArrowPatternMatch(string arg1, int arg2)
            =>
        (arg1, arg2) switch
        {
            ("A", 1) => new TestClassResult()
            {
                StringProperty = "AAA Incorrect",
                IntProperty = 111,
            },
            ("B", 2) => new TestClassResult()
            {
                StringProperty = "BBB Incorrect",
                IntProperty = 222,
            },
            _ => throw new InvalidOperationException(),
        };

        public static TestClassResult GetExpectedClassResultArrowPatternMatchWithSingleArg1Value(string arg1, int arg2) =>
        (arg1, arg2) switch
        {
            ("A", 1) => new TestClassResult()
            {
                StringProperty = "AAA 1 Incorrect",
                IntProperty = 111,
            },
            ("A", 2) => new TestClassResult()
            {
                StringProperty = "AAA 2 Incorrect",
                IntProperty = 222,
            },
            _ => throw new InvalidOperationException(),
        };

        public static int GetExpectedClassResultArrowPatternMatchWithWhenCondition(int arg1, int arg2) => arg1 switch
        {
            2 when 0 < arg2 => 4,
            2 => 5,
            _ => 6,
        };

        #region Ignored Methods
        public static void VoidMethodToIgnore()
        {
            throw new InvalidOperationException("Should not be called because this is an example of void method");
        }

        public static Task TaskMethodToIgnore()
        {
            throw new InvalidOperationException("Should not be called because this is an example of Task method");
        }

        public static Task<int> TaskIntMethodToIgnore()
        {
            throw new InvalidOperationException("Should not be called because this is an example of Task method");
        }

        public static async Task<int> TaskIntAsyncMethodToIgnore()
        {
            await Task.CompletedTask;
            throw new InvalidOperationException("Should not be called because this is an example of Task method");
        }

        public static async ValueTask<int> TaskValueTaskMethodToIgnore()
        {
            await Task.CompletedTask;
            throw new InvalidOperationException("Should not be called because this is an example of Task method");
        }
        #endregion
    }
}
