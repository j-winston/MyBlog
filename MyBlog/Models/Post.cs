using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public int? AuthorId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public IdentityUser? Author { get; set; }
        public List<Comment>? Comments { get; set; }
        public DateTime? AuthoredDate { get; set; }

    }
}
