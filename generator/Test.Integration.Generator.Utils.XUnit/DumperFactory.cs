using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Utils;
using Scand.StormPetrel.Generator.Utils.DumperDecorator;
using System.Globalization;
using Test.Integration.Generator.Utils.XUnit;

internal static class DumperFactory
{
    public static IGeneratorDumper CreateDecorated(IGeneratorDumper dumper) =>
        new ImplicitObjectCreationDumperDecorator(
            new CollectionExpressionDumperDecorator(
                new LiteralExpressionDumperDecorator(
                    new CustomDumperDecorator(
                        new RemoveAssignmentDumperDecorator(dumper, new Dictionary<string, IEnumerable<string>>
                        {
                            {
                                "" /*means 'all types'*/, [nameof(AddResult.GuidPropertyIgnoredForAllTypes)]
                            },
                            {
                                nameof(AddResult), [nameof(AddResult.StringPropertyIgnored)]
                            },
                        })),
                    GetLiteralExpressionDecoratingFunc())));

    /// <summary>
    /// Decorates literal expressions with constants of <see cref="Constants"/> if possible.
    /// </summary>
    /// <returns></returns>
    private static Func<LiteralExpressionDumpContext, SyntaxNode> GetLiteralExpressionDecoratingFunc()
    {
        var verbatimStringDecoratingFunc = LiteralExpressionDumperDecorator.GetVerbatimStringDecoratingFunc(new Dictionary<string, IEnumerable<string>>
        {
            {
                nameof(AddResult), [nameof(AddResult.ValueAsVerbatimString)]
            },
        });
        var rawStringDecoratingFunc = LiteralExpressionDumperDecorator.GetRawStringDecoratingFunc(new Dictionary<string, IEnumerable<RawStringProperty>>
        {
            {
                nameof(AddResult),
                [
                    new()
                    {
                        Name = nameof(AddResult.ValueAsRawString),
                        Comment = RawStringPropertyComment.LangJson,
                    }
                ]
            },
        });

        return context =>
        {
            string constantName = "";
            if (context.TokenText == Constants.IntFour.ToString(CultureInfo.InvariantCulture))
            {
                constantName = nameof(Constants.IntFour);
            }
            else if (context.TokenValueText == Constants.StringFourHex)
            {
                constantName = nameof(Constants.StringFourHex);
            }
            if (!string.IsNullOrEmpty(constantName))
            {
                return SyntaxFactory
                        .MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName("Constants"),
                            SyntaxFactory.IdentifierName(constantName))
                        .WithTriviaFrom(context.OriginalLiteralExpression);
            }
            var rawStringNode = rawStringDecoratingFunc(context);
            if (rawStringNode != null)
            {
                return rawStringNode;
            }
            return verbatimStringDecoratingFunc(context);
        };
    }
}
