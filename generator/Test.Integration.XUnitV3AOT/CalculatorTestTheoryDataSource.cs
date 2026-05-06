using Test.Integration.XUnitV3AOT.LibraryBeingTested;

namespace Test.Integration.XUnitV3AOT;

public sealed class CalculatorTestTheoryDataSource : TheoryData<int, int, AddResult>
{
    public CalculatorTestTheoryDataSource()
    {
        foreach (var useCase in DataMethod())
        {
            Add((int)useCase[0], (int)useCase[1], (AddResult)useCase[2]);
        }
    }
    private static IEnumerable<object[]> DataMethod() =>
    [
        [1, 2, new AddResult()],
        [-2, 2, new AddResult()],
        [int.MinValue, -1, new AddResult()],
        [-4, -6, new AddResult()],
    ];
}
