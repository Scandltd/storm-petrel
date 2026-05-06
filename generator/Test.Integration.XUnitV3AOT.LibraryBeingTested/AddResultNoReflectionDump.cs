namespace Test.Integration.XUnitV3AOT.LibraryBeingTested;

/// <summary>
/// We won't use reflection to dump the result of this class.
/// </summary>
public class AddResultNoReflectionDump
{
    public int Value { get; set; }
    public string ValueAsHexString { get; set; } = string.Empty;
}
