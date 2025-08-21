using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator
{
    internal class MethodHelper
    {
        public const string StormPetrelUseCaseIndexParameterName = "stormPetrelUseCaseIndex";
        public static IEnumerable<PropertyDeclarationSyntax> GetExpectedVarPropertyInvocationExpressions(SyntaxNode root)
        {
            var nodes = new List<CSharpSyntaxNode>();
            GetDescendantNodesOptimized(root, nodes, node =>
            {
                if (node is PropertyDeclarationSyntax p)
                {
                    if (MethodHelperCommon.IsExpectedVarInvocationExpressionCandidate(p))
                    {
                        return (p, false);
                    }
                    return (null, false);
                }
                return (null, true);
            }, false);
            return nodes.Cast<PropertyDeclarationSyntax>();
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
                var (attributeList, attribute) = MethodHelperCommon
                                                    .GetAttributePairs(newAttributes, x => SupportedMethodInfo.Instance.AttributeNamesForTestCase.Contains(x.Name.ToString()))
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
