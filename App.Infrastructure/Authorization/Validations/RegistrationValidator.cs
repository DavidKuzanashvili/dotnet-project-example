using App.Infrastructure.Authorization.Models.Registration;
using FluentValidation;
using System.Text.RegularExpressions;

namespace App.Infrastructure.Authorization.Validations
{
    public class RegistrationValidator : AbstractValidator<Register>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.FirstName)
                .Must(x => !Regex.IsMatch(x, @"[^\p{L}_]", RegexOptions.IgnoreCase))
                .WithMessage("FirstName.Invalid.MustContaineOnlyCharacter");

            RuleFor(x => x.LastName)
                .Must(x => !Regex.IsMatch(x, @"[^\p{L}_]", RegexOptions.IgnoreCase))
                .WithMessage("FirstName.Invalid.MustContaineOnlyCharacter");
        }
    }
}
