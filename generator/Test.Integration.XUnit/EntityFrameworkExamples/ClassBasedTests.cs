using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Test.Integration.XUnit.EntityFrameworkExamples
{
    public class ClassBasedTests
    {
        [Fact]
        public async Task UserWithPosts()
        {
            var dbName = $"{GetType().FullName}-{"classUser"}";

            //Arrange
            var user = new User
            {
                Email = "user@test.com",
                Posts =
                {
                    new Post { Title = "Post 1" },
                    new Post { Title = "Post 2" }
                }
            };

            using var arrangeDb = DbContextFactory.Create(dbName);
            arrangeDb.Users.Add(user);
            await arrangeDb.SaveChangesAsync();

            //Act
            using var actDb = DbContextFactory.Create(dbName);
            var actual = await actDb.Users
                .Include(u => u.Posts)
                .SingleAsync();

            foreach (var post in actual.Posts)
            {
                post.User = null!;
            }

            //Assert
            var expected = new User();

            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(nameof(GetUsersWithPostsData))]
        public async Task UserWithPostsTheory(
            string useCaseTitle,
            string email,
            int postCount,
            string? postTitlePrefix,
            User expected)
        {
            ArgumentNullException.ThrowIfNull(useCaseTitle);

            var dbName = $"{GetType().FullName}-{useCaseTitle.Replace(" ", "-", StringComparison.Ordinal)}";

            using (var db = DbContextFactory.Create(dbName))
            {
                var posts = new List<Post>();
                for (int i = 0; i < postCount; i++)
                {
                    posts.Add(new Post { Title = $"{postTitlePrefix} {i + 1}" });
                }

                var user = new User
                {
                    Email = email,
                };
                foreach (var post in posts)
                {
                    user.Posts.Add(post);
                }

                db.Users.Add(user);
                await db.SaveChangesAsync();
            }

            using var actDb = DbContextFactory.Create(dbName);
            var tempActual = await actDb.Users
                .Include(u => u.Posts)
                .SingleAsync();

            foreach (var post in tempActual.Posts)
            {
                post.User = null!;
            }
            var actual = tempActual;

            actual.Should().BeEquivalentTo(expected);
        }

        public static TheoryData<string, string, int, string?, User> GetUsersWithPostsData =>
        new()
        {
        {
            "userWith2Posts",
            "theoryuser1@test.com",
            1,
            "Theory Post A",
            new User()
        },
        {
            "userWith1Post",
            "theoryuser2@test.com",
            2,
            "Theory Post B",
            new User()
        },
        {
            "userWith0Posts",
            "theoryuser3@test.com",
            0,
            null,
            new User()
        }
        };

    }
}
