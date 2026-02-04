using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter;
using Scand.StormPetrel.Rewriter.Extension;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace Scand.StormPetrel.Rewriter.Test
{
    public class MainTest
    {
        private const string EmptyList = "new List<string>() { }";
        private const string EmptyListWithComment = "new List<string>() { } //new code";
        private const string Const100 = "100";
        private const string Const123 = "123";
        private const string Const111Quoted = "\"111\"";
        private const string List2ElementsMultiline = @"new List<string>()
{
    ""1"",
    ""2""
}";

        [Theory]
        [InlineData("010_LocalVariable", new[] { "Foo", "Bla", "localVar" }, EmptyList, "010_LocalVariable_NewCode_Then_Expected")]
        [InlineData("010_LocalVariable", new[] { "Foo", "Bla", "localVar" }, "file:010_LocalVariable_NewCodeMultiline", "010_LocalVariable_NewCodeMultiline_Then_Expected")]
        [InlineData("010_LocalVariable", new[] { "Foo", "Bla", "localVar" }, "file:010_LocalVariable_NewCodeMultilineString", "010_LocalVariable_NewCodeMultilineString_Then_Expected")]
        [InlineData("015_LocalVariableWithNamespace", new[] { "TestNamespace.SomeSubSpace", "Foo", "Bla", "localVar" }, EmptyList, "015_LocalVariableWithNamespace_NewCode_Then_Expected")]
        [InlineData("020_LocalVariables", new[] { "Foo", "Bla", "localVar" }, EmptyList, "020_LocalVariables_NewCode_Then_Expected")]
        [InlineData("020_LocalVariables", new[] { "Foo", "Bla", "localVar" }, "file:020_LocalVariables_NewCodeMultiline", "020_LocalVariables_NewCodeMultiline_Then_Expected")]
        [InlineData("020_LocalVariables", new[] { "Foo", "Bla2", "localVar" }, EmptyList, "020_LocalVariables_NewCodeBla2_Then_Expected")]
        [InlineData("030_Properties", new[] { "Foo", "TestProperty" }, EmptyList, "030_Properties_NewCode_Then_Expected")]
        [InlineData("030_Properties", new[] { "Foo", "TestProperty" }, "file:030_Properties_NewCodeMultiline", "030_Properties_NewCodeMultiline_Then_Expected")]
        [InlineData("030_Properties", new[] { "Foo2", "TestProperty" }, "file:030_Properties_NewCodeFoo2", "030_Properties_NewCodeFoo2_Then_Expected")]
        [InlineData("040_MethodArrow", new[] { "Foo", "TestMethod" }, EmptyList, "040_MethodArrow_NewCode_Then_Expected")]
        [InlineData("040_MethodArrow", new[] { "Foo", "TestMethod" }, "file:040_MethodArrow_NewCodeMultiline", "040_MethodArrow_NewCodeMultiline_Then_Expected")]
        [InlineData("040_MethodArrow", new[] { "Foo2", "TestMethod" }, EmptyList, "040_MethodArrow_NewCodeFoo2_Then_Expected")]
        [InlineData("060_LocalVariableWithCommentAndTabs", new[] { "Foo", "Bla", "localVar" }, "file:060_LocalVariableWithCommentAndTabs_NewCode", "060_LocalVariableWithCommentAndTabs_NewCode_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2", "localVar" }, EmptyListWithComment, "070_LocalVariableWithMultipleAssignments_NewCode_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo[0]", "Bla2[0]", "localVar" }, EmptyListWithComment, "070_LocalVariableWithMultipleAssignments_NewCode_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2", "localVar[2]" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo[1]", "Bla2", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2[1]", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeDeclaration2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2[2]", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("080_LocalFunction", new[] { "Foo", "Bla", "BlaInBla", "localVar" }, EmptyList, "080_LocalFunction_NewCode_Then_Expected")]
        [InlineData("080_LocalFunction", new[] { "Foo", "Bla", "BlaInBla[1]", "localVar" }, EmptyList, "080_LocalFunction_NewCode1_Then_Expected")]
        [InlineData("080_LocalFunction", new[] { "Foo", "Bla", "localVar" }, EmptyList, "080_LocalFunction_NewCodeAssignmentInParent_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "Expected" }, Const100, "110_Property_NewCode_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "ExpectedArrow" }, Const100, "110_PropertyArrow_NewCode_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "ExpectedGetArrow" }, Const100, "110_PropertyGetArrow_NewCode_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "ExpectedGetSet" }, Const100, "110_PropertyGetSet_NewCode_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "ExpectedReturn" }, Const100, "110_PropertyReturn_NewCode_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "ExpectedReturnField" }, Const100, "110_PropertyReturnField_NewCode_Then_Expected")]
        [InlineData("110_Property", new[] { "Test.Integration.XUnit", "UnitTest1Helper", "ExpectedReturnMultiple" }, "file:110_PropertyReturnMultiple_NewCode", "110_PropertyReturnMultiple_NewCode_Then_Expected")]
        [InlineData("ExpectedInlineInstanceTest01", new[] { "Test.Integration.Performance.XUnit.ExpectedInlineInstance", "ExpectedInlineInstanceTest01", "Test10", "expected" }, "file:ExpectedInlineInstanceTest01_NewCode", "ExpectedInlineInstanceTest01_NewCode_Then_Expected")] //performance
        public async Task DeclarationRewriterTest(string inputCodeResourceName, string[] declarationPath, string initializeCode, string expectedResourceFileName)
            => await RewriteTestImplementation(async () => new DeclarationRewriter(declarationPath, await ResourceOrSelfAsync(initializeCode)), inputCodeResourceName, expectedResourceFileName);

        [Theory]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, "new List<MessageTreeDoc>()", "050_Assignment_NewCode_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, "file:050_Assignment_NewCodeMultiline", "050_Assignment_NewCodeMultiline_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, @"new List<MessageTreeDoc>(){
    """"""Single line"""""",
    """"""
    Line1
    Line2
    """""",
    // lang=json
    """"""
    Line1
    Line2
    """""",
    ""A non-raw string"", """"""
    Line1
    Line2
    """""",
}", "050_Assignment_NewCodeWithRawStrings_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, @"""""""New code is single line raw string""""""", "050_Assignment_NewCodeIsSingleLineRawString_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, @"""""""
Multiline raw string
Line 2
""""""", "050_Assignment_NewCodeIsMultiLineRawString_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, @"// lang=json
""""""
Multiline raw string
Line 2
""""""", "050_Assignment_NewCodeIsMultiLineRawStringWithComment_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, @"""\""\""\""""", "050_Assignment_NewCodeIsNotRawStringOf3DoubleQuotes_Then_Expected")]
        [InlineData("050_Assignment", new[] { "Scand.StormPetrel.Rewriter.Resource.Test", "GivenExample01FromSpec_ThenOutputDoc", "GetData", "Docs" }, @"@""""""""""""""""", "050_Assignment_NewCodeIsNotRawStringOf3DoubleQuotesVerbatim_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2", "localVar[0]" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment0_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2", "localVar[1]" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment1_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2", "localVar[2]" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo[1]", "Bla2", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2[1]", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", new[] { "Foo", "Bla2[2]", "localVar" }, EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("080_LocalFunction", new[] { "Foo", "Bla", "BlaInBla", "localVar" }, EmptyList, "080_LocalFunction_NewCodeAssignment_Then_Expected")]
        public async Task AssignmentRewriterTest(string inputCodeResourceName, string[] declarationPath, string initializeCode, string expectedResourceFileName)
            => await RewriteTestImplementation(async () => new AssignmentRewriter(declarationPath, await ResourceOrSelfAsync(initializeCode)), inputCodeResourceName, expectedResourceFileName);

        [Theory]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "Bla" }, SyntaxKind.SimpleAssignmentExpression, 0, EmptyList, "090_ExpressionRewriter_NewCode_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "ArrowMethod" }, SyntaxKind.ArrowExpressionClause, 0, "456", "090_ExpressionRewriter_NewCode_ArrowMethod_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "ArrowMethodArray" }, SyntaxKind.ArrowExpressionClause, 0, "file:090_ExpressionRewriter_NewCode_ArrowMethodArray", "090_ExpressionRewriter_NewCode_ArrowMethodArray_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "Bla" }, SyntaxKind.LocalDeclarationStatement, 0, EmptyList, "090_ExpressionRewriter_NewCode_LocalDeclaration_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "Bla[0]" }, SyntaxKind.LocalDeclarationStatement, 0, EmptyList, "090_ExpressionRewriter_NewCode_LocalDeclaration_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "Bla[1]" }, SyntaxKind.LocalDeclarationStatement, 0, List2ElementsMultiline, "090_ExpressionRewriter_NewCode_MethodOverload1_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "Bla[2]" }, SyntaxKind.LocalDeclarationStatement, 0, EmptyList, "090_ExpressionRewriter_NewCode_MethodOverload2_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "Bla[3]" }, SyntaxKind.SimpleAssignmentExpression, 1, EmptyList, "090_ExpressionRewriter_NewCode_MultipleAssignment_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "ReturnMethod" }, SyntaxKind.ReturnStatement, 0, "456", "090_ExpressionRewriter_NewCode_ReturnMethod_Then_Expected")]
        [InlineData("090_ExpressionRewriter", new[] { "Foo", "ReturnMethodArray" }, SyntaxKind.ReturnStatement, 0, "file:090_ExpressionRewriter_NewCode_ReturnMethodArray", "090_ExpressionRewriter_NewCode_ReturnMethodArray_Then_Expected")]
        [InlineData("100_PatternMatch", new[] { "Foo", "ArrowMethodWithPatternMatch" }, SyntaxKind.SwitchExpressionArm, 0, "file:100_PatternMatch_NewCode_ArrowMethod", "100_PatternMatch_NewCode_ArrowMethod_Then_Expected")]
        [InlineData("100_PatternMatch", new[] { "Foo", "ArrowMethodWithPatternMatch" }, SyntaxKind.SwitchExpressionArm, 1, "file:100_PatternMatch_NewCode_ArrowMethodSwitch2", "100_PatternMatch_NewCode_ArrowMethodSwitch2_Then_Expected")]
        [InlineData("105_InvocationExpressionParametersOnly", new[] { "Foo", "ShouldBe123" }, SyntaxKind.NumericLiteralExpression, 2, Const100, "105_InvocationExpressionParametersOnly_Then_Expected")]
        [InlineData("105_InvocationExpressionParametersOnly", new[] { "Foo", "ShouldBe123" }, SyntaxKind.Argument, 0, Const100, "105_InvocationExpressionParametersOnly_Then_Expected", 1)]
        [InlineData("105_InvocationExpressionParametersOnly", new[] { "Foo", "ShouldBeArgumentWithNameColon" }, SyntaxKind.Argument, 1, Const100, "105_InvocationExpressionParametersOnly_ArgumentWithNameColon_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeCorrectTrivia" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromArgumentAndMethodBodyStatement" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromArgumentAndMethodBodyStatement_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromArgumentAndLongestTriviaFromAncestors" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromArgumentAndLongestTriviaFromAncestors_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromArgumentAndLongestTriviaFromArgumentList" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromArgumentAndLongestTriviaFromArgumentList_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromCodeLineWithoutDot" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromCodeLineWithoutDot_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromAssertEqual" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromAssertEqual_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromLongestTriviaOfAncestorBodyStatement" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromLongestTriviaOfAncestorBodyStatement_Then_Expected", 0)]
        [InlineData("106_InvocationExpressionParametersTrivia", new[] { "Foo", "ShouldBeTriviaFromLongestTriviaOfExpressionBody" }, SyntaxKind.Argument, 0, List2ElementsMultiline, "106_InvocationExpressionParametersTrivia_ShouldBeTriviaFromLongestTriviaOfExpressionBody_Then_Expected", 0)]
        public async Task ExpressionRewriterTest(string inputCodeResourceName, string[] declarationPath, SyntaxKind expressionKind, int expressionIndex, string initializeCode, string expectedResourceFileName, int? methodBodyStatementIndex = null)
            => await RewriteTestImplementation(
                        async () => new ExpressionRewriter(declarationPath, (int)expressionKind, expressionIndex, await ResourceOrSelfAsync(initializeCode), methodBodyStatementIndex),
                        inputCodeResourceName,
                        expectedResourceFileName);

        [Theory]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethod" }, "InlineData", 0, 0, Const123, "120_Attributes_NewCode_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethod" }, "InlineData", 0, 1, Const123, "120_Attributes_NewCode01_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethod" }, "InlineData", 1, 3, "true", "120_Attributes_NewCode13_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethod" }, "InlineData", 2, 3, Const123, "120_Attributes_NewCode23_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethod" }, "InlineData", 2, 3, List2ElementsMultiline, "120_Attributes_NewCode23_MultilineValue_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethod" }, "InlineData", 3, 0, Const123, "120_Attributes_NewCode30_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethodAllArgsWithDefaults" }, "InlineData", 0, 0, Const123, "120_Attributes_TestMethodAllArgsWithDefaults_NewCode00_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethodAllArgsWithDefaults" }, "InlineData", 0, 1, "\"test_expected\"", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode01_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethodAllArgsWithDefaults" }, "InlineData", 0, 2, "true", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode02_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethodAllArgsWithDefaults" }, "InlineData", 0, 3, "1", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode03_Then_Expected")]
        [InlineData("120_Attributes", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesTest", "TestMethodAllArgsWithDefaults" }, "InlineData", 0, 4, "nonExistingToken", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode04_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 0, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode00_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 1, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode10_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 2, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode20_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 3, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode30_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 4, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode40_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 5, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode50_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 6, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode60_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 7, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode70_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 8, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode80_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 9, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode90_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 10, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode100_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 11, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode110_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethod" }, "InlineData", 12, 0, List2ElementsMultiline, "125_AttributesWithIndentedArgs_NewCode120_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 0, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode01_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 1, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode11_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 2, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode21_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 3, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode31_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 4, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode41_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 5, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode51_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 6, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode61_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 7, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode71_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 8, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode81_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 9, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode91_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 10, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode101_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 11, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode111_Then_Expected")]
        [InlineData("125_AttributesWithIndentedArgs", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "AttributesWithIndentedArgsTest", "TestMethodWithExtraArg" }, "InlineData", 12, 1, List2ElementsMultiline, "125_AttributesWithIndentedArgsAndExtraArg_NewCode121_Then_Expected")]
        public async Task AttributeRewriterTest(string inputCodeResourceName, string[] methodPath, string attributeName, int attributeIndex, int attributeParameterIndex, string initializeCode, string expectedResourceFileName)
            => await RewriteTestImplementation(
                        async () => new AttributeRewriter(methodPath, attributeName, attributeIndex, attributeParameterIndex, await ResourceOrSelfAsync(initializeCode)),
                        inputCodeResourceName,
                        expectedResourceFileName);

        [Theory]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 0, 0, Const111Quoted, "130_EnumerableMethod_BaseCase00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 1, 1, Const111Quoted, "130_EnumerableMethod_BaseCase11_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 2, 2, Const111Quoted, "130_EnumerableMethod_BaseCase22_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 0, 0, Const111Quoted, "130_EnumerableMethod_VariedInitializers00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 0, 2, Const111Quoted, "130_EnumerableMethod_VariedInitializers02_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 1, 0, Const111Quoted, "130_EnumerableMethod_VariedInitializers10_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 1, 2, Const111Quoted, "130_EnumerableMethod_VariedInitializers12_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 1, 3, Const111Quoted, "130_EnumerableMethod_VariedInitializers13_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 2, 0, Const111Quoted, "130_EnumerableMethod_VariedInitializers20_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 2, 2, Const111Quoted, "130_EnumerableMethod_VariedInitializers22_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 3, 0, Const111Quoted, "130_EnumerableMethod_VariedInitializers30_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 1, 3, Const111Quoted, "130_EnumerableMethod_VariedInitializers13_DefaultParams_Then_Expected", new[] { "default(int)", "default(int)", "default(int)", "default(int)" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 2, 3, Const111Quoted, "130_EnumerableMethod_VariedInitializers23_DefaultParams_Then_Expected", new[] { "default(int)", "default(int)", "default(int)", "default(int)" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataProperty" }, 0, 0, Const111Quoted, "130_EnumerableMethod_GetTestDataProperty_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataPropertyArrow" }, 0, 0, Const111Quoted, "130_EnumerableMethod_GetTestDataPropertyArrow_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataPropertyGetExplicit" }, 0, 0, Const111Quoted, "130_EnumerableMethod_GetTestDataPropertyGetExplicit_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataViaEnumerableTuple" }, 1, 2, Const111Quoted, "130_EnumerableMethod_Tuple12_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataViaEnumerableTuple" }, 0, 3, Const111Quoted, "130_EnumerableMethod_Tuple03_Then_Expected", new[] { "default(int)", "default(int)", "default(int)", "default(int)" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryData" }, 3, 2, Const111Quoted, "130_EnumerableMethod_TheoryData32_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryData" }, 0, 3, Const111Quoted, "130_EnumerableMethod_TheoryData03_Then_Expected", new[] { "default(int)", "default(int)", "default(int)", "default(int)" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreation" }, 1, 2, List2ElementsMultiline, "130_EnumerableMethod_TheoryDataImplicitObjectCreation12_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithEnumerableCells" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethod_TheoryDataWithEnumerableCells00_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithEnumerableCells" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethod_TheoryDataWithEnumerableCells01_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithEnumerableCells" }, 1, 0, List2ElementsMultiline, "130_EnumerableMethod_TheoryDataWithEnumerableCells10_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithEnumerableCells" }, 1, 1, List2ElementsMultiline, "130_EnumerableMethod_TheoryDataWithEnumerableCells11_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData3D" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethod_GetTestData3D00_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData3D" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethod_GetTestData3D01_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData3D" }, 0, 2, List2ElementsMultiline, "130_EnumerableMethod_GetTestData3D02_Then_Expected")]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 3, 0, List2ElementsMultiline, "130_EnumerableMethod_GetTestData30_Then_Expected", new[] { "default(int)" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 4, 1, List2ElementsMultiline, "130_EnumerableMethod_GetTestData41_Then_Expected", new[] { "default(int)", "-5" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 5, 1, List2ElementsMultiline, "130_EnumerableMethod_GetTestData51_Then_Expected", new[] { "default(int)", "6" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "ClassDataMethod" }, 1, 0, List2ElementsMultiline, "130_EnumerableMethod_ClassDataMethod10_Then_Expected", new[] { "default(int)", "6" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "ClassDataMethod" }, 2, 1, List2ElementsMultiline, "130_EnumerableMethod_ClassDataMethod21_Then_Expected", new[] { "default(int)", "6" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "ClassDataMethod" }, 3, 0, List2ElementsMultiline, "130_EnumerableMethod_ClassDataMethod30_Then_Expected", new[] { "default(int)", "6" })]
        [InlineData("130_EnumerableMethod", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "ClassDataMethod" }, 4, 1, List2ElementsMultiline, "130_EnumerableMethod_ClassDataMethod41_Then_Expected", new[] { "default(int)", "6" })]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestData00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestData01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 1, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestData10_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataWithVariedInitializers00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataWithVariedInitializers" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataWithVariedInitializers01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataProperty" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataProperty00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataProperty" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataProperty01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataPropertyArrow" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataPropertyArrow01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataPropertyGetExplicit" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataPropertyGetExplicit00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataViaEnumerableTuple" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataViaEnumerableTuple00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataViaEnumerableTuple" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataViaEnumerableTuple01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryData" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryData00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryData" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryData01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreation" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreation00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreation" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreation01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreationSpecialIndents" }, 0, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents00_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreationSpecialIndents" }, 0, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents01_NewCode_Then_Expected")]
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreationSpecialIndents" }, 3, 0, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents30_NewCode_Then_Expected")]
        //The result formatting seems strange, but totally corresponds to input ugly formatting.
        [InlineData("130_EnumerableMethodIndent", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataImplicitObjectCreationSpecialIndents" }, 3, 1, List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents31_NewCode_Then_Expected")]
        [InlineData("135_EnumerableMethodThrow", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestData" }, 0, 0, Const111Quoted, "135_EnumerableMethodThrow_NewCode_Then_Expected")]
        [InlineData("135_EnumerableMethodThrow", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataArrow" }, 0, 0, Const111Quoted, "135_EnumerableMethodThrow_NewCode_Then_Expected")]
        [InlineData("135_EnumerableMethodThrow", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataProperty" }, 0, 0, Const111Quoted, "135_EnumerableMethodThrow_NewCode_Then_Expected")]
        [InlineData("135_EnumerableMethodThrow", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataPropertyArrow" }, 0, 0, Const111Quoted, "135_EnumerableMethodThrow_NewCode_Then_Expected")]
        [InlineData("135_EnumerableMethodThrow", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "GetTestDataPropertyGetExplicit" }, 0, 0, Const111Quoted, "135_EnumerableMethodThrow_NewCode_Then_Expected")]
        [InlineData("135_EnumerableMethodThrow", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryData" }, 0, 0, Const111Quoted, "135_EnumerableMethodThrow_NewCode_Then_Expected")]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestData" }, 0, 1, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData01_NewCode_Then_Expected")]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestData" }, 1, 2, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData12_NewCode_Then_Expected")]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestData" }, 2, 0, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData20_NewCode_Then_Expected")]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestData" }, 3, 0, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData30_NewCode_Then_Expected", new[] { "default(int)" })]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestData" }, 4, 1, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData41_NewCode_Then_Expected", new[] { "default(int)", "\"test\"" })]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestDataPropertyGetExplicit" }, 0, 1, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestDataPropertyGetExplicit01_NewCode_Then_Expected")]
        [InlineData("140_EnumerableMethodYieldReturn", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodYieldReturnClass", "GetTestDataViaEnumerableTuple" }, 3, 2, List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestDataViaEnumerableTuple32_NewCode_Then_Expected")]
        [InlineData("150_EnumerableMethodTheoryDataComments", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithComments" }, 0, 2, List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_EnumerableMethodClass02_NewCode_Then_Expected")]
        [InlineData("150_EnumerableMethodTheoryDataComments", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithComments" }, 1, 1, List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_EnumerableMethodClass11_NewCode_Then_Expected")]
        [InlineData("150_EnumerableMethodTheoryDataComments", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithRegions" }, 1, 2, List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_TheoryDataWithRegions12_NewCode_Then_Expected")]
        [InlineData("150_EnumerableMethodTheoryDataComments", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithRegions" }, 3, 2, List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_TheoryDataWithRegions32_NewCode_Then_Expected")]
        [InlineData("150_EnumerableMethodTheoryDataComments", new[] { "Scand.StormPetrel.Rewriter.Test.Resource", "EnumerableMethodClass", "TheoryDataWithRegions" }, 4, 0, List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_TheoryDataWithRegions40_NewCode_Then_Expected")]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowArray" }, 0, 0, Const123, "160_TheoryContractTest_TheoryDataRowIsArray_Then_Expected")]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowImplicitArray" }, 1, 2, Const123, "160_TheoryContractTest_TheoryDataRowImplicitArray_Then_Expected")]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowMethodYieldReturn" }, 0, 1, Const123, "160_TheoryContractTest_MethodYieldReturnTheoryDataRow_Then_Expected")]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowImplicitObjectArray" }, 0, 1, List2ElementsMultiline, "160_TheoryContractTest_TheoryDataRowImplicitObjectArray_Then_Expected")]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowImplicitArrayAndDefaultParameterValues" }, 0, 1, Const123, "160_TheoryContractTest_TheoryDataRowImplicitArrayAndDefaultParameterValues01_Then_Expected", new[] { "default", "default" })]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowImplicitArrayAndDefaultParameterValues" }, 1, 2, Const123, "160_TheoryContractTest_TheoryDataRowImplicitArrayAndDefaultParameterValues12_Then_Expected", new[] { "default", "default", "default" })]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowMethodYieldReturnAndDefaultParameterValues" }, 0, 0, Const123, "160_TheoryContractTest_TheoryDataRowMethodYieldReturnAndDefaultParameterValues00_Then_Expected", new[] { "default", "default" })]
        [InlineData("160_TheoryContractTest", new[] { "TheoryContractTest", "TheoryDataRowMethodYieldReturnAndDefaultParameterValues" }, 1, 1, Const123, "160_TheoryContractTest_TheoryDataRowMethodYieldReturnAndDefaultParameterValues11_Then_Expected", new[] { "default", "default" })]
        [InlineData("170_MissedCellsInTestDataRow", new[] { "DataSource", "GetRows" }, 0, 0, List2ElementsMultiline, "170_MissedCellsInTestDataRow_GetRows00_Then_Expected", new[] { "default(int)" })]
        [InlineData("170_MissedCellsInTestDataRow", new[] { "DataSource", "GetRows" }, 0, 1, List2ElementsMultiline, "170_MissedCellsInTestDataRow_GetRows01_Then_Expected", new[] { "default(int)" })]
        [InlineData("170_MissedCellsInTestDataRow", new[] { "DataSource", "GetRows" }, 1, 1, List2ElementsMultiline, "170_MissedCellsInTestDataRow_GetRows11_Then_Expected", new[] { "default(int)", "default(string)" })]
        [InlineData("170_MissedCellsInTestDataRow", new[] { "DataSource", "GetRows" }, 1, 1, List2ElementsMultiline, "170_MissedCellsInTestDataRow_GetRows11_InsufficientDefaultExpressions_Then_Expected", new[] { "default(int)" })]
        public async Task EnumerableResultRewriterTest(string inputCodeResourceName, string[] methodPath, int resultRowIndex, int resultColumnIndex, string initializeCode, string expectedResourceFileName, string[]? rowDefaultExpressions = null)
            => await RewriteTestImplementation(
                        async () => new EnumerableResultRewriter(methodPath, resultRowIndex, resultColumnIndex, await ResourceOrSelfAsync(initializeCode), rowDefaultExpressions),
                        inputCodeResourceName,
                        expectedResourceFileName);

        private static Assembly GetAssembly() => Assembly.GetAssembly(typeof(MainTest))
                ?? throw new InvalidOperationException("Assembly with MainTest must not be null at this point");
        private static async Task<string> ReadResourceAsync(string resourceFileName)
        {
            using var stream = GetAssembly().GetManifestResourceStream(typeof(MainTest), $"Resource.{resourceFileName}")
                ?? throw new InvalidOperationException("Manifest Resource Stream must not be null at this point");
            using var streamReader = new StreamReader(stream);
            return await streamReader.ReadToEndAsync();
        }
        private static async Task<string> ResourceOrSelfAsync(string initializeCode)
        {
            if (initializeCode.StartsWith("file:", StringComparison.Ordinal))
            {
                initializeCode = await ReadResourceAsync($"{initializeCode.Replace("file:", "", StringComparison.Ordinal)}.cs");
            }
            return initializeCode;
        }

        /// <summary>
        /// Executes <see cref="CSharpSyntaxRewriterExtension.Rewrite(CSharpSyntaxRewriter, Stream, Stream)"/> and <see cref="CSharpSyntaxRewriterExtension.RewriteAsync(CSharpSyntaxRewriter, Stream, Stream)"/> for the rewriter.
        /// </summary>
        /// <param name="rewriterFactory">Some rewriters like <see cref="EnumerableResultRewriter"/> can change its state while rewriting.
        /// So, use new instance for both Rewrite and RewriteAsync method calls via <paramref name="rewriterFactory"/>.</param>
        /// <param name="inputCodeResourceName"></param>
        /// <param name="expectedResourceFileName"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private static async Task RewriteTestImplementation(Func<Task<CSharpSyntaxRewriter>> rewriterFactory, string inputCodeResourceName, string expectedResourceFileName)
        {
            //Arrange
            using var inStream = GetAssembly().GetManifestResourceStream(typeof(MainTest), $"Resource.{inputCodeResourceName}.cs")
                ?? throw new InvalidOperationException("Manifest Resource Stream must not be null at this point"); ;
            expectedResourceFileName = $"{expectedResourceFileName.AsSpan()}.cs";
            var expectedCode = await ReadResourceAsync(expectedResourceFileName);

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
            stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromSeconds(10), "Typical execution time is less than ~3 seconds for worst case on `11th Gen Intel(R) Core(TM) i7-1165G7 @ 2.80GHz`");

            async Task<string> GetActualCode(bool isAsyncOtherwiseSync)
            {
                using var outStream = new MemoryStream();
                inStream.Position = 0;

                var rewriter = await rewriterFactory();
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
                return await actualStreamReader.ReadToEndAsync();
            }
        }
    }
}
