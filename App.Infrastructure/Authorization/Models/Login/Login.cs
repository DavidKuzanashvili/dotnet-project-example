using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Authorization.Models.Login
{
    public class Login
    {
        [Required(ErrorMessage = "Email.Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password.Required")]
        public string Password { get; set; }
    }
}
