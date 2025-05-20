using VarDump.Visitor;

namespace Test.Integration.XUnit;
internal static partial class UtilsStormPetrel
{
    public static DumpOptions GetDumpOptions()
    {
        var options = new DumpOptions
        {
            Descriptors =
            {
                new IgnoredMembersMiddleware(),
            }
        };
        return options;
    }

    public static (int NodeKind, int NodeIndex) GetDumpOptionsStormPetrel()
    {
        var options = new DumpOptions
        {
            Descriptors =
            {
                new IgnoredMembersMiddleware(),
            }
        };
        return (8805, 0);
    }
}