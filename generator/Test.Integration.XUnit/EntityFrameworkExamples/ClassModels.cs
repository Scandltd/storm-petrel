using System.Collections.ObjectModel;

namespace Test.Integration.XUnit.EntityFrameworkExamples
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public ICollection<Post> Posts { get; init; } = [];
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? ModeratorId { get; set; }
        public User? Moderator { get; set; }
    }
}
