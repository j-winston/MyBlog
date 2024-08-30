using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public string? Content { get; set; }
        public DateTime? DatePosted { get; set; }
        public IdentityUser? Author { get; set; }




    }
}

