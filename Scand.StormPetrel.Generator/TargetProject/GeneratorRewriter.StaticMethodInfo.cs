namespace Scand.StormPetrel.Generator.TargetProject
{
    partial class GeneratorRewriter
    {
        private static StaticMethodInfo[] _staticMethodInfoArray = null;

        private sealed class StaticMethodInfo
        {
            public string[] MethodPath { get; set; }
            public int MethodArgsCount { get; set; }
            public string FilePath { get; set; }
        }
    }
}
