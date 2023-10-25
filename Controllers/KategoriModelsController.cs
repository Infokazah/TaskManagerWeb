using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class KategoriModelsController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public KategoriModelsController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: KategoriModels
        public IActionResult Index(int id)
        {

              ProjectModel project = _context.ProjectDb.Include(t => t.ProjectKategories).FirstOrDefault(o => o.ProjectId == id);

            return View(project.ProjectKategories);
        }

        // GET: KategoriModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.KategoriDb == null)
            {
                return NotFound();
            }

            var kategoriModel = await _context.KategoriDb
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategoriModel == null)
            {
                return NotFound();
            }

            return View(kategoriModel);
        }

        public IActionResult Create(ProjectModel projectModel)
        {
            KategoriModel kategoriModel = new KategoriModel()
            {
                Projectid = projectModel.ProjectId
            };
            return View("Create", kategoriModel);
        }

        [HttpPost]

        public async Task<IActionResult> Create(KategoriModel kategoriModel)
        {

             _context.Add(kategoriModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create","TasksModel",kategoriModel);
        }

        // GET: KategoriModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KategoriDb == null)
            {
                return NotFound();
            }

            var kategoriModel = await _context.KategoriDb.FindAsync(id);
            if (kategoriModel == null)
            {
                return NotFound();
            }
            return View(kategoriModel);
        }

        // POST: KategoriModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KategoriId,KategoriName,Projectid")] KategoriModel kategoriModel)
        {
            if (id != kategoriModel.KategoriId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoriModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriModelExists(kategoriModel.KategoriId))
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
            return View(kategoriModel);
        }

        // GET: KategoriModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.KategoriDb == null)
            {
                return NotFound();
            }

            var kategoriModel = await _context.KategoriDb
                .FirstOrDefaultAsync(m => m.KategoriId == id);
            if (kategoriModel == null)
            {
                return NotFound();
            }

            return View(kategoriModel);
        }

        // POST: KategoriModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.KategoriDb == null)
            {
                return Problem("Entity set 'TaskManagerDbContext.KategoriDb'  is null.");
            }
            var kategoriModel = await _context.KategoriDb.FindAsync(id);
            if (kategoriModel != null)
            {
                _context.KategoriDb.Remove(kategoriModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriModelExists(int id)
        {
          return (_context.KategoriDb?.Any(e => e.KategoriId == id)).GetValueOrDefault();
        }
    }
}
