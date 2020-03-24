using System.ComponentModel.DataAnnotations;

namespace Authorization.Models
{
    public class RegisterModel
    {

        [EmailAddress(ErrorMessage ="Incorrect format of email")]
        [Required(ErrorMessage = "The field is compulsory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is compulsory")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The field is compulsory")]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}
