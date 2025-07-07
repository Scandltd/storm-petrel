namespace Test.Integration.ObjectDumper.XUnit;

#if DEBUG
partial class IgnoreInDebug
#else
partial class IgnoreInRelease
#endif
{
    /// <summary>
    /// This file is ignored by Storm Petrel. That's why the compilation should fail
    /// if proper appsettings.StormPetrel.XXX.json file with proper "IgnoreFilePathRegex" value is not applied.
    /// </summary>
    private const int EnsureProperIgnoreFilePathRegexIsAppliedConstant = 1;
}
