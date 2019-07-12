using FluentValidation;
using System.Linq;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;

namespace ParentEspoir.Application
{
    public class CreateVolunteeringTypeCommandValidator : AbstractValidator<CreateProfilOptionCommand<VolunteeringType>>
    {

        public CreateVolunteeringTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(v => v.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false && 
                context.Set<VolunteeringType>().Where(vt => vt.IsDelete == false)
                .Any(v => StringNormalizer.Normalize(v.Name) == StringNormalizer.Normalize(n)) == false)
                .WithMessage("Cette option existe déjà");
        }
    }
}