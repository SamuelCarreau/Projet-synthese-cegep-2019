using FluentValidation;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateCitizenStatusCommandValidator : AbstractValidator<CreateProfilOptionCommand<CitizenStatus>>
    {
        public CreateCitizenStatusCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Le nom ne peut pas être vide");

            RuleFor(c => c.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.Set<CitizenStatus>().Where(vt => vt.IsDelete == false)
                .Any(c => StringNormalizer.Normalize(c.Name) == StringNormalizer.Normalize(n)) == false)
                .WithMessage("Cette option existe déjà");
        }
    }
}