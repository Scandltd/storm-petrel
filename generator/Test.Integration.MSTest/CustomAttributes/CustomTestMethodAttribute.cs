using System.Runtime.CompilerServices;

namespace MSTest;

[AttributeUsage(AttributeTargets.Method)]
public sealed class CustomTestMethodAttribute([CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = -1) : TestMethodAttribute(callerFilePath, callerLineNumber) {
    public string? CallerFilePath { get; }
    public int CallerLineNumber { get; }
}
