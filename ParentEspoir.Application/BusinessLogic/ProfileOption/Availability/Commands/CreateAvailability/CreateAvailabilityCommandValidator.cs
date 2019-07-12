using FluentValidation;
using System.Linq;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class CreateAvailabilityCommandValidator : AbstractValidator<CreateProfilOptionCommand<Availability>>
    {
        public CreateAvailabilityCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("La chaine ne peut pas être vide");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false)
                .WithMessage("La chaine ne peut pas être remplis d'espace");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.Availabilities
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false)
                .WithMessage("L'option existe déjà");
        }
    }
}