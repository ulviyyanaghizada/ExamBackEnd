using System.ComponentModel.DataAnnotations;

namespace ExamBack.Models.Base
{
    public class BaseEntityName:BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
