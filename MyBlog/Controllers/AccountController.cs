using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;
using Microsoft.EntityFrameworkCore;


namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticationService _authService;
        private readonly ApplicationDbContext _context;

        public AccountController(AuthenticationService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            Microsoft.AspNetCore.Identity.SignInResult result = await _authService.LoginUserAsync(model);

            if (result.Succeeded)
            {
                return Redirect("AdminPanel");
            }


            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            IdentityResult result = await _authService.RegisterUserAsync(model);

            if (result.Succeeded)
            {
                return Redirect("AdminPanel");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);

            }

            return View();



        }

        [Authorize]
        public async Task<IActionResult> AdminPanel()
        {

            // Assign logged in user to current list model 
            var user = await _authService.GetLoggedInUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var posts = await _context.Posts
                 .Include(p => p.Author)
                 .Where(p => p.AuthorId == user.Id)
                 .ToListAsync();


            ListModel model = new ListModel()
            {
                User = user,
                Posts = posts
            };


            return View(model);


        }







    }


}
