namespace Scand.StormPetrel.Generator.Utils.DumperDecorator
{
    /// <summary>
    /// Indicates a comment according to <see cref="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/raw-string"/>.
    /// </summary>
    public enum RawStringPropertyComment
    {
        None = 0,
        LangJson,
        LangRegex,
    }
}
