using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TaskManagerDbContext _context;

        public HomeController(ILogger<HomeController> logger, TaskManagerDbContext context)
        {
            _logger = logger;
            _context = context;
        }
     
        public IActionResult Index(int? id,PersonModel person)
        {

            if (id != null) 
            {
                var personModel = _context.PersonDb.FirstOrDefault(p => p.PersonId == id);
                return View("Index", personModel);
            }

            if (string.IsNullOrEmpty(person.PersonName)) 
            {
                person.PersonName = "unregistered";
            }
            return View("Index", person);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}