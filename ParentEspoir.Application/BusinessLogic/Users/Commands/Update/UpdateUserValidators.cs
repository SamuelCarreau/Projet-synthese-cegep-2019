using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Domain.Entities;

namespace ParentEspoir.Application
{
    public class UpdateUserValidators : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidators(UserManager<AppUser> userManager)
        {
            RuleFor(c => c.Id).Must(i => userManager.FindByIdAsync(i).Result != null);

            RuleFor(c => c.UserName)
                .EmailAddress()
                .WithMessage("Le nom de l'utilisateur doit être une adresse courriel");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("L'adresse courriel doit être au bon format");

            RuleFor(c => c.PasswordToken).NotEmpty();
            RuleFor(c => c.NewPassword)
                .Must(p => string.IsNullOrEmpty(p) || (p.Length >= 6 && p.Length <= 100))
                .WithMessage("Le mot de passe doit être au minimum 6 caractère et au maximum 100");
        }
    }
}
