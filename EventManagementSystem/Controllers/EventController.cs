using EventManagementSystem.Domain.Db;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [SessionAuth]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Event eventModel)
        { 
            if (ModelState.IsValid)
            {
                _context.Events.Add(eventModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           
            var eventToEdit = _context.Events.Find(id);
            return View(eventToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Event eventModel)
        { 
            if (ModelState.IsValid)
            {
                _context.Events.Update(eventModel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventModel);
        }

        public IActionResult Delete(int id)
        {
            var eventToDelete = _context.Events.Find(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
