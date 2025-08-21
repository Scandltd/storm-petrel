using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class AssignmentRewriter : AbstractValueRewriter
    {
        public AssignmentRewriter(IEnumerable<string> assignmentPath, string assignmentNewCode)
            : base(assignmentPath, assignmentNewCode)
        {
        }

        public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            var isMatch = IsMatchWithValuePath(node.Left as IdentifierNameSyntax);
            if (!isMatch)
            {
                return base.VisitAssignmentExpression(node);
            }
            return WithRight(node);
        }
    }
}
