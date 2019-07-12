using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateAvailabilityCommandValidator : AbstractValidator<UpdateProfilOptionCommand<Availability>>
    {
        public UpdateAvailabilityCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(a => a.Name)
                .Must(name => string.IsNullOrWhiteSpace(name) == false)
                .WithMessage("Le nom ne peut pas être remplis d'espace");

            RuleFor(a => a)
                .Must(n => (context.Availabilities.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false && 
                context.Availabilities
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.Name.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false)
                .WithMessage("Cette option exite déjà")
                .OverridePropertyName("Name");
        }
    }
}