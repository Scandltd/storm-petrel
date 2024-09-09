// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1035:Do not use APIs banned for analyzers", Justification = "We call file system API one time only, but facilitate the source generator configuration", Scope = "member", Target = "~M:Scand.StormPetrel.Generator.GeneratorInfoCache.GetProjectRootFromFileSystem(System.String)")]
