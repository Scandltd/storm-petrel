namespace Test.Integration.XUnit;

partial class TestCaseSourceMemberDataTest
{
    public static TheoryData<int, int, string> MemberDataInPartialFile() =>
    new()
    {
        { 1, 2, "0x0" },
        { -2, 2, "0x0" },
        { int.MinValue, -1, "0x0" },
        { -4, -6, "0x0" },
    };

    public static TheoryData<int, int, string> MemberDataPropertyInPartialFile =>
    new()
    {
        { 1, 2, "0x0" },
        { int.MinValue, -1, "0x0" },
    };
}
