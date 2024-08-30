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
            return View();
        }
    }


}
