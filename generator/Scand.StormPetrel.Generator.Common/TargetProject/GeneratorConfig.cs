namespace Scand.StormPetrel.Generator.Common.TargetProject
{
    public sealed class GeneratorConfig
    {
        public string BackuperExpression { get; set; } = $"new global::{MainConfig.GeneratorTargetProjectNamespace}.GeneratorBackuper()";
        public string DumperExpression { get; set; } = $"new global::{MainConfig.GeneratorTargetProjectNamespace}.GeneratorDumper(new global::VarDump.CSharpDumper())";
        public string RewriterExpression { get; set; } = $"new global::{MainConfig.GeneratorTargetProjectNamespace}.GeneratorRewriter()";
    }
}
