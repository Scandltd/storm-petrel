using FluentAssertions;

namespace Test.Integration.XUnit
{
    public class CalculatorTestTheory
    {
        [Theory]
        [InlineData(1, 5)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        public void AddTest(int a, int b)
        {
            //Arrange
            var expected = AddTestGetExpected(a, b);

            //Act
            var actual = Calculator.Add(a, b);

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        /// <summary>
        /// Possible variations of AddTestGetExpected static method are:
        /// - Method body may have pattern matches within pattern matches.
        /// - Method body may have `switch` and/or `if` expressions with return statements returning expected baselines.
        /// - The method may be placed in another class and/or file.
        /// </summary>
        private static AddResult AddTestGetExpected(int a, int b) => (a, b) switch
        {
            (1, 5) => new AddResult(), // should be overwritten with correct expected baseline after
                                       // CalculatorTestTheoryStormPetrel.AddTestStormPetrel execution
            (2, 2) => new AddResult(),
            (2, 3) => new AddResult(),
            _ => throw new InvalidOperationException(),
        };
    }
}
