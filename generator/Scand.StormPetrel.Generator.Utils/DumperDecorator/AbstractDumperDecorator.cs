using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;

namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    public abstract class AbstractDumperDecorator : IGeneratorDumper
    {
        private readonly IGeneratorDumper _dumper;
        private protected AbstractDumperDecorator(IGeneratorDumper dumper)
        {
            _dumper = dumper;
        }
        public string Dump(GenerationDumpContext generationDumpContext)
        {
            string dump = _dumper.Dump(generationDumpContext);
            var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
            rootNode = DumpImplementation(rootNode);
            return rootNode.ToFullString();
        }
        protected abstract SyntaxNode DumpImplementation(SyntaxNode node);

        protected static (string FirstAncestorName, string AssignmentLeftName)? GetAssignmentNodeInfo(SyntaxNode node)
        {
            string firstAncestorName = null;
            string assignmentLeftName = null;
            if (node.IsKind(SyntaxKind.SimpleAssignmentExpression)
                    && node.Parent?.IsKind(SyntaxKind.ObjectInitializerExpression) == true)
            {
                if (node.Parent.Parent?.IsKind(SyntaxKind.ObjectCreationExpression) == true)
                {
                    var type = ((ObjectCreationExpressionSyntax)node.Parent.Parent).Type;
                    if (type is IdentifierNameSyntax identifierName)
                    {
                        firstAncestorName = identifierName.Identifier.Text;
                    }
                    else if (type is QualifiedNameSyntax qualifiedName)
                    {
                        firstAncestorName = GetQualifiedNameSyntaxText(qualifiedName);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (node.Parent.Parent?.IsKind(SyntaxKind.ImplicitObjectCreationExpression) == true)
                {
                    firstAncestorName = "";
                }
                assignmentLeftName = ((IdentifierNameSyntax)((AssignmentExpressionSyntax)node).Left).Identifier.Text ?? "";
            }
            else if (node.IsKind(SyntaxKind.AnonymousObjectMemberDeclarator)
                        && node is AnonymousObjectMemberDeclaratorSyntax anonymousMemberDeclarator
                            && node.Parent?.IsKind(SyntaxKind.AnonymousObjectCreationExpression) == true)
            {
                firstAncestorName = "";
                assignmentLeftName = anonymousMemberDeclarator.NameEquals.Name.Identifier.Text;
            }
            if (firstAncestorName == null || assignmentLeftName == null)
            {
                return null;
            }
            return (firstAncestorName, assignmentLeftName);
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
