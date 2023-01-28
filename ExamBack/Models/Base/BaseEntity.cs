using System.ComponentModel.DataAnnotations;

namespace ExamBack.Models.Base
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }
    }
}
