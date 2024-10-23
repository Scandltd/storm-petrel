// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "We have intentionally developed synchronous version within asynchronous method", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.Extension.CSharpSyntaxRewriterExtension.RewriteAsync(Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter,System.IO.Stream,System.IO.Stream,System.Boolean)~System.Threading.Tasks.Task")]
