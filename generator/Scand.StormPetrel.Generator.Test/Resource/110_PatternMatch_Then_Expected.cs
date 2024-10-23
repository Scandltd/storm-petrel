using FluentAssertions;
using System.Threading.Tasks;
using System;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTestHelperWithoutTestMethodsForPatternMatchStormPetrel
    {
        private static Foo ExpectedArrowMethod(string arg) => arg switch
        {
            "case1" => new Foo(1),
            "case2" => new Foo(2),
            _ => throw new InvalidOperationException(),
        };
        private static (int NodeKind, int NodeIndex) ExpectedArrowMethodStormPetrel(string arg) => arg switch
        {
            "case1" => (9026, 0),
            "case2" => (9026, 1),
            _ => (9026, 2),
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

        private static (int NodeKind, int NodeIndex) ExpectedReturnMethodStormPetrel(string arg)
        {
            return arg switch
            {
                "case1" => (9026, 0),
                "case2" => (9026, 1),
                _ => (9026, 2),
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
        private static (int NodeKind, int NodeIndex) ExpectedArrowMethodWithMultipleArgsStormPetrel(string arg, int arg2) => (arg, arg2) switch
        {
            ("case1", 1) => (9026, 0),
            ("case1", 2) => (9026, 1),
            ("case2", 3) => (9026, 2),
            ("case2", 4) => (9026, 3),
            _ => (9026, 4),
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
        private static (int NodeKind, int NodeIndex) ExpectedArrowMethodWithNestedPatternMatchStormPetrel(string arg, int arg2) => arg switch
        {
            "case1" => arg2 switch
            {
                1 => (9026, 0),
                2 => (9026, 1),
            },
            "case2" => arg2 switch
            {
                3 => (9026, 2),
                4 => (9026, 3),
            },
            _ => (9026, 4),
        };
    }
}