using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    internal class MethodHelper
    {
        public const string StormPetrelUseCaseIndexParameterName = "stormPetrelUseCaseIndex";
        private static bool IsStatic(MethodDeclarationSyntax method)
            => method.Modifiers.Any(x => x.IsKind(SyntaxKind.StaticKeyword));

        private static bool IsStatic(PropertyDeclarationSyntax property)
            => property.Modifiers.Any(x => x.IsKind(SyntaxKind.StaticKeyword));

        public static IEnumerable<string> GetTestAttributeNames(MethodDeclarationSyntax method)
            => GetAttributes(method, x => SupportedMethodInfo.AttributeNames.Contains(x.Name.ToString()))
                .Select(x => x.Name.ToString());
        public static IEnumerable<string> GetTestCaseAttributeNames(MethodDeclarationSyntax method)
            => GetAttributes(method, x => SupportedMethodInfo.AttributeNamesForTestCase.Contains(x.Name.ToString()))
                .Select(x => x.Name.ToString());

        public static AttributeSyntax GetTestCaseSourceAttribute(MethodDeclarationSyntax method)
            => GetAttributes(method, x => SupportedMethodInfo.AttributeNamesForTestCaseSource.Contains(x.Name.ToString()))
                .FirstOrDefault();

        private static IEnumerable<AttributeSyntax> GetAttributes(MethodDeclarationSyntax method, Func<AttributeSyntax, bool> predicate)
            => GetAttributePairs(method.AttributeLists, predicate)
                .Select(x => x.Attribute);

        private static IEnumerable<(AttributeListSyntax AttributeList, AttributeSyntax Attribute)> GetAttributePairs(SyntaxList<AttributeListSyntax> attributeList, Func<AttributeSyntax, bool> predicate) =>
            attributeList
                .SelectMany(a => a.Attributes
                                    .Where(b => predicate(b))
                                    .Select(b => (a, b)));

        public static bool IsExpectedVarInvocationExpressionCandidate(MethodDeclarationSyntax method)
            => IsStatic(method)
                && !GetTestAttributeNames(method).Any()
                && method.ReturnType != null
                && method.ReturnType.ToString() != "void"
                && method.ReturnType.ToString().IndexOf("Task", StringComparison.Ordinal) < 0;


        public static bool IsExpectedVarInvocationExpressionCandidate(PropertyDeclarationSyntax property)
            => IsStatic(property)
                && property.Type != null
                && property.Type.ToString().IndexOf("Task", StringComparison.Ordinal) < 0;

        public static (IEnumerable<MethodDeclarationSyntax> Methods, IEnumerable<PropertyDeclarationSyntax> Properties) GetExpectedVarInvocationExpressions(SyntaxNode root, out bool hasTestMethod)
        {
            var nodes = new List<CSharpSyntaxNode>();
            var hasTestMethodLocal = false;
            GetDescendantNodesOptimized(root, nodes, node =>
            {
                if (node is MethodDeclarationSyntax m)
                {
                    var hasTestAttributes = GetTestAttributeNames(m).Any();
                    if (hasTestAttributes)
                    {
                        hasTestMethodLocal = true;
                    }
                    if (IsExpectedVarInvocationExpressionCandidate(m))
                    {
                        return (m, false);
                    }
                    return (null, false);
                }
                else if (node is PropertyDeclarationSyntax p)
                {
                    if (IsExpectedVarInvocationExpressionCandidate(p))
                    {
                        return (p, false);
                    }
                    return (null, false);
                }
                return (null, true);
            }, false);
            hasTestMethod = hasTestMethodLocal;
            return (FilterAndCast<CSharpSyntaxNode, MethodDeclarationSyntax>(nodes), FilterAndCast<CSharpSyntaxNode, PropertyDeclarationSyntax>(nodes));
        }

        public static List<T> GetDescendantNodesOptimized<T>(SyntaxNode root, Func<SyntaxNode, (T Converted, bool DescendIntoChildren)> nodeConverter, bool stopOnFirstConverted = false) where T : SyntaxNode
        {
            var nodes = new List<T>();
            GetDescendantNodesOptimized(root, nodes, nodeConverter, stopOnFirstConverted);
            return nodes;
        }

        private static void GetDescendantNodesOptimized<T>(SyntaxNode node, List<T> convertedNodes, Func<SyntaxNode, (T Converted, bool DescendIntoChildren)> nodeConverter, bool stopOnFirstConverted) where T : SyntaxNode
        {
            var _ = node.DescendantNodes(nd =>
            {
                if (stopOnFirstConverted && convertedNodes.Count != 0)
                {
                    return false;
                }
                var (converted, descendIntoChildren) = nodeConverter(nd);
                if (converted != null)
                {
                    convertedNodes.Add(converted);
                    if (stopOnFirstConverted)
                    {
                        return false;
                    }
                }
                return descendIntoChildren;
            }).Count();
        }

        private static IEnumerable<T> FilterAndCast<TBase, T>(IEnumerable<TBase> values)
            => values.Where(x => x is T).Cast<T>();

        public static MethodDeclarationSyntax WithStormPetrelTestCaseRelatedStuff(MethodDeclarationSyntax method)
        {
            var newMethod = method;

            var newParameter = SyntaxFactory
                                .Parameter(
                                    SyntaxFactory.List<AttributeListSyntax>(),
                                    SyntaxFactory.TokenList(),
                                    SyntaxFactory.ParseTypeName("int"),
                                    SyntaxFactory.Identifier(StormPetrelUseCaseIndexParameterName),
                                    null);
            var newMethodParameters = newMethod.ParameterList.Parameters.Insert(0, newParameter);
            var newParameterList = newMethod.ParameterList.WithParameters(newMethodParameters);
            newMethod = newMethod.WithParameterList(newParameterList);

            int i = -1;
            var newAttributes = method.AttributeLists;
            do
            {
                i++;
                var (attributeList, attribute) = GetAttributePairs(newAttributes, x => SupportedMethodInfo.AttributeNamesForTestCase.Contains(x.Name.ToString()))
                                                    .Skip(i)
                                                    .FirstOrDefault();

                if (attribute == null)
                {
                    break;
                }
                if (attribute.ArgumentList == null)
                {
                    throw new InvalidOperationException("ArgumentList must not be null at this point");
                }
                var newArg = SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(i));
                var newArgs = attribute.ArgumentList.Arguments.Insert(0, SyntaxFactory.AttributeArgument(newArg));
                var newAttribute = attribute.WithArgumentList(attribute.ArgumentList.WithArguments(newArgs));
                var newAttributeList = attributeList.ReplaceNode(attribute, newAttribute);
                newAttributes = newAttributes.Replace(attributeList, newAttributeList);
            }
            while (true);

            newMethod = newMethod.WithAttributeLists(newAttributes);
            return newMethod;
        }
    }
}
