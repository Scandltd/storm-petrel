namespace Test.Integration.XUnit
{
    internal class UnitTest1Helper
    {
        private static int _field = 123;
        private static int Expected { get; }
        private static int ExpectedGetSet { get; set; }
        private static int ExpectedReturn
        {
            get
            {
                return 123;
            }
        }
        private static int ExpectedReturnField
        {
            get
            {
                return _field;
            }
        }
        private static int ExpectedReturnMultiple
        {
            get
            {
                if (_field == -1)
                {
                    return 123;
                }
                return 124;
            }
        }
        private static int ExpectedArrow => 100;
        private static int ExpectedGetArrow { get => 123; }
    }
}
