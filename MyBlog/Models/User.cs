using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{
    public class User : IdentityUser
    {
        public DateTime? CreationDate { get; set; }
        public ICollection<Post>? BlogPosts { get; set; }
    }
}
