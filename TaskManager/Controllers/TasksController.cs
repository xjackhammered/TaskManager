using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _db;
        public TasksController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Overview()
        {
            var tasks = await _db.Tasks.Include(t => t.Details).Include(c => c.Category).ToListAsync();
            return View(tasks);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id","Content", "Name", "CategoryId", "Details")]TaskItem task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;
                task.isCompleted = false;
                _db.Tasks.Add(task);
                await _db.SaveChangesAsync();
                return RedirectToAction("Overview");
            }
            return View(task);
        }
        public async Task<IActionResult> Complete(int id)
        {
            var task = await _db.Tasks.FindAsync(id);
            if (task != null)
            {
                task.isCompleted = true;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Overview");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task != null)
            {
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Overview");
        }

        public async Task<IActionResult> Edit(int id)
        {   
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            var task = await _db.Tasks.Include(t => t.Details).FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return NotFound();

            }
            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id", "Content", "Name", "CategoryId", "Details")] TaskItem task)
        {
            if (ModelState.IsValid)
            {
                var existingTask = await _db.Tasks.Include(t => t.Details).Include(c => c.Category).FirstOrDefaultAsync(t => t.Id == task.Id);
                if (existingTask == null)
                {
                    return NotFound();
                }
                existingTask.Name = task.Name;
                existingTask.Content = task.Content;
                existingTask.isCompleted = task.isCompleted;
                existingTask.CategoryId = task.CategoryId;
        
                existingTask.Details.Note = task.Details.Note;

             
                await _db.SaveChangesAsync();
                return RedirectToAction("Overview");
            }
            return View(task);
        }
    }
}