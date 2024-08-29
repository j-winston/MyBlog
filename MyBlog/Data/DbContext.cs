using Microsoft.EntityFrameworkCore;
using MyBlog.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts;

}


