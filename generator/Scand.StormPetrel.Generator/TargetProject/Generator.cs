using System;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class Generator : IGenerator
    {
        private readonly IGeneratorDumper _dumper;
        private readonly IGeneratorRewriter _rewriter;
        internal Generator()
        {
            //placeholders to be overwritten while code generating
            _dumper = (IGeneratorDumper)null;
            _rewriter = (IGeneratorRewriter)null;
        }

        /// <summary>
        /// Generates expected baseline based on <paramref name="generationContext"/> and rewrites test code accordingly.
        /// </summary>
        /// <param name="generationContext"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public void GenerateBaseline(GenerationContext generationContext)
        {
            var actualDump = _dumper.Dump(new GenerationDumpContext()
            {
                GenerationContext = generationContext,
                Value = generationContext.Actual,
            });
            var expectedDump = _dumper.Dump(new GenerationDumpContext()
            {
                GenerationContext = generationContext,
                Value = generationContext.Expected,
            });
            if (actualDump == expectedDump)
            {
                return;
            }
            var rewriteResult = _rewriter.Rewrite(new GenerationRewriteContext()
            {
                GenerationContext = generationContext,
                Value = actualDump,
            });
            if (!generationContext.IsLastVariablePair)
            {
                return;
            }
            var message = rewriteResult.BackupFilePath == null
                            ? "StormPetrel has regenerated baseline(s) and intentionally fails to not execute test assertions. Original code is NOT saved according to the configuration."
                            : $"StormPetrel has regenerated baseline(s) and intentionally fails to not execute test assertions. Original code is saved in {rewriteResult.BackupFilePath}.";
            throw new InvalidOperationException(message);
        }
    }
}
