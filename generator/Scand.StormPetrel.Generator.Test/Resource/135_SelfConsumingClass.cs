using FluentAssertions;
using Xunit.Sdk;

namespace Test.Integration.XUnit
{
    public class SelfConsumingClassTest
    {
        [Fact]
        public void ConstructorReferenceTest()
        {
            //Arrange
            var expected = new SelfConsumingClassTest();

            //Act
            SelfConsumingClassTest actual = new();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public void WhenFullNameReferenceThenDoNotReplaceOriginalNameTest()
        {
            //Arrange
            var expected = new Test.Integration.XUnit.SelfConsumingClassTest();

            //Act
            Test.Integration.XUnit.SelfConsumingClassTest actual = new();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public void MethodReferenceTest()
        {
            //Arrange
            var expected = StaticMethodReturningThisClassInstance();

            //Act
            SelfConsumingClassTest actual = InstanceMethodReturningThisClassInstance();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        private static SelfConsumingClassTest PropertyReturningThisClassInstance => new SelfConsumingClassTest();
        private static SelfConsumingClassTest? PropertyNullableReturningThisClassInstance => null;
        private static SelfConsumingClassTest FieldReturningThisClassInstance = new SelfConsumingClassTest();
        private static SelfConsumingClassTest? FieldNullableReturningThisClassInstance = null;
        private static SelfConsumingClassTest StaticMethodReturningThisClassInstance()
        {
            // Ignore rename: QualifiedNameSyntax. Workaround for test code: use short name syntax
            new Test.Integration.XUnit.SelfConsumingClassTest();
            // Ignore rename: AliasQualifiedNameSyntax. Workaround for test code: use short name syntax
            new global::SelfConsumingClassTest();
            // Ignore rename: MemberAccessExpressionSyntax
            FooClass.SelfConsumingClassTest;
            FooClass.SelfConsumingClassTest();
            FooClass.SelfConsumingClassTest(1, 2, 3);
            // Ignore rename: MemberBindingExpressionSyntax
            FooClass.Instance?.SelfConsumingClassTest;
            FooClass.Instance?.SelfConsumingClassTest();
            // Ignore rename: named tuple elements
            var myTuple = (SelfConsumingClassTest: new object(), AnotherValue: 10);
            (object SelfConsumingClassTest, int AnotherValue) myTupleWithExplicitType = (new(), 20);
            (object SelfConsumingClassTest, int AnotherValue) = (new(), 30);
            // Ignore rename: local variable declaration with initializer
            var SelfConsumingClassTest = new FooClass
            {
                SelfConsumingClassTest = 1,
            };
            // Ignore rename: nested calls. Workaround for test code: use NestedWithSelfConsumingClass directly
            SelfConsumingClassTest.NestedWithSelfConsumingClass.InstanceMethodReturningThisClassInstance(1);

            // Do not ignore rename: type token
            SelfConsumingClassTest myVar = new();
            SelfConsumingClassTest? myVarNullable = null;
            (SelfConsumingClassTest FooField, int AnotherValue) = (new(), 40);
            (SelfConsumingClassTest? FooFieldNullable, int AnotherValueNullable) = (new(), 50);
            FooGeneric<string, SelfConsumingClassTest, SelfConsumingClassTest?, FooGenericLevel2<SelfConsumingClassTest, SelfConsumingClassTest?>> myGeneric = new FooGeneric<string, SelfConsumingClassTest, SelfConsumingClassTest?, FooGenericLevel2<SelfConsumingClassTest, SelfConsumingClassTest?>>();

            // Do not ignore rename: short name syntax type token
            return new SelfConsumingClassTest();
        }
        /// <summary>
        /// When a comment reference Then should not be replaced:  <see cref="SelfConsumingClassTest"/>.
        /// </summary>
        /// <returns></returns>
        private SelfConsumingClassTest InstanceMethodReturningThisClassInstance() => new SelfConsumingClassTest();
        private class NestedWithSelfConsumingClass
        {
            public SelfConsumingClassTest InstanceMethodReturningThisClassInstance(int arg) => arg switch
            {
                1 => new SelfConsumingClassTest(),
                2 => new SelfConsumingClassTest(),
                _ => throw new XunitException("Invalid argument"),
            };
        }
    }

    internal class SelfConsumingClassWithStatic
    {
        public static SelfConsumingClassWithStatic StaticMethod() => new SelfConsumingClassWithStatic();
        public static Test.Integration.XUnit.SelfConsumingClassWithStatic StaticMethodWithFullNameReference() => new Test.Integration.XUnit.SelfConsumingClassWithStatic();
        public SelfConsumingClassWithStatic InstanceMethod() => new SelfConsumingClassWithStatic();
        public Test.Integration.XUnit.SelfConsumingClassWithStatic InstanceMethodWithFullNameReference() => new Test.Integration.XUnit.SelfConsumingClassWithStatic();
        public class NestedWithSelfConsumingClass
        {
            public SelfConsumingClassWithStatic InstanceMethodReturningThisClassInstance() => new SelfConsumingClassWithStatic();
        }
        [SelfConsumingClassWithStatic]
        public static int MethodWithAttributesWhichNameMatchesTheClassName([SelfConsumingClassWithStatic] SelfConsumingClassWithStatic arg) => 42;
        [FooClassAttribute(SelfConsumingClassWithStatic = 1)]
        public static int MethodWithAttributesParametersMatchingTheClassName([FooArgAttribute(SelfConsumingClassWithStatic = 1)] SelfConsumingClassWithStatic arg) => 42;
    }
}
