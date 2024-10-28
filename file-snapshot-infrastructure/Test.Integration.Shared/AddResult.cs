using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Integration.Shared
{
    public sealed class AddResult
    {
        public int Value { get; set; }
        public string ValueAsHexString { get; set; } = string.Empty;
    }
}
