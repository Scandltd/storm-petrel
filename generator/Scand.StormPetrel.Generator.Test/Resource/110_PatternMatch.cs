using FluentAssertions;
using System.Threading.Tasks;
using System;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTestHelperWithoutTestMethodsForPatternMatch
    {
        private static Foo ExpectedArrowMethod(string arg) => arg switch
        {
            "case1" => new Foo(1),
            "case2" => new Foo(2),
            _ => throw new InvalidOperationException(),
        };

        private static Foo ExpectedReturnMethod(string arg)
        {
            return arg switch
            {
                "case1" => new Foo(1),
                "case2" => new Foo(2),
                _ => throw new InvalidOperationException(),
            };
        }

        private static Foo ExpectedArrowMethodWithMultipleArgs(string arg, int arg2) => (arg, arg2) switch
        {
            ("case1", 1) => new Foo(1, 1),
            ("case1", 2) => new Foo(1, 2),
            ("case2", 3) => new Foo(2, 3),
            ("case2", 4) => new Foo(2, 4),
            _ => throw new InvalidOperationException(),
        };

        private static Foo ExpectedArrowMethodWithNestedPatternMatch(string arg, int arg2) => arg switch
        {
            "case1" => arg2 switch
            {
                1 => new Foo(1, 1),
                2 => new Foo(1, 2),
            },
            "case2" => arg2 switch
            {
                3 => new Foo(2, 3),
                4 => new Foo(2, 4),
            },
            _ => throw new InvalidOperationException(),
        };
    }
}
