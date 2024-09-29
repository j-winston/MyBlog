using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Utilities;


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
            return View(new CreatePostModel());

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(CreatePostModel model)
        {
            var loggedInUser = await _authService.GetLoggedInUser();

            if (model != null)
            {
                var post = new Post()
                {
                    Title = model.Title,
                    Content = model.Content,
                    AuthorId = loggedInUser?.Id,
                    AuthoredDate = DateTime.Today,
                    Slug = SlugHelper.GenerateSlug(model.Title),

                    CoverImagePath = model.CoverImage.FileName,
                    CoverImage = model.CoverImage

                };


                if (post.CoverImage != null)
                {
                    await UploadFile(post.CoverImage);
                }

                _context.Posts?.Add(post);

                _context.SaveChanges();

                return RedirectToAction("AdminPanel", "Account");
            }


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

                    return RedirectToAction("AdminPanel", "Account");
                }
            }

            return RedirectToAction("Create", "BlogPost");

        }


        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Post? post = _context.Posts.Where(p => p.ID == id).FirstOrDefault();


            return View(post);

        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(Post post)
        {
            // get original post 
            if (post != null)
            {
                var postDb = _context.Posts?.Where(p => p.ID == post.ID).FirstOrDefault();

                if (postDb != null)
                {
                    // update content and title 
                    postDb.Content = post.Content;
                    postDb.Title = post.Title;

                    // save changes to db 
                    _context.SaveChanges();
                    Console.WriteLine($"Updated Title: {postDb.Title}");
                    return RedirectToAction("AdminPanel", "Account");
                }
            }

            return View();

        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile fileUpload)
        {
            if (fileUpload != null && fileUpload.Length > 0)
            {
                if (fileUpload.ContentType == "image/jpeg" || fileUpload.ContentType == "image/jpg" || fileUpload.ContentType == "image/png")
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileUpload.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(stream);
                    }

                    ViewBag.Message = "File upload successful";
                    return View();
                }

                ViewBag.Message = "Please upload a valid jpeg";

                return View();
            }

            ViewBag.Message = "No file selected";
            return View();

        }

    }

}


