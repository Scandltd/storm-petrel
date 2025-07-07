#if !DEBUG
namespace Test.Integration.ObjectDumper.XUnit;

public static partial class IgnoreInRelease
{
    public static int TestDataSourceCandidate() => EnsureProperIgnoreFilePathRegexIsAppliedConstant;
}
#endif
