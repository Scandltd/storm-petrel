namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext
{
    /// <summary>
    /// Data about the test case source (method or property returning enumerable data) to be rewritten.
    /// </summary>
    public sealed class TestCaseSourceContext : AbstractExtraContext
    {
        public string[] Path { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
    }
}
