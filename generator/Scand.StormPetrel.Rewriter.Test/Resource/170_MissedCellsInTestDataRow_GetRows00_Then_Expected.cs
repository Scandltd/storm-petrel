public class DataSource
{
    public static object[][] GetRows() =>
    [
        [new List<string>()
        {
            "1",
            "2"
        }], //Empty row
        [1], //One cell
    ];
}
