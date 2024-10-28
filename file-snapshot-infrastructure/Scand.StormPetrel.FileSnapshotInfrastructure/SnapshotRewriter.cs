using Scand.StormPetrel.Generator.Abstraction;
using Scand.StormPetrel.FileSnapshotInfrastructure.Attributes;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Scand.StormPetrel.FileSnapshotInfrastructure
{
    /// <summary>
    /// Implements <see cref="IGeneratorRewriter"/> to rewrite file snapshots via Storm Petrel generated test methods.
    /// </summary>
    public sealed class SnapshotRewriter : IGeneratorRewriter
    {
        private readonly SnapshotOptions _options;
        private readonly Encoding _textFileEncoding;
        private static readonly string UseCaseIdAttributeFullName = typeof(UseCaseIdAttribute).FullName
                                                                        .Substring(0, typeof(UseCaseIdAttribute).FullName.Length - "Attribute".Length);
        public SnapshotRewriter(SnapshotOptions options = null, Encoding textFileEncoding = null)
        {
            _options = options ?? SnapshotOptions.Current;
            _textFileEncoding = textFileEncoding;
        }
        /// <summary>
        /// CAUTION: the method does not create snapshot file backup. Use control version systems like Git to track snapshot file history instead.
        /// </summary>
        /// <param name="generationRewriteContext"></param>
        /// <returns></returns>
        public RewriteResult Rewrite(GenerationRewriteContext generationRewriteContext)
        {
            var context = generationRewriteContext.GenerationContext;
            string useCaseId = "";
            var useCaseParameters = context
                                        .MethodSharedContext
                                        .Parameters
                                        .Where(x => "useCaseId".Equals(x.Name, StringComparison.OrdinalIgnoreCase)
                                                        || x.Attributes.Any(y => UseCaseIdAttributeFullName.EndsWith(y.Name, StringComparison.Ordinal)))
                                        .ToArray();
            if (useCaseParameters.Length > 0)
            {
                if (useCaseParameters.Length != 1)
                {
                    var names = string.Join(", ", useCaseParameters.Select(x => x.Name));
                    throw new InvalidOperationException($"Multiple parameters match `Use Case Id` criteria: {names}");
                }
                if (useCaseParameters[0].Value != null)
                {
                    useCaseId = useCaseParameters[0].Value.ToString();
                }
            }
            _ = SnapshotProvider.ExecuteFuncWithMissedDirectoryOrFileCreation(_options, useCaseId, context.MethodSharedContext.FilePath, context.MethodSharedContext.MethodName, snapshotFilePath =>
            {
                if (context.Actual is byte[] bytes)
                {
                    File.WriteAllBytes(snapshotFilePath, bytes);
                }
                else if (context.Actual is Stream stream)
                {
                    var initialPosition = stream.Position;
                    //The file may be opened for read in current or other threads. Typically we expected it open by SnapshotProvider.OpenReadWithShareWrite in a test
                    using (var snapshotFileStream = File.Open(snapshotFilePath, FileMode.Open, FileAccess.Write, FileShare.Read))
                    {
                        snapshotFileStream.SetLength(0); //truncate the file
                        stream.CopyTo(snapshotFileStream);
                    }
                    //Reset the position because user cannot explicitely control this method call flow
                    stream.Position = initialPosition;
                }
                else if (_textFileEncoding == null)
                {
                    File.WriteAllText(snapshotFilePath, generationRewriteContext.Value);
                }
                else
                {
                    File.WriteAllText(snapshotFilePath, generationRewriteContext.Value, _textFileEncoding);
                }
                return true;
            });
            return new RewriteResult
            {
                IsRewritten = true,
            };
        }
    }
}
