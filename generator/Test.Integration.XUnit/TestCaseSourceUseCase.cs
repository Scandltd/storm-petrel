namespace Test.Integration.XUnit;

/// <summary>
/// For use case with one input parameter.
/// The equality operator (==) compares <paramref name="Name"/> only.
/// Similar records can be created for two or more input parameters.
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <param name="Name"></param>
/// <param name="Input"></param>
public record TestCaseSourceUseCase<T1>(string Name, T1 Input1)
{
    public virtual bool Equals(TestCaseSourceUseCase<T1>? other) => other is not null && Name == other.Name;
    public override int GetHashCode() => Name.GetHashCode(StringComparison.Ordinal);
}
