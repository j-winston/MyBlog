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

            IEnumerable<Post> allPosts = _context.Posts.Include(p => p.Author)
                .Include(p => p.Comments)
                .ToList();

            if (allPosts != null)
            {
                return View(allPosts);
            }
            else
            {
                return View();
            }
        }
    }


}
