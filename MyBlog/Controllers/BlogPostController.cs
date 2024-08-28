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
        public IActionResult Create(Post testPost)
        {

            if (ModelState.IsValid && _testUser != null)
            {
                // Add author and date
                testPost.Author = _testUser;
                testPost.AuthoredDate = DateTime.Today;

                // Add posts
                _testUser.BlogPosts = new List<Post>();
                _testUser.BlogPosts.Add(testPost);

                // Add comments 
                Comment comment = new Comment
                {
                    Content = "Bro, this is hot.",
                    DatePosted = DateTime.Today,
                    Author = _testUser

                };

                // add to database
                _context.Posts.Add(testPost);

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


