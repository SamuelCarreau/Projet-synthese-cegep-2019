using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateFamilyTypeCommandValidator : AbstractValidator<UpdateProfilOptionCommand<FamilyType>>
    {
        public UpdateFamilyTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(a => a)
                .Must(n => (context.FamilyTypes.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.FamilyTypes
                .Where(a => a.IsDelete == false)
                .Any(acontext => n.Name.ToUpper().Trim() == acontext.Name.ToUpper().Trim())
                == false).WithMessage("Cette option existe déjà").OverridePropertyName("Name");
        }
    }
}