namespace Test.Integration.XUnit;

public struct TestedStructure(int x, int y) : IEquatable<TestedStructure>
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public bool Equals(TestedStructure other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is TestedStructure && Equals((TestedStructure)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(TestedStructure left, TestedStructure right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(TestedStructure left, TestedStructure right)
    {
        return !(left == right);
    }
}

