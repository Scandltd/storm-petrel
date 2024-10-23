using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Shared
{
    internal static class SyntaxNodeHelper
    {
        public static string[] GetValuePath(CSharpSyntaxNode node)
        {
            var valuePath = new List<string>();
            VisitValuePath(node, (nd, ndIndex) =>
            {
                if (nd == null)
                {
                    return false;
                }
                var identifierName = GetIdentifierName(nd);
                if (identifierName == null)
                {
                    return true;
                }
                var pathSegment = GetPathSegment(identifierName, ndIndex, out var _, out var __);
                valuePath.Add(pathSegment);
                return true;
            });
            valuePath.Reverse();
            return valuePath.ToArray();
        }

        public static bool IsMatchWithValuePath(CSharpSyntaxNode node, string[] valuePath)
        {
            int i = valuePath.Length - 1;
            bool result = false;
            VisitValuePath(node, (nd, ndIndex) =>
            {
                if (i < 0 && nd == null)
                {
                    result = true;
                    return false;
                }
                if (i < 0 || nd == null)
                {
                    return false;
                }
                var identifierName = GetIdentifierName(nd);
                if (identifierName == null)
                {
                    return true;
                }
                if (!(GetPathSegment(identifierName, ndIndex, out var secondaryPathSegment, out var anyIndexPathSegment) == valuePath[i]
                        || secondaryPathSegment != null && secondaryPathSegment == valuePath[i]
                        || anyIndexPathSegment == valuePath[i]))
                {
                    return false;
                }
                i--;
                return true;
            });
            return result;
        }

        private static void VisitValuePath(CSharpSyntaxNode node, Func<SyntaxNode, int, bool> visitor)
        {
            var syntaxNode = (SyntaxNode)node;
            SyntaxNode prevPathLevelSyntaxNode = null;
            var prevPathLevelAncestors = new List<SyntaxNode>();
            do
            {
                if (syntaxNode == null)
                {
                    if (prevPathLevelSyntaxNode != null && !IsVisitorCallOk())
                    {
                        break;
                    }
                    visitor(syntaxNode, 0);
                    break;
                }
                var identifierName = GetIdentifierName(syntaxNode);
                if (identifierName == null)
                {
                    if (syntaxNode == node)
                    {
                        throw new InvalidOperationException("Very first node must have a name, otherwise we cannot detect the node by a path");
                    }
                    syntaxNode = syntaxNode.Parent;
                    prevPathLevelAncestors.Add(syntaxNode);
                    continue;
                }
                if (prevPathLevelSyntaxNode != null && !IsVisitorCallOk())
                {
                    break;
                }
                prevPathLevelAncestors.Clear();
                prevPathLevelSyntaxNode = syntaxNode;
                syntaxNode = syntaxNode.Parent;
                prevPathLevelAncestors.Add(syntaxNode);
                if (syntaxNode is CompilationUnitSyntax)
                {
                    syntaxNode = syntaxNode.Parent;
                    prevPathLevelAncestors.Add(syntaxNode);
                }
            }
            while (true);

            bool IsVisitorCallOk()
            {
                int i = -1;
                var allChilds = prevPathLevelAncestors
                                    .AsEnumerable()
                                    .Reverse()
                                    .Where(x => x != null) // prefer filtering out `null` values against multiple `not null` checks
                                    .SelectMany(x => x.IsKind(SyntaxKind.MethodDeclaration)
                                                        ? DescendantNodesExceptLocalFunctions(x)
                                                        : x.ChildNodes())
                                    .Distinct();
                var prevKind = prevPathLevelSyntaxNode.Kind();
                var prevIdentifierName = GetIdentifierName(prevPathLevelSyntaxNode);
                foreach (var child in allChilds)
                {
                    if (child.IsKind(prevKind) && GetIdentifierName(child) == prevIdentifierName)
                    {
                        i++;
                    }
                    if (ReferenceEquals(child, prevPathLevelSyntaxNode))
                    {
                        break;
                    }
                }
                if (i == -1)
                {
                    throw new InvalidOperationException("Cannot detect the index");
                }
                return visitor(prevPathLevelSyntaxNode, i);

                IEnumerable<SyntaxNode> DescendantNodesExceptLocalFunctions(SyntaxNode currentNode) =>
                currentNode
                    .DescendantNodes(nd => !nd.IsKind(SyntaxKind.LocalFunctionStatement))
                    .Where(nd => !nd.IsKind(SyntaxKind.LocalFunctionStatement));
            }
        }

        private static string GetPathSegment(string identifierName, int index, out string secondaryPathSegment, out string anyIndexPathSegment)
        {
            var pathSegment = $"{identifierName}[{index}]";
            secondaryPathSegment = null;
            anyIndexPathSegment = $"{identifierName}[*]";
            if (index == 0)
            {
                secondaryPathSegment = pathSegment;
                return identifierName;
            }
            return pathSegment;
        }

        private static string GetIdentifierName(SyntaxNode syntaxNode)
        {
            if (syntaxNode is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
            {
                return GetIdentifier(localDeclarationStatementSyntax.Declaration);
            }
            else if (syntaxNode is IdentifierNameSyntax identifierNameSyntax)
            {
                return identifierNameSyntax.Identifier.Text;
            }
            else if (syntaxNode is MethodDeclarationSyntax methodDeclarationSyntax)
            {
                return methodDeclarationSyntax.Identifier.Text;
            }
            else if (syntaxNode is LocalFunctionStatementSyntax localFunctionStatementSyntax)
            {
                return localFunctionStatementSyntax.Identifier.Text;
            }
            else if (syntaxNode is PropertyDeclarationSyntax propertyDeclarationSyntax)
            {
                return propertyDeclarationSyntax.Identifier.Text;
            }
            //TODO: support fields
            //else if (syntaxNode is FieldDeclarationSyntax fieldDeclarationSyntax)
            //{
            //    return GetIdentifier(fieldDeclarationSyntax.Declaration);
            //}
            else if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
            {
                return classDeclarationSyntax.Identifier.Text;
            }
            else if (syntaxNode is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
            {
                return namespaceDeclarationSyntax.Name.ToString();
            }
            //TODO: support structs, records
            return null;

            string GetIdentifier(VariableDeclarationSyntax variableDeclarationSyntax)
            {
                return variableDeclarationSyntax.Variables.Single().Identifier.Text;
            }
        }
    }
}
