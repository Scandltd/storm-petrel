using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Test.Integration.XUnit.EntityFrameworkExamples;

public class AnonymousObjectTests
{
    [Fact]
    public async Task UserProjectionAsAnonymousTest()
    {
        //Arrange
        var dbName = $"{GetType().FullName}-{nameof(UserProjectionAsAnonymousTest)}";

        var user = new User
        {
            Email = "anonymous@test.com",
            Posts =
            {
                new Post
                {
                    Title = "Anonymous Post 1",
                },
                new Post
                {
                    Title = "Anonymous Post 2",
                }
            }
        };

        using var arrangeDb = DbContextFactory.Create(dbName);
        arrangeDb.Users.Add(user);
        await arrangeDb.SaveChangesAsync();

        //Act
        using var actDb = DbContextFactory.Create(dbName);
        var actual = await actDb.Users
            .Where(u => u.Email == "anonymous@test.com")
            .Select(u => new
            {
                u.Email,
                PostCount = u.Posts.Count,
            })
            .SingleAsync();

        //Assert
        var expected = new 
        {
            Email = string.Empty,
            PostCount = 0
        };

        actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(GetUserProjectionAsAnonymousData))]
    public async Task UserProjectionAsAnonymousTheoryTest(
        string useCaseTitle,
        string email,
        int postCount,
        string? postPrefix,
        object expected
    )
    {
        //Arrange
        ArgumentNullException.ThrowIfNull(useCaseTitle);

        var dbName = $"{GetType().FullName}-{useCaseTitle.Replace(" ", "-", StringComparison.Ordinal)}";

        using var arrangeDb = DbContextFactory.Create(dbName);
        var posts = new List<Post>();

        for (int i = 0; i < postCount; i++)
        {
            posts.Add(new Post { Title = $"{postPrefix} {i + 1}" });
        }

        var user = new User
        {
            Email = email,
            Posts = posts,
        };

        arrangeDb.Users.Add(user);
        await arrangeDb.SaveChangesAsync();

        //Act
        using var actDb = DbContextFactory.Create(dbName);
        var actual = await actDb.Users
            .Where(u => u.Email == email)
            .Select(u => new
            {
                u.Email,
                u.Posts,
            })
            .SingleAsync();

        //Assert
        actual.Should().BeEquivalentTo(expected);
    }

    public static TheoryData<string, string, int, string?, object> GetUserProjectionAsAnonymousData =>
    new()
    {
        { 
            "anonymousObjectWith1Post",
            "theory1@test.com",
            1,
            "Post A",
            new()
        },
        {
            "anonymousObjectWith2Posts",
            "theory2@test.com",
            2,
            "Post B",
            new()
        },
        {
            "anonymousObjectWith0Posts",
            "theory3@test.com",
            0,
            null,
            new()
        }
    };
}