using Shouldly;
using System.Globalization;

namespace Test.Integration.XUnit;

public class NoExpectedVarShouldlyTest
{
    [Fact]
    public void ShouldBeTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        actual.ShouldBe(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentWhenMultipleArgsTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        actual.ShouldBe(123, "some explanation");
        actual.ShouldBe(customMessage: "some explanation", expected: 123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualWithPropertyTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual.IntProperty.ShouldBe(123);
    }

    [Fact]
    public void ShouldDetectExpectedArgumentAndActualWithMethodTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual.IntPropertyAsMethod().ShouldBe(123);
    }

    [Fact]
    public void ShouldBeWhenMultipleActualsTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();
        var actual2 = 100;

        //Assert
        actual.ShouldBe(123);
        actual2.ShouldBe(123);
    }

    [Fact]
    public void ShouldBeWhithInvocationExpressionTest()
    {
        //Act
        var actual = TestedClass.TestedMethod();

        //Assert
        //The assertion does not fail by default. Also, StormPetrel does not replace the invocation expression below.
        //But we keep this use case to indicate no unexpected failures in StormPetrel here.
        actual.ShouldBe(TestedClass.TestedMethod());
        actual.ShouldBe(TestedClass.ReturnInput(TestedClass.TestedMethod()));
    }

    [Fact]
    public void ShouldBeWhenNullConditionalOperatorTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual?.StringNullableProperty.ShouldBe("Incorrect value");
        actual?.StringNullableProperty.ShouldBe(null);
        actual?.StringNullableProperty?.ShouldBe("Incorrect value");
        actual?
            .StringNullablePropertyAsMethod()?
            .ShouldBe("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty?.ShouldBe("Incorrect value");
    }

    [Fact]
    public void ShouldBeWhenNullConditionalOperatorAssertsNullTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        actual.StringNullableProperty = null;

        //Assert
        /*Test comment*/
        actual?.StringNullableProperty.ShouldBe("Incorrect not null");
        //Test comment
        actual?.StringNullableProperty.ShouldBe(null);
        //Test multiline
        //comment
        actual?.StringNullableProperty?.ShouldBe("Incorrect not null");
        /*Test comment*/
        actual?
            .StringNullablePropertyAsMethod()?
            .ShouldBe("Incorrect not null");
        ArgumentNullException.ThrowIfNull(actual);
        /*Test multiline
         *comment*/
        actual.StringNullableProperty?.ShouldBe("Incorrect not null");
    }

    [Fact]
    public void ShouldBeWhenNullForgivenOperatorTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual!.StringNullableProperty.ShouldBe("Incorrect value");
        actual!.StringNullableProperty.ShouldBe(null);
        actual!.StringNullableProperty!.ShouldBe("Incorrect value");
        actual!
            .StringNullablePropertyAsMethod()!
            .ShouldBe("Incorrect value");
        ArgumentNullException.ThrowIfNull(actual);
        actual.StringNullableProperty!.ShouldBe("Incorrect value");
    }

    [Fact]
    public void ShouldBeWhenNullCoalescingOperatorTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();
        if (actual == null)
        {
            throw new InvalidOperationException();
        }

        //Assert
        actual.StringNullableProperty.ShouldBe("Incorrect value");
        actual.StringNullableProperty.ShouldBe(null);
        actual.StringNullableProperty?.ShouldBe("Incorrect value");
        (actual.StringNullablePropertyAsMethod() ?? throw new InvalidOperationException())
            .ShouldBe("Incorrect value");
        (actual.StringNullableProperty ?? throw new InvalidOperationException()).ShouldBe("Incorrect value");
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenAnonymousObjectCreationTest()
    {
        //Act
        var actual = new
        {
            Amount = 100,
            Message = "Hello"
        };

        //Assert
        actual.ShouldBe(new
        {
            Amount = 123,
            Message = "Hello incorrect"
        });
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxTest()
    {
        //Act
        var actual = 100;

        //Assert
        actual.ShouldBe(123);
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxStringTest()
    {
        //Act
        var actual = "Hello, World incorrect!";

        //Assert
        actual.ShouldBe("Hello, World!");
    }

    [Fact]
    public void ShouldBeDetectExpectedArgumentWhenLiteralExpressionSyntaxCharTest()
    {
        //Act
        var actual = 'B';

        //Assert
        actual.ShouldBe('A');
    }

    [Fact]
    public void ShouldBeDetectExpectedAndActualWhenComparerUsedTest()
    {
        //Act
        var actual = TestedClass.TestedClassResultMethod();

        //Assert
        actual.ShouldBe(new TestClassResult
        {
            IntProperty = 123,
            DateTimeProperty = DateTime.ParseExact("2025-03-17T18:11:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind),
        }, new TestClassResultEqualityComparer());
    }
}
