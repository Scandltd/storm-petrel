using Scand.StormPetrel.InternalUtils.TextReplacer;

if (args.Length != 2)
{
    throw new ArgumentOutOfRangeException(nameof(args), "Incorrect arguments count");
}
var configFilePath = args[0];
var targetFilePath = args[1];

var configFileText = await File.ReadAllTextAsync(configFilePath);
var targetFileText = await File.ReadAllTextAsync(targetFilePath);

var config = TextReplacer.DeserializeConfig(configFileText);
var converted = TextReplacer.Replace(config, targetFileText);
await File.WriteAllTextAsync(targetFilePath, converted);
