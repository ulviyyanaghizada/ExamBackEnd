using System.ComponentModel.DataAnnotations;

namespace ExamBack.ViewModels
{
    public class UserLoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistant { get; set; }
    }
}
