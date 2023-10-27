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

        public IActionResult Forme(int id)
        {
            var model = _context.TaskDb.FirstOrDefault(o => o.TaskId == id);
            TaskForm form = new TaskForm
            { 
                TaskModelFormId = model.TaskId,
                TaskModelForm = model,
            }; 
            return View(form);
        }
        public async Task<IActionResult> FormSafe(TaskForm form)
        {
            _context.TaskFormDb.Add(form);
            await _context.SaveChangesAsync();

            var taskModel = _context.TaskDb.FirstOrDefault(o => o.TaskId == form.TaskModelFormId);
            var kategoriModel = _context.KategoriDb.FirstOrDefault(o => o.KategoriId == taskModel.KategoriId);

            return RedirectToAction("Index", kategoriModel);
        }
        // GET: TaskModels
        public async Task<IActionResult> Index(int? id, KategoriModel model)
        {
            Console.WriteLine(id);

            if (id == null)
            {
                model = await _context.KategoriDb.Include(t => t.KategoriTasks)
                    .FirstOrDefaultAsync(o => o.KategoriName == model.KategoriName);

                if (model == null)
                {
                    // Handle the case where the model is not found
                    return NotFound();
                }

                id = model.KategoriId;
            }

            var projectModel = await _context.KategoriDb.Include(t => t.KategoriTasks)
                .FirstOrDefaultAsync(o => o.KategoriId == id);

            if (projectModel == null)
            {
                // Handle the case where projectModel is not found
                return NotFound();
            }

            // Sort KategoriTasks in descending order by TaskStatus
            projectModel.KategoriTasks = projectModel.KategoriTasks.OrderByDescending(task => task.TaskImportance).ToList();

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

        public IActionResult Create(int id, KategoriModel kategori)
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
            var kategoriId = taskModel.KategoriId;
            return RedirectToAction("Index", new { id = kategoriId });
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
        public async Task<IActionResult> Edit(int id, TaskModel taskModel)
        {
            if (id != taskModel.TaskId)
            {
                return NotFound();
            }

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

            // Get the KategoriId associated with the edited task
            var kategoriId = taskModel.KategoriId;

            // Redirect to the Index action with the id as a route parameter
            return RedirectToAction("Index", new { id = kategoriId });
        }



        // GET: TaskModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(TaskModel taskModel)
        {
            if (_context.TaskDb == null)
            {
                return Problem("Entity set 'TaskManagerDbContext.TaskDb' is null.");
            }

            var id = taskModel.TaskId;
            var taskToDelete = await _context.TaskDb.FindAsync(id);

            if (taskToDelete == null)
            {
                return NotFound();
            }

            // Access the Confirmation field


            // Check the confirmation and proceed with deletion if it matches a predefined value

            try
            {
                int kategoriIdToDelete = taskToDelete.KategoriId;
                var tasksWithSameKategoriId = await _context.TaskDb
                    .Where(t => t.KategoriId == kategoriIdToDelete)
                    .ToListAsync();

                _context.TaskDb.Remove(taskToDelete);
                await _context.SaveChangesAsync();

                // Redirect to the Index action with the id as a route parameter
                return RedirectToAction("Index", new { id = kategoriIdToDelete });
            }
            catch (Exception ex)
            {
                return Problem("Error occurred while deleting the task: " + ex.Message);
            }


        }


        private bool TaskModelExists(int id)
        {
            return (_context.TaskDb?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
