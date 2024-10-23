using VarDump.Visitor;

namespace Test.Integration.XUnit
{
    internal static partial class Utils
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
    }
}
