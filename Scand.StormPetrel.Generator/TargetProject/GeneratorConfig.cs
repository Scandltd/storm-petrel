namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class GeneratorConfig
    {
        public string BackuperExpression { get; set; } = $"new {typeof(GeneratorConfig).Namespace}.GeneratorBackuper()";
        public string DumperExpression { get; set; } = $"new {typeof(GeneratorConfig).Namespace}.GeneratorDumper(new VarDump.CSharpDumper())";
        public string RewriterExpression { get; set; } = $"new {typeof(GeneratorConfig).Namespace}.GeneratorRewriter()";
    }
}
