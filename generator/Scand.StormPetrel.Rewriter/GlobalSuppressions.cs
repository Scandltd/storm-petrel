// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "We have intentionally developed synchronous version within asynchronous method", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.Extension.CSharpSyntaxRewriterExtension.RewriteAsync(Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter,System.IO.Stream,System.IO.Stream,System.Boolean)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Performance", "CA1851:Possible multiple enumerations of 'IEnumerable' collection", Justification = "We use memory only and don't call db or network in this case. So, prefer multiple enumerations against extra memory consumption", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.CSharp.SyntaxRewriter.EnumerableResultRewriter.VisitImplementation``1(``0,System.Collections.Generic.IEnumerable{Microsoft.CodeAnalysis.SyntaxNode},System.Func{``0,Microsoft.CodeAnalysis.SyntaxNode})~Microsoft.CodeAnalysis.SyntaxNode")]
