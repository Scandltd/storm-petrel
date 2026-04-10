using System;

namespace Scand.StormPetrel.Generator.Abstraction.ExtraContext
{
    /// <summary>
    /// Data about the test case source (method or property returning enumerable data) to be rewritten.
    /// </summary>
    public sealed class TestCaseSourceContext : AbstractExtraContext
    {
        /// <summary>
        /// Path to method or property returning enumerable data, which is used as a source for test cases.
        /// </summary>
        public string[] Path { get; set; } = Array.Empty<string>();
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        /// <summary>
        /// Path to property or field of the cell instance within method or property corresponding to <see cref="Path"/>.
        /// </summary>
        public string[] InvocationPath { get; set; } = Array.Empty<string>();
    }
}
