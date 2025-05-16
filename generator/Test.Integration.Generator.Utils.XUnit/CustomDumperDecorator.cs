using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Scand.StormPetrel.Generator.Abstraction;
using System.Globalization;

namespace Test.Integration.Generator.Utils.XUnit;

/// <summary>
/// Replaces:
/// - `DateTime.ParseExact(...)` equal to <see cref="Constants.DateTimeFour"/> to <see cref="Constants.DateTimeFour"/>
/// - `DateTime.ParseExact(...)` expressions with `new DateTime(...)`
/// </summary>
/// <param name="dumper"></param>
internal sealed class CustomDumperDecorator(IGeneratorDumper dumper) : IGeneratorDumper
{
    private readonly IGeneratorDumper _dumper = dumper;
    public string Dump(GenerationDumpContext generationDumpContext)
    {
        string dump = _dumper.Dump(generationDumpContext);
        var rootNode = CSharpSyntaxTree.ParseText(dump).GetRoot();
        rootNode = DumpImplementation(rootNode);
        return rootNode.ToFullString();
    }
    private static SyntaxNode DumpImplementation(SyntaxNode node)
    {
        var originalNodeToReplacement = node
                                            .DescendantNodesAndSelf()
                                            .OfType<InvocationExpressionSyntax>()
                                            .Select(x => (OriginalNode: x, Replacement: GetDateTimeParseReplacement(x)))
                                            .Where(x => x.Replacement != null)
                                            .ToDictionary(x => x.OriginalNode, x => x.Replacement);
        var updatedNode = node.ReplaceNodes(originalNodeToReplacement.Keys, (x, _) => originalNodeToReplacement[x]!);
        return updatedNode;
    }

    private static ExpressionSyntax? GetDateTimeParseReplacement(InvocationExpressionSyntax x)
    {
        //Detect `DateTime.ParseExact`
        if (x.Expression is MemberAccessExpressionSyntax memberAccess
                            && memberAccess.Name.Identifier.Text == "ParseExact"
                            && memberAccess.Expression is IdentifierNameSyntax identifierName
                            && identifierName.Identifier.Text == "DateTime"
                            && x.ArgumentList?.Arguments.FirstOrDefault()?.Expression is LiteralExpressionSyntax literalExpression
                            && DateTime.TryParse(literalExpression.Token.ValueText, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var parsedDate))
        {
            if (parsedDate == Constants.DateTimeFour)
            {
                return SyntaxFactory.ParseExpression($"{nameof(Constants)}.{nameof(Constants.DateTimeFour)}").WithTriviaFrom(x);
            }
            var newDateTimeExpression = $"new DateTime({parsedDate.Year}, {parsedDate.Month}, {parsedDate.Day}";
            if (parsedDate.TimeOfDay != TimeSpan.Zero)
            {
                newDateTimeExpression += $", {parsedDate.Hour}, {parsedDate.Minute}, {parsedDate.Second}";
            }
            newDateTimeExpression += ")";
            return SyntaxFactory
                    .ParseExpression(newDateTimeExpression)
                    .WithTriviaFrom(x);
        }
        return null;
    }
}
