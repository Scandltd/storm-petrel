using FluentAssertions;
using NSubstitute;
using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Utils.DumperDecorator;
using System.Reflection;
using System.Text.Json;

namespace Scand.StormPetrel.Generator.Utils.Test;

public class MainTest
{
    private readonly GenerationDumpContext _generationDumpContext = new() { Value = new GenerationContext() };
    private readonly IGeneratorDumper _generatorDumper = Substitute.For<IGeneratorDumper>();
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
    };

    [Theory]
    [InlineData("010_ImplicitArrayCreation")]
    [InlineData("020_ArrayFormatted")]
    [InlineData("030_JaggedArray")]
    [InlineData("040_SingleDimensionalArray")]
    [InlineData("050_ListInitializer")]
    [InlineData("060_HashSetInitializer")]
    [InlineData("070_DictionaryInitializer")]
    [InlineData("080_QueueInitializer")]
    [InlineData("090_StackInitializer")]
    [InlineData("100_ArrayListInitializer")]
    [InlineData("110_LinkedListInitializer")]
    [InlineData("120_SortedListInitializer")]
    [InlineData("130_StringCollectionInitializer")]
    [InlineData("140_BitArrayInitializer")]
    [InlineData("150_NameValueCollectionInitializer")]
    [InlineData("160_ConcurrentQueueInitializer")]
    [InlineData("170_ConcurrentStackInitializer")]
    [InlineData("180_ConcurrentDictionaryInitializer")]
    [InlineData("190_BlockingCollectionInitializer")]
    [InlineData("200_HashtableInitializer")]
    [InlineData("210_EmptyInitializer")]
    [InlineData("220_Trivia")]
    [InlineData("230_ImplicitObjectCreation", nameof(ImplicitObjectCreationDumperDecorator))]
    public async Task SourceGeneratorTest(string inputReplacementCodeResourceName, string decorator = nameof(CollectionExpressionDumperDecorator))
        => await SourceGeneratorTestImplementation(inputReplacementCodeResourceName, decorator == nameof(CollectionExpressionDumperDecorator)
                                                                                        ? new CollectionExpressionDumperDecorator(_generatorDumper)
                                                                                        : new ImplicitObjectCreationDumperDecorator(_generatorDumper));
    [Theory]
    [InlineData("240_RemoveAssignmentDumperDecorator_Base", @"{
  ""FooClass"": [""DoubleProperty"", ""IntProperty"", ""StringProperty"", ""StringPropertyMultiline"", ""StringPropertyWithComments"", ""NestedObjectProperty"", ""DateTimeProperty"", ""TargetTypedNewObject"", ""LatestPropertyWithoutComma""],
  ""NestedObject"": [""IntProperty"", ""StringProperty""],
  ""MyNamespace.MySubspace.NestedObject"": [""IntProperty"", ""NestedAnonymousObjectPropertyOneMoreLevel""],
  //For Anonymous or target-typed objects
  """": [""IntProperty"", ""StringProperty"", ""RemovedFromAnyParent""]
}")]
    public async Task RemoveAssignmentDumperDecoratorTest(string inputReplacementCodeResourceName, string dictionarySerialized)
        => await SourceGeneratorTestImplementation(inputReplacementCodeResourceName,
                                                    new RemoveAssignmentDumperDecorator(_generatorDumper,
                                                        JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(dictionarySerialized, JsonOptions)));

    [Theory]
    [InlineData("250_LiteralExpressionDumperDecorator", @"{
  ""FooClass"": [""DoubleProperty"" /*to assert no failure for double*/, ""NoCharsToEscape"", ""NoCharsToEscapeButVerbatimToken"", ""LessThanThreeCharsButDoubleQuote"", ""MoreThanTwoCharsWithDoubleQuote"", ""MultilineWithSlashNforAnyObject"", ""MultilineWithSlashR"", ""WithCommentsAndDoubleQuote""],
  ""NestedObject"": [""MultilineWithSlashNforNestedObject"", ""MultilineWithSlashNforAnyObject""],
  ""MyNamespace.MySubspace.NestedObject"": [""MultilineWithSlashNforFullNameNestedObject"", ""MultilineWithSlashNforAnyObject""],
  //For Anonymous or target-typed objects
  """": [""IntProperty"" /*to assert no failure for int*/, ""MultilineWithSlashNforAnyObject"", ""NestedObjectProperty"" /*to assert no failure for nested*/]
}")]
    [InlineData("255_LiteralExpressionDumperDecorator_ExtraCases", @"{
  //For root and other strings
  """": [""""]
}", true, 0)]
    [InlineData("255_LiteralExpressionDumperDecorator_ExtraCases", null, true, 0)]
    [InlineData("260_LiteralExpressionDumperDecorator_RootString", @"{
  """": [""""]
}")]
    [InlineData("260_LiteralExpressionDumperDecorator_RootString", null)]
    [InlineData("265_LiteralExpressionDumperDecorator_RootStringUnescaped", @"{
  """": [""""]
}", true, 30)]
    [InlineData("265_LiteralExpressionDumperDecorator_RootStringUnescaped", null, true, 30)]
    [InlineData("270_LiteralExpressionDumperDecorator_NumberToHex", "", false)]
    [InlineData("275_LiteralExpressionDumperDecorator_NumberToHex_RootNumber", "", false)]
    public async Task LiteralExpressionDumperDecoratorTest(string inputReplacementCodeResourceName, string? verbatimStringConfigSerialized, bool useGetVerbatimStringDecoratingFunc = true, int maxPreservedStringLength = 2)
        => await SourceGeneratorTestImplementation(inputReplacementCodeResourceName,
                                                    useGetVerbatimStringDecoratingFunc
                                                        ? new LiteralExpressionDumperDecorator(_generatorDumper,
                                                            verbatimStringConfigSerialized != null
                                                                ? JsonSerializer.Deserialize<Dictionary<string, IEnumerable<string>>>(verbatimStringConfigSerialized, JsonOptions)
                                                                : null,
                                                            maxPreservedStringLength)
                                                        : new LiteralExpressionDumperDecorator(_generatorDumper, LiteralExpressionDumperDecorator.GetNumberToHexDecoratingFunc()));

    [Theory]
    [InlineData("280_CSharpSyntaxDumperDecorator", false)]
    [InlineData("285_CSharpSyntaxDumperDecorator_NoChange", true)]
    public async Task CSharpSyntaxDumperDecoratorTest(string inputReplacementCodeResourceName, bool skipAllDecorations)
    {
        //Arrange
        CSharpSyntaxDumperDecorator decorator = skipAllDecorations
            ? new(_generatorDumper, false)
            : new(
                _generatorDumper,
                true,
                new RemoveAssignmentDumperDecorator(_generatorDumper, new Dictionary<string, IEnumerable<string>>
                {
                    { "FooClass", ["ToRemove"] },
                }),
                new LiteralExpressionDumperDecorator(_generatorDumper, LiteralExpressionDumperDecorator.GetNumberToHexDecoratingFunc()));
        //Act, Assert
        await SourceGeneratorTestImplementation(inputReplacementCodeResourceName, decorator);
    }

    [Theory]
    [InlineData("300_LiteralExpressionRawStringDecorator", null, null)]
    [InlineData("300_LiteralExpressionRawStringDecorator", "300_LiteralExpressionRawStringDecorator_When_PropertyComments_Then_Expected.cs",
        // lang=json
        """
        {
            "FooClass": [
                {
                    "Name": "MultiLineProperty",
                    "Comment": 1
                },
                {
                    "Name": "SingleLineProperty",
                    "Comment": 2
                },
                {
                    "Name": "MultiLinePropertyWithVeryFirstLFAsEndOfLine",
                    "Comment": 0
                },
                {
                    "Name": "MultiLinePropertyWithExtraTrivia",
                    "Comment": 1
                }
            ]
        }
        """)]
    [InlineData("300_LiteralExpressionRawStringDecorator", "300_LiteralExpressionRawStringDecorator_When_PropertyConfiguration_Then_ExpectedByConfigOrder.cs",
        // lang=json
        """
        {
            "FooClass": [
                {
                    "Name": "SingleLineProperty",
                    "Comment": 1
                }
            ],
            "": [
                {
                    "Name": "SingleLineProperty",
                    "Comment": 2
                }
            ]
        }
        """)]
    [InlineData("300_LiteralExpressionRawStringDecorator", "300_LiteralExpressionRawStringDecorator_When_PropertyConfiguration_Then_ExpectedByConfigOrder.cs",
        // lang=json
        """
        {
            "": [
                {
                    "Name": "SingleLineProperty",
                    "Comment": 1
                }
            ],
            "FooClass": [
                {
                    "Name": "SingleLineProperty",
                    "Comment": 2
                }
            ]
        }
        """)]
    [InlineData("300_LiteralExpressionRawStringDecorator", "300_LiteralExpressionRawStringDecorator_When_AnyPropertyIsJson_Then_Expected.cs",
        // lang=json
        """
        {
            "": [
                {
                    "Name": "",
                    "Comment": 1
                }
            ]
        }
        """)]
    public async Task LiteralExpressionRawStringDecoratorTest(string inputReplacementCodeResourceName, string? expectedResourceFileName, string? rawStringConfigSerialized)
        => await SourceGeneratorTestImplementation(inputReplacementCodeResourceName,
                                                        new LiteralExpressionDumperDecorator(_generatorDumper, LiteralExpressionDumperDecorator.GetRawStringDecoratingFunc(rawStringConfigSerialized != null
                                                                ? JsonSerializer.Deserialize<Dictionary<string, IEnumerable<RawStringProperty>>>(rawStringConfigSerialized, JsonOptions)
                                                                : null)),
                                                        expectedResourceFileName,
                                                        true);

    private async Task SourceGeneratorTestImplementation(string inputReplacementCodeResourceName, IGeneratorDumper dumper, string? expectedResourceFileName = null, bool skipNormalizeLineEndings = false)
    {
        //Arrange
        var assembly = Assembly.GetAssembly(typeof(MainTest));
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
        var inputCode = await ReadResourceAsync(assembly, $"{inputReplacementCodeResourceName}.cs");
        expectedResourceFileName ??= $"{inputReplacementCodeResourceName}_Then_Expected.cs";
        string? expectedCode = await ReadResourceAsync(assembly, expectedResourceFileName);
        _generatorDumper
            .Dump(_generationDumpContext)
            .Returns(inputCode);

        //Act
        string? actual = dumper.Dump(_generationDumpContext);

        if (!skipNormalizeLineEndings)
        {
            actual = NormalizeLineEndings(actual);
        }
        if (expectedResourceFileName != null)
        {
            //Keep commented in git. Uncomment to overwrite baselines while development only.
            //await File.WriteAllTextAsync(@$"..\..\..\Resource\{expectedResourceFileName}", actual);
        }
        actual.Should().Be(expectedCode);
    }

    private static async Task<string> ReadResourceAsync(Assembly assembly, string resourceFileName)
    {
        using var stream = assembly.GetManifestResourceStream(typeof(MainTest), $"Resource.{resourceFileName}");
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));
        using var streamReader = new StreamReader(stream);
        return await streamReader.ReadToEndAsync();
    }

    private static string? NormalizeLineEndings(string? s) => s?.Replace("\r\n", "\n", StringComparison.OrdinalIgnoreCase);
}