using System.ComponentModel.DataAnnotations;

namespace Authorization.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="The field is compulsory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The field is compulsory")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}