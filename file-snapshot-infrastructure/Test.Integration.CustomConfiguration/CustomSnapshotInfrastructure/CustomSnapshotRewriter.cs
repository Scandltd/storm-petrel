using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.FileSnapshotInfrastructure;

namespace Test.Integration.CustomConfiguration.CustomSnapshotInfrastructure
{
    internal class CustomSnapshotRewriter : IGeneratorRewriter
    {
        /// <summary>
        /// Rewrites snapshot files based on custom options from <see cref="CustomSnapshotOptions.Get(string, string)"/>.
        /// </summary>
        /// <param name="generationRewriteContext"></param>
        /// <returns></returns>
        public RewriteResult Rewrite(GenerationRewriteContext generationRewriteContext)
        {
            var methodContext = generationRewriteContext.GenerationContext.MethodSharedContext;
            SnapshotOptions options = CustomSnapshotOptions.Get(methodContext.ClassName, methodContext.MethodName);
            var rewriter = new SnapshotRewriter(options);
            return rewriter.Rewrite(generationRewriteContext);
        }
    }
}
