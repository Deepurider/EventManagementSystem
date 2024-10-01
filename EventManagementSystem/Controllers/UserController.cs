using EventManagementSystem.Domain.Db;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            User? users;
            if (username == "admin" && password == "admin@123")
            {
                users = new User()
                {
                    UserName = "admin",
                    Password = "admin@123"
                };
            }
            else
            {
                users = _context.Users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            }

            if (users != null)
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "Event");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}
