using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;

        }

        // Define actions
        public async Task<IActionResult> Index()
        {
            // get posts 
            var posts = await _context.Posts.OrderBy(p => p.AuthoredDate).ToListAsync();

            return View(posts);

        }


    }


}
