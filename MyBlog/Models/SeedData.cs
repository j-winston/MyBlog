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

            await context.SaveChangesAsync();

        }
    }

}






