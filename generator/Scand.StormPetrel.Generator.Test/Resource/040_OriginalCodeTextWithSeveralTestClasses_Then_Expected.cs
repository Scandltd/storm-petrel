using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class UnitTest1StormPetrel
    {
        [Fact]
        public void Test1StormPetrel()
        {
            //Arrange
            int expected = 1;
            //Act
            var actual = TestedClass.TestedMethod1();
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest1",
                MethodName = "Test1",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "Test1",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest1",
                    "Test1",
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

    public class UnitTest2StormPetrel
    {
        [Fact]
        public void Test1StormPetrel()
        {
            //Arrange
            int expected = 1;
            //Act
            var actual = TestedClass.TestedMethod2();
            var stormPetrelContext = new Scand.StormPetrel.Generator.TargetProject.GenerationContext()
            {
                FilePath = "C:\\temp\\temp.cs",
                ClassName = "UnitTest2",
                MethodName = "Test1",
                MethodTestAttributeNames = new[]
                {
                    "Fact"
                },
                Actual = actual,
                ActualVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest2",
                    "Test1",
                    "actual"
                },
                Expected = expected,
                ExpectedVariablePath = new[]
                {
                    "Test.Integration.XUnit",
                    "UnitTest2",
                    "Test1",
                    "expected"
                },
                IsLastVariablePair = true,
                RewriterKind = Scand.StormPetrel.Generator.TargetProject.RewriterKind.Declaration
            };
            ((Scand.StormPetrel.Generator.TargetProject.IGenerator)new Scand.StormPetrel.Generator.TargetProject.Generator()).GenerateBaseline(stormPetrelContext);
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

    public static class TestedClassStormPetrel
    {
        public static int TestedMethod1()
        {
            return 100;
        }

        public static (int NodeKind, int NodeIndex) TestedMethod1StormPetrel()
        {
            return (8805, 0);
        }

        public static int TestedMethod2()
        {
            return 200;
        }

        public static (int NodeKind, int NodeIndex) TestedMethod2StormPetrel()
        {
            return (8805, 0);
        }
    }
}