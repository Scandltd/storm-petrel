public class DataSource
{
    public static object[][] GetRows() =>
    [
        [], //Empty row
        [1, new List<string>()
        {
            "1",
            "2"
        }], //One cell
    ];
}
