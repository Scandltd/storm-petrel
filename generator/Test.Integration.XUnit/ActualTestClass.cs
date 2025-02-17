namespace Test.Integration.XUnit;

public class ActualTestClass
{
    private readonly int actual = 100;

    public async Task<int> TestMethodAsync()
    {
        await Task.Delay(100);
        return actual;
    }
}