using ExamBack.DAL;
using ExamBack.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace ExamBack.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
          
        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM { Employees = _context.Employees , Positions = _context.Positions};
            return View(home);
        }
    }
}
