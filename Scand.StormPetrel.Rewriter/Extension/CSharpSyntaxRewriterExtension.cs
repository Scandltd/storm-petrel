using Microsoft.CodeAnalysis.CSharp;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Scand.StormPetrel.Rewriter.Extension
{
    public static class CSharpSyntaxRewriterExtension
    {
        public static async Task RewriteAsync(this CSharpSyntaxRewriter rewriter, string filePath)
            => await RewriteAsync(rewriter, filePath, true).ConfigureAwait(false);

        public static async Task RewriteAsync(this CSharpSyntaxRewriter rewriter, Stream inputStream, Stream outputStream)
            => await RewriteAsync(rewriter, inputStream, outputStream, true).ConfigureAwait(false);

        public static void Rewrite(this CSharpSyntaxRewriter rewriter, Stream inputStream, Stream outputStream)
            => RewriteAsync(rewriter, inputStream, outputStream, false).Wait();

        public static void Rewrite(this CSharpSyntaxRewriter rewriter, string filePath)
            => RewriteAsync(rewriter, filePath, false).Wait();

        private static async Task RewriteAsync(CSharpSyntaxRewriter rewriter, string filePath, bool isAsyncOtherwiseSync)
        {
            using (var inStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var outStream = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                await RewriteAsync(rewriter, inStream, outStream, isAsyncOtherwiseSync).ConfigureAwait(false);
                outStream.SetLength(outStream.Position); //truncate the file if need
            }
        }

        private static async Task RewriteAsync(CSharpSyntaxRewriter rewriter, Stream inputStream, Stream outputStream, bool isAsyncOtherwiseSync)
        {
            if (rewriter == null)
            {
                throw new ArgumentNullException(nameof(rewriter));
            }
            var utf8NoBomPreamble = new UTF8Encoding(false);
            using (var reader = new StreamReader(inputStream, utf8NoBomPreamble, true, 1024, true))
            {
                string inputCode;
                if (isAsyncOtherwiseSync)
                {
                    inputCode = await reader.ReadToEndAsync().ConfigureAwait(false);
                }
                else
                {
                    inputCode = reader.ReadToEnd();
                }
                var root = SyntaxFactory.ParseSyntaxTree(inputCode).GetRoot();
                var newSourceNode = rewriter.Visit(root);
                using (var writer = new StreamWriter(outputStream, reader.CurrentEncoding, 1024, true))
                {
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
                }
            }
        }
    }
}
