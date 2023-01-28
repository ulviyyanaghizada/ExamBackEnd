using ExamBack.Models;

namespace ExamBack.ViewModels.Home
{
    public class HomeVM
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Position> Positions { get; set; }
    }
}
