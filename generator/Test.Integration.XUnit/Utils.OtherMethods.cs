using System.Text.Json;

namespace Test.Integration.XUnit;

partial class Utils
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
    public static string? ToExceptionInfoJson(this Exception? exception)
        => ToExceptionInfo(exception)?.ToJsonText();
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
