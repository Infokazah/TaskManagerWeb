using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Models;
using TaskManager.Data;
using Microsoft.AspNetCore.Http;

namespace TaskManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly TaskManagerDbContext _context;
        public AccountController(TaskManagerDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string name)
        {
            return View(name);
        }
        //Post
        public IActionResult Register()
        {
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(PersonModel obj)
        {
            _context.PersonDb.Add(obj);
            _context.SaveChanges();
            ViewData["name"] = obj.PersonName;
            return RedirectToAction("Index","Home", obj);
        }
        public IActionResult Login()
        {
            return View();
        }

        // Метод для обработки входа (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(PersonModel obj)
        {
            IEnumerable<PersonModel> objList = _context.PersonDb;
            foreach (PersonModel person in objList)
            {
                if (person.PersonName == obj.PersonName && person.PersonPassword == obj.PersonPassword)
                {
                    HttpContext.Session.SetString("IsLoggedIn", "true");
                    HttpContext.Session.SetString("LoggedInPerson", obj.PersonName);

                    return RedirectToAction("Index","Home", obj);
                }
            }
            return View();
        }
    }


}


