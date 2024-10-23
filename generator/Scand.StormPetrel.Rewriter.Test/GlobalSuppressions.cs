// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1849:Call async methods when in an async method", Justification = "We have intentionally developed synchronous version within asynchronous method", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.Test.MainTest.RewriteTest(System.String,System.String,System.String,System.String,System.String)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments", Justification = "We have to use it due to attribute arguments syntax limitation", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.Test.DataSourceHelperTest.WhenEnumerateWithParametersThenExpectedArray(System.String,System.Object[],System.Int32[])")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "To test use case", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.Test.DataSourceHelperTestData.PublicNonStaticMethod~System.Collections.Generic.IEnumerable{System.Object[]}")]
[assembly: SuppressMessage("Performance", "CA1823:Avoid unused private fields", Justification = "To test use case", Scope = "member", Target = "~F:Scand.StormPetrel.Rewriter.Test.DataSourceHelperTestData.PrivateStaticField")]
[assembly: SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "To test use case", Scope = "member", Target = "~F:Scand.StormPetrel.Rewriter.Test.DataSourceHelperTestData.PrivateStaticField")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "To test use case", Scope = "member", Target = "~P:Scand.StormPetrel.Rewriter.Test.DataSourceHelperTestData.PrivateStaticProperty")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "To test use case", Scope = "member", Target = "~M:Scand.StormPetrel.Rewriter.Test.DataSourceHelperTestData.PrivateStaticMethod~System.Collections.Generic.IEnumerable{System.Object[]}")]
