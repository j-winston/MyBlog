using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {

        private Post? _myPost;
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }


        // Define actions
        public ViewResult Index()
        {
            User admin = new User();

            admin.UserName = "James";
            admin.Email = "kenjameswinston@gmail.com";
            admin.PasswordHash = "null";
            admin.CreationDate = DateTime.Today;
            admin.BlogPosts = new List<Post>
            {
                new Post {
                Title = "How to Finish Tough Stuff",
                Content = "A lot of moving parts",
                Author = admin,
                Comments = new List<Comment> {
                    new Comment {
                        Content = ">>Best post ever, bro!",
                        DatePosted = DateTime.Today,
                        Author = admin,
                    }
                },


                }



            };

            _context.Users.Add(admin);
            _context.SaveChanges();

            IEnumerable<Post> allPosts = _context.Posts.Include(p => p.Author)
                .Include(p => p.Comments)
                .ToList();


            return View(allPosts);
        }
    }


}
