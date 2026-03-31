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
#pragma warning disable CS0168 // Variable is declared but never used. Suppress for testing purposes: avoid auto-generating code compilation failure.
        catch (Exception e)
        {
            return e;
        }
#pragma warning restore CS0168 // Variable is declared but never used
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
    public static string MethodWithIgnoredArrowsAndReturns()
    {
#pragma warning disable CS8321 // Local function is declared but never used. Suppress for testing purposes: avoid auto-generating code compilation failure.
        static string localFunction() => "LocalFunctionResult";
        static string localFunctionWithReturn()
        {
            return "LocalFunctionWithReturnResult";
        }
#pragma warning restore CS8321 // Local function is declared but never used
        var sum = new[] { 1, 2 }.Select(x => x * 2).ToArray();
        var sumWithReturn = new[] { 1, 2 }.Select(x =>
        {
            return x * 2;
        }).ToArray();
        var sumViaParenthesizedLambda = new Func<int, int, int>((x, y) => x + y);
        var sumViaParenthesizedLambdaAndReturn = new Func<int, int, int>((x, y) =>
        {
            return x + y;
        });
        var sumViaAnonymousMethod = new Func<int, int, int>(delegate (int x, int y)
        {
            return x + y;
        });
        return $"Stuff above result: {localFunction()}, {localFunctionWithReturn()}, {sum}, {sumWithReturn}, {sumViaParenthesizedLambda(1, 2)}, {sumViaParenthesizedLambda(3, 4)}, {sumViaAnonymousMethod(5, 6)}";
    }
}
