namespace Scand.StormPetrel.InternalUtils.TextReplacer;

public sealed class ConfigModel
{
    public string RootFolderName { get; set; } = "";
    public string[] BaseUrls { get; set; } =
    [
        $"https://raw.githubusercontent.com/Scandltd/storm-petrel/main/{{{nameof(RootFolderName)}}}/",
        $"https://github.com/Scandltd/storm-petrel/blob/main/{{{nameof(RootFolderName)}}}/",
    ];
    public ConfigReplacement[] Replacements { get; set; } =
    [
        new ()
        {
            Regex = @"(]\()(assets)([^\)]*)(\))",
            ReplacePattern = "$1{BaseUrls[0]}$2$3$4",
        },
        new ()
        {
            Regex = @"(]\()((?!http|#)[^\)]*)(\))",
            ReplacePattern = "$1{BaseUrls[1]}$2$3",
        },
    ];
}
