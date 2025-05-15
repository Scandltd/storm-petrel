// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1035:Do not use APIs banned for analyzers", Justification = "We call file system API one time only, but facilitate the source generator configuration", Scope = "member", Target = "~M:Scand.StormPetrel.Generator.GeneratorInfoCache.GetProjectRootFromFileSystem(System.String)")]
[assembly: SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "To simplify Data Transfer Object (DTO) class", Scope = "member", Target = "~P:Scand.StormPetrel.Generator.TargetProject.MainConfig.TestVariablePairConfigs")]
[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1035:Do not use APIs banned for analyzers", Justification = "We call file the API one time only, but allow custom attributes feature", Scope = "member", Target = "~M:Scand.StormPetrel.Generator.Config.GeneratorPrimaryConfig.#ctor")]
[assembly: SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "To simplify public API we use for config deserialization only", Scope = "member", Target = "~P:Scand.StormPetrel.Generator.Config.GeneratorPrimaryConfigModel.CustomTestAttributes")]
