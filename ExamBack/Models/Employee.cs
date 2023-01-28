using ExamBack.Models.Base;
using Microsoft.Build.Framework;

namespace ExamBack.Models
{
    public class Employee:BaseEntityName
    {
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Description { get; set; }
        public  string? TwitterLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkedinLink { get; set; }
        [Required]
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
