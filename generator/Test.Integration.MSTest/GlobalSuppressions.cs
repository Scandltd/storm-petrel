// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Style", "IDE0074:Use compound assignment", Justification = "We use the code in projects where compound assignment is not supported", Scope = "member", Target = "~M:Test.Integration.Shared.BackupHelper.DeleteBackup(System.String,System.Action{Test.Integration.Shared.BackupHelperResult},System.String,System.Boolean)")]
[assembly: SuppressMessage("Style", "IDE0057:Use range operator", Justification = "We use the code in projects where range operator is not supported", Scope = "member", Target = "~M:Test.Integration.Shared.BackupHelper.DeleteBackup(System.String,System.Action{Test.Integration.Shared.BackupHelperResult},System.String,System.Boolean)")]
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "We use the code in projects where target-typed new expression is not supported", Scope = "member", Target = "~F:Test.Integration.Shared.BackupHelper.ClassFullNameToLock")]
[assembly: SuppressMessage("Style", "IDE0090:Use 'new(...)'", Justification = "We use the code in projects where target-typed new expression is not supported", Scope = "member", Target = "~F:Test.Integration.Shared.BackupHelper.ClassFullNameToWasHandled")]
