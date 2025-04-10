using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils
{
    /// <summary>
    /// Removes assignments from <see cref="IGeneratorDumper"/> output according to <see cref="RemoveAssignmentDumperDecorator"/> configuration.
    /// </summary>
    public sealed class RemoveAssignmentDumperDecorator : AbstractDumperDecorator
    {
        private readonly Dictionary<string, HashSet<string>> _typeNameToRemovePropertyNames;
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="dumper"></param>
        /// <param name="typeNameToRemovePropertyNames">A dictionary where:
        /// - Key is object constructor token. Use empty string for anonymous or target-typed objects;
        /// - Value is list of assigment (e.g. property or field) names to remove in context of parent key object.</param>
        public RemoveAssignmentDumperDecorator(IGeneratorDumper dumper, IDictionary<string, IEnumerable<string>> typeNameToRemovePropertyNames)
                : base(dumper)
        {
            _typeNameToRemovePropertyNames = typeNameToRemovePropertyNames
                                                .ToDictionary(x => x.Key, x => new HashSet<string>(x.Value));
        }
        private protected override SyntaxNode DumpImplementation(SyntaxNode node)
            => DecorateByRemoveAssignment(node, _typeNameToRemovePropertyNames);
        public SyntaxNode DecorateByRemoveAssignment(SyntaxNode node)
            => DecorateByRemoveAssignment(node, _typeNameToRemovePropertyNames);
        public static SyntaxNode DecorateByRemoveAssignment(SyntaxNode node, Dictionary<string, HashSet<string>> typeNameToRemovePropertyNames)
        {
            var nodesToRemove = GetNodesToRemove(node, typeNameToRemovePropertyNames).ToArray();
            var lastChilds = nodesToRemove
                                .Select(x => x.Parent?.ChildNodes().LastOrDefault())
                                .Where(x => x != null)
                                .ToImmutableHashSet();
            node = node.RemoveNodes(nodesToRemove.Where(x => !lastChilds.Contains(x)), SyntaxRemoveOptions.KeepNoTrivia);
            var nodesToRemoveLeft = GetNodesToRemove(node, typeNameToRemovePropertyNames);
            return node.RemoveNodes(nodesToRemoveLeft, SyntaxRemoveOptions.KeepEndOfLine);
        }
        private static IEnumerable<SyntaxNode> GetNodesToRemove(SyntaxNode node, Dictionary<string, HashSet<string>> typeNameToRemovePropertyNames) =>
            node
                .DescendantNodes(x => !IsForRemove(x, typeNameToRemovePropertyNames) /*optimize for the case of nested objects*/)
                .Where(x => IsForRemove(x, typeNameToRemovePropertyNames));
        private static bool IsForRemove(SyntaxNode node, Dictionary<string, HashSet<string>> typeNameToRemovePropertyNames)
        {
            string firstAncestorCreationName = null;
            string assignmentLeftName = null;
            if (node.IsKind(SyntaxKind.SimpleAssignmentExpression)
                    && node.Parent?.IsKind(SyntaxKind.ObjectInitializerExpression) == true)
            {
                if (node.Parent.Parent?.IsKind(SyntaxKind.ObjectCreationExpression) == true)
                {
                    var type = ((ObjectCreationExpressionSyntax)node.Parent.Parent).Type;
                    if (type is IdentifierNameSyntax identifierName)
                    {
                        firstAncestorCreationName = identifierName.Identifier.Text;
                    }
                    else if (type is QualifiedNameSyntax qualifiedName)
                    {
                        firstAncestorCreationName = GetQualifiedNameSyntaxText(qualifiedName);
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (node.Parent.Parent?.IsKind(SyntaxKind.ImplicitObjectCreationExpression) == true)
                {
                    firstAncestorCreationName = "";
                }
                assignmentLeftName = ((IdentifierNameSyntax)((AssignmentExpressionSyntax)node).Left).Identifier.Text ?? "";
            }
            else if (node.IsKind(SyntaxKind.AnonymousObjectMemberDeclarator)
                        && node is AnonymousObjectMemberDeclaratorSyntax anonymousMemberDeclarator
                            && node.Parent?.IsKind(SyntaxKind.AnonymousObjectCreationExpression) == true)
            {
                firstAncestorCreationName = "";
                assignmentLeftName = anonymousMemberDeclarator.NameEquals.Name.Identifier.Text;
            }
            if (firstAncestorCreationName == null || assignmentLeftName == null)
            {
                return false;
            }
            typeNameToRemovePropertyNames.TryGetValue(firstAncestorCreationName, out var namesToRemove);
            typeNameToRemovePropertyNames.TryGetValue("", out var namesToRemoveForEmptyAncestorName);
            return namesToRemove?.Contains(assignmentLeftName) == true
                    || namesToRemoveForEmptyAncestorName?.Contains(assignmentLeftName) == true;
        }
        private static string GetQualifiedNameSyntaxText(QualifiedNameSyntax qualifiedName)
        {
            if (qualifiedName.Left is IdentifierNameSyntax identifierLeft
                    && qualifiedName.Right is IdentifierNameSyntax identifierRight)
            {
                return $"{identifierLeft.Identifier.Text}.{identifierRight.Identifier.Text}";
            }
            else if (qualifiedName.Left is QualifiedNameSyntax left2
                        && qualifiedName.Right is IdentifierNameSyntax right2)
            {
                return $"{GetQualifiedNameSyntaxText(left2)}.{right2.Identifier.Text}";
            }
            return $"#UnexpectedCaseIn{nameof(GetQualifiedNameSyntaxText)}-for-{qualifiedName.ToFullString().Substring(0, 50)}";
        }
    }
}
