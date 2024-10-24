﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.TargetProject;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Scand.StormPetrel.Generator
{
    internal partial class VarHelper
    {
        private const RegexOptions RegexOptionsDefault = RegexOptions.Compiled | RegexOptions.CultureInvariant;
        private readonly (Regex ActualRegex, Regex ExpectedRegex)[] _varNameRegexPairs;
        private static readonly Regex TypeOfRegex = new Regex(@"typeof\s*\(", RegexOptionsDefault);

        #region Source Names
        /// <summary>
        /// For xUnit, NUnit, MSTest
        /// </summary>
        private static readonly string[] SourceMemberArgumentNames = new[] { "memberName", "sourceName", "dynamicDataSourceName" };
        private static readonly string[] SourceTypeArgumentNamesForNunitMstest = new[] { "sourceType", "dynamicDataDeclaringType" };
        private static readonly string[] SourceParameterNamesForXunitNunit = new[] { "parameters", "methodParams" };
        #endregion

        public VarHelper(TestVariablePairConfig[] testVariablePairConfigs)
        {
            _varNameRegexPairs = testVariablePairConfigs
                                    .Select(a => (new Regex(a.ActualVarNameTokenRegex, RegexOptionsDefault), new Regex(a.ExpectedVarNameTokenRegex, RegexOptionsDefault)))
                                    .ToArray();
        }
        public List<VarPairInfo> GetVarPairs(MethodDeclarationSyntax method)
        {
            int index = -1;
            int expectedVarParameterIndex = -1;
            var regexToVarNameToStatementInfo = new Dictionary<Regex, Dictionary<string, VarInfo>>(_varNameRegexPairs.Length);
            VarInfo varInfo = null;
            var testCaseAttributeName = MethodHelper
                                            .GetTestCaseAttributeNames(method)
                                            .FirstOrDefault();
            var testCaseSourceAttribute = MethodHelper
                                            .GetTestCaseSourceAttribute(method);

            var nonExpectedParameterInfo = method
                                                .ParameterList
                                                .Parameters
                                                .Where(x => !_varNameRegexPairs.Any(y => y.ExpectedRegex.IsMatch(x.Identifier.Text)))
                                                .Select(x => (Type: x.Type.GetText().ToString(), Name: x.Identifier.Text))
                                                .ToArray();

            foreach (var statement in method
                                        .ParameterList
                                        .Parameters
                                        .Cast<object>()
                                        .Union(method.Body.Statements))
            {
                index++;
                foreach (var (actualRegex, expectedRegex) in _varNameRegexPairs)
                {
                    string varName = null;
                    Regex regex = null;
                    if (statement is ExpressionStatementSyntax expressionStatement
                                                && expressionStatement.Expression is AssignmentExpressionSyntax assignmentExpression
                                                && assignmentExpression.OperatorToken.ValueText == "="
                                                && assignmentExpression.Left is IdentifierNameSyntax identifierNameSyntax)
                    {
                        varName = identifierNameSyntax.Identifier.ValueText;
                        regex = actualRegex.IsMatch(varName)
                                    ? actualRegex
                                    : expectedRegex.IsMatch(varName)
                                        ? expectedRegex
                                        : null;
                        varInfo = new VarInfo();
                        if (!CheckIsInvocationExpressionAndUpdateVarInfo(assignmentExpression.Right, varInfo))
                        {
                            varInfo.RewriterKind = RewriterKind.Assignment;
                        }

                        varInfo.Path = SyntaxNodeHelper.GetValuePath(identifierNameSyntax);
                    }
                    else if (statement is LocalDeclarationStatementSyntax localDeclarationStatement)
                    {
                        var localVarInfo = localDeclarationStatement
                                            .Declaration
                                            .Variables
                                            .Select(a => (Name: a.Identifier.Text, Variable: a))
                                            .Select(a => (IsActualMatch: actualRegex.IsMatch(a.Name), IsExpectedMatch: expectedRegex.IsMatch(a.Name), VarName: a.Name, a.Variable))
                                            .FirstOrDefault(a => a.IsActualMatch || a.IsExpectedMatch);
                        if (localVarInfo != default)
                        {
                            varInfo = new VarInfo();
                            if (CheckIsInvocationExpressionAndUpdateVarInfo(localVarInfo.Variable.Initializer?.Value, varInfo))
                            {
                                //No additional logic, all stuff is done in the condition call
                            }
                            else if (localVarInfo.Variable.Initializer?.Value is IdentifierNameSyntax
                                        || localVarInfo.Variable.Initializer?.Value is MemberAccessExpressionSyntax)
                            {
                                varInfo.RewriterKind = RewriterKind.PropertyExpression;
                                varInfo.InvocationExpression = localVarInfo.Variable.Initializer?.Value.ToString();
                            }
                            else
                            {
                                varInfo.RewriterKind = RewriterKind.Declaration;
                            }

                            varName = localVarInfo.VarName;
                            regex = localVarInfo.IsActualMatch ? actualRegex : expectedRegex;
                            varInfo.Path = SyntaxNodeHelper.GetValuePath(localDeclarationStatement);
                        }
                    }
                    else if (statement is ParameterSyntax parameter)
                    {
                        index--;
                        expectedVarParameterIndex++;
                        if (expectedRegex.IsMatch(parameter.Identifier.Text))
                        {
                            regex = expectedRegex;
                            varName = parameter.Identifier.Text;
                            varInfo = new VarInfo
                            {
                                Path = SyntaxNodeHelper.GetValuePath(method),
                            };
                            if (!string.IsNullOrEmpty(testCaseAttributeName))
                            {
                                varInfo.RewriterKind = RewriterKind.Attribute;
                                varInfo.ExpectedVarParameterInfo = new VarParameterInfo
                                {
                                    ParameterIndex = expectedVarParameterIndex,
                                    TestCaseAttributeName = testCaseAttributeName,
                                };
                            }
                            else if (testCaseSourceAttribute != null)
                            {
                                GetTestCaseSourceExpressions(method, testCaseSourceAttribute, out var testCaseSourceExpression, out var testCaseSourcePathExpression);
                                varInfo.RewriterKind = RewriterKind.EnumerableResultRewriter;
                                varInfo.ExpectedVarParameterTestCaseSourceInfo = new VarParameterTestCaseSourceInfo
                                {
                                    NonExpectedParameterTypes = nonExpectedParameterInfo.Select(x => x.Type).ToArray(),
                                    NonExpectedParameterNames = nonExpectedParameterInfo.Select(x => x.Name).ToArray(),
                                    ParameterIndex = expectedVarParameterIndex,
                                    TestCaseSourceExpression = testCaseSourceExpression,
                                    TestCaseSourcePathExpression = testCaseSourcePathExpression,
                                };
                            }
                        }
                    }
                    if (regex != null)
                    {
                        if (!regexToVarNameToStatementInfo.TryGetValue(regex, out var dict))
                        {
                            dict = new Dictionary<string, VarInfo>();
                            regexToVarNameToStatementInfo.Add(regex, dict);
                        }
                        varInfo.StatementIndex = index;
                        dict[varName] = varInfo;
                    }
                }
            }
            var varPairInfo = new List<VarPairInfo>();
            foreach (var (actualRegex, expectedRegex) in _varNameRegexPairs)
            {
                if (!regexToVarNameToStatementInfo.TryGetValue(actualRegex, out var actualVarToInfo)
                        || !regexToVarNameToStatementInfo.TryGetValue(expectedRegex, out var expectedVarToInfo))
                {
                    continue;
                }
                var actualVarToInfoOrdered = actualVarToInfo
                                            .OrderBy(a => a.Value.StatementIndex)
                                            .ToList();
                foreach (var expectedKeyValue in expectedVarToInfo
                                            .OrderBy(a => a.Value.ExpectedVarParameterInfo == null
                                                            ? int.MaxValue
                                                            : a.Value.ExpectedVarParameterInfo.ParameterIndex)
                                            .ThenBy(a => a.Value.StatementIndex))
                {
                    var actualKeyValue = actualVarToInfoOrdered
                                        .FirstOrDefault(a => expectedRegex.Replace(expectedKeyValue.Key, "") == actualRegex.Replace(a.Key, ""));
                    if (actualKeyValue.Key == null && actualKeyValue.Value == null)
                    {
                        continue;
                    }
                    var expressionPath = expectedKeyValue.Value.InvocationExpression?.Split('.');
                    var expressionPathStormPetrel = expressionPath?.ToArray() ?? Array.Empty<string>();
                    if (expressionPath?.Length == 1)
                    {
                        var path = expectedKeyValue.Value.Path;
                        expressionPath = path
                                            .Take(path.Length - 2)
                                            .Concat(Enumerable.Repeat(expressionPath[0], 1))
                                            .ToArray();
                    }

                    if (expressionPath != null)
                    {
                        var methodNameIndex = expressionPath.Length - 1;
                        //Select any method. Appropriate overload should be selected by method arguments
                        expressionPath[methodNameIndex] = expressionPath[methodNameIndex]
                            + (expectedKeyValue.Value.RewriterKind != RewriterKind.PropertyExpression ? "[*]" : "");
                    }
                    for (var i = 0; i < 2; i++)
                    {
                        if (expressionPathStormPetrel.Length >= (i + 1))
                        {
                            expressionPathStormPetrel[i] += "StormPetrel";
                        }
                    }
                    varPairInfo.Add(new VarPairInfo
                    {
                        ActualVarName = actualKeyValue.Key,
                        ActualVarPath = actualKeyValue.Value.Path,
                        ExpectedVarName = expectedKeyValue.Key,
                        ExpectedVarPath = expectedKeyValue.Value.Path,
                        ExpectedVarInvocationExpressionPath = expressionPath,
                        ExpectedVarInvocationExpressionStormPetrel = string.Join(".", expressionPathStormPetrel),
                        ExpectedVariableInvocationExpressionArgs = expectedKeyValue.Value.InvocationExpressionArgs,
                        ExpectedVarParameterInfo = expectedKeyValue.Value.ExpectedVarParameterInfo,
                        ExpectedVarParameterTestCaseSourceInfo = expectedKeyValue.Value.ExpectedVarParameterTestCaseSourceInfo,
                        RewriterKind = expectedKeyValue.Value.RewriterKind,
                        StatementIndex = Math.Max(expectedKeyValue.Value.StatementIndex, actualKeyValue.Value.StatementIndex),
                    });
                    actualVarToInfoOrdered.Remove(actualKeyValue);
                }
                regexToVarNameToStatementInfo =
                    regexToVarNameToStatementInfo
                        .ToDictionary(a => a.Key, a => a.Value
                                                            //filter out used names
                                                            .Where(b => !varPairInfo.Any(c => b.Key == c.ActualVarName || b.Key == c.ExpectedVarName))
                                                            .ToDictionary(b => b.Key, b => b.Value));
            }
            return varPairInfo;

            bool CheckIsInvocationExpressionAndUpdateVarInfo(ExpressionSyntax expression, VarInfo v)
            {
                if (expression is InvocationExpressionSyntax invocationExpression
                    && (invocationExpression.Expression.IsKind(SyntaxKind.IdentifierName) || invocationExpression.Expression.IsKind(SyntaxKind.SimpleMemberAccessExpression)))
                {
                    v.RewriterKind = RewriterKind.MethodExpression;
                    v.InvocationExpression = invocationExpression.Expression.ToString();
                    v.InvocationExpressionArgs = invocationExpression.ArgumentList;
                    return true;
                }
                return false;
            }
        }

        private static void GetTestCaseSourceExpressions(MethodDeclarationSyntax method, AttributeSyntax testCaseSourceAttribute, out string testCaseSourceExpression, out string testCaseSourcePathExpression)
        {
            var testCaseSourceExpressionSb = new StringBuilder();
            var testCaseSourcePathExpressionSb = new StringBuilder();
            var attributeName = testCaseSourceAttribute.Name.ToString();
            if (attributeName == "MemberData" || attributeName == "TestCaseSource" || attributeName == "DynamicData")
            {
                bool isXunit = attributeName == "MemberData";
                bool isMstest = attributeName == "DynamicData";
                testCaseSourceExpressionSb.Append("Scand.StormPetrel.Rewriter.DataSourceHelper.Enumerate(");
                testCaseSourcePathExpressionSb.Append("Scand.StormPetrel.Rewriter.DataSourceHelper.GetPath(");
                var args = testCaseSourceAttribute.ArgumentList.Arguments;
                var memberTypeArg = isXunit
                                            ? args
                                                .Where(x => x.NameEquals != null && x.NameEquals.Name.Identifier.Text == "MemberType")
                                                .SingleOrDefault()
                                            : args
                                                .Where(x => x.NameEquals == null)
                                                .Where(x => x.NameColon == null && x.Expression != null && TypeOfRegex.IsMatch(x.Expression.ToString())
                                                                || x.NameColon != null && SourceTypeArgumentNamesForNunitMstest.Contains(x.NameColon.Name.Identifier.Text))
                                                .FirstOrDefault();
                var memberTypeExpression = memberTypeArg?.Expression.ToString();
                if (string.IsNullOrEmpty(memberTypeExpression))
                {
                    var classNode = method
                                        .Ancestors()
                                        .Cast<ClassDeclarationSyntax>()
                                        .FirstOrDefault();
                    if (classNode == null)
                    {
                        memberTypeExpression = "Member Type is not found";
                    }
                    else
                    {
                        memberTypeExpression = "typeof(" + classNode.Identifier.Text + ")";
                    }
                }
                testCaseSourceExpressionSb.Append(memberTypeExpression);
                testCaseSourcePathExpressionSb.Append(memberTypeExpression);
                var memberNameArg = args
                                        .Where(x => x != memberTypeArg
                                                        && (x.NameColon == null || SourceMemberArgumentNames.Contains(x.NameColon.Name.Identifier.Text)))
                                        .FirstOrDefault();
                if (memberNameArg != null)
                {
                    testCaseSourceExpressionSb
                        .Append(", ")
                        .Append(memberNameArg.Expression.ToString());
                    testCaseSourcePathExpressionSb
                        .Append(", ")
                        .Append(memberNameArg.Expression.ToString());
                }
                foreach (var arg in args.Where(x => !isMstest
                                                        && x != memberTypeArg
                                                        && x != memberNameArg
                                                        && x.NameEquals == null
                                                        && (x.NameColon == null
                                                                || SourceParameterNamesForXunitNunit.Contains(x.NameColon.Name.Identifier.Text))))
                {
                    testCaseSourceExpressionSb
                        .Append(", ")
                        .Append(arg.Expression.GetText().ToString());
                }
            }
            else if (attributeName == "ClassData")
            {
                var classExpression = testCaseSourceAttribute
                                        .ArgumentList
                                        .Arguments
                                        .Where(x => x.NameColon == null || x.NameColon.Name.Identifier.Text == "@class")
                                        .Select(x => x.Expression.ToString())
                                        .FirstOrDefault();
                if (classExpression != null)
                {
                    var className = TypeOfRegex.Replace(classExpression, "");
                    className = className.Replace(")", "");
                    testCaseSourceExpressionSb.Append("new " + className + "(");
                    testCaseSourcePathExpressionSb
                        .Append("Scand.StormPetrel.Rewriter.DataSourceHelper.GetEnumerableStaticMemberPath(")
                        .Append(classExpression);
                }
            }
            testCaseSourceExpressionSb.Append(')');
            testCaseSourcePathExpressionSb.Append(')');
            testCaseSourceExpression = testCaseSourceExpressionSb.ToString();
            testCaseSourcePathExpression = testCaseSourcePathExpressionSb.ToString();
        }
    }
}