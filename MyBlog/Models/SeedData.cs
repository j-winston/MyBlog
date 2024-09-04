using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyBlog.Utilities;



namespace MyBlog.Models
{
    public static class SeedData
    {


        // Get required services to create a default user and dummy posts
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            var defaultUser = await userManager.FindByEmailAsync("defaultuser@example.com");
            if (defaultUser == null)
            {
                var user = new IdentityUser
                {
                    UserName = "defaultuser@example.com",
                    Email = "defaultuser@example.com",
                    EmailConfirmed = true

                };

                var result = await userManager.CreateAsync(user, "**REMOVED**");
                if (result.Succeeded)
                {
                    defaultUser = user;
                }
            }


            if (defaultUser != null && !context.Posts.Any())
            {
                var posts = new List<Post>
                {

                new Post

                {
                    Title = "First Post",
                    Content = "This is a the content of the second post.",
                    AuthoredDate = DateTime.Now,
                    Author = defaultUser,
                    AuthorId = defaultUser.Id,
                    Slug = SlugHelper.GenerateSlug("First Post")
                },
                    new Post
                    {
                        Title = "Second Post",
                        Content = "This is the second post",
                        AuthoredDate = DateTime.Now,
                        Author = defaultUser,
                        AuthorId = defaultUser.Id,
                        Slug = SlugHelper.GenerateSlug("Second Post")
                     }
                };


                context.Posts.AddRange(posts);
                await context.SaveChangesAsync();

            }
        }

    }

}




