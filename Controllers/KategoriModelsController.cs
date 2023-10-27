using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TaskManager.Controllers
{
    public class KategoriModelsController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public KategoriModelsController(TaskManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id, ProjectModel model)
        {
            if (id == null)
            {
                model = await _context.ProjectDb.Include(t => t.ProjectKategories).FirstOrDefaultAsync(o => o.ProjectName == model.ProjectName);
                id = model.ProjectId;
            }
            var projectModel = await _context.ProjectDb.Include(t => t.ProjectKategories).FirstOrDefaultAsync(o => o.ProjectId == id);
            return View(projectModel.ProjectKategories);
        }

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

        public IActionResult Create(int id, ProjectModel projectModel)
        {
            KategoriModel kategoriModel;
            if (id != 0)
            {
                kategoriModel = new KategoriModel();
                {
                    kategoriModel.Projectid = id;
                }
            }
            else
            {
                kategoriModel = new KategoriModel();
                {
                    kategoriModel.KategoriProject = projectModel;
                    kategoriModel.Projectid = projectModel.ProjectId;
                }
            }
            return View(kategoriModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpload(KategoriModel kategoriModel)
        {
            foreach (var item in _context.KategoriDb)
            {
                if (item.KategoriName == kategoriModel.KategoriName)
                {
                    return View(kategoriModel);
                }
            }
            
            _context.Add(kategoriModel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", "TaskModels", kategoriModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KategoriModel kategoriModel)
        {
            var KategoriModel = _context.KategoriDb.FirstOrDefault(q => q.KategoriId == kategoriModel.KategoriId);

            if (KategoriModel != null)
            {
                KategoriModel.KategoriId = kategoriModel.KategoriId;
                KategoriModel.KategoriProject = kategoriModel.KategoriProject;
                KategoriModel.KategoriName = kategoriModel.KategoriName;
                KategoriModel.KategoriTasks = kategoriModel.KategoriTasks;
                KategoriModel.Projectid = kategoriModel.Projectid;

                _context.KategoriDb.Update(KategoriModel);
                await _context.SaveChangesAsync();
                Console.WriteLine("success");
            }

            ProjectModel project = _context.ProjectDb.FirstOrDefault(o => o.ProjectId == KategoriModel.Projectid);
            return RedirectToAction("Index","KategoriModels",project);
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
            var projectModel = _context.ProjectDb.FirstOrDefault(o => o.ProjectId == kategoriModel.Projectid);
            if (kategoriModel != null)
            {
                _context.KategoriDb.Remove(kategoriModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","KategoriModels", projectModel);
        }

        private bool KategoriModelExists(int id)
        {
            return (_context.KategoriDb?.Any(e => e.KategoriId == id)).GetValueOrDefault();
        }
    }
}
