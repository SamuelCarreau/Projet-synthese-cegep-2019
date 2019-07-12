using FluentValidation;
using Microsoft.AspNetCore.Identity;
using ParentEspoir.Application.Users;
using ParentEspoir.Domain.Entities;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateUserValidators : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidators(UserManager<AppUser> userManager)
        {
            RuleFor(c => c.Password)
                .NotNull()
                .WithMessage("Le mot de passe est requit");

            RuleFor(c => c.Email)
                .Must(e => userManager.FindByNameAsync(e).Result == null)
                .WithMessage("Le compte exite déjà");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("L'adresse courriel doit avoir le bon format");

            RuleFor(c => c.Email)
                .Must(username => 
                userManager
                .FindByNameAsync(userManager.NormalizeKey(username))
                .Result == null)
                .WithMessage("Le compte existe déjà");
        }
    }
}
