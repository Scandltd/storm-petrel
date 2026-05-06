using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Rewriter.Extension
{
    public static class CSharpSyntaxRewriterExtension
    {
        public static async Task<bool> RewriteAsync(this CSharpSyntaxRewriter rewriter, string filePath, int openFileRetriesCount = 3)
            => await RewriteAsync(rewriter, filePath, true, openFileRetriesCount).ConfigureAwait(false);

        public static async Task<bool> RewriteAsync(this CSharpSyntaxRewriter rewriter, Stream inputStream, Stream outputStream)
            => await RewriteAsync(rewriter, inputStream ?? throw new ArgumentNullException(nameof(inputStream)), outputStream, true).ConfigureAwait(false);

        public static bool Rewrite(this CSharpSyntaxRewriter rewriter, Stream inputStream, Stream outputStream)
            => RewriteAsync(rewriter, inputStream ?? throw new ArgumentNullException(nameof(inputStream)), outputStream, false).Result;

        public static bool Rewrite(this CSharpSyntaxRewriter rewriter, string filePath, int openFileRetriesCount = 3)
            => RewriteAsync(rewriter, filePath, false, openFileRetriesCount).Result;

        private static async Task<bool> RewriteAsync(CSharpSyntaxRewriter rewriter, string filePath, bool isAsyncOtherwiseSync, int openFileRetriesCount = 3)
        {
            if (openFileRetriesCount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(openFileRetriesCount), "The number of retries must be non-negative.");
            }
            FileStream fileStream = null;
            try
            {
                int attemptsLeftCount = openFileRetriesCount + 1;
                while (attemptsLeftCount > 0)
                {
                    attemptsLeftCount--;
                    try
                    {
                        fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
                        break; // Successfully opened the file, exit the loop
                    }
                    catch (IOException)
                    {
                        if (attemptsLeftCount == 0)
                        {
                            throw;
                        }
                        // Workaround: For unknown reason, sometimes the file may be locked while executing xUnit AOT test via `dotnet test`. So, retry File.Open(...).
                        int delay = (int)Math.Pow(2, openFileRetriesCount - attemptsLeftCount - 1);
                        if (isAsyncOtherwiseSync)
                        {
                            await Task.Delay(delay).ConfigureAwait(false);
                        }
                        else
                        {
                            Thread.Sleep(delay);
                        }
                    }
                }
                var result = await RewriteAsync(rewriter, fileStream, fileStream, isAsyncOtherwiseSync).ConfigureAwait(false);
                if (result)
                {
                    fileStream.SetLength(fileStream.Position); //truncate the file if need
                }
                return result;
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        private static async Task<bool> RewriteAsync(CSharpSyntaxRewriter rewriter, Stream inputStream, Stream outputStream, bool isAsyncOtherwiseSync)
        {
            if (rewriter == null)
            {
                throw new ArgumentNullException(nameof(rewriter));
            }
            var utf8NoBomPreamble = new UTF8Encoding(false);
            SyntaxNode newSourceNode;
            Encoding currentEncoding;
            using (var reader = new StreamReader(inputStream, utf8NoBomPreamble, true, 1024, true))
            {
                var inputStreamPosition = inputStream.Position;
                string inputCode;
                if (isAsyncOtherwiseSync)
                {
                    inputCode = await reader.ReadToEndAsync().ConfigureAwait(false);
                }
                else
                {
                    inputCode = reader.ReadToEnd();
                }
                inputStream.Position = inputStreamPosition; //reset the position of the input stream
                var root = SyntaxFactory.ParseSyntaxTree(inputCode).GetRoot();
                newSourceNode = rewriter.Visit(root);
                if (root == newSourceNode)
                {
                    return false;
                }
                currentEncoding = reader.CurrentEncoding;
            }
            using var writer = new StreamWriter(outputStream, currentEncoding, 1024, true);
            var newSource = newSourceNode.ToFullString();
            if (isAsyncOtherwiseSync)
            {
                await writer.WriteAsync(newSource).ConfigureAwait(false);
                await writer.FlushAsync().ConfigureAwait(false);
            }
            else
            {
                writer.Write(newSource);
                writer.Flush();
            }
            return true;
        }
    }
}
