using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogPostController : Controller
    {
        // TODO: create field for database context
        private ApplicationDbContext? _context;

        // TODO: Inject ApplicationDbContext to interact with database 
        public BlogPostController(ApplicationDbContext context)
        {
            _context = context;
        }


        // TODO: Create action to handle GET requests to display the form for 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.AuthoredDate = DateTime.Now;

                _context?.Posts.Add(blogPost);

                // save changes to the database
                _context?.SaveChanges();

                // Redirect to Index on success
                return RedirectToAction("Index");

            }
            // If validation fails show post again
            return View(blogPost);
        }

    }
}

