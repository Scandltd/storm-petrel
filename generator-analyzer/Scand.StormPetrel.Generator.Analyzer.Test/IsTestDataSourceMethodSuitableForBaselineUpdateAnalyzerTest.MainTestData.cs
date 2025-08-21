using Microsoft.CodeAnalysis.Testing;
using System.Globalization;

namespace Scand.StormPetrel.Generator.Analyzer.Test;

partial class IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest
{
    private const string CommonUsings = @"
using System;
using System.Collections.Generic;
using Xunit;
";
    public static TheoryData<string,
                                (string filename, string content)[],
                                (string filename, string content)[],
                                DiagnosticResult[]> MainTestData =>
    new()
    {
        {
            "When test class has data method not suitable for update Then the diagnostic",
            [],
            [("FooTest.cs", @"
using System.Collections.Generic;
using Xunit;
namespace TestNamespace.Test
{
    internal class FooTest
    {
        [Theory]
        [MemberData(nameof(DataMethod))]
        public void TestInt(int x)
        {
        }
        public static IEnumerable<object[]> DataMethod()
        {
            throw new System.NotImplementedException(""To have the dianostic reported"");
        }
    }
}")],
            [new DiagnosticResult(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "DataMethod"))
                    .WithLocation("FooTest.cs", 13, 9)]
        },
        {
            "When test class has data method suitable for update Then no diagnostic",
            [],
            [("FooTest.cs", @"
using System.Collections.Generic;
using Xunit;
namespace TestNamespace.Test
{
    internal class FooTest
    {
        [Theory]
        [MemberData(nameof(DataMethod))]
        public void TestInt(int x)
        {
        }
        public static IEnumerable<object[]> DataMethod() =>
        [
            [1],
        ];
    }
}")],
            []
        },
        {
            "Data source class has data method not suitable for update Then the diagnostic",
            [],
            [("Foo.cs", @"
using TestNamespace.TestData;
using Xunit;
namespace TestNamespace.Test
{
    internal class FooTest
    {
        [Theory]
        [MemberData(parameters: [1, ""22""], memberName: nameof(TestCaseSourceMemberData.Data), MemberType = typeof(TestCaseSourceMemberData))]
        public void TestInt(int x)
        {
        }
    }
}"),
("TestCaseSourceMemberData.cs", @"
using System.Collections.Generic;
namespace TestNamespace.TestData
{
    internal class TestCaseSourceMemberData
    {
        public static IEnumerable<object[]> Data(int i, string s)
        {
            throw new System.NotImplementedException(""To have the dianostic reported"");
        }
    }
}")],
            [new DiagnosticResult(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "Data"))
                    .WithLocation("TestCaseSourceMemberData.cs", 7, 9)]
        },
        {
            "Data source class has data method suitable for update Then no diagnostic",
            [],
            [("Foo.cs", @"
using TestNamespace.TestData;
using Xunit;
namespace TestNamespace.Test
{
    internal class FooTest
    {
        [Theory]
        [MemberData(parameters: [1, ""22""], memberName: nameof(TestCaseSourceMemberData.Data), MemberType = typeof(TestCaseSourceMemberData))]
        public void TestInt(int x)
        {
        }
    }
}"),
("TestCaseSourceMemberData.cs", @"
using System.Collections.Generic;
namespace TestNamespace.TestData
{
    internal class TestCaseSourceMemberData
    {
        public static IEnumerable<object[]> Data(int i, string s)
        {
            yield return [i + s.Length];
        }
    }
}")],
            []
        },
        {
            "When integration test case Then the diagnostic",
            [],
            [
                ("IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest.cs", CommonUsings + TestUtils.ReadResource("IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest")),
                ("TestDataSource.cs", CommonUsings + TestUtils.ReadResource("TestDataSource"))],
            [
            new DiagnosticResult(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "DataMethod"))
                    .WithLocation("IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest.cs", 41, 9),
            new DiagnosticResult(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "AttributeListDataMethod"))
                    .WithLocation("IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerTest.cs", 50, 9),
            new DiagnosticResult(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "TestDataSourceDataMethod"))
                    .WithLocation("TestDataSource.cs", 9, 5),
            ]
        },
        {
            "When integration test case Then the diagnostic",
            [],
            [
                ("IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerClassDataTest.cs", CommonUsings + TestUtils.ReadResource("IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerClassDataTest")),
                ("TestCaseSourceClassData.cs", CommonUsings + TestUtils.ReadResource("TestCaseSourceClassData"))],
            [new DiagnosticResult(IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule)
                    .WithMessage(string.Format(CultureInfo.InvariantCulture, IsTestDataSourceMethodSuitableForBaselineUpdateAnalyzerHelpers.Rule.MessageFormat.ToString(), "TestCaseSourceClassDataMethod"))
                    .WithLocation("TestCaseSourceClassData.cs", 13, 5)]
        },
    };

    public static IEnumerable<object[]> MSTestFileData =>
    [
        [// Skip processing of StormPetrel-generated classes
            new[] { ("FooTest.cs", @"
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Integration.MSTest
{
    [TestClass]
    public class TestCaseSourceTestStormPetrel
    {
        public static IEnumerable<object[]> Data() =>
            throw new NotImplementedException(""To have the diagnostic reported"");

        [TestMethod]
        [DynamicData(nameof(Data))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
        }
    }
}") }
        ],
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Integration.MSTest
{
    [TestClass]
    public partial class TestCaseSourceTest
    {
        public static partial IEnumerable<object[]> Data();

        [TestMethod]
        [DynamicData(nameof(Data))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
        }
    }
}"), ("TestCaseSource.cs", @"
using System.Collections.Generic;
namespace Test.Integration.MSTest
{
    public partial class TestCaseSourceTest
    {
        public static partial IEnumerable<object[]> Data() =>
        [
            [1, 2, 0]
        ];
    }
}") }
        ],
        [// When Data partial method implementation is in the first file but declaration is in the second
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Integration.MSTest
{
    [TestClass]
    public partial class TestCaseSourceTest
    {
        public static partial IEnumerable<object[]> Data() =>
        [
            [1, 2, 3]
        ];
        [TestMethod]
        [DynamicData(nameof(Data))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
        }
    }
}"), ("TestCaseSource.cs", @"
using System.Collections.Generic;
namespace Test.Integration.MSTest
{
    public partial class TestCaseSourceTest
    {
        public static partial IEnumerable<object[]> Data();
    }
}") }
        ],
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Integration.MSTest
{
    [TestClass]
    public class TestCaseSourceTest
    {
        [TestMethod]
        [DynamicData(nameof(TestCaseSource.Data), typeof(TestCaseSource))]
        public void WhenMemberDataWithMemberTypeThenItIsUpdated(int x, int y, int expected)
        {
        }
    }
}"), ("TestCaseSource.cs", @"
using System.Collections.Generic;
namespace Test.Integration.MSTest
{
    internal static class TestCaseSource
    {
        public static IEnumerable<object[]> Data() =>
        [
            [1, 2, 0]
        ];
    }
}") }
        ],
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Integration.MSTest
{
    [TestClass]
    public class TestCaseSourceTest
    {
        [TestMethod]
        [DynamicData(dynamicDataSourceName: nameof(DataProperty))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
        }
        private static IEnumerable<object[]> DataProperty =>
        [
            [1, 2, 0]
        ];
    }
}") }
        ],
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Test.Integration.MSTest
{
    [TestClass]
    public class TestCaseSourceTest
    {
        [TestMethod]
        [DynamicData(nameof(DataMethod), dynamicDataSourceType: DynamicDataSourceType.Method)]
        public void WhenMemberDataMethodThenItIsUpdated(int x, int y, int expected)
        {
        }
        private static IEnumerable<object[]> DataMethod() =>
        [
            [1, 2, 3]
        ];
    }
}") }
        ]
    ];

    public static IEnumerable<object[]> NUnitFileData =>
    [
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using NUnit.Framework;
namespace Test.Integration.NUnit
{
    public class TestCaseSourceTest
    {
        [TestCaseSource(typeof(NUnitTestCaseSource), nameof(NUnitTestCaseSource.DataMethod))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
        }
    }
}"), ("NUnitTestCaseSource.cs", @"
using System.Collections.Generic;
namespace Test.Integration.NUnit
{
    internal static class NUnitTestCaseSource
    {
        public static IEnumerable<object[]> DataMethod() =>
        [
            [1, 2, 0]
        ];
    }
}") }
        ],
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using NUnit.Framework;
namespace Test.Integration.NUnit
{
    public sealed class TestCaseSourceTest
    {
        [TestCaseSource(nameof(DataMethod))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
        }
        private static IEnumerable<object[]> DataMethod() =>
        [
            [1, 2, 3]
        ];
    }
}") }
        ],
        [
            new[] { ("FooTest.cs", @"
using System.Collections.Generic;
using NUnit.Framework;
namespace Test.Integration.NUnit
{
    public sealed class TestCaseSourceTest
    {
        [TestCaseSource(nameof(DataProperty))]
        public void WhenMemberDataPropertyThenItIsUpdated(int x, int y, int expected)
        {
        }
        private static IEnumerable<object[]> DataProperty =>
        [
            [1, 2, 3]
        ];
    }
}") }
        ]
    ];
}
