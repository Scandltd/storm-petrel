using FluentAssertions;
using Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter;
using Scand.StormPetrel.Rewriter.Extension;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Scand.StormPetrel.Rewriter.Test
{
    public class CSharpSyntaxRewriterExtensionTest
    {
        private static readonly string[] declarationPaths = new[] { "MyClass", "MyMethod", "foo" };

        [Theory]
        [InlineData("utf8", false)]
        [InlineData("utf8", true)]
        //[InlineData("utf32", false)] //do not support this case
        [InlineData("utf32", true)]
        public async Task WhenWriteThenPreserveBomPreambleTest(string fileEncoding, bool useBomPreamble)
        {
            //Arrange
            var fileName = nameof(CSharpSyntaxRewriterExtensionTest) + ".cs";
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            Encoding encoding = fileEncoding switch
            {
                "utf8" => new UTF8Encoding(useBomPreamble),
                "utf32" => new UTF32Encoding(true, useBomPreamble),
                _ => throw new InvalidOperationException(),
            };
            var code =
@"class MyClass
{
    void MyMethod()
    {
        var foo = 1;
    }
}
";
            var expectedCode = code.Replace('1', '2');
            await File.WriteAllTextAsync(fileName, code, encoding);
            var rewriter = new DeclarationRewriter(declarationPaths, "2");

            //Act
            await rewriter.RewriteAsync(fileName);
            long actualLength = new FileInfo(fileName).Length;

            //Assert
            string? actualCode = null;
            byte[]? actualPreamble = null;
            using (var reader = new StreamReader(fileName, encoding, true))
            {
                actualCode = await reader.ReadToEndAsync();
                actualPreamble = reader.CurrentEncoding.GetPreamble();
            }
            File.Delete(fileName);
            actualCode.Should().Be(expectedCode);
            actualLength.Should().Be(GetExpectedFileLength());
            actualPreamble.Length.Should().Be(GetPreambleLength());

            int GetExpectedFileLength() => fileEncoding switch
            {
                "utf8" => expectedCode.Length + GetPreambleLength(),
                "utf32" => 4 * expectedCode.Length + GetPreambleLength(),
                _ => throw new InvalidOperationException(),
            };

            int GetPreambleLength() => fileEncoding switch
            {
                "utf8" => useBomPreamble ? 3 : 0,
                "utf32" => useBomPreamble ? 4 : 0,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
