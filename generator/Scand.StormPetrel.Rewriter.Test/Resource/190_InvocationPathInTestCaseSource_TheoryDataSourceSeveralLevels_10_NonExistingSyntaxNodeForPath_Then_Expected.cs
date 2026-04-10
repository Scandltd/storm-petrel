public class DataSource
{
    public static object[][] GetRows() =>
    [
        [
            new FooClass()
            {
                BlaProperty = "some value",
            }
        ],
    ];
    public static TheoryData<int, AddResult, AddResult> TheoryDataSource =>
    new()
    {
        {
            2,
            new()
            {
                Value = 2,
            },
            new AddResult()
            {
                Value = 5,
                ValueAsHexString = "0x5 - Incorrect",
            }
        },
        {
            2,
            // With comment
            new() // With comment
            {
                Value = 2,
            },
            new AddResult
            {
                Value = 5,
                ValueAsHexString = "0x5 - Incorrect",
            }
        },
        {
            2,
            new(new WhenConstructorArgumentThenShouldBeIgnored()
            {
                Value = 100
            })
            {
                Value = 2,
            },
            new(),
        }
    };
    public static TheoryData<AddResultWrapper, int, int> TheoryDataSourceSeveralLevels =>
    new()
    {
        {
            new AddResultWrapper()
            {
                WrappedValue = new AddResult()
                {
                    Value = 5,
                }
            },
            2,
            2,
        },
        {
            new()
            {
                WrappedValue = new()
                {
                    Value = 5,
                }
            },
            -2,
            100,
        },
    };
}