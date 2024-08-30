using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;

namespace MyBlog.Controllers
{
    public class BlogPostController : Controller
    {
        private ApplicationDbContext _context;

        private User _testUser = new User
        {
            UserName = "James",
            Email = "kenjameswinston@gmail.com",
            PasswordHash = "null",
            CreationDate = DateTime.Today

        };


        public BlogPostController(ApplicationDbContext context)
        {
            _context = context;

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(Post post)
        {


            if (ModelState.IsValid)
            {
                // Add author and date
                post.AuthoredDate = DateTime.Today;

                // Add posts
                _testUser.BlogPosts = new List<Post>();
                _testUser.BlogPosts.Add(post);

                // Add comments 
                Comment comment = new Comment
                {
                    Content = "Bro, this is hot.",
                    DatePosted = DateTime.Today,
                    Author = _testUser

                };

                // add to database
                _context.Posts.Add(post);

                // save
                _context.SaveChanges();

                // Redirect to Index on success
                return RedirectToAction("Index", "Home");

            }
            // If validation fails show post again
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                Post? post = _context.Posts.Where(p => p.ID == id).FirstOrDefault();

                if (post != null)
                {
                    List<Comment>? commentsToDelete = post.Comments;

                    if (commentsToDelete != null)
                    {
                        _context.Comments.RemoveRange(commentsToDelete);

                        _context.SaveChanges();

                    }

                    _context.Posts.Remove(post);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Create", "BlogPost");

        }


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


        [HttpPost]
        public IActionResult Update(Post post)
        {

            if (post == null)
            {
                return NotFound();
            }

            post.Author = _testUser;

            _context.Posts.Update(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


    }

}


