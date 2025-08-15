using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
using Scand.StormPetrel.Generator.ExtraContextInternal;
using Scand.StormPetrel.Generator.TargetProject;
using Scand.StormPetrel.Shared;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        private static readonly string[] SourceParameterNamesForXunitNunit = new[] { "parameters", "methodParams", "arguments" };
        #endregion

        public VarHelper(TestVariablePairConfig[] testVariablePairConfigs)
        {
            _varNameRegexPairs = testVariablePairConfigs
                                    .Select(a => (string.IsNullOrEmpty(a.ActualVarNameTokenRegex)
                                                      ? null
                                                      : new Regex(a.ActualVarNameTokenRegex, RegexOptionsDefault),
                                                  string.IsNullOrEmpty(a.ExpectedVarNameTokenRegex)
                                                      ? null
                                                      : new Regex(a.ExpectedVarNameTokenRegex, RegexOptionsDefault)))
                                    .ToArray();
        }
        public List<VarPairInfo> GetVarPairs(MethodDeclarationSyntax method)
        {
            IEnumerable<object> bodyStatements = null;
            if (method.Body != null)
            {
                bodyStatements = method.Body.Statements.Select(x => x);
            }
            else if (method.ExpressionBody != null)
            {
                bodyStatements = new[] { method.ExpressionBody };
            }
            else
            {
                return new List<VarPairInfo>(0);
            }

            var statements = method
                            .ParameterList
                            .Parameters
                            .Select(x => (object)x)
                            .Union(bodyStatements);
            var regexToVarNameToStatementInfo = new Dictionary<Regex, Dictionary<string, VarInfo>>(_varNameRegexPairs.Length);
            var expectedExpressionInfo = new ExpectedExpressionInfo();

            CollectVarPairs(method, statements, _varNameRegexPairs, regexToVarNameToStatementInfo, expectedExpressionInfo, null);
            var collectedVarPairInfo = expectedExpressionInfo.GetCollectedPairInfo();
            //We may have more expected identifiers among collected expected expressions
            var expectedIdentifierToInfo = GetExpectedIdentifierToInfo(collectedVarPairInfo);
            if (expectedIdentifierToInfo.Keys.Count != 0)
            {
                var noActualRegex = new Regex("^$", RegexOptionsDefault);
                var extendedRegexPairs = expectedIdentifierToInfo
                                            .Keys
                                            .Select(x => (noActualRegex, new Regex($@"\b{x}\b", RegexOptionsDefault)))
                                            .ToArray();
                CollectVarPairs(method, statements, extendedRegexPairs, regexToVarNameToStatementInfo, expectedExpressionInfo, (varName, varInfo) =>
                {
                    var info = expectedIdentifierToInfo[varName];
                    var pairInfo = info.VarPairInfo;
                    expectedIdentifierToInfo[varName] = (pairInfo, IsVisited: true);
                    pairInfo.ExpectedVarName = varName;
                    pairInfo.ExpectedVarExtraContextInternal = varInfo.ExtraContextInternal;
                    pairInfo.ExpectedVarPath = varInfo.Path;
                    //Preserve below properties to have them calculated by ExpectedExpressionInfo.GetCollectedPairInfo:
                    //pairInfo.StatementIndex, pairInfo.StatementIndexForSubOrder
                });
                var nonVisited = expectedIdentifierToInfo
                                    .Values
                                    .Where(x => !x.IsVisited)
                                    .Select(x => x.VarPairInfo)
                                    .ToImmutableHashSet();
                collectedVarPairInfo = collectedVarPairInfo
                                        .Where(x => !nonVisited.Contains(x))
                                        .ToList();
            }
            List<VarPairInfo> varPairInfo = ToVarPairs(regexToVarNameToStatementInfo);
            return varPairInfo
                    .Union(collectedVarPairInfo)
                    .ToList();
        }

        private List<VarPairInfo> ToVarPairs(Dictionary<Regex, Dictionary<string, VarInfo>> regexToVarNameToStatementInfo)
        {
            var varPairInfo = new List<VarPairInfo>();
            foreach (var (actualRegex, expectedRegex) in _varNameRegexPairs)
            {
                if (actualRegex == null
                        || expectedRegex == null
                        || !regexToVarNameToStatementInfo.TryGetValue(actualRegex, out var actualVarToInfo)
                        || !regexToVarNameToStatementInfo.TryGetValue(expectedRegex, out var expectedVarToInfo))
                {
                    continue;
                }
                var actualVarToInfoOrdered = actualVarToInfo
                                            .OrderBy(a => a.Value.StatementIndex)
                                            .ToList();
                foreach (var expectedKeyValue in expectedVarToInfo
                                            .OrderBy(a => a.Value.ExtraContextInternal is AttributeContextInternal attributeContext
                                                            ? attributeContext.PartialExtraContext.ParameterIndex
                                                            : int.MaxValue)
                                            .ThenBy(a => a.Value.StatementIndex))
                {
                    var actualKeyValue = actualVarToInfoOrdered
                                        .FirstOrDefault(a => expectedRegex.Replace(expectedKeyValue.Key, "") == actualRegex.Replace(a.Key, ""));
                    if (actualKeyValue.Key == null && actualKeyValue.Value == null)
                    {
                        continue;
                    }
                    varPairInfo.Add(new VarPairInfo
                    {
                        ActualVarName = actualKeyValue.Key,
                        ActualVarPath = actualKeyValue.Value.Path,
                        ExpectedVarName = expectedKeyValue.Key,
                        ExpectedVarPath = expectedKeyValue.Value.Path,
                        StatementIndex = Math.Max(expectedKeyValue.Value.StatementIndex, actualKeyValue.Value.StatementIndex),
                        ExpectedVarExtraContextInternal = expectedKeyValue.Value.ExtraContextInternal,
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
        }

        private void CollectVarPairs(MethodDeclarationSyntax method,
            IEnumerable<object> statements, (Regex ActualRegex, Regex ExpectedRegex)[] varNameRegexPairs,
            Dictionary<Regex, Dictionary<string, VarInfo>> regexToVarNameToStatementInfo, ExpectedExpressionInfo expectedExpressionInfo,
            Action<string, VarInfo> newVarNameInfoHandler)
        {
            foreach (var (actualRegex, expectedRegex) in varNameRegexPairs)
            {
                int indexOfBodyStatement = -1;
                int expectedVarParameterIndex = -1;
                foreach (var statement in statements)
                {
                    var isBodyStatement = !(statement is ParameterSyntax);
                    if (isBodyStatement)
                    {
                        indexOfBodyStatement++;
                    }
                    else
                    {
                        expectedVarParameterIndex++;
                    }
                    string varName = null;
                    VarInfo varInfo = null;
                    Regex regex = null;
                    if (expectedRegex == null)
                    {
                        if (isBodyStatement)
                        {
                            var varPairs = ToVarPairs(regexToVarNameToStatementInfo);
                            var expectedVarNamesToSkip = varPairs
                                                            .Select(x => x.ExpectedVarName)
                                                            .ToImmutableHashSet();
                            expectedExpressionInfo.TryCollectExpectedExpression(statement, method, actualRegex, indexOfBodyStatement, expectedVarNamesToSkip);
                        }
                        continue;
                    }
                    else if (TryParseAsExpressionStatement(statement, out varInfo, out varName))
                    {
                        regex = actualRegex.IsMatch(varName)
                                    ? actualRegex
                                    : expectedRegex.IsMatch(varName)
                                        ? expectedRegex
                                        : null;
                    }
                    else if (TryParseAsLocalDeclarationStatement(statement, actualRegex, expectedRegex, out varInfo, out varName, out regex))
                    {
                        //No additional logic, all stuff is done in the condition call
                    }
                    else if (TryParseAsParameter(statement, method, expectedRegex, expectedVarParameterIndex, out varInfo, out varName))
                    {
                        regex = expectedRegex;
                    }
                    if (regex != null)
                    {
                        if (!regexToVarNameToStatementInfo.TryGetValue(regex, out var dict))
                        {
                            dict = new Dictionary<string, VarInfo>();
                            regexToVarNameToStatementInfo.Add(regex, dict);
                        }
                        varInfo.StatementIndex = indexOfBodyStatement;
                        newVarNameInfoHandler?.Invoke(varName, varInfo);
                        var expectedIdentifierToInfo = GetExpectedIdentifierToInfo(expectedExpressionInfo.GetCollectedPairInfo());
                        bool isCollectedInExpectedExpressions = expectedIdentifierToInfo.ContainsKey(varName);
                        if (!isCollectedInExpectedExpressions)
                        {
                            dict[varName] = varInfo;
                        }
                    }
                }
            }
        }
        private static Dictionary<string, (VarPairInfo VarPairInfo, bool IsVisited)> GetExpectedIdentifierToInfo(IEnumerable<VarPairInfo> collectedVarPairInfo) =>
            collectedVarPairInfo
                .Select(x => (Identifier: string.IsNullOrEmpty(x.ExpectedVarName)
                                            && x.ExpectedVarExtraContextInternal is InvocationExpressionWithEmbeddedExpectedContextInternal contextInternal
                                            && contextInternal.ExpectedExpression is IdentifierNameSyntax identifier
                                            ? identifier.Identifier.Text
                                            : null,
                                VarPairInfo: x))
                .Where(x => x.Identifier != null)
                .ToDictionary(x => x.Identifier, x => (x.VarPairInfo, false));

        private static bool CheckIsInvocationExpressionAndUpdateVarInfo(ExpressionSyntax expression, VarInfo v)
        {
            if (expression is InvocationExpressionSyntax invocationExpression
                && (invocationExpression.Expression.IsKind(SyntaxKind.IdentifierName) || invocationExpression.Expression.IsKind(SyntaxKind.SimpleMemberAccessExpression)))
            {
                v.ExtraContextInternal = new InvocationExpressionContextInternal(invocationExpression.Expression.ToString())
                {
                    MethodArgs = invocationExpression.ArgumentList,
                    PartialExtraContext = new InvocationSourceContext()
                    {
                        MethodInfo = new InvocationSourceMethodInfo()
                        {
                            ArgsCount = invocationExpression.ArgumentList?.Arguments.Count ?? 0,
                        },
                    },
                };
                return true;
            }
            return false;
        }

        private static InitializerContextInternal NewInitializerContextInternal(InitializerContextKind kind) => new InitializerContextInternal
        {
            PartialExtraContext = new InitializerContext
            {
                Kind = kind,
            },
        };

        private static void GetTestCaseSourceExpressions(MethodDeclarationSyntax method, AttributeSyntax testCaseSourceAttribute, out string testCaseSourceExpression, out string testCaseSourcePathExpression)
        {
            var testCaseSourceExpressionSb = new StringBuilder();
            var testCaseSourcePathExpressionSb = new StringBuilder();
            var attributeName = testCaseSourceAttribute.Name.ToString();
            if (SupportedMethodInfo.IsXUnitNonClassDataAttributeForTestCaseSource(attributeName)
                || SupportedMethodInfo.IsNUnitAttributeForTestCaseSource(attributeName)
                || SupportedMethodInfo.MSTestAttributeForTestCaseSource(attributeName))
            {
                bool isXunit = SupportedMethodInfo.IsXUnitNonClassDataAttributeForTestCaseSource(attributeName);
                bool isMstest = SupportedMethodInfo.MSTestAttributeForTestCaseSource(attributeName);
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
            else if (SupportedMethodInfo.IsXUnitClassDataAttributeForTestCaseSource(attributeName))
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

        private static bool TryParseAsExpressionStatement(object statement, out VarInfo varInfo, out string varName)
        {
            varInfo = null;
            varName = null;
            if (statement is ExpressionStatementSyntax expressionStatement
                                                && expressionStatement.Expression is AssignmentExpressionSyntax assignmentExpression
                                                && assignmentExpression.OperatorToken.ValueText == "="
                                                && assignmentExpression.Left is IdentifierNameSyntax identifierNameSyntax)
            {
                varName = identifierNameSyntax.Identifier.ValueText;
                varInfo = new VarInfo();
                if (!CheckIsInvocationExpressionAndUpdateVarInfo(assignmentExpression.Right, varInfo))
                {
                    varInfo.ExtraContextInternal = NewInitializerContextInternal(InitializerContextKind.VariableAssignment);
                }

                varInfo.Path = SyntaxNodeHelper.GetValuePath(identifierNameSyntax);
                return true;
            }
            return false;
        }

        private static bool TryParseAsLocalDeclarationStatement(object statement, Regex actualRegex, Regex expectedRegex, out VarInfo varInfo, out string varName, out Regex regex)
        {
            varInfo = null;
            regex = null;
            varName = null;
            if (statement is LocalDeclarationStatementSyntax localDeclarationStatement)
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
                        varInfo.ExtraContextInternal = new InvocationExpressionContextInternal(localVarInfo.Variable.Initializer.Value.ToString())
                        {
                            PartialExtraContext = new InvocationSourceContext(),
                        };
                    }
                    else
                    {
                        varInfo.ExtraContextInternal = NewInitializerContextInternal(InitializerContextKind.VariableDeclaration);
                    }

                    varName = localVarInfo.VarName;
                    regex = localVarInfo.IsActualMatch ? actualRegex : expectedRegex;
                    varInfo.Path = SyntaxNodeHelper.GetValuePath(localDeclarationStatement);
                    return true;
                }
            }
            return false;
        }

        private bool TryParseAsParameter(object statement, MethodDeclarationSyntax method, Regex expectedRegex, int expectedVarParameterIndex, out VarInfo varInfo, out string varName)
        {
            varInfo = null;
            varName = null;
            if (statement is ParameterSyntax parameter)
            {
                if (expectedRegex.IsMatch(parameter.Identifier.Text))
                {
                    varName = parameter.Identifier.Text;
                    varInfo = new VarInfo
                    {
                        Path = SyntaxNodeHelper.GetValuePath(method),
                    };
                    var testCaseAttributeName = MethodHelper
                                                    .GetTestCaseAttributeNames(method)
                                                    .FirstOrDefault();
                    var testCaseSourceAttribute = MethodHelper
                                                    .GetTestCaseSourceAttribute(method);
                    if (!string.IsNullOrEmpty(testCaseAttributeName))
                    {
                        varInfo.ExtraContextInternal = new AttributeContextInternal
                        {
                            PartialExtraContext = new AttributeContext
                            {
                                ParameterIndex = expectedVarParameterIndex,
                                Name = testCaseAttributeName,
                            },
                        };
                    }
                    else if (testCaseSourceAttribute != null)
                    {
                        var nonExpectedParameterInfo = method
                                                            .ParameterList
                                                            .Parameters
                                                            .Where(x => !_varNameRegexPairs.Any(y => y.ExpectedRegex?.IsMatch(x.Identifier.Text) == true))
                                                            .Select(x => (Type: x.Type.GetText().ToString(), Name: x.Identifier.Text))
                                                            .ToArray();
                        GetTestCaseSourceExpressions(method, testCaseSourceAttribute, out var testCaseSourceExpression, out var testCaseSourcePathExpression);
                        varInfo.ExtraContextInternal = new TestCaseSourceContextInternal
                        {
                            NonExpectedParameterTypes = nonExpectedParameterInfo.Select(x => x.Type).ToArray(),
                            NonExpectedParameterNames = nonExpectedParameterInfo.Select(x => x.Name).ToArray(),
                            TestCaseSourceExpression = testCaseSourceExpression,
                            TestCaseSourcePathExpression = testCaseSourcePathExpression,
                            PartialExtraContext = new TestCaseSourceContext
                            {
                                ColumnIndex = expectedVarParameterIndex,
                            },
                        };
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
