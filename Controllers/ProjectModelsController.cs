using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        public IActionResult JoinProject(PersonModel person)
        {
            JoinViewModel model = new JoinViewModel()
            {
                PersonNameJoin = person.PersonName,
            };
            return View("JoinProject", model);
        }
        //Post
        [HttpPost]
        public IActionResult JoinProjectUpload(JoinViewModel person)
        {
            ProjectModel projectModel = _context.ProjectDb.FirstOrDefault(m => m.ProjectName == person.ProjectNameJoin);
            PersonModel personModel = _context.PersonDb.FirstOrDefault(m => m.PersonName == person.PersonNameJoin);
            foreach (ProjectModel item in _context.ProjectDb)
            {
                if (item.ProjectName == projectModel.ProjectName && item.ProjectPassword == projectModel.ProjectPassword)
                {
                    if (item != null)
                    {
                        if (item.ProgectPerson == null)
                        {
                            item.ProgectPerson = new List<PersonModel>();
                        }
                        foreach (PersonModel name in item.ProgectPerson)
                        {
                            if (personModel.PersonName == name.PersonName)
                            {
                                return View(person);
                            }
                        }
                        item.ProgectPerson.Add(personModel);
                        _context.ProjectDb.Update(item);
                        _context.SaveChangesAsync();
                        Console.WriteLine("success");
                    }
                    return RedirectToAction("Index", "KategoriModels", item);
                }
            }
            return View(person);
        }
        // GET: ProjectModels
        public async Task<IActionResult> Index(PersonModel person)
        {
            List<ProjectModel> PrList = new List<ProjectModel>();
            foreach(var item in _context.ProjectDb)
            {
                if(item.CreatorName==person.PersonName)
                {
                    PrList.Add(item);
                }
            }
            return View(PrList);
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
        public IActionResult Create(PersonModel CreatorNameCon)
        {
            Console.WriteLine(CreatorNameCon.PersonName);
            ProjectModel model = new ProjectModel()
            {
                CreatorName = CreatorNameCon.PersonName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectModel projectModel) 
        {
            foreach (var item in _context.ProjectDb)
            {
                if(item.ProjectName==projectModel.ProjectName)
                {
                    PersonModel model = _context.PersonDb.Include(t => t.PersonName).FirstOrDefault(o => o.PersonName == projectModel.CreatorName);
                    return View("Create",model);
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
                return Problem("Entity set 'TaskManagerDbContext.ProjectDb' is null.");
            }

            var projectModel = await _context.ProjectDb.FindAsync(id);

            if (projectModel != null)
            {
                var creatorNameToDelete = projectModel.CreatorName; // Get the CreatorName of the project to be deleted
                _context.ProjectDb.Remove(projectModel);
                await _context.SaveChangesAsync();

                // Fetch the remaining projects with the same CreatorName
                List<ProjectModel> remainingProjects = await _context.ProjectDb
                    .Where(p => p.CreatorName == creatorNameToDelete)
                    .ToListAsync();

                return View("Index", remainingProjects);
            }

            // Handle the case where the project to be deleted was not found
            return NotFound();
        }

        private bool ProjectModelExists(int id)
        {
          return (_context.ProjectDb?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
