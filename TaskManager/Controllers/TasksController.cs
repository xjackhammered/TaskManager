using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _db;
        public TasksController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Overview()
        {
            var tasks = _db.Tasks.ToList();
            return View(tasks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;
                task.isCompleted = false;
                _db.Tasks.Add(task);
                _db.SaveChanges();
                return RedirectToAction("Overview");
            }
            return View(task);
        }
        public IActionResult Complete(int id)
        {
            var task = _db.Tasks.Find(id);
            if (task != null)
            {
                task.isCompleted = true;
                _db.SaveChanges();
            }
            return RedirectToAction("Overview");
        }
        public IActionResult Delete(int id)
        {
            var task = _db.Tasks.Find(id);

            if (task != null)
            {
                _db.Tasks.Remove(task);
                _db.SaveChanges();
            }
            return RedirectToAction("Overview");
        }

        public IActionResult Edit(int id)
        {
            var task = _db.Tasks.Find(id);
            if (task == null)
            {
                return NotFound();

            }
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                var existingTask = _db.Tasks.Find(task.Id);
                if (existingTask == null)
                {
                    return NotFound();
                }
                existingTask.Name = task.Name;
                existingTask.Content = task.Content;
                existingTask.isCompleted = task.isCompleted;

                _db.SaveChanges();
                return RedirectToAction("Overview");
            }
            return View(task);
        }
    }
}