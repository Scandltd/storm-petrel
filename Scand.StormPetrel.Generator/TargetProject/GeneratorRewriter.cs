using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter;
using Scand.StormPetrel.Rewriter.Extension;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Scand.StormPetrel.Generator.TargetProject
{
    public sealed partial class GeneratorRewriter : IGeneratorRewriter
    {
        public readonly IGeneratorBackuper _backuper = (IGeneratorBackuper)null;
        private static ConcurrentDictionary<string, object> filePathToLock = new ConcurrentDictionary<string, object>();

        public RewriteResult Rewrite(GenerationRewriteContext generationRewriteContext)
        {
            var generationContext = generationRewriteContext.GenerationContext;
            var filePath = generationContext.FilePath;
            AbstractRewriter rewriter = null;
            switch (generationContext.RewriterKind)
            {
                case RewriterKind.Declaration:
                    rewriter = new DeclarationRewriter(generationContext.ExpectedVariablePath, generationRewriteContext.Value);
                    break;
                case RewriterKind.Assignment:
                    rewriter = new AssignmentRewriter(generationContext.ExpectedVariablePath, generationRewriteContext.Value);
                    break;
                case RewriterKind.Attribute:
                    var info = generationContext.TestCaseAttributeInfo;
                    rewriter = new AttributeRewriter(generationContext.ExpectedVariablePath, info.Name, info.Index, info.ParameterIndex, generationRewriteContext.Value);
                    break;
                case RewriterKind.MethodExpression:
                    var methodExpressionInfo = generationContext.ExpectedVariableInvocationExpressionInfo;
                    var methodsExpressionNodeInfo = methodExpressionInfo.NodeInfo;
                    var staticMethodInfo = GetStaticMethodInfo(methodExpressionInfo.Path, methodExpressionInfo.ArgsCount);
                    filePath = staticMethodInfo.FilePath;
                    rewriter = new ExpressionRewriter(staticMethodInfo.MethodPath, methodsExpressionNodeInfo.NodeKind, methodsExpressionNodeInfo.NodeIndex, generationRewriteContext.Value);
                    break;
                case RewriterKind.PropertyExpression:
                    var propertyExpressionInfo = generationContext.ExpectedVariableInvocationExpressionInfo;
                    var staticPropertyInfo = GetStaticPropertyInfo(propertyExpressionInfo.Path);
                    filePath = staticPropertyInfo.FilePath;
                    rewriter = new DeclarationRewriter(staticPropertyInfo.PropertyPath, generationRewriteContext.Value);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            string backupFilePath = null;
            if (_backuper != null)
            {
                backupFilePath = _backuper.Backup(new GenerationBackupContext()
                {
                    GenerationContext = generationContext,
                    FilePath = filePath,
                });
            }

            lock (filePathToLock.GetOrAdd(filePath, new object()))
            {
                CSharpSyntaxRewriterExtension.Rewrite(rewriter, filePath);
            }

            return new RewriteResult { IsRewritten = true, BackupFilePath = backupFilePath };
        }

        private static StaticPropertyInfo GetStaticPropertyInfo(string[] staticPropertyPath)
        {
            var match = _staticPropertyInfoArray
                                .Where(x => x.PropertyPath.SequenceEqual(staticPropertyPath))
                                .FirstOrDefault();
            if (match == null)
            {
                // False positive match is possible. However, we prefer it against semantic analysis.
                match = _staticPropertyInfoArray
                                .OrderByDescending(x => x.PropertyPath.Length)
                                .Where(x => x.PropertyPath.Length >= staticPropertyPath.Length
                                                ? x.PropertyPath.Skip(x.PropertyPath.Length - staticPropertyPath.Length).SequenceEqual(staticPropertyPath)
                                                : false)
                                .FirstOrDefault();
            }
            if (match == null)
            {
                string path = string.Join(".", staticPropertyPath);
                throw new InvalidOperationException($"Cannot find static property file path for {path}");
            }
            return match;
        }

        private static StaticMethodInfo GetStaticMethodInfo(string[] staticMethodPath, int methodArgsCount)
        {
            var match = _staticMethodInfoArray
                                .Where(x => x.MethodPath.SequenceEqual(staticMethodPath) && x.MethodArgsCount == methodArgsCount)
                                .FirstOrDefault();
            if (match == null)
            {
                // False positive match is possible. However, we prefer it against semantic analysis.
                match = _staticMethodInfoArray
                                .OrderByDescending(x => x.MethodPath.Length)
                                .Where(x => x.MethodPath.Length >= staticMethodPath.Length
                                                ? x.MethodArgsCount == methodArgsCount
                                                    && x.MethodPath.Skip(x.MethodPath.Length - staticMethodPath.Length).SequenceEqual(staticMethodPath)
                                                : false)
                                .FirstOrDefault();
            }
            if (match == null)
            {
                string path = string.Join(".", staticMethodPath);
                throw new InvalidOperationException($"Cannot find static method file path for {path}");
            }
            return match;
        }

    }
}
