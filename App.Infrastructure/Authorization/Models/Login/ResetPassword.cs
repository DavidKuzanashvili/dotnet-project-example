using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Authorization.Models.Login
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Email.Required")]
        [EmailAddress(ErrorMessage = "Email.Invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password.Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword.Required")]
        [Compare("Password", ErrorMessage = "ConfirmPassword.Invalid")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
