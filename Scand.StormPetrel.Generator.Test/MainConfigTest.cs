using FluentAssertions;
using Scand.StormPetrel.Generator.TargetProject;

namespace Scand.StormPetrel.Generator.Test
{
    public class MainConfigTest
    {
        [Theory]
        [InlineData(null, "FooFile.cs", false)]
        [InlineData("", "FooFile.cs", false)]
        [InlineData(@"FooFile\.cs", null, false)]
        [InlineData(@"FooFile\.cs", "", false)]
        [InlineData(@"FooFile\.cs", "FooFile.cs")]
        [InlineData(@"FooFile\.cs", "foofile.cs")]
        [InlineData(@"FooFile\.cs", @"c:\temp\foofile.cs")]
        [InlineData(@"FooFile\.cs", @"/c/temp/foofile.cs")]
        [InlineData(@"FooFile\.cs", @"c:\temp\myfoofile.cs")]
        [InlineData(@"FooFile\.cs", @"/c/temp/myfoofile.cs")]
        [InlineData(@"(FooFile\.cs)|(BlaFile\.cs)", @"c:\temp\myfoofile.cs")]
        [InlineData(@"(FooFile\.cs)|(BlaFile\.cs)", @"/c/temp/myfoofile.cs")]
        [InlineData(@"(FooFile\.cs)|(BlaFile\.cs)", @"c:\temp\myblafile.cs")]
        [InlineData(@"(FooFile\.cs)|(BlaFile\.cs)", @"/c/temp/myblafile.cs")]
        [InlineData(@"(FooFile\.cs)|(BlaFile\.cs)", @"c:\temp\myb-lafile.cs", false)]
        [InlineData(@"(FooFile\.cs)|(BlaFile\.cs)", @"/c/temp/myb-lafile.cs", false)]
        public void IsMatchToIgnoreFileFileRegExTest(string? regex, string? path, bool expected = true)
        {
            //Arrange
            var config = new MainConfig
            {
                IgnoreFilePathRegex = regex,
            };

            //Act
            var actual = config.IsMatchToIgnoreFilePathRegex(path);

            //Assert
            actual.Should().Be(expected);
        }
    }
}
