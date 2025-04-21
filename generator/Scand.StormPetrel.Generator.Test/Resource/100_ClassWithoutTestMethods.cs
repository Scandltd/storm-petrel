using FluentAssertions;
using System.Threading.Tasks;
using System;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTestHelperWithoutTestMethods
    {
        private static object Expected() => new object();
        public static object ExpectedStatic() => new object();
        private object NotStaticMethod() => new object();
        private static Foo ExpectedReturnOnly()
        {
            return new Foo();
        }

        private static Foo ExpectedWithCoupleReturns(string arg)
        {
            switch (arg)
            {
                case "case1":
                    return new Foo(1);
                case "case2":
                    return new Foo(2);
                default:
                    throw new System.InvalidOperationException();
            }
            return new Foo();
        }

        #region A region with static methods
        /// <summary>
        /// A comment
        /// </summary>
        /// <returns></returns>
        private static Foo ExpectedReturnOnlyInRegion()
        {
            return new Foo();
        }

        /// <summary>
        /// One more comment
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private static Foo ExpectedWithCoupleReturnsInRegion(string arg)
        {
            switch (arg)
            {
                case "case1":
                    return new Foo(1);
                case "case2":
                    return new Foo(2);
                default:
                    throw new System.InvalidOperationException();
            }
            return new Foo();
        }
        #endregion

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
