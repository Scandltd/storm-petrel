namespace Scand.StormPetrel.Generator.Common.TargetProject
{
    public sealed class GeneratorConfig
    {
        public string BackuperExpression { get; set; } = $"new {MainConfig.GeneratorTargetProjectNamespace}.GeneratorBackuper()";
        public string DumperExpression { get; set; } = $"new {MainConfig.GeneratorTargetProjectNamespace}.GeneratorDumper(new VarDump.CSharpDumper())";
        public string RewriterExpression { get; set; } = $"new {MainConfig.GeneratorTargetProjectNamespace}.GeneratorRewriter()";
    }
}
