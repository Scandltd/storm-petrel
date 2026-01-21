using System.Globalization;
using FluentAssertions;

namespace Scand.StormPetrel.Rewriter.Test.Resource
{
    internal class AttributesWithIndentedArgsTest
    {
        [Theory]
        [InlineData(new[] { "no separators" })]
        [InlineData( new[] { "white space as a separator" })]
        [InlineData(    new List<string>()
        {
            "1",
            "2"
        })]
        [InlineData(				new[] { "couple tabs as a separator" })]
        [InlineData(									new[] { "more tabs then parent white spaces as a separator" })]
        [InlineData(
new[] { "start from new line" })]
        [InlineData(
        new[] { "new line with indent as in parent attribute" })]
        [InlineData(
				new[] { "new line with couple tabs indent" })]
        [InlineData(
            new[] { "new line with extra indent" })]
        [InlineData(new[] {
                "new line with extra indent for array element" })]
        [InlineData(
            new[] {
                "new line with extra indent for array element" })]
        [InlineData(
            new[]
            {
                "new line with extra indent for array body and element" })]
        [InlineData(new[]
            {
                "new line with extra indent for array body and element" })]
        public void TestMethod(string[] expected)
        {
            //Act
            string[] actual = ["one_actual"];

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0,new[] { "no separators" })]
        [InlineData(10, new[] { "white space as a separator" })]
        [InlineData(20,    new[] { "couple white spaces as a separator" })]
        [InlineData(30,				new[] { "couple tabs as a separator" })]
        [InlineData(40									new[] { "more tabs then parent white spaces as a separator" })]
        [InlineData(50,
new[] { "start from new line" })]
        [InlineData(60,
        new[] { "new line with indent as in parent attribute" })]
        [InlineData(70,
				new[] { "new line with couple tabs indent" })]
        [InlineData(80,
            new[] { "new line with extra indent" })]
        [InlineData(90, new[] {
                "new line with extra indent for array element" })]
        [InlineData(100,
            new[] {
                "new line with extra indent for array element" })]
        [InlineData(110,
            new[]
            {
                "new line with extra indent for array body and element" })]
        [InlineData(120, new[]
            {
                "new line with extra indent for array body and element" })]
        public void TestMethodWithExtraArg(int extraArg, string[] expected)
        {
            //Act
            string[] actual = ["one_actual"];

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
