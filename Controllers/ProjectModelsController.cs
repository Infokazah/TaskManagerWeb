using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager.Controllers
{
    public class ProjectModelsController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public ProjectModelsController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: ProjectModels
        public async Task<IActionResult> Index()
        {
              return _context.ProjectDb != null ? 
                          View(await _context.ProjectDb.ToListAsync()) :
                          Problem("Entity set 'TaskManagerDbContext.ProjectDb'  is null.");
        }

        // GET: ProjectModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectDb == null)
            {
                return NotFound();
            }

            var projectModel = await _context.ProjectDb.FirstOrDefaultAsync(m => m.ProjectId == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        // GET: ProjectModels/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectModel projectModel) 
        {
            foreach(var item in _context.ProjectDb )
            {
                Console.WriteLine(item.CreatorName);
                if(item.ProjectName == projectModel.ProjectName)
                {
                    return View();
                }
            }
            _context.Update(projectModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "KategoriModels", projectModel);
        }

        // GET: ProjectModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectDb == null)
            {
                return NotFound();
            }

            var projectModel = await _context.ProjectDb.FindAsync(id);
            if (projectModel == null)
            {
                return NotFound();
            }
            return View(projectModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName")] ProjectModel projectModel)
        {
            if (id != projectModel.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectModelExists(projectModel.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectModel);
        }

        // GET: ProjectModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectDb == null)
            {
                return NotFound();
            }

            var projectModel = await _context.ProjectDb
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        // POST: ProjectModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectDb == null)
            {
                return Problem("Entity set 'TaskManagerDbContext.ProjectDb'  is null.");
            }
            var projectModel = await _context.ProjectDb.FindAsync(id);
            if (projectModel != null)
            {
                _context.ProjectDb.Remove(projectModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectModelExists(int id)
        {
          return (_context.ProjectDb?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
