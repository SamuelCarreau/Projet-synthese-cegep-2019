using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateCitizenStatusCommandValidator : AbstractValidator<UpdateProfilOptionCommand<CitizenStatus>>
    {

        public UpdateCitizenStatusCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("La chaine ne peut pas être vide");

            RuleFor(c => c)
                .Must(entity => 
                string.IsNullOrWhiteSpace(entity.Name) == false &&
                (context.CitizenStatuses
                .Where(c => c.Id == entity.Id && c.Name == entity.Name)
                .Any() ||
                context.CitizenStatuses
                .Where(vt => vt.IsDelete == false)
                .Any(c => StringNormalizer.Normalize(c.Name) == StringNormalizer.Normalize(entity.Name)) == false))
                .WithMessage("Cette option existe déjà")
                .OverridePropertyName("Name");

        }
    }
}