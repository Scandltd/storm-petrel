using Microsoft.CodeAnalysis;
using Scand.StormPetrel.Generator.Abstraction;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
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
        /// - Key is object constructor token. Use empty string for anonymous or target-typed objects or any constructor token;
        /// - Value is a list of assigment (e.g. property or field) names to remove in context of parent key object.</param>
        public RemoveAssignmentDumperDecorator(IGeneratorDumper dumper, IDictionary<string, IEnumerable<string>> typeNameToRemovePropertyNames)
                : base(dumper)
        {
            _typeNameToRemovePropertyNames = typeNameToRemovePropertyNames
                                                .ToDictionary(x => x.Key, x => new HashSet<string>(x.Value));
        }
        protected override SyntaxNode DumpImplementation(SyntaxNode node)
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
            var nodeInfo = GetAssignmentNodeInfo(node);
            if (nodeInfo == null)
            {
                return false;
            }
            typeNameToRemovePropertyNames.TryGetValue(nodeInfo.Value.FirstAncestorName, out var namesToRemove);
            typeNameToRemovePropertyNames.TryGetValue("", out var namesToRemoveForEmptyAncestorName);
            return namesToRemove?.Contains(nodeInfo.Value.AssignmentLeftName) == true
                    || namesToRemoveForEmptyAncestorName?.Contains(nodeInfo.Value.AssignmentLeftName) == true;
        }
    }
}
