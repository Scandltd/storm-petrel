using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class ClassWithConstructorTest
    {
        private int _argFromConstructor;
        public ClassWithConstructorTest()
        {
        }

        /// <summary>
        /// Test comment
        /// </summary>
        /// <param name="argFromConstructor"></param>
        public ClassWithConstructorTest(int argFromConstructor)
        {
            _argFromConstructor = argFromConstructor;
        }

        [Fact]
        public void SomeTest()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = _argFromConstructor;

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

    public class ClassWithConstructorAndTwoTestMethodsTest
    {
        public ClassWithConstructorTest()
        {
        }

        [Fact]
        public void SomeTest1()
        {
            //Arrange
            int expected = 123;

            //Act
            var actual = 234;

            //Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public void SomeTest2()
        {
            //Arrange
            int expected = 111;

            //Act
            var actual = 222;

            //Assert
            actual.Should().Be(expected);
        }
    }

    internal class ClassWithConstructorWithStatic
    {
        public ClassWithConstructorWithStatic()
        {
        }

        public static int StaticMethod() => 1;
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

    internal class ClassWithConstructorWithTwoStatic
    {
        public ClassWithConstructorWithTwoStatic()
        {
        }

        public static int StaticMethod1() => 111;
        public static int StaticMethod2() => 222;
    }

    internal class ClassWithConstructorWithoutStatic
    {
        public ClassWithConstructorWithoutStatic()
        {
        }
    }
}
