using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Test.Integration.XUnit.EntityFrameworkExamples
{
    public class AnonymousObjectTests
    {
        [Fact]
        public async Task UserProjectionAnonymous()
        {
            var dbName = $"{GetType().FullName}-{"anonymousObject"}"; ;

            var user = new User
            {
                Email = "anon@test.com",
                Posts =
                {
                    new Post { Title = "Anon Post 1" },
                    new Post { Title = "Anon Post 2" }
                }
            };

            using var arrangeDb = DbContextFactory.Create(dbName);
            arrangeDb.Users.Add(user);
            await arrangeDb.SaveChangesAsync();

            using var actDb = DbContextFactory.Create(dbName);
            var actual = await actDb.Users
                .Where(u => u.Email == "anon@test.com")
                .Select(u => new
                {
                    u.Email,
                    PostCount = u.Posts.Count
                })
                .SingleAsync();

            var expected = new 
            {
                Email = string.Empty,
                PostCount = 0
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetUserProjectionData))]
        public async Task UserProjectionAnonymousTheory(
            string useCaseTitle,
            string email,
            int postCount,
            string? postPrefix,
            object expected
        )
        {
            ArgumentNullException.ThrowIfNull(useCaseTitle);

            var dbName = $"{GetType().FullName}-{useCaseTitle.Replace(" ", "-", StringComparison.Ordinal)}";

            using (var arrangeDb = DbContextFactory.Create(dbName))
            {
                var posts = new List<Post>();

                for (int i = 0; i < postCount; i++)
                {
                    posts.Add(new Post { Title = $"{postPrefix} {i + 1}" });
                }

                var user = new User
                {
                    Email = email
                };

                foreach (var post in posts)
                {
                    user.Posts.Add(post);
                }

                arrangeDb.Users.Add(user);
                await arrangeDb.SaveChangesAsync();
            }

            using var actDb = DbContextFactory.Create(dbName);

            var actual = await actDb.Users
                .Where(u => u.Email == email)
                .Select(u => new
                {
                    u.Email,
                    u.Posts
                })
                .SingleAsync();

            actual.Should().BeEquivalentTo(expected);
        }

        public static TheoryData<string, string, int, string?, object> GetUserProjectionData =>
        new()
        {
            { 
                "anonymousObjectWith1Post",
                "theory1@test.com",
                1,
                "Post A",
                new User()
            },
            {
                "anonymousObjectWith2Posts",
                "theory2@test.com",
                2,
                "Post B",
                new User()
            },
            {
                "anonymousObjectWith0Posts",
                "theory3@test.com",
                0,
                null,
                new User()
            }
        };

    }
}