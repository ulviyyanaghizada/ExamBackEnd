using ExamBack.DAL;
using ExamBack.Models;
using ExamBack.Utilities;
using ExamBack.Utilities.Enum;
using ExamBack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ExamBack.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]

    public class EmployeeController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }

        public EmployeeController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Positions.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Employees.Include(e => e.Position).Skip((page - 1) * 5).Take(5).ToList());
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Employee exist = _context.Employees.Include(e => e.Position).FirstOrDefault(p => p.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
            _context.Employees.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM createEmployee)
        {
            var image = createEmployee.Image;
            if (!_context.Positions.Any(p => p.Id == createEmployee.PositionId))
            {
                ModelState.AddModelError("PositionId", "bele id li position yoxdur");
                return View();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            Employee employee = new Employee()
            {
                Name = createEmployee.Name,
                Surname = createEmployee.Surname,
                Description = createEmployee.Description,
                PositionId = createEmployee.PositionId,
                FacebookLink= createEmployee.FacebookLink,
                TwitterLink= createEmployee.TwitterLink,
                InstagramLink= createEmployee.InstagramLink,
                LinkedinLink= createEmployee.LinkedinLink,
                ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"))

            };
            _context.Add(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Employee exist = _context.Employees.Include(e => e.Position).FirstOrDefault(p => p.Id == id);
            if (exist is null) return NotFound();
            UpdateEmployeeVM updateEmployee = new UpdateEmployeeVM()
            {
                Name = exist.Name,
                Surname = exist.Surname,
                Description = exist.Description,
                ImageUrl = exist.ImageUrl,
                FacebookLink= exist.FacebookLink,
                InstagramLink=exist.InstagramLink,
                TwitterLink= exist.TwitterLink,
                LinkedinLink= exist.LinkedinLink,
                PositionId = exist.PositionId
            };
            ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
            ViewBag.Image = _context.Employees.FirstOrDefault(p => p.Id == id).ImageUrl;
            return View(updateEmployee);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateEmployeeVM updateEmployee)
        {
            if (id is null || id == 0) return BadRequest();
            var image = updateEmployee.Image;
            string result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (!_context.Positions.Any(p => p.Id == updateEmployee.PositionId))
            {
                ModelState.AddModelError("PositionId", "bele id li position yoxdur");
                return View();
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Positions = new SelectList(_context.Positions.ToList(), nameof(Position.Id), nameof(Position.Name));
                ViewBag.Image = _context.Employees.FirstOrDefault(p => p.Id == id).ImageUrl;
                return View();
            }
            Employee exist = _context.Employees.Include(e => e.Position).FirstOrDefault(e => e.Id == id);
            if (exist is null) return BadRequest();
            if (image != null)
            {
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/img");
                updateEmployee.ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "img"));
            }
            else
            {
                updateEmployee.ImageUrl = exist.ImageUrl;
            }
            exist.Name = updateEmployee.Name;
            exist.Surname = updateEmployee.Surname;
            exist.Description = updateEmployee.Description;
            exist.ImageUrl = updateEmployee.ImageUrl;
            exist.PositionId = updateEmployee.PositionId;
            exist.FacebookLink= updateEmployee.FacebookLink;
            exist.TwitterLink= updateEmployee.TwitterLink;
            exist.LinkedinLink= updateEmployee.LinkedinLink;
            exist.InstagramLink= updateEmployee.InstagramLink;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
