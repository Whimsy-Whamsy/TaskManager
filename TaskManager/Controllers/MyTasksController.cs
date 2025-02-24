using Microsoft.AspNetCore.Mvc;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers;

public class MyTasksController : Controller
{
    TasksContext _context;

    public MyTasksController(TasksContext context)
    {
        _context = context;
    }

    // GET
    public IActionResult Index()
    {
        return View(_context.Tasks.ToList());
    }

    public IActionResult Create()
    {
        ViewBag.Statuses = new List<string> { "In Progress", "Completed", "Overdue", "Canceled" };
        return View(new TasksModel());
    }
    
    [HttpPost]
    public IActionResult Create(TasksModel model)
    {
        if (ModelState.IsValid)
        {
            _context.Tasks.Add(model);
            _context.SaveChanges();
            
            //model.DueDate = DateTime.SpecifyKind(model.DueDate, DateTimeKind.Utc);
            
            return RedirectToAction("Index");
        }
        
        return View();
    }
    
    public IActionResult Edit(int taskid)
    {
        ViewBag.Statuses = new List<string> { "In Progress", "Completed", "Overdue", "Canceled" };
        var task = _context.Tasks.FirstOrDefault(tid => tid.Id == taskid);
        ViewBag.SelectedStatus = task.Status;
        return View(task);
    }
    
    [HttpPost]
    public IActionResult Edit(TasksModel model)
    {
        if (ModelState.IsValid)
        {
            if (_context.Tasks.Any(tid => tid.Id == model.Id))
            {
                var task = _context.Tasks.FirstOrDefault(tid => tid.Id == model.Id);
                
                task.Title = model.Title;
                task.Description = model.Description;
                task.DueDate = model.DueDate;
                task.Status = model.Status;
                
                _context.Tasks.Update(task);
                _context.SaveChanges();
            
                return RedirectToAction("Index");
            }
        }
        
        ViewBag.Statuses = new List<string> { "In Progress", "Completed", "Overdue", "Canceled" };
        
        return View(model);
    }

    public IActionResult Delete(int taskid)
    {
        var task = _context.Tasks.FirstOrDefault(tid => tid.Id == taskid);
        _context.Tasks.Remove(task);
        _context.SaveChanges();
        
        return RedirectToAction("Index");
    }
}