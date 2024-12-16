using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter;
using Scand.StormPetrel.Rewriter.Extension;
using System.Diagnostics;
using System.Globalization;
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
        [InlineData("010_LocalVariable", "Foo;Bla;localVar", EmptyList, "010_LocalVariable_NewCode_Then_Expected")]
        [InlineData("010_LocalVariable", "Foo;Bla;localVar", "file:010_LocalVariable_NewCodeMultiline", "010_LocalVariable_NewCodeMultiline_Then_Expected")]
        [InlineData("010_LocalVariable", "Foo;Bla;localVar", "file:010_LocalVariable_NewCodeMultilineString", "010_LocalVariable_NewCodeMultilineString_Then_Expected")]
        [InlineData("015_LocalVariableWithNamespace", "TestNamespace.SomeSubSpace;Foo;Bla;localVar", EmptyList, "015_LocalVariableWithNamespace_NewCode_Then_Expected")]
        [InlineData("020_LocalVariables", "Foo;Bla;localVar", EmptyList, "020_LocalVariables_NewCode_Then_Expected")]
        [InlineData("020_LocalVariables", "Foo;Bla;localVar", "file:020_LocalVariables_NewCodeMultiline", "020_LocalVariables_NewCodeMultiline_Then_Expected")]
        [InlineData("020_LocalVariables", "Foo;Bla2;localVar", EmptyList, "020_LocalVariables_NewCodeBla2_Then_Expected")]
        [InlineData("030_Properties", "Foo;TestProperty", EmptyList, "030_Properties_NewCode_Then_Expected")]
        [InlineData("030_Properties", "Foo;TestProperty", "file:030_Properties_NewCodeMultiline", "030_Properties_NewCodeMultiline_Then_Expected")]
        [InlineData("030_Properties", "Foo2;TestProperty", "file:030_Properties_NewCodeFoo2", "030_Properties_NewCodeFoo2_Then_Expected")]
        [InlineData("040_MethodArrow", "Foo;TestMethod", EmptyList, "040_MethodArrow_NewCode_Then_Expected")]
        [InlineData("040_MethodArrow", "Foo;TestMethod", "file:040_MethodArrow_NewCodeMultiline", "040_MethodArrow_NewCodeMultiline_Then_Expected")]
        [InlineData("040_MethodArrow", "Foo2;TestMethod", EmptyList, "040_MethodArrow_NewCodeFoo2_Then_Expected")]
        [InlineData("050_Assignment", "Scand.StormPetrel.Rewriter.Resource.Test;GivenExample01FromSpec_ThenOutputDoc;GetData;Docs", "new List<MessageTreeDoc>()", "050_Assignment_NewCode_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("050_Assignment", "Scand.StormPetrel.Rewriter.Resource.Test;GivenExample01FromSpec_ThenOutputDoc;GetData;Docs", "file:050_Assignment_NewCodeMultiline", "050_Assignment_NewCodeMultiline_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("060_LocalVariableWithCommentAndTabs", "Foo;Bla;localVar", "file:060_LocalVariableWithCommentAndTabs_NewCode", "060_LocalVariableWithCommentAndTabs_NewCode_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar", EmptyListWithComment, "070_LocalVariableWithMultipleAssignments_NewCode_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo[0];Bla2[0];localVar", EmptyListWithComment, "070_LocalVariableWithMultipleAssignments_NewCode_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[0]", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment0_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[1]", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment1_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[2]", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2;localVar[2]", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo[1];Bla2;localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo[1];Bla2;localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[1];localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeDeclaration2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[1];localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[2];localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected")]
        [InlineData("070_LocalVariableWithMultipleAssignments", "Foo;Bla2[2];localVar", EmptyList, "070_LocalVariableWithMultipleAssignments_NewCodeAssignment2_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("080_LocalFunction", "Foo;Bla;BlaInBla;localVar", EmptyList, "080_LocalFunction_NewCode_Then_Expected")]
        [InlineData("080_LocalFunction", "Foo;Bla;BlaInBla[1];localVar", EmptyList, "080_LocalFunction_NewCode1_Then_Expected")]
        [InlineData("080_LocalFunction", "Foo;Bla;BlaInBla;localVar", EmptyList, "080_LocalFunction_NewCodeAssignment_Then_Expected", nameof(AssignmentRewriter))]
        [InlineData("080_LocalFunction", "Foo;Bla;localVar", EmptyList, "080_LocalFunction_NewCodeAssignmentInParent_Then_Expected")]
        [InlineData("090_ExpressionRewriter", "Foo;Bla#SimpleAssignmentExpression:0", EmptyList, "090_ExpressionRewriter_NewCode_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ArrowMethod#ArrowExpressionClause:0", "456", "090_ExpressionRewriter_NewCode_ArrowMethod_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ArrowMethodArray#ArrowExpressionClause:0", "file:090_ExpressionRewriter_NewCode_ArrowMethodArray", "090_ExpressionRewriter_NewCode_ArrowMethodArray_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla#LocalDeclarationStatement:0", EmptyList, "090_ExpressionRewriter_NewCode_LocalDeclaration_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[0]#LocalDeclarationStatement:0", EmptyList, "090_ExpressionRewriter_NewCode_LocalDeclaration_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[1]#LocalDeclarationStatement:0", EmptyList, "090_ExpressionRewriter_NewCode_MethodOverload1_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[2]#LocalDeclarationStatement:0", EmptyList, "090_ExpressionRewriter_NewCode_MethodOverload2_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;Bla[3]#SimpleAssignmentExpression:1", EmptyList, "090_ExpressionRewriter_NewCode_MultipleAssignment_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ReturnMethod#ReturnStatement:0", "456", "090_ExpressionRewriter_NewCode_ReturnMethod_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("090_ExpressionRewriter", "Foo;ReturnMethodArray#ReturnStatement:0", "file:090_ExpressionRewriter_NewCode_ReturnMethodArray", "090_ExpressionRewriter_NewCode_ReturnMethodArray_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("100_PatternMatch", "Foo;ArrowMethodWithPatternMatch#SwitchExpressionArm:0", "file:100_PatternMatch_NewCode_ArrowMethod", "100_PatternMatch_NewCode_ArrowMethod_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("100_PatternMatch", "Foo;ArrowMethodWithPatternMatch#SwitchExpressionArm:1", "file:100_PatternMatch_NewCode_ArrowMethodSwitch2", "100_PatternMatch_NewCode_ArrowMethodSwitch2_Then_Expected", nameof(ExpressionRewriter))]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;Expected", Const100, "110_Property_NewCode_Then_Expected")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedArrow", Const100, "110_PropertyArrow_NewCode_Then_Expected")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedGetArrow", Const100, "110_PropertyGetArrow_NewCode_Then_Expected")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedGetSet", Const100, "110_PropertyGetSet_NewCode_Then_Expected")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedReturn", Const100, "110_PropertyReturn_NewCode_Then_Expected")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedReturnField", Const100, "110_PropertyReturnField_NewCode_Then_Expected")]
        [InlineData("110_Property", "Test.Integration.XUnit;UnitTest1Helper;ExpectedReturnMultiple", "file:110_PropertyReturnMultiple_NewCode", "110_PropertyReturnMultiple_NewCode_Then_Expected")]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:0:0", Const123, "120_Attributes_NewCode_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:0:1", Const123, "120_Attributes_NewCode01_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:1:3", "true", "120_Attributes_NewCode13_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:2:3", Const123, "120_Attributes_NewCode23_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethod#InlineData:3:0", Const123, "120_Attributes_NewCode30_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:0", Const123, "120_Attributes_TestMethodAllArgsWithDefaults_NewCode00_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:1", "\"test_expected\"", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode01_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:2", "true", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode02_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:3", "1", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode03_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("120_Attributes", "Scand.StormPetrel.Rewriter.Test.Resource;AttributesTest;TestMethodAllArgsWithDefaults#InlineData:0:4", "nonExistingToken", "120_Attributes_TestMethodAllArgsWithDefaults_NewCode04_Then_Expected", nameof(AttributeRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestData#0:0", Const111Quoted, "130_EnumerableMethod_BaseCase00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestData#1:1", Const111Quoted, "130_EnumerableMethod_BaseCase11_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestData#2:2", Const111Quoted, "130_EnumerableMethod_BaseCase22_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#0:0", Const111Quoted, "130_EnumerableMethod_VariedInitializers00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#0:2", Const111Quoted, "130_EnumerableMethod_VariedInitializers02_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#1:0", Const111Quoted, "130_EnumerableMethod_VariedInitializers10_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#1:2", Const111Quoted, "130_EnumerableMethod_VariedInitializers12_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#1:3", Const111Quoted, "130_EnumerableMethod_VariedInitializers13_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#2:0", Const111Quoted, "130_EnumerableMethod_VariedInitializers20_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#2:2", Const111Quoted, "130_EnumerableMethod_VariedInitializers22_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#3:0", Const111Quoted, "130_EnumerableMethod_VariedInitializers30_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataProperty#0:0", Const111Quoted, "130_EnumerableMethod_GetTestDataProperty_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataPropertyArrow#0:0", Const111Quoted, "130_EnumerableMethod_GetTestDataPropertyArrow_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataPropertyGetExplicit#0:0", Const111Quoted, "130_EnumerableMethod_GetTestDataPropertyGetExplicit_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataViaEnumerableTuple#1:2", Const111Quoted, "130_EnumerableMethod_Tuple12_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryData#3:2", Const111Quoted, "130_EnumerableMethod_TheoryData32_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethod", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreation#1:2", List2ElementsMultiline, "130_EnumerableMethod_TheoryDataImplicitObjectCreation12_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestData#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestData00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestData#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestData01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestData#1:0", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestData10_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataWithVariedInitializers00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataWithVariedInitializers#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataWithVariedInitializers01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataProperty#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataProperty00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataProperty#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataProperty01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataPropertyArrow#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataPropertyArrow01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataPropertyGetExplicit#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataPropertyGetExplicit00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataViaEnumerableTuple#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataViaEnumerableTuple00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;GetTestDataViaEnumerableTuple#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_GetTestDataViaEnumerableTuple01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryData#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryData00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryData#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryData01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreation#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreation00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreation#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreation01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreationSpecialIndents#0:0", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents00_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreationSpecialIndents#0:1", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreationSpecialIndents#3:0", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents30_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        //The result formatting seems strange, but totally corresponds to input ugly formatting.
        [InlineData("130_EnumerableMethodIndent", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataImplicitObjectCreationSpecialIndents#3:1", List2ElementsMultiline, "130_EnumerableMethodIndent_TheoryDataImplicitObjectCreationSpecialIndents31_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("140_EnumerableMethodYieldReturn", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodYieldReturnClass;GetTestData#0:1", List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("140_EnumerableMethodYieldReturn", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodYieldReturnClass;GetTestData#1:2", List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData12_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("140_EnumerableMethodYieldReturn", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodYieldReturnClass;GetTestData#2:0", List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestData20_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("140_EnumerableMethodYieldReturn", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodYieldReturnClass;GetTestDataPropertyGetExplicit#0:1", List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestDataPropertyGetExplicit01_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("140_EnumerableMethodYieldReturn", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodYieldReturnClass;GetTestDataViaEnumerableTuple#3:2", List2ElementsMultiline, "140_EnumerableMethodYieldReturn_GetTestDataViaEnumerableTuple32_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("150_EnumerableMethodTheoryDataComments", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataWithComments#0:2", List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_EnumerableMethodClass02_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("150_EnumerableMethodTheoryDataComments", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataWithComments#1:1", List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_EnumerableMethodClass11_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("150_EnumerableMethodTheoryDataComments", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataWithRegions#1:2", List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_TheoryDataWithRegions12_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("150_EnumerableMethodTheoryDataComments", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataWithRegions#3:2", List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_TheoryDataWithRegions32_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("150_EnumerableMethodTheoryDataComments", "Scand.StormPetrel.Rewriter.Test.Resource;EnumerableMethodClass;TheoryDataWithRegions#4:0", List2ElementsMultiline, "150_EnumerableMethodTheoryDataComments_TheoryDataWithRegions40_NewCode_Then_Expected", nameof(EnumerableResultRewriter))]
        [InlineData("ExpectedInlineInstanceTest01", "Test.Integration.Performance.XUnit.ExpectedInlineInstance;ExpectedInlineInstanceTest01;Test10;expected", "file:ExpectedInlineInstanceTest01_NewCode", "ExpectedInlineInstanceTest01_NewCode_Then_Expected")] //performance
        public async Task RewriteTest(string inputCodeResourceName, string declarationPathSemicolonDelimited, string initializeCode, string expectedResourceFileName, string rewriterClassName = nameof(DeclarationRewriter))
        {
            ArgumentNullException.ThrowIfNull(declarationPathSemicolonDelimited);
            ArgumentNullException.ThrowIfNull(initializeCode);
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
            if (initializeCode.StartsWith("file:", StringComparison.Ordinal))
            {
                initializeCode = await readResourceAsync(assembly, $"{initializeCode.Replace("file:", "", StringComparison.Ordinal)}.cs");
            }
            expectedResourceFileName = $"{expectedResourceFileName.AsSpan()}.cs";
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
                nameof(EnumerableResultRewriter) => GetEnumerableResultRewriter(),
                _ => throw new InvalidOperationException(),
            };

            ExpressionRewriter GetExpressionRewriter()
            {
                var split = declarationPathSemicolonDelimited.Split('#');
                var split2 = split[1].Split(':');
                var kind = Enum.Parse<SyntaxKind>(split2[0]);
                return new(split[0].Split(';'), (int)kind, int.Parse(split2[1], CultureInfo.InvariantCulture), initializeCode);
            }

            AttributeRewriter GetAttributeRewriter()
            {
                var split = declarationPathSemicolonDelimited.Split('#');
                var split2 = split[1].Split(':');
                return new(split[0].Split(';'), split2[0], int.Parse(split2[1], CultureInfo.InvariantCulture), int.Parse(split2[2], CultureInfo.InvariantCulture), initializeCode);
            }

            EnumerableResultRewriter GetEnumerableResultRewriter()
            {
                var split = declarationPathSemicolonDelimited.Split('#');
                var split2 = split[1].Split(':');
                return new(split[0].Split(';'), int.Parse(split2[0], CultureInfo.InvariantCulture), int.Parse(split2[1], CultureInfo.InvariantCulture), initializeCode);
            }
        }
    }
}
