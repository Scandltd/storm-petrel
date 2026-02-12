using Microsoft.EntityFrameworkCore;

namespace Test.Integration.XUnit.EntityFrameworkExamples
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public static DbSet<User> Users => Set<User>();
        public static DbSet<Post> Posts => Set<Post>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Moderator)
                .WithMany()
                .HasForeignKey(p => p.ModeratorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public static class DbContextFactory
    {
        public static AppDbContext Create(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            return new AppDbContext(options);
        }
    }
}