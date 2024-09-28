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


            // If there's no default user, create one. 
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
                    Title = "Why I Keep Going Back to Neovim",
                    Content = "Neovim is strangely carthartic to me",
                    AuthoredDate = DateTime.Now.Date,
                    Author = defaultUser,
                    AuthorId = defaultUser.Id,
                    Slug = SlugHelper.GenerateSlug("why-i-keep-going-back-to-nvim")
                },
                    new Post
                    {
                        Title = "My Personal 80/20 Linux Commands List",
                        Content = "These are the most useful commands and flags",

                        AuthoredDate = DateTime.Now.Date,
                        Author = defaultUser,
                        AuthorId = defaultUser.Id,
                        Slug = SlugHelper.GenerateSlug("my-personal-80-20-linux-commands")
                     }
                };


                context.Posts.AddRange(posts);
                await context.SaveChangesAsync();

            }
        }

    }

}




