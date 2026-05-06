namespace Test.Integration.XUnitV3AOT.LibraryBeingTested;

public class AddResult
{
    public int Value { get; set; }
    public string ValueAsHexString { get; set; } = string.Empty;
    public AddResultNestedInfo NestedInfo { get; set; } = new();
}
