using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required]
        [DataType(DataType.Upload)]
        public IFormFile? ProfilePic { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required]
        public string PhoneNumber { get; set; }
    }
}
