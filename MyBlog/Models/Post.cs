using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }

        public string? AuthorId { get; set; }

        // Nav property
        [ForeignKey("AuthorId")]
        public IdentityUser? Author { get; set; }

        public DateTime? AuthoredDate { get; set; }

    }
}
