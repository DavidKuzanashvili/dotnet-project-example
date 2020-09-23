using System.ComponentModel.DataAnnotations;

namespace App.Infrastructure.Authorization.Models.Registration
{
    public class Register
    {
        [Required(ErrorMessage = "Email.Required")]
        [EmailAddress(ErrorMessage = "Email.Invalid")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Email.Invalid")]
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        [Required(ErrorMessage = "Password.Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword.Required")]
        [Compare("Password", ErrorMessage = "ConfirmPassword.Invalid")]
        public string ConfirmPassword { get; set; }
    }
}
