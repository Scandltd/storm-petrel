#if DEBUG
namespace Test.Integration.ObjectDumper.XUnit;

public static partial class IgnoreInDebug
{
    public static int TestDataSourceCandidate() => EnsureProperIgnoreFilePathRegexIsAppliedConstant;
}
#endif
