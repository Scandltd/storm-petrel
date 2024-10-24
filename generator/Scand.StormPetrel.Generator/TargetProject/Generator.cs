using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Abstraction.Exceptions;
using System;
using System.Collections.Concurrent;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed class Generator : IGenerator
    {
        private static ConcurrentDictionary<MethodContext, string> MethodContextToFirstBackupFilePath = new ConcurrentDictionary<MethodContext, string>();
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
            var sharedContext = generationContext.MethodSharedContext;
            var isLastVariablePair = sharedContext.VariablePairCurrentIndex == (sharedContext.VariablePairsCount - 1);
            if (actualDump == expectedDump)
            {
                if (isLastVariablePair)
                {
                    CompleteGenerationContext(sharedContext);
                }
                return;
            }
            var rewriteResult = _rewriter.Rewrite(new GenerationRewriteContext()
            {
                GenerationContext = generationContext,
                Value = actualDump,
            });
            if (!MethodContextToFirstBackupFilePath.TryGetValue(sharedContext, out var firstBackupFilePath)
                    || !string.IsNullOrEmpty(rewriteResult.BackupFilePath) && string.IsNullOrEmpty(firstBackupFilePath))
            {
                if (string.IsNullOrEmpty(firstBackupFilePath))
                {
                    MethodContextToFirstBackupFilePath[sharedContext] = rewriteResult.BackupFilePath;
                }
            }
            if (isLastVariablePair)
            {
                CompleteGenerationContext(sharedContext);
            }
        }

        private static void CompleteGenerationContext(MethodContext sharedContext)
        {
            if (!MethodContextToFirstBackupFilePath.TryRemove(sharedContext, out var backupPath))
            {
                //No code rewrites, just exit
                return;
            }
            var message = string.IsNullOrEmpty(backupPath)
                            ? "StormPetrel has regenerated baseline(s) and intentionally fails to not execute test assertions. Original code is NOT saved according to the configuration."
                            : $"StormPetrel has regenerated baseline(s) and intentionally fails to not execute test assertions. Original code is saved in {backupPath}.";
            throw new BaselineUpdatedException(message);
        }
    }
}
