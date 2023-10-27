using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Controllers
{
    public class MessangerModelController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public MessangerModelController(TaskManagerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            var model = _context.TaskDb.FirstOrDefault(o => o.TaskId == id);
            if (model.MessengerList != null)
            {
                model.MessengerList = new List<MessengerModel>();
            }
            List<MessengerModel> messeges = _context.TaskMessengerDb.Where(m => m.TaskModelid == id).ToList();
            ViewBag.Id = id;
            return View("Index",messeges);
        }
        [HttpPost]
        public async Task<IActionResult> Send(MessengerModel message)
        {
            var model = _context.TaskDb.Include(t => t.MessengerList).FirstOrDefault(o => o.TaskId == message.TaskModelid);
            message.MessengerTaskModel = model;
            _context.TaskMessengerDb.Add(message);
            await _context.SaveChangesAsync();
            if (model.MessengerList == null)
            {
                model.MessengerList = new List<MessengerModel>();
            }
            model.MessengerList.Add(message);
            _context.TaskDb.Update(model);
            _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = message.TaskModelid });
        }


    }
}
