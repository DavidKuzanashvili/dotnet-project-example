using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Authorization.Models.Login
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Email.Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CurrentPassword.Required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "NewPassword.Required")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "ConfirmNewPassword.NotValid")]
        [Required(ErrorMessage = "ConfirmNewPassword.Required")]
        public string ConfirmNewPassword { get; set; }
    }
}
