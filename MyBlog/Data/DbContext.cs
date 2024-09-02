using Microsoft.EntityFrameworkCore;


namespace MyBlog.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
                DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Post>? Posts { get; set; }

    }

}
