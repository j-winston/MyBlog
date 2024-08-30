using Microsoft.AspNetCore.Authorization;
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
                return Redirect("Admin");
            }


            return View();
        }

        [HttpPost]
        public void RegisterNewUser(LoginViewModel model)
        {

        }








    }


}
