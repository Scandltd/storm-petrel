using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class ClassWithConstructorTestStormPetrel
    {
        private int _argFromConstructor;
        public ClassWithConstructorTestStormPetrel()
        {
        }

        /// <summary>
        /// Test comment
        /// </summary>
        /// <param name = "argFromConstructor"></param>
        public ClassWithConstructorTestStormPetrel(int argFromConstructor)
        {
            _argFromConstructor = argFromConstructor;
        }

        [Fact]
        public void SomeTestStormPetrel()
        {
            //Arrange
            int expected = 123;
            //Act
            var actual = _argFromConstructor;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassWithConstructorTest",
                MethodName = "SomeTest",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "ClassWithConstructorTest",
                    "SomeTest",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "ClassWithConstructorTest",
                    "SomeTest",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        private class NestedClassWithConstructor
        {
            public NestedClassWithConstructor()
            {
            }
        }

        private class NestedClassWithConstructorAndStaticMethod
        {
            public NestedClassWithConstructorAndStaticMethod()
            {
            }

            public static int StaticMethod() => 1;
        }
    }

    public class ClassWithConstructorAndTwoTestMethodsTestStormPetrel
    {
        public ClassWithConstructorTestStormPetrel()
        {
        }

        [Fact]
        public void SomeTest1StormPetrel()
        {
            //Arrange
            int expected = 123;
            //Act
            var actual = 234;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassWithConstructorAndTwoTestMethodsTest",
                MethodName = "SomeTest1",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "ClassWithConstructorAndTwoTestMethodsTest",
                    "SomeTest1",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "ClassWithConstructorAndTwoTestMethodsTest",
                    "SomeTest1",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void SomeTest2StormPetrel()
        {
            //Arrange
            int expected = 111;
            //Act
            var actual = 222;
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "ClassWithConstructorAndTwoTestMethodsTest",
                MethodName = "SomeTest2",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "ClassWithConstructorAndTwoTestMethodsTest",
                    "SomeTest2",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "ClassWithConstructorAndTwoTestMethodsTest",
                    "SomeTest2",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
            //Assert
            actual.Should().Be(expected);
        }
    }

    internal class ClassWithConstructorWithStaticStormPetrel
    {
        public ClassWithConstructorWithStaticStormPetrel()
        {
        }

        public static int StaticMethod() => 1;
        public static (int NodeKind, int NodeIndex) StaticMethodStormPetrel() => (8917, 0);
        private class NestedClassWithConstructor
        {
            public NestedClassWithConstructor()
            {
            }
        }

        private class NestedClassWithConstructorAndStaticMethod
        {
            public NestedClassWithConstructorAndStaticMethod()
            {
            }

            public static int StaticMethod() => 1;
        }
    }

    internal class ClassWithConstructorWithTwoStaticStormPetrel
    {
        public ClassWithConstructorWithTwoStaticStormPetrel()
        {
        }

        public static int StaticMethod1() => 111;
        public static (int NodeKind, int NodeIndex) StaticMethod1StormPetrel() => (8917, 0);
        public static int StaticMethod2() => 222;
        public static (int NodeKind, int NodeIndex) StaticMethod2StormPetrel() => (8917, 0);
    }
}