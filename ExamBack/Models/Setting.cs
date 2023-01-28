using ExamBack.Models.Base;
using Microsoft.Build.Framework;

namespace ExamBack.Models
{
    public class Setting:BaseEntity
    {
        [Required]
        public string Key { get; set; }
        public string? Value { get; set; }
    }
}
