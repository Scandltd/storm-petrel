//Auto-generated by PerformanceTestsGenerator.GeneratePerformanceTests. Do not change it manually.
using FluentAssertions;
namespace Test.Integration.Performance.XUnit.ExpectedInMethod
{
    [Trait("TestCategory", "Performance")]
    public sealed class ExpectedInMethodTest01
    {

        [Fact]
        public static void Test01()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected01();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 2);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test02()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected02();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test03()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected03();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 2);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test04()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected04();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test05()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected05();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 2);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test06()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected06();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test07()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected07();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 2);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test08()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected08();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test09()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected09();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 2);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
        [Fact]
        public static void Test10()
        {
            //Arrange
            var expected = ExpectedInMethodTest01Data.GetExpected10();
            //Act
            var actual = PerformanceTestsGenerator.GetTestDto(true, 1);
            //Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
