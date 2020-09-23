using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Authorization.Models.Login
{
    public class ForgotPassword
    {
        [Required(ErrorMessage = "Email.Required")]
        [EmailAddress(ErrorMessage = "Email.Invalid")]
        public string Email { get; set; }

        [Url(ErrorMessage = "ReturnUrl.Invalid")]
        [Required(ErrorMessage = "ReturnUrl.Required")]
        public string ReturnUrl { get; set; }
    }
}
