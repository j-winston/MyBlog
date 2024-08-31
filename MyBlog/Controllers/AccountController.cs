using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models;
using MyBlog.Services;


namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticationService _authService;

        public AccountController(AuthenticationService authService)
        {
            _authService = authService;
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
                return Redirect("/BlogPost/AdminPanel");
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
                return Redirect("/BlogPost/AdminPanel");
            }

            foreach (IdentityError err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);

            }

            return View();



        }

        [Authorize]
        public IActionResult AdminPanel()
        {
            ListModel model = new ListModel();

            model.Users = _authService.GetUsers();

            return View(model);


        }







    }


}
