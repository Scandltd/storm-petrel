using FluentAssertions;
using System.Threading.Tasks;
using System;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTestHelperWithoutTestMethodsStormPetrel
    {
        private static object Expected() => new object ();
        private static (int NodeKind, int NodeIndex) ExpectedStormPetrel() => (8917, 0);
        public static object ExpectedStatic() => new object ();
        public static (int NodeKind, int NodeIndex) ExpectedStaticStormPetrel() => (8917, 0);
        private object NotStaticMethod() => new object ();
        private static Foo ExpectedReturnOnly()
        {
            return new Foo();
        }

        private static (int NodeKind, int NodeIndex) ExpectedReturnOnlyStormPetrel()
        {
            return (8805, 0);
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

        private static (int NodeKind, int NodeIndex) ExpectedWithCoupleReturnsStormPetrel(string arg)
        {
            switch (arg)
            {
                case "case1":
                    return (8805, 0);
                case "case2":
                    return (8805, 1);
                default:
                    throw new System.InvalidOperationException();
            }

            return (8805, 2);
        }

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