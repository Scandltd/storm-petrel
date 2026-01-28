using System.Text.Json;

namespace Test.Integration.XUnit;
partial class UtilsStormPetrel
{
    private readonly static JsonSerializerOptions options = new()
    {
        WriteIndented = false
    };
    public static Exception? SafeExecute(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            return e;
        }

        return null;
    }

    public static (int NodeKind, int NodeIndex) SafeExecuteStormPetrel(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            return (8805, 0);
        }

        return (8805, 1);
    }
}