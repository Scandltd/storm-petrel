using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class BodyStatementRewriter : AbstractExpressionRewriter
    {
        private readonly int _methodBodyStatementIndex;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodPath"></param>
        /// <param name="valueNewCode"></param>
        /// <param name="methodBodyStatementIndex"></param>
        /// <param name="statementNodeKind"></param>
        /// <param name="statementNodeIndex"></param>
        /// <exception cref="ArgumentOutOfRangeException">When <paramref name="statementNodeKind"/> is not <see cref="SyntaxKind.Argument"/></exception>
        public BodyStatementRewriter(IEnumerable<string> methodPath, string valueNewCode, int methodBodyStatementIndex, int statementNodeKind, int statementNodeIndex) :
            base(methodPath, statementNodeKind, statementNodeIndex, valueNewCode, Array.Empty<string>())
        {
            if (_expressionKind != SyntaxKind.Argument)
            {
                throw new ArgumentOutOfRangeException(nameof(statementNodeKind));
            }
            _methodBodyStatementIndex = methodBodyStatementIndex;
        }

        protected override bool IsDescendantNodeVisitable(object onBeforeVisitDescendantNodesState, SyntaxNode descendantNode, out bool isResetExpressionIndex)
        {
            isResetExpressionIndex = false;
            if (onBeforeVisitDescendantNodesState == null)
            {
                return true;
            }
            var methodBodyStatementToInfo = (Dictionary<StatementSyntax, (int Index, bool IsIndexReset)>)onBeforeVisitDescendantNodesState;
            (int Index, bool IsIndexReset) info = default;
            var statement = descendantNode.FirstAncestorOrSelf<StatementSyntax>(x => methodBodyStatementToInfo.TryGetValue(x, out info))
                                ?? throw new InvalidOperationException("Cannot find body statement");
            if (info.Index == _methodBodyStatementIndex)
            {
                if (!info.IsIndexReset)
                {
                    isResetExpressionIndex = true;
                    methodBodyStatementToInfo[statement] = (info.Index, true);
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        protected override object OnBeforeVisitDescendantNodesState(MethodDeclarationSyntax node)
        {
            if (node.Body == null)
            {
                return null;
            }
            var methodBodyStatementToInfo = new Dictionary<StatementSyntax, (int Index, bool IsIndexReset)>();
            int i = 0;
            foreach (var statement in node.Body.Statements)
            {
                methodBodyStatementToInfo.Add(statement, (i++, false));
            }
            return methodBodyStatementToInfo;
        }
    }
}
