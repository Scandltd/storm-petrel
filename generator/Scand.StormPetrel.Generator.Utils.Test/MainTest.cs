using System.Reflection;
using FluentAssertions;
using NSubstitute;
using Scand.StormPetrel.Generator.Abstraction;

namespace Scand.StormPetrel.Generator.Utils.Test;

public class MainTest
{
    private readonly GenerationDumpContext _generationDumpContext = new() { Value = new GenerationContext() };
    private readonly IGeneratorDumper _generatorDumper = Substitute.For<IGeneratorDumper>();

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
    {
        //Arrange
        var assembly = Assembly.GetAssembly(typeof(MainTest));
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
        var inputCode = await ReadResourceAsync(assembly, $"{inputReplacementCodeResourceName}.cs");
        string? expectedResourceFileName = $"{inputReplacementCodeResourceName}_Then_Expected.cs"; ;
        string? expectedCode = await ReadResourceAsync(assembly, expectedResourceFileName);
        _generatorDumper
            .Dump(_generationDumpContext)
            .Returns(inputCode);

        //Act
        IGeneratorDumper collectionExpressionDumper = decorator == nameof(CollectionExpressionDumperDecorator)
            ? new CollectionExpressionDumperDecorator(_generatorDumper)
            : new ImplicitObjectCreationDumperDecorator(_generatorDumper);

        string? actual = collectionExpressionDumper.Dump(_generationDumpContext);

        actual = NormalizeLineEndings(actual);
        if (expectedResourceFileName != null)
        {
            //Keep commented in git. Uncomment to overwrite baselines while development only.
            //await File.WriteAllTextAsync(@$"..\..\..\Resource\{expectedResourceFileName}", actual);
        }
        actual.Should().Be(expectedCode);
    }

    static async Task<string> ReadResourceAsync(Assembly assembly, string resourceFileName)
    {
        using var stream = assembly.GetManifestResourceStream(typeof(MainTest), $"Resource.{resourceFileName}");
        ArgumentNullException.ThrowIfNull(stream, nameof(stream));
        using var streamReader = new StreamReader(stream);
        return await streamReader.ReadToEndAsync();
    }

    private static string? NormalizeLineEndings(string? s) => s?.Replace("\r\n", "\n", StringComparison.OrdinalIgnoreCase);
}