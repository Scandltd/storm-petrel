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

    public static string? ToExceptionInfoJson(this Exception? exception) => ToExceptionInfo(exception)?.ToJsonText();
    public static ExceptionInfo? ToExceptionInfo(this Exception? exception)
    {
        if (exception == null)
        {
            return null;
        }

        return new()
        {
            Type = exception.GetType().FullName,
            Message = exception.Message,
            Source = exception.Source,
        };
    }

    public static string ToJsonText<T>(this T obj) => JsonSerializer.Serialize(obj, options);
}