using Microsoft.CodeAnalysis;
using System;

namespace Scand.StormPetrel.Generator
{
    internal struct SyntaxTreeInfo : IEquatable<SyntaxTreeInfo>
    {
        public bool IsVeryFirstEntry { get; private set; }
        public SyntaxTree SourceTree { get; private set; }
        public SyntaxTreeInfo(SyntaxTree sourceTree, bool isVeryFirstEntry)
        {
            SourceTree = sourceTree;
            IsVeryFirstEntry = isVeryFirstEntry;
        }
        public override int GetHashCode() => SourceTree?.GetHashCode() ?? 0;
        public override bool Equals(object obj)
        {
            if (obj is SyntaxTreeInfo other)
            {
                return EqualsImplementation(other);
            }
            return false;
        }
        public bool Equals(SyntaxTreeInfo other) => EqualsImplementation(other);
        private bool EqualsImplementation(SyntaxTreeInfo other) =>
            SourceTree == other.SourceTree && IsVeryFirstEntry == other.IsVeryFirstEntry;
    }
}
