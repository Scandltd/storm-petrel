using System;
using System.Collections.Generic;

namespace Scand.StormPetrel.Generator.Abstraction
{
    /// <summary>
    /// Data about the current test method.
    /// </summary>
    public sealed class MethodContext
    {
        public string FilePath { get; set; } = "";
        public string ClassName { get; set; } = "";
        public string MethodName { get; set; } = "";
        public int VariablePairCurrentIndex { get; set; }
        public int VariablePairsCount { get; set; }
        public ParameterInfo[] Parameters { get; set; } = Array.Empty<ParameterInfo>();
        /// <summary>
        /// Custom properties that can be used to store any additional data related to the current test method
        /// in implementations of <see cref="IGenerator"/> and other interfaces.
        /// </summary>
        public IDictionary<string, object?> CustomProperties { get; } = new Dictionary<string, object?>();
    }
}
