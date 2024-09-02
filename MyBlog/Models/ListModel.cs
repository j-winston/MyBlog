using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models
{

    // Used to display all users in the AdminPanel view
    public class ListModel
    {
        //public IEnumerable<IdentityUser> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IdentityUser? User { get; set; }

        public ListModel()
        {
            Posts = Enumerable.Empty<Post>();

        }


    }
}
