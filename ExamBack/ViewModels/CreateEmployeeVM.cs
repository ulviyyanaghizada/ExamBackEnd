using ExamBack.Models;

namespace ExamBack.ViewModels
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string? TwitterLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkedinLink { get; set; }
        public int PositionId { get; set; }
        public IFormFile Image { get; set; }
    }
}
