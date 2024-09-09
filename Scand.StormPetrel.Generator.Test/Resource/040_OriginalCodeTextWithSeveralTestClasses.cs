using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class SomeClassToBeDeleted
    {
        private class SomeClassToBeDeletedChild
        {
            public static void SomeMethod()
            {
            }
        }
        private enum SomeClassToBeDeletedChildEnum
        {
            None,
            One,
            Two,
        }
    }

    internal interface SomeInterfaceToBeDeleted
    {
        public void SomeMethod();
    }

    internal enum SomeEnumToBeDeleted
    {
        None,
        One,
        Two,
    }

    internal record SomeRecordToBeDeleted
    {
        public int SomeProperty { get; set; }
        public void SomeMethod()
        {
        }
    }

    public struct SomeStructToBeDeleted
    {
        public int SomeProperty { get; set; }
        public void SomeMethod()
        {
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            int expected = 1;

            //Act
            var actual = TestedClass.TestedMethod1();

            //Assert
            actual.Should().Be(expected);
        }
    }

    public class UnitTest2
    {
        [Fact]
        public void Test1()
        {
            //Arrange
            int expected = 1;

            //Act
            var actual = TestedClass.TestedMethod2();

            //Assert
            actual.Should().Be(expected);
        }

        private class UnitTest2ChildClassShouldNotBeDeleted
        {
            public static void SomeMethod()
            {
            }

            private class UnitTest2ChildChildClassShouldNotBeDeleted
            {
                public static void SomeMethod()
                {
                }
            }
            private enum UnitTest2ChildChildEnumShouldNotBeDeleted
            {
                None,
                One,
                Two,
            }
        }

        internal interface UnitTest2ChildInterfaceShouldNotBeDeleted
        {
            public void SomeMethod();
        }

        private enum UnitTest2ChildEnumShouldNotBeDeleted
        {
            None,
            One,
            Two,
        }

        internal record UnitTest2ChildRecordShouldNotBeDeleted
        {
            public int SomeProperty { get; set; }
            public void SomeMethod()
            {
            }
        }

        public struct UnitTest2ChildStructShouldNotBeDeleted
        {
            public int SomeProperty { get; set; }
            public void SomeMethod()
            {
            }
        }
    }

    public static class TestedClass
    {
        public static int TestedMethod1()
        {
            return 100;
        }
        public static int TestedMethod2()
        {
            return 200;
        }
    }
}