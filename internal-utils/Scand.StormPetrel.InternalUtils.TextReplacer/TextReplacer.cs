using System.Text.Json;
using System.Text.RegularExpressions;

namespace Scand.StormPetrel.InternalUtils.TextReplacer;

public static class TextReplacer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip
    };
    private const RegexOptions TextReplacerRegexOptions = RegexOptions.IgnoreCase | RegexOptions.CultureInvariant;

    public static ConfigModel DeserializeConfig(string configText) =>
        JsonSerializer.Deserialize<ConfigModel>(configText, JsonOptions) ?? throw new InvalidOperationException();

    public static string Replace(ConfigModel config, string targetText)
    {
        ArgumentNullException.ThrowIfNull(config);
        var baseUrls = config.BaseUrls.ToArray();
        if (!string.IsNullOrEmpty(config.RootFolderName))
        {
            for (var i = 0; i < baseUrls.Length; i++)
            {
                baseUrls[i] = baseUrls[i].Replace($"{{{nameof(ConfigModel.RootFolderName)}}}", config.RootFolderName, StringComparison.OrdinalIgnoreCase);
            }
        }
        foreach (var replacement in config.Replacements)
        {
            var replacementText = ReplacePattern(replacement.ReplacePattern, baseUrls);
            targetText = Regex.Replace(targetText, replacement.Regex, replacementText, TextReplacerRegexOptions);
        }
        return targetText;

        static string ReplacePattern(string pattern, string[] baseUrls)
        {
            for (var i = 0; i < baseUrls.Length; i++)
            {
                pattern = pattern.Replace($"{{baseUrls[{i}]}}", baseUrls[i], StringComparison.OrdinalIgnoreCase);
            }
            return pattern;
        }
    }
}
