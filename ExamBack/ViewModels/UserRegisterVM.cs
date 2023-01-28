using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ExamBack.ViewModels
{
    public class UserRegisterVM
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Surname { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Username { get; set; }
        [System.ComponentModel.DataAnnotations.Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Required, DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
