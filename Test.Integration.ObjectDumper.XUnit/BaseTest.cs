using FluentAssertions;
using System.Globalization;

namespace Test.Integration.ObjectDumper.XUnit
{
    public class BaseTest
    {
        [Fact]
        public void WhenObjectDumperIsUsedThenOkTest()
        {
            //Arrange
            var expected = new Result
            {
                IntValue = 1,
                StringValue = "Incorrect Value",
                DateTimeValue = DateTime.ParseExact("2024-07-04T00:00:00.0000000", "O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind)
            };
            //Act
            var actual = new Result
            {
                IntValue = 2,
                StringValue = "Correct Value",
                DateTimeValue = new DateTime(2024, 7, 5),
            };
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        public static DumpOptions DumpOptions => new()
        {
            DumpStyle = DumpStyle.CSharp,
            IndentSize = 4,
        };
    }

    public sealed class Result
    {
        public int IntValue { get; set; }
        public string? StringValue { get; set; }
        public DateTime DateTimeValue { get; set; }
    }
}