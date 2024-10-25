using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.Generator.Abstraction.ExtraContext;
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
            var filePath = generationContext.MethodSharedContext.FilePath;
            AbstractRewriter rewriter = null;
            var extraContext = generationContext.ExtraContext;
            if (extraContext is InitializerContext initializerContext)
            {
                switch (initializerContext.Kind)
                {
                    case InitializerContextKind.VariableDeclaration:
                        rewriter = new DeclarationRewriter(generationContext.ExpectedVariablePath, generationRewriteContext.Value);
                        break;
                    case InitializerContextKind.VariableAssignment:
                        rewriter = new AssignmentRewriter(generationContext.ExpectedVariablePath, generationRewriteContext.Value);
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected Kind = " + initializerContext.Kind);
                }
            }
            else if (extraContext is AttributeContext attributeContext)
            {
                rewriter = new AttributeRewriter(generationContext.ExpectedVariablePath, attributeContext.Name, attributeContext.Index, attributeContext.ParameterIndex, generationRewriteContext.Value);
            }
            else if (extraContext is InvocationSourceContext invocationSourceContext)
            {
                var methodInfo = invocationSourceContext.MethodInfo;
                if (methodInfo != null)
                {
                    var staticMethodInfo = GetStaticMethodInfo(invocationSourceContext.Path, methodInfo.ArgsCount);
                    filePath = staticMethodInfo.FilePath;
                    rewriter = new ExpressionRewriter(staticMethodInfo.MethodPath, methodInfo.NodeKind, methodInfo.NodeIndex, generationRewriteContext.Value);
                }
                else
                {
                    var staticPropertyInfo = GetStaticPropertyInfo(invocationSourceContext.Path, true);
                    filePath = staticPropertyInfo.FilePath;
                    rewriter = new DeclarationRewriter(staticPropertyInfo.PropertyPath, generationRewriteContext.Value);
                }
            }
            else if (extraContext is TestCaseSourceContext testCaseSourceContext)
            {
                var testCaseSourcePropertyInfo = GetStaticPropertyInfo(testCaseSourceContext.Path, false);
                string[] testCaseSourcePath = null;
                if (testCaseSourcePropertyInfo == null)
                {
                    var testCaseSourceMethodInfo = GetStaticMethodInfo(testCaseSourceContext.Path, -1);
                    testCaseSourcePath = testCaseSourceMethodInfo.MethodPath;
                    filePath = testCaseSourceMethodInfo.FilePath;
                }
                else
                {
                    testCaseSourcePath = testCaseSourcePropertyInfo.PropertyPath;
                    filePath = testCaseSourcePropertyInfo.FilePath;
                }
                rewriter = new EnumerableResultRewriter(testCaseSourcePath, testCaseSourceContext.RowIndex, testCaseSourceContext.ColumnIndex, generationRewriteContext.Value);
            }
            else
            {
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

        private static StaticPropertyInfo GetStaticPropertyInfo(string[] staticPropertyPath, bool throwIfNotFound)
        {
            staticPropertyPath = staticPropertyPath.ToArray(); //make a copy for later normalization
            staticPropertyPath[staticPropertyPath.Length - 1] = staticPropertyPath[staticPropertyPath.Length - 1]
                                                                    .Replace("[*]", "");
            var match = _staticPropertyInfoArray
                                .Where(x => x.PropertyPath.SequenceEqual(staticPropertyPath))
                                .FirstOrDefault();
            if (match == null)
            {
                // False positive match is possible. However, we prefer it against semantic analysis.
                match = _staticPropertyInfoArray
                                .OrderByDescending(x => x.PropertyPath.Length)
                                .Where(x => ParentPathHasChild(x.PropertyPath, staticPropertyPath))
                                .FirstOrDefault();
                if (match == null)
                {
                    match = _staticPropertyInfoArray
                                    .OrderBy(x => x.PropertyPath.Length)
                                    .Where(x => ParentPathHasChild(staticPropertyPath, x.PropertyPath))
                                    .FirstOrDefault();
                }
            }
            if (throwIfNotFound && match == null)
            {
                string path = string.Join(".", staticPropertyPath);
                throw new InvalidOperationException($"Cannot find static property file path for {path}");
            }
            return match;
        }

        private static StaticMethodInfo GetStaticMethodInfo(string[] staticMethodPath, int methodArgsCount)
        {
            var match = _staticMethodInfoArray
                                .Where(x => x.MethodPath.SequenceEqual(staticMethodPath) && (methodArgsCount < 0 || x.MethodArgsCount == methodArgsCount))
                                .FirstOrDefault();
            if (match == null)
            {
                var argsAsMethodInfo = new StaticMethodInfo
                {
                    MethodPath = staticMethodPath,
                    MethodArgsCount = methodArgsCount,
                };
                // False positive match is possible. However, we prefer it against semantic analysis.
                match = _staticMethodInfoArray
                                .OrderByDescending(x => x.MethodPath.Length)
                                .Where(x => ParentMethodHasChild(x, argsAsMethodInfo))
                                .FirstOrDefault();
                if (match == null)
                {
                    match = _staticMethodInfoArray
                                .OrderBy(x => x.MethodPath.Length)
                                .Where(x => ParentMethodHasChild(argsAsMethodInfo, x))
                                .FirstOrDefault();
                }
            }
            if (match == null)
            {
                string path = string.Join(".", staticMethodPath);
                throw new InvalidOperationException($"Cannot find static method file path for {path}");
            }
            return match;
        }

        private static bool ParentMethodHasChild(StaticMethodInfo parent, StaticMethodInfo child) =>
            parent.MethodPath.Length >= child.MethodPath.Length
                ? (parent.MethodArgsCount < 0 || child.MethodArgsCount < 0 || parent.MethodArgsCount == child.MethodArgsCount)
                    && parent.MethodPath.Skip(parent.MethodPath.Length - child.MethodPath.Length).SequenceEqual(child.MethodPath)
                : false;

        private static bool ParentPathHasChild(string[] parent, string[] child) =>
            parent.Length >= child.Length
                ? parent.Skip(parent.Length - child.Length).SequenceEqual(child)
                : false;
    }
}
