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
    public class TaskModelsController : Controller
    {
        private readonly TaskManagerDbContext _context;

        public TaskModelsController(TaskManagerDbContext context)
        {
            _context = context;
        }

        // GET: TaskModels
        public IActionResult Index(int? id, KategoriModel model)
        {
            Console.WriteLine(id);
            if (id == null)
            {
                model = _context.KategoriDb.Include(t => t.KategoriTasks).FirstOrDefault(o => o.KategoriName == model.KategoriName);
                id = model.KategoriId;
            }
            var projectModel = _context.KategoriDb.Include(t => t.KategoriTasks).FirstOrDefault(o => o.KategoriId == id);
            return View(projectModel.KategoriTasks);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskDb == null)
            {
                return NotFound();
            }

            var taskModel = await _context.TaskDb
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        public IActionResult Create(int id,KategoriModel kategori)
        {
            TaskModel kategoriModel;
            if (id != 0)
            {
                kategoriModel = new TaskModel();
                {
                    kategoriModel.KategoriId = id;
                }
            }
            else
            {
                kategoriModel = new TaskModel();
                {
                    kategoriModel.KategoriId = kategori.KategoriId;
                    kategoriModel.TackKategori = kategori;
                }
            }
            return View(kategoriModel);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateUpload(TaskModel taskModel)
        {
            _context.Add(taskModel);
            await _context.SaveChangesAsync();
            KategoriModel kat = _context.KategoriDb.FirstOrDefault(o => o.KategoriId == taskModel.KategoriId);
            return RedirectToAction("Create","TaskModels",kat);
        }

        // GET: TaskModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskDb == null)
            {
                return NotFound();
            }

            var taskModel = await _context.TaskDb.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }
            return View(taskModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskName,TaskDeadline,TaskStatus,TaskImportance,KategoriId")] TaskModel taskModel)
        {
            if (id != taskModel.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskModelExists(taskModel.TaskId))
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
            return View(taskModel);
        }

        // GET: TaskModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.TaskDb == null)
            {
                return NotFound();
            }

            var taskModel = await _context.TaskDb
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        // POST: TaskModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskDb == null)
            {
                return Problem("Entity set 'TaskManagerDbContext.TaskDb'  is null.");
            }
            var taskModel = await _context.TaskDb.FindAsync(id);
            if (taskModel != null)
            {
                _context.TaskDb.Remove(taskModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskModelExists(int id)
        {
          return (_context.TaskDb?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
