// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1035:Do not use APIs banned for analyzers", Justification = "We call file system API one time only, but facilitate the source generator configuration", Scope = "member", Target = "~M:Scand.StormPetrel.Generator.GeneratorInfoCache.GetProjectRootFromFileSystem(System.String)")]
[assembly: SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "We need boolean property in lower case here according to C# syntax", Scope = "member", Target = "~M:Scand.StormPetrel.Generator.SyntaxHelper.GetNewCodeBlock(System.String,System.String,System.String[],Scand.StormPetrel.Generator.VarPairInfo,System.Int32,System.Boolean)~System.Collections.Generic.List{Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax}")]
[assembly: SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "To simplify Data Transfer Object (DTO) class", Scope = "member", Target = "~P:Scand.StormPetrel.Generator.TargetProject.MainConfig.TestVariablePairConfigs")]
