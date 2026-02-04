using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    internal abstract class StringDecoratingFuncProviderAbstract
    {
        protected abstract LiteralExpressionSyntax CreateLiteral(LiteralExpressionDumpContext context);
        protected abstract bool ShouldSkip(LiteralExpressionDumpContext context);
        protected abstract IReadOnlyDictionary<string, IEnumerable<string>> GetDecoratingTypeNameToPropertyNames();
        public Func<LiteralExpressionDumpContext, SyntaxNode> GetDecoratingFunc()
        {
            return (LiteralExpressionDumpContext context) =>
            {
                var nd = context.OriginalLiteralExpression;
                if (!nd.IsKind(SyntaxKind.StringLiteralExpression)
                        || context.TokenValueText == null
                        || context.TokenText == null
                        || ShouldSkip(context))
                {
                    return null;
                }
                var decoratingTypeNameToPropertyNames = GetDecoratingTypeNameToPropertyNames();
                if (decoratingTypeNameToPropertyNames == null
                    || context.FirstAncestorName != null
                        && decoratingTypeNameToPropertyNames.TryGetValue(context.FirstAncestorName, out var propertyNames)
                            && (propertyNames.Contains("") || propertyNames.Contains(context.AssignmentLeftName))
                    || decoratingTypeNameToPropertyNames.TryGetValue("", out var propertyNamesDefault)
                        && (propertyNamesDefault.Contains("") || propertyNamesDefault.Contains(context.AssignmentLeftName)))
                {
                    return CreateLiteral(context)
                            ?.WithTriviaFrom(nd);
                }
                return null;
            };
        }
    }
}
