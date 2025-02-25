using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.FileSnapshotInfrastructure;
using System.Linq;
using System;

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
            var actualVarName = generationRewriteContext
                                    .GenerationContext
                                    .ActualVariablePath
                                    .LastOrDefault();
            //SnapshotProvider Convention in the rewriter: implicitly conclude prefferedKind from the variable name
            var prefferedKind = actualVarName?.EndsWith("png", StringComparison.OrdinalIgnoreCase) == true
                                    ? CustomSnapshotKind.Png
                                    : CustomSnapshotKind.None;
            SnapshotOptions options = CustomSnapshotOptions.Get(methodContext.ClassName, methodContext.MethodName, prefferedKind);

            //SnapshotProvider Convention in the rewriter: use default SnapshotRewriter to properly handle `useCaseId` test parameter
            var rewriter = new SnapshotRewriter(options);
            return rewriter.Rewrite(generationRewriteContext);
        }
    }
}
