using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace MyBlog.Controllers
{
    public class BlogPostController : Controller
    {
        private ApplicationDbContext _context;
        private AuthenticationService _authService;


        public BlogPostController(ApplicationDbContext context, AuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult CreatePost()
        {
            return View();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostModel model)
        {
            var loggedInUser = await _authService.GetLoggedInUser();

            var post = new Post()
            {
                Title = model.Title,
                Content = model.Content,
                AuthorId = loggedInUser.Id,
                AuthoredDate = DateTime.Today

            };

            Console.WriteLine("<>><<<<<<");
            _context.Posts.Add(post);

            _context.SaveChanges();

            return RedirectToAction("AdminPanel", "Account");


        }


        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Post? post = _context.Posts.Where(p => p.ID == id).FirstOrDefault();

                if (post != null)
                {


                    _context.Posts.Remove(post);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Create", "BlogPost");

        }


        [Authorize]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post? post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);

        }



        [Authorize]
        [HttpPost]
        public IActionResult Update(Post post)
        {

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }

}


