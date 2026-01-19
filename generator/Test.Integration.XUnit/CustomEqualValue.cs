namespace Test.Integration.XUnit;

public sealed class CustomEqualValue
{
    public int Value { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is not CustomEqualValue customEqual)
        {
            return false;
        }
        return customEqual.Value == Value;
    }
    public override int GetHashCode() => Value.GetHashCode();
}
