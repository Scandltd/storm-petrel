// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1035:Do not use APIs banned for analyzers", Justification = "We have to use the API. We use a cache to mitigate performance issue.", Scope = "member", Target = "~M:Scand.StormPetrel.Generator.Analyzer.AppSettingsFileReferenceAnalyzer.#ctor")]
