using ExamBack.DAL;
using ExamBack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamBack.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class PositionController : Controller
    {
        readonly AppDbContext _context;
        readonly IWebHostEnvironment _env;
        public PositionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Positions.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Positions.Skip((page-1)*5).Take(5).ToList());
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Position position= _context.Positions.FirstOrDefault(p=>p.Id == id);
            if (position == null) return NotFound();
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Positions.Add(position);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if(id is null || id == 0) return BadRequest();
            Position position= _context.Positions.FirstOrDefault(p=> p.Id == id);
            if(position == null) return NotFound();
            return View(position);
        }
        [HttpPost]
        public IActionResult Update(int? id ,Position position)
        {
            if (id is null || id == 0) return BadRequest();
            Position exist = _context.Positions.FirstOrDefault(p => p.Id == id);
            if (position == null) return NotFound();
            if(!ModelState.IsValid)
            {
                return View();
            }
            exist.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
            
    }
}
