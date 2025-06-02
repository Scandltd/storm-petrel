using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter
{
    public sealed class AttributeRewriter : AbstractValueRewriter
    {
        private readonly string _attributeName;
        private readonly int _attributeIndex;
        private readonly int _attributeParameterIndex;
        public AttributeRewriter(IEnumerable<string> methodPath, string attributeName, int attributeIndex, int attributeParameterIndex, string attributeParameterNewCode)
            : base(methodPath, attributeParameterNewCode)
        {
            _attributeName = attributeName;
            _attributeIndex = attributeIndex;
            _attributeParameterIndex = attributeParameterIndex;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }
            if (!IsMatchWithValuePath(node))
            {
                return base.VisitMethodDeclaration(node);
            }
            var attribute = node
                                .AttributeLists
                                .SelectMany(x => x.Attributes)
                                .Where(x => x.Name.ToString() == _attributeName)
                                .Skip(_attributeIndex)
                                .FirstOrDefault();
            if (attribute == null)
            {
                return base.VisitMethodDeclaration(node);
            }
            if (attribute.ArgumentList == null)
            {
                return base.VisitMethodDeclaration(node);
            }
            var newArgumentListArgs = attribute.ArgumentList.Arguments;
            while (newArgumentListArgs.Count <= _attributeParameterIndex)
            {
                var parameter = node
                                    .ParameterList
                                    .Parameters
                                    .Skip(newArgumentListArgs.Count)
                                    .FirstOrDefault();
                if (parameter?.Default == null)
                {
                    return base.VisitMethodDeclaration(node);
                }
                var missedArg = SyntaxFactory.AttributeArgument(parameter.Default.Value);
                if (newArgumentListArgs.Count > 0)
                {
                    missedArg = missedArg.WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
                }
                //Add missed default values
                newArgumentListArgs = newArgumentListArgs.Add(missedArg);
            }
            var newAttribute = attribute
                                    .WithArgumentList(attribute
                                                        .ArgumentList
                                                        .WithArguments(newArgumentListArgs));
            var arg = newAttribute
                            .ArgumentList
                            .Arguments
                            .Skip(_attributeParameterIndex)
                            .First();
            var attributeParent = (AttributeListSyntax)attribute.Parent; //returns `[InlineData(...)]` when the attribute is `InlineData(...)`
            var newArg = SyntaxFactory.AttributeArgument(CreateInitializeExpressionSyntax(attributeParent));
            bool isMissedDefaultWithTrivia = arg.HasLeadingTrivia;
            if (isMissedDefaultWithTrivia)
            {
                newArg = newArg.WithLeadingTrivia(SyntaxFactory.Whitespace(" "));
            }
            newArgumentListArgs = newAttribute
                                    .ArgumentList
                                    .Arguments
                                    .Replace(arg, newArg);
            newAttribute = newAttribute
                                    .WithArgumentList(newAttribute
                                                        .ArgumentList
                                                        .WithArguments(newArgumentListArgs));
            return node.ReplaceNode(attribute, newAttribute);
        }
    }
}
