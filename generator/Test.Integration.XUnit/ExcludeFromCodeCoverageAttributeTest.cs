using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace Test.Integration.XUnit
{
    public class ExcludeFromCodeCoverageClassAttributeShouldBeAddedTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageClassAttributeExistsTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageFullNameTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [System
        .Diagnostics
        .CodeAnalysis
        .ExcludeFromCodeCoverage]
    public class ExcludeFromCodeCoverageFullNameMultilineTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [ExcludeFromCodeCoverage()]
    public class ExcludeFromCodeCoverageWithRoundBracketsTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [ExcludeFromCodeCoverage(Justification = "a justification")]
    public class ExcludeFromCodeCoverageWithArgumentsTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [ExcludeFromCodeCoverage/*a comment injected*/(Justification = "a justification")]
    public class ExcludeFromCodeCoverageWithCommentsTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    [ExcludeFromCodeCoverage
        (Justification
            = "a justification")]
    public class ExcludeFromCodeCoverageMultilineTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
#pragma warning disable CA1041 // Multiple attributes with pragma
    [Obsolete, ExcludeFromCodeCoverage
#pragma warning restore CA1041 // Multiple attributes with pragma
        (Justification
            = "a justification")]
    public class ExcludeFromCodeCoverageMultipleAttributesTest
    {
        [Fact]
        public void SomeTest()
        {
            2.Should().Be(1);
        }
    }
    public static class ExcludeFromCodeCoverageAttributeShouldBeAdded
    {
        public static int PotentialDataSource() => 1;
    }
    [ExcludeFromCodeCoverage]
    public static class ExcludeFromCodeCoverageClassAttributeExistsAndStaticMethodOnly
    {
        public static int PotentialDataSource() => 1;
    }
}