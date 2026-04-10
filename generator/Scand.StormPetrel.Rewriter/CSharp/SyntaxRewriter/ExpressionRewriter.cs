using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class ExpressionRewriter : AbstractExpressionRewriter
    {
        /// <summary>
        /// A constructor.
        /// </summary>
        /// <param name="methodPath"></param>
        /// <param name="expressionKind"></param>
        /// <param name="expressionIndex"></param>
        /// <param name="valueNewCode"></param>
        public ExpressionRewriter(IEnumerable<string> methodPath, int expressionKind, int expressionIndex, string valueNewCode, IEnumerable<string> invocationPath)
            : base(methodPath, expressionKind, expressionIndex, valueNewCode, invocationPath)
        {
        }
        protected override bool IsDescendantNodeVisitable(object onBeforeVisitDescendantNodesState, SyntaxNode descendantNode, out bool isResetExpressionIndex)
        {
            isResetExpressionIndex = false;
            return true;
        }
        protected override object OnBeforeVisitDescendantNodesState(MethodDeclarationSyntax node) => null;
    }
}
