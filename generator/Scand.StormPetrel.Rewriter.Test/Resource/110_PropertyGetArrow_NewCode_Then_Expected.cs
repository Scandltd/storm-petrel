namespace Test.Integration.XUnit
{
    internal class UnitTest1Helper
    {
        private static int _field = 123;
        private static int Expected { get; }
        private static AddResult ExpectedAddResult { get; } = new AddResult
        {
            Value = 5,
        };
        private static int ExpectedGetSet { get; set; }
        private static AddResult ExpectedGetSetAddResult { get; set; } = new()
        {
            Value = 5,
        };
        private static AddResult ExpectedReturn
        {
            get
            {
                return new AddResult()
                {
                    Value = 5,
                };
            }
        }
        private static int ExpectedReturnField
        {
            get
            {
                return _field;
            }
        }
        private static AddResult ExpectedReturnMultiple
        {
            get
            {
                if (_field == -1)
                {
                    return new AddResult()
                    {
                        Value = 5,
                    };
                }
                return new AddResult()
                {
                    Value = 10,
                };
            }
        }
        private static AddResult ExpectedArrow => new()
        {
            Value = 5,
        };
        private static AddResult ExpectedGetArrow { get => 100; }
    }
}
