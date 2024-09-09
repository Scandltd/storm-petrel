using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter;
using Scand.StormPetrel.Rewriter.Extension;
using Xunit;

namespace Scand.StormPetrel.Rewriter.Test
{
    public class MainTest
    {
        [Theory]
        [InlineData("010_LocalVariable", "Foo;Bla;localVar", "010_LocalVariable_NewCode")]
        [InlineData("010_LocalVariable", "Foo;Bla;localVar", "010_LocalVariable_NewCodeMultiline")]
        [InlineData("015_LocalVariableWithNamespace", "TestNamespace.SomeSubSpace;Foo;Bla;localVar", "015_LocalVariableWithNamespace_NewCode")]
        [InlineData("020_LocalVariables", "Foo;Bla;localVar", "020_LocalVariables_NewCode")]
        [InlineData("020_LocalVariables", "Foo;Bla;localVar", "020_LocalVariables_NewCodeMultiline")]
        [InlineData("020_LocalVariables", "Foo;Bla2;localVar", "020_LocalVariables_NewCodeBla2")]
        [InlineData("030_Properties", "Foo;TestProperty", "030_Properties_NewCode")]
        [InlineData("030_Properties", "Foo;TestProperty", "030_Properties_NewCodeMultiline")]
        [InlineData("030_Properties", "Foo2;TestProperty", "030_Properties_NewCodeFoo2")]
        [InlineData("040_MethodArrow", "Foo;TestMethod", "040_MethodArrow_NewCode")]
        [InlineData("040_MethodArrow", "Foo;TestMethod", "040_MethodArrow_NewCodeMultiline")]
        [InlineData("040_MethodArrow", "Foo2;TestMethod", "040_MethodArrow_NewCodeFoo2")]
        [InlineData("050_Assignment", "Scand.StormPetrel.Rewriter.Resource.Test;GivenExample01FromSpec_ThenOutputDoc;GetData;Docs", "050_Assignment_NewCode", nameof(AssignmentRewriter))]
        [InlineData("050_Assignment", "Scand.StormPetrel.Rewriter.Resource.Test;GivenExample01FromSpec_ThenOutputDoc;GetData;Docs", "050_Assignment_NewCodeMultiline", nameof(AssignmentRewriter))]
        [InlineData("060_LocalVariableWithCommentAndTabs", "Foo;Bla;localVar", "060_LocalVariableWithCommentAndTabs_NewCode")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar", "070_LocalVariableWithMultipleAssignments_NewCode")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo[0];Bla2[0];localVar", "070_LocalVariableWithMultipleAssignments_NewCode")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[0]", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment0", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[1]", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment1", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[2]", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[2]", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo[1];Bla2;localVar", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo[1];Bla2;localVar", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[1];localVar", "070_LocalVariableWithMultipleAssignments_NewCodeDeclaration2")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[1];localVar", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[2];localVar", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[2];localVar", "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2", nameof(AssignmentRewriter))]
        [InlineData("080_LocalFunction", "Foo;Bla;BlaInBla;localVar", "080_LocalFunction_NewCode")]
        [InlineData("080_LocalFunction", "Foo;Bla;BlaInBla[1];localVar", "080_LocalFunction_NewCode1")]
        [InlineData("080_LocalFunction", "Foo;Bla;BlaInBla;localVar", "080_LocalFunction_NewCodeAssignment", nameof(AssignmentRewriter))]
        [InlineData("080_LocalFunction", "Foo;Bla;localVar", "080_LocalFunction_NewCodeAssignmentInParent")]
        [InlineData("090_ExpressionRewriter", "Foo;Bla#SimpleAssignmentExpression:0", "090_ExpressionRewriter_NewCode", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ArrowMethod#ArrowExpressionClause:0", "090_ExpressionRewriter_NewCode_ArrowMethod", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ArrowMethodArray#ArrowExpressionClause:0", "090_ExpressionRewriter_NewCode_ArrowMethodArray", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla#LocalDeclarationStatement:0", "090_ExpressionRewriter_NewCode_LocalDeclaration", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[0]#LocalDeclarationStatement:0", "090_ExpressionRewriter_NewCode_LocalDeclaration", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[1]#LocalDeclarationStatement:0", "090_ExpressionRewriter_NewCode_MethodOverload1", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[2]#LocalDeclarationStatement:0", "090_ExpressionRewriter_NewCode_MethodOverload2", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[3]#SimpleAssignmentExpression:1", "090_ExpressionRewriter_NewCode_MultipleAssignment", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ReturnMethod#ReturnStatement:0", "090_ExpressionRewriter_NewCode_ReturnMethod", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ReturnMethodArray#ReturnStatement:0", "090_ExpressionRewriter_NewCode_ReturnMethodArray", nameof(ExpressionRewriter))]
        [InlineData("100_PatternMatch", "Foo;ArrowMethodWithPatternMatch#SwitchExpressionArm:0", "100_PatternMatch_NewCode_ArrowMethod", nameof(ExpressionRewriter))]
        [InlineData("100_PatternMatch", "Foo;ArrowMethodWithPatternMatch#SwitchExpressionArm:1", "100_PatternMatch_NewCode_ArrowMethodSwitch2", nameof(ExpressionRewriter))]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;Expected", "110_Property_NewCode")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedArrow", "110_PropertyArrow_NewCode")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedGetArrow", "110_PropertyGetArrow_NewCode")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedGetSet", "110_PropertyGetSet_NewCode")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedReturn", "110_PropertyReturn_NewCode")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedReturnField", "110_PropertyReturnField_NewCode")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedReturnMultiple", "110_PropertyReturnMultiple_NewCode")]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:0:0", "120_Attributes_NewCode", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:0:1", "120_Attributes_NewCode01", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:1:3", "120_Attributes_NewCode13", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:2:3", "120_Attributes_NewCode23", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:3:0", "120_Attributes_NewCode30", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:0", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode00", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:1", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode01", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:2", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode02", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:3", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode03", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:4", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode04", nameof(AttributeRewriter))]
        [InlineData("ExpectedInlineInstanceTest01", "Test.Integration.Performance.XUnit.ExpectedInlineInstance;ExpectedInlineInstanceTest01;Test10;expected", "ExpectedInlineInstanceTest01_NewCode")] //performance
        public async Task RewriteTest(string inputCodeResourceName, string declarationPathSemicolonDelimited, string inputReplacementCodeResourceName, string rewriterClassName = nameof(DeclarationRewriter))
        {
            ArgumentNullException.ThrowIfNull(declarationPathSemicolonDelimited);
            //Arrange
            string[] declarationPath = declarationPathSemicolonDelimited.Split(';');
            static async Task<string> readResourceAsync(Assembly assembly, string resourceFileName)
            {
                using var stream = assembly.GetManifestResourceStream(typeof(MainTest), $"Resource.{resourceFileName}")
                    ?? throw new InvalidOperationException("Manifest Resource Stream must not be null at this point");
                using var streamReader = new StreamReader(stream);
                return await streamReader.ReadToEndAsync();
            }
            var assembly = Assembly.GetAssembly(typeof(MainTest))
                ?? throw new InvalidOperationException("Assembly with MainTest must not be null at this point");
            using var inStream = assembly.GetManifestResourceStream(typeof(MainTest), $"Resource.{inputCodeResourceName}.cs")
                ?? throw new InvalidOperationException("Manifest Resource Stream must not be null at this point"); ;
            var initializeCode = await readResourceAsync(assembly, $"{inputReplacementCodeResourceName}.cs");
            var expectedResourceFileName = $"{inputReplacementCodeResourceName}_Then_Expected.cs";
            var expectedCode = await readResourceAsync(assembly, expectedResourceFileName);

            //Act
            var stopwatch = Stopwatch.StartNew();
            var actualCodeAsync = await GetActualCode(true);
            var actualCodeSync = await GetActualCode(false);
            stopwatch.Stop();

            //Assert
            //Keep commented in git. Uncomment to overwrite baselines while development only.
            //File.WriteAllText(@$"..\..\..\Resource\{expectedResourceFileName}", actualCodeAsync);

            actualCodeAsync.Should().Be(expectedCode);
            actualCodeSync.Should().Be(expectedCode);
            stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(5), "Typical execution time is less than ~3 seconds for worst case on `11th Gen Intel(R) Core(TM) i7-1165G7 @ 2.80GHz`");

            async Task<string> GetActualCode(bool isAsyncOtherwiseSync)
            {
                using var outStream = new MemoryStream();
                var rewriter = GetRewriter();
                inStream.Position = 0;

                if (isAsyncOtherwiseSync)
                {
                    await rewriter.RewriteAsync(inStream, outStream);
                }
                else
                {
                    rewriter.Rewrite(inStream, outStream);
                }

                outStream.Position = 0;
                using var actualStreamReader = new StreamReader(outStream);
                return actualStreamReader.ReadToEnd();
            }

            CSharpSyntaxRewriter GetRewriter() => rewriterClassName switch
            {
                nameof(DeclarationRewriter) => new DeclarationRewriter(declarationPath, initializeCode),
                nameof(AssignmentRewriter) => new AssignmentRewriter(declarationPath, initializeCode),
                nameof(ExpressionRewriter) => GetExpressionRewriter(),
                nameof(AttributeRewriter) => GetAttributeRewriter(),
                _ => throw new InvalidOperationException(),
            };

            ExpressionRewriter GetExpressionRewriter()
            {
                var split = declarationPathSemicolonDelimited.Split('#');
                var split2 = split[1].Split(':');
                var kind = Enum.Parse<SyntaxKind>(split2[0]);
                return new ExpressionRewriter(split[0].Split(';'), (int)kind, int.Parse(split2[1], CultureInfo.InvariantCulture), initializeCode);
            }

            AttributeRewriter GetAttributeRewriter()
            {
                var split = declarationPathSemicolonDelimited.Split('#');
                var split2 = split[1].Split(':');
                return new AttributeRewriter(split[0].Split(';'), split2[0], int.Parse(split2[1], CultureInfo.InvariantCulture), int.Parse(split2[2], CultureInfo.InvariantCulture), initializeCode);
            }
        }
    }
}
